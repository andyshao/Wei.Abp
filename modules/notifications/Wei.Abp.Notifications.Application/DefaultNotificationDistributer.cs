﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using Volo.Abp.Settings;
using Volo.Abp.SettingManagement;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Logging.Abstractions;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// </summary>
    public class DefaultNotificationDistributer :  INotificationDistributer,Volo.Abp.DependencyInjection.ITransientDependency
    {
        protected INotificationStore NotificationStore;
        protected IGuidGenerator GuidGenerator;
        protected NotificationOptions Options;
        protected IRealTimeNotifier RealTime;
        protected ICurrentTenant CurrentTenant;
        protected ISettingManager SettingManager;
        protected IServiceProvider ServiceProvider;
        protected ILogger Logger;
        protected INotificationDefinitionManager NotificationDefinitionManager;

        public DefaultNotificationDistributer(INotificationStore notificationStore,
            INotificationDefinitionManager notificationDefinitionManager,
            IGuidGenerator guidGenerator,
            IOptions<NotificationOptions> options,
            IRealTimeNotifier realTime,
            ICurrentTenant currentTenant,
            ISettingManager settingManager,
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory)
        {
            NotificationStore = notificationStore;
            GuidGenerator = guidGenerator;
            Options = options.Value;
            RealTime = realTime;
            CurrentTenant = currentTenant;
            SettingManager = settingManager;
            ServiceProvider = serviceProvider;
            Logger = loggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance;
            NotificationDefinitionManager = notificationDefinitionManager;
        }

        public async Task DistributeAsync(Guid notificationId)
        {
            var notificationInfo = await NotificationStore.GetNotificationOrNullAsync(notificationId);
            if (notificationInfo == null)
            {
                Logger.LogWarning("NotificationDistributionJob can not continue since could not found notification by id: " + notificationId);
                return;
            }

            var users = await GetUsersAsync(notificationInfo);

            var userNotifications = await SaveUserNotificationsAsync(users, notificationInfo);

            //await NotificationStore.DeleteNotificationAsync(notificationInfo.Id);

            foreach (var item in userNotifications)
            {
                await NotifyAsync(item);
            }
            
        }



        [UnitOfWork]
        protected async Task<Guid[]> GetUsersAsync(Notification notificationInfo)
        {
            var ids = new List<Guid>();
            foreach (var item in notificationInfo.UserIds)
            {
                var receiveNotificationsStr = await SettingManager.GetOrNullForUserAsync(NotificationSettingNames.ReceiveNotifications, item);
                if (receiveNotificationsStr.IsNullOrWhiteSpace() ? true : Convert.ToBoolean(receiveNotificationsStr))
                {
                    if (await NotificationDefinitionManager.IsAvailableAsync(notificationInfo.NotificationName, item))
                    {
                        ids.Add(item);
                    }
                }
            }
            return ids.ToArray();
        }

        [UnitOfWork]
        protected virtual async Task<List<UserNotification>> SaveUserNotificationsAsync(Guid[] users, Notification notificationInfo)
        {
            var userNotifications = new List<UserNotification>();

            foreach (var user in users)
            {
                var userNotification = new UserNotification()
                {
                    TenantId = notificationInfo.TenantId,
                    UserId = user,
                    NotificationId = notificationInfo.Id
                };
                await NotificationStore.InsertUserNotificationAsync(userNotification);
                userNotifications.Add(userNotification);
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
                    await notifier.SendNotificationsAsync(userNotifications.ToUserNotificationInfo());
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