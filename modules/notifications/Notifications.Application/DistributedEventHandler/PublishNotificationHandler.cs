using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Wei.Abp.Notifications.Eto;

namespace Wei.Abp.Notifications.DistributedEventHandler
{
    /// <summary>
    /// 发布通知分布式订阅消息的处理
    /// </summary>
    public class PublishNotificationHandler : IDistributedEventHandler<PublishNotificationEto>, ITransientDependency
    {
        protected INotificationPublisher NotificationPublisher;
        protected ICurrentTenant CurrentTenant;
        public PublishNotificationHandler(ICurrentTenant currentTenant, INotificationPublisher notificationPublisher)
        {
            NotificationPublisher = notificationPublisher;
            CurrentTenant = currentTenant;
        }

        public virtual async Task HandleEventAsync(PublishNotificationEto eventData)
        {
            using (CurrentTenant.Change(eventData.TenantId))
            {
                await NotificationPublisher.PublishAsync(eventData.NotificationName, eventData.UserId, eventData.NotificationData, severity: eventData.Severity);
            }

        }
    }
}
