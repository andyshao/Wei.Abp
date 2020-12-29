using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Wei.Abp.Notifications.Dto;

namespace Wei.Abp.Notifications
{
    public interface IUserNotificationSubscriptionAppService:IApplicationService
    {
        /// <summary>
        /// 为用户订阅指定的通知
        /// </summary>
        /// <param name="user">User</param>
        Task SubscribeAsync(string notificationName);

        /// <summary>
        /// 为用户订阅所有可用的通知
        /// </summary>
        /// <param name="user">User.</param>
        Task SubscribeToAllAvailableNotificationsAsync();


        /// <summary>
        /// 取消订阅通知
        /// </summary>
        /// <param name="user">User.</param>
        Task UnsubscribeAsync(string notificationName);

        //Task<List<NotificationSubscriptionDto>> GetSubscriptionsAsync(string notificationName);
        /// <summary>
        /// 获取用户所有通知情况
        /// </summary>
        Task<List<NotificationSubscriptionDto>> GetAllAsync();
        /// <summary>
        /// 获取用户所有已订阅的通知
        /// </summary>
        //Task<List<NotificationSubscriptionDto>> GetSubscribedNotificationsAsync();
    }
}
