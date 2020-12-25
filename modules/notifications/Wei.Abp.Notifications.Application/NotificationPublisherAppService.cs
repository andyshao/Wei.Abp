using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationPublisher"/>.
    /// </summary>
    public class NotificationPublisherAppService : ApplicationService, INotificationPublisherAppService
    {
        public const int MaxUserCountToDirectlyDistributeANotification = 5;


        private readonly INotificationStore _store;
        private readonly IBackgroundJobManager _backgroundJobManager;
        protected NotificationOptions Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPublisherAppService"/> class.
        /// </summary>
        public NotificationPublisherAppService(
            IOptions<NotificationOptions> options,
            INotificationStore store,
            IBackgroundJobManager backgroundJobManager
        )
        {
            _store = store;
            _backgroundJobManager = backgroundJobManager;
            Options = options.Value;
        }

        /// <summary>
        /// 发布用户消息
        ///  <summary>
        [UnitOfWork]
        public virtual async Task PublishAsync(PublishNotificationInput input)
        {
            if (input.NotificationName.IsNullOrEmpty())
            {
                throw new ArgumentException("NotificationName can not be null or whitespace!", "notificationName");
            }

            var notification = ObjectMapper.Map<PublishNotificationInput, Notification>(input);
            notification.TenantId = CurrentTenant.Id;

            var subscriptions = await _store.GetSubscriptionsAsync(notification.NotificationName);

            //检查是否启用通知与用户是否订阅
            if (notification.UserIds.Count > 0)
            {
                var userIds = notification.ExcludedUserIds.Count <= 0 ? subscriptions.Select(c => c.UserId).ToArray() :
                    subscriptions.Where(c => !notification.ExcludedUserIds.Any(d => d == c.UserId)).Select(c => c.UserId).ToArray();
                notification.UserIds.AddRange(userIds);
            }
            // 发送给订阅用户 排除不接收的用户
            if (notification.UserIds.Count <= 0)
            {
                var userIds = notification.ExcludedUserIds.Count <= 0 ? subscriptions.Select(c => c.UserId).ToArray() :
                    subscriptions.Where(c => !notification.ExcludedUserIds.Any(d => d == c.UserId)).Select(c => c.UserId).ToArray();
                notification.UserIds.AddRange(userIds);
            }

            await _store.InsertNotificationAsync(notification);
            await CurrentUnitOfWork.SaveChangesAsync(); //To get Id of the notification
           
            if (notification.UserIds.Count <= MaxUserCountToDirectlyDistributeANotification)
            {
                //We can directly distribute the notification since there are not much receivers
                foreach (var notificationDistributerType in Options.Distributers)
                {
                    var notificationDistributer = ServiceProvider.GetRequiredService(notificationDistributerType) as INotificationDistributer;
                    await notificationDistributer.DistributeAsync(notification.Id);
                }
            }
            else
            {
                //We enqueue a background job since distributing may get a long time
                await _backgroundJobManager.EnqueueAsync(new NotificationDistributionJobArgs(
                          notification.Id,
                          CurrentTenant.Id
                      ));
            }
        }
    }
}