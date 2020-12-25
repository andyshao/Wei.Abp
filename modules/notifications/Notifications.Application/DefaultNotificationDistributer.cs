using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.Uow;
using Volo.Abp.SettingManagement;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Settings;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// </summary>
    public class DefaultNotificationDistributer : DomainService, INotificationDistributer
    {
        private readonly INotificationStore _notificationStore;
        private readonly IGuidGenerator _guidGenerator;
        protected NotificationOptions Options;
        public ISettingProvider SettingProvider { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDistributionJob"/> class.
        /// </summary>
        public DefaultNotificationDistributer(
            IOptions<NotificationOptions> options,
            IRealTimeNotifier realTime,
            INotificationStore notificationStore,
            IGuidGenerator guidGenerator)
        {
            _notificationStore = notificationStore;
            _guidGenerator = guidGenerator;
            Options = options.Value;
        }

        public async Task DistributeAsync(Guid notificationId)
        {
            var notificationInfo = await _notificationStore.GetNotificationOrNullAsync(notificationId);
            if (notificationInfo == null)
            {
                Logger.LogWarning("NotificationDistributionJob can not continue since could not found notification by id: " + notificationId);
                return;
            }

            var users = await GetUsersAsync(notificationInfo);

            var userNotifications = await SaveUserNotificationsAsync(users, notificationInfo);

            await _notificationStore.DeleteNotificationAsync(notificationInfo.Id);
            foreach (var item in userNotifications)
            {

            }

        }



        [UnitOfWork]
        protected async Task<Guid[]> GetUsersAsync(NotificationInfo notificationInfo)
        {
            List<Guid> userIds = new List<Guid>();


            if (!notificationInfo.UserIds.IsNullOrEmpty())
            {

                foreach (var item in notificationInfo
                    .UserIds)
                {
                    var receiveNotifications= await SettingProvider.GetAsync<bool>(NotificationSettingNames.ReceiveNotifications,false);
                    if (receiveNotifications)
                    {
                        userIds.Add(item);
                    }
                }
            }
            // else
            // {
            //     //Get subscribed users
            //     List<NotificationSubscriptionInfo> subscriptions = await _notificationStore.GetSubscriptionsAsync(notificationInfo.NotificationName);
            //
            //     //Remove invalid subscriptions
            //     var invalidSubscriptions = new Dictionary<Guid, NotificationSubscriptionInfo>();
            //
            //     //TODO: Group subscriptions per tenant for potential performance improvement
            //     foreach (var subscription in subscriptions)
            //     {
            //         var ReceiveNotifications = await SettingProvider.GetAsync(NotificationSettingNames.ReceiveNotifications, false);
            //             if (!await _notificationDefinitionManager.IsAvailableAsync(notificationInfo.NotificationName, subscription.UserId) ||
            //                 !ReceiveNotifications)
            //             {
            //                 invalidSubscriptions[subscription.Id] = subscription;
            //             }
            //     }
            //
            //     subscriptions.RemoveAll(s => invalidSubscriptions.ContainsKey(s.Id));
            //
            //     //Get user ids
            //     userIds = subscriptions
            //         .Select(s => s.UserId)
            //         .ToList();
            // }


            userIds.RemoveAll(uid => notificationInfo.ExcludedUserIds.Any(euid => euid.Equals(uid)));

            return userIds.ToArray();
        }

        [UnitOfWork]
        protected virtual async Task<List<UserNotificationInfo>> SaveUserNotificationsAsync(Guid[] users, NotificationInfo notificationInfo)
        {
            var userNotifications = new List<UserNotificationInfo>();

            foreach (var user in users)
            {
                var userNotification = new UserNotificationInfo()
                {
                    TenantId = CurrentTenant.Id,
                    UserId = user,
                    NotificationId = notificationInfo.Id
                };
                userNotifications.Add(userNotification);
                await _notificationStore.InsertUserNotificationAsync(userNotification);
            }
            return userNotifications;
        }

        #region Protected methods

        protected virtual async Task NotifyAsync(UserNotification userNotifications)
        {
            foreach (var notifierType in Options.Notifiers)
            {
                try
                {
                    var notifier = ServiceProvider.GetRequiredService(notifierType) as IRealTimeNotifier;
                    await notifier.SendNotificationsAsync(userNotifications);
                }
                catch (Exception ex)
                {
                    Logger.LogWarning(ex.ToString(), ex);
                }
            }
        }

        #endregion
    }
}