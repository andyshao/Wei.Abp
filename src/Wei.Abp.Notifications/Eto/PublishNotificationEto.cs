using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace Wei.Abp.Notifications.Eto
{
    /// <summary>
    /// 通知消息的ETO
    /// </summary>
    public class PublishNotificationEto : IMultiTenant
    {
        public PublishNotificationEto()
        {
            NotificationData = new NotificationData();
        }
        /// <summary>
        /// 消息通知唯一名称
        /// </summary>
        public virtual string NotificationName { get; set; }

        public NotificationData NotificationData { get; set; }
        public Guid? TenantId { get; set; }
        /// <summary>
        /// User Id.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Notification severity.
        /// </summary>
        public virtual NotificationSeverity Severity { get; set; }
        /// <summary>
        /// 消息通知类型
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }
    }
}
