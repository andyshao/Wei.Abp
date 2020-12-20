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
    public class NotificationPublisher : ApplicationService, INotificationPublisher
    {
        public const int MaxUserCountToDirectlyDistributeANotification = 5;


        private readonly INotificationStore _store;
        private readonly IBackgroundJobManager _backgroundJobManager;
        protected INotificationDistributer[] Distributers;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPublisher"/> class.
        /// </summary>
        public NotificationPublisher(
            INotificationDistributer[] distributers,
            INotificationStore store,
            IBackgroundJobManager backgroundJobManager
        )
        {
            _store = store;
            _backgroundJobManager = backgroundJobManager;
            Distributers = distributers;
        }

        //Create EntityIdentifier includes entityType and entityId.
        /// <summary>
        /// 发布用户消息
        /// </summary>
        /// <param name="notificationName">订阅消息的名称</param>
        /// <param name="data">消息内容</param>
        /// <param name="severity">消息的类型</param>
        /// <param name="userIds">用户的集合</param>
        /// <param name="excludedUserIds">例外的用户</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task PublishAsync(
            string notificationName,
            NotificationData data = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null)
        {
            if (notificationName.IsNullOrEmpty())
            {
                throw new ArgumentException("NotificationName can not be null or whitespace!", "notificationName");
            }

            var notificationInfo = new NotificationInfo()
            {
                NotificationName = notificationName,
                Severity = severity,
                UserIds = userIds,
                ExcludedUserIds = excludedUserIds,
                TenantId = CurrentTenant.Id,
                Data = data == null ? null : System.Text.Json.JsonSerializer.Serialize(data),
            };

            await _store.InsertNotificationAsync(notificationInfo);

            //if (userIds != null && userIds.Length > 0)
            //{
            //    foreach (var item in userIds)
            //    {
            //        var user = new UserNotificationInfo();
            //        user.NotificationName = notificationName;
            //        user.UserId = item;
            //        user.NotificationId = notificationInfo.Id;
            //        user.TenantId = CurrentTenant.Id;
            //        await _store.InsertUserNotificationAsync(user);
            //    }
            //}

            //// 发送给订阅用户 排除不接收的用户
            //else
            //{
            //    var subscriptions = await _store.GetSubscriptionsAsync(notificationName);

            //    foreach (var item in excludedUserIds==null?subscriptions: subscriptions.Where(c=>!excludedUserIds.Any(d=>d==c.UserId)))
            //    {
            //        var user = new UserNotificationInfo();
            //        user.NotificationName = notificationName;
            //        user.UserId = item.UserId;
            //        user.NotificationId = notificationInfo.Id;
            //        user.TenantId = CurrentTenant.Id;
            //        await _store.InsertUserNotificationAsync(user);
            //    }
            //}

            await CurrentUnitOfWork.SaveChangesAsync(); //To get Id of the notification

            //TODO:考虑发布分布式队列消息 让job服务去执行发送消息给用户
            if (userIds != null && userIds.Length <= MaxUserCountToDirectlyDistributeANotification)
            {
                //We can directly distribute the notification since there are not much receivers
                if (Distributers == null) return;
                foreach (var notificationDistributer in Distributers)
                {
                    //var notificationDistributer = ServiceProvider.GetRequiredService(notificationDistributorType) as INotificationDistributer;
                    await notificationDistributer.DistributeAsync(notificationInfo.Id);
                }
            }
            else
            {
                //We enqueue a background job since distributing may get a long time
                await _backgroundJobManager.EnqueueAsync(new NotificationDistributionJobArgs(
                          notificationInfo.Id,
                          CurrentTenant.Id
                      ));
            }
        }

        public async Task PublishAsync(string notificationName, Guid userId, NotificationData data = null, NotificationSeverity severity = NotificationSeverity.Info)
        {
            if (notificationName.IsNullOrEmpty())
            {
                throw new ArgumentException("NotificationName can not be null or whitespace!", "notificationName");
            }

            var notificationInfo = new NotificationInfo()
            {
                NotificationName = notificationName,
                Severity = severity,
                UserIds = new Guid[] { userId },
                TenantId = CurrentTenant.Id,
                Data = data == null ? null : System.Text.Json.JsonSerializer.Serialize(data),
            };

            await _store.InsertNotificationAsync(notificationInfo);


            //var user = new UserNotificationInfo();
            //user.NotificationName = notificationName;
            //user.UserId = userId;
            //user.NotificationId = notificationInfo.Id;
            //user.TenantId = CurrentTenant.Id;
            //await _store.InsertUserNotificationAsync(user);

            await CurrentUnitOfWork.SaveChangesAsync(); //To get Id of the notification

            //TODO:考虑发布分布式队列消息 让job服务去执行发送消息给用户

            if (Distributers == null) return;
            foreach (var notificationDistributer in Distributers)
            {
                //var notificationDistributer = ServiceProvider.GetRequiredService(notificationDistributorType) as INotificationDistributer;
                await notificationDistributer.DistributeAsync(notificationInfo.Id);
            }
        }
    }
}