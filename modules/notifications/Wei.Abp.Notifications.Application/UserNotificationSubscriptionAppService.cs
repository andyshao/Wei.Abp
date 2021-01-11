using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Wei.Abp.Notifications.Dto;

namespace Wei.Abp.Notifications
{
    [Authorize]
    public class UserNotificationSubscriptionAppService :ApplicationService, IUserNotificationSubscriptionAppService
    {
        protected INotificationSubscriptionStore NotificationSubscriptionStore { get;private set; }
        protected INotificationDefinitionManager NotificationDefinitionManager { get; private set; }
        public UserNotificationSubscriptionAppService(INotificationSubscriptionStore notificationSubscriptionStore
            , INotificationDefinitionManager notificationDefinitionManager)
        {
            NotificationSubscriptionStore = notificationSubscriptionStore;
            NotificationDefinitionManager = notificationDefinitionManager;
        }
        /// <summary>
        /// 获取用户所有订阅情况
        /// </summary>
        /// <returns></returns>
        public async Task<List<NotificationSubscriptionDto>> GetAllAsync()
        {
            var subscriptions = await NotificationSubscriptionStore.GetSubscribedNotificationsAsync(CurrentUser.Id.Value);

            var allDefineNotify = NotificationDefinitionManager.GetAll();
            var outputs = new List<NotificationSubscriptionDto>();

            foreach (var item in allDefineNotify)
            {
                var subscriptionDto = new NotificationSubscriptionDto
                {
                    Subscription = subscriptions.Any(c => c.NotificationName == item.Name),
                    NotificationName = item.Name,
                    Description = item.Description.Localize(StringLocalizerFactory),
                    DisplayName = item.DisplayName.Localize(StringLocalizerFactory)
                };
                outputs.Add(subscriptionDto);
            }
            return outputs;
        }

        public Task SubscribeAsync(string notificationName)
        {
            return NotificationSubscriptionStore.SubscribeAsync(CurrentUser.Id.Value, notificationName);
        }

        public Task SubscribeToAllAvailableNotificationsAsync()
        {
            return NotificationSubscriptionStore.SubscribeToAllAvailableNotificationsAsync(CurrentUser.Id.Value);
        }

        public Task UnsubscribeAsync(string notificationName)
        {
            return NotificationSubscriptionStore.UnsubscribeAsync(CurrentUser.Id.Value,notificationName);
        }
    }
}
