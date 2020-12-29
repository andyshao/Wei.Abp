using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Wei.Abp.Notifications.DistributedEventHandler
{
    /// <summary>
    /// 发布通知分布式订阅消息的处理
    /// </summary>
    public class PublishNotificationHandler : IDistributedEventHandler<PublishNotificationEto>, ITransientDependency
    {
        protected INotificationPublisherAppService NotificationPublisher;
        protected ICurrentTenant CurrentTenant;
        public PublishNotificationHandler(ICurrentTenant currentTenant,
            INotificationPublisherAppService notificationPublisher)
        {
            NotificationPublisher = notificationPublisher;
            CurrentTenant = currentTenant;
        }
        
        [UnitOfWork]
        public virtual async Task HandleEventAsync(PublishNotificationEto eventData)
        {
            using (CurrentTenant.Change(eventData.TenantId))
            {
                var input = new PublishNotificationInput(eventData.NotificationName, eventData.Message, eventData.Severity, eventData.UserIds, eventData.ExcludedUserIds);
                input.ExtraProperties = eventData.ExtraProperties;
                await NotificationPublisher.PublishAsync(input);
            }

        }
    }
}
