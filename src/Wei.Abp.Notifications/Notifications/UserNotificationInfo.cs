using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;

namespace Wei.Abp.Notifications
{
    public class UserNotificationInfo: IHasExtraProperties
    {
        /// <summary>
        /// Tenant Id.
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// User Id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Unique notification name.
        /// </summary>
        public virtual string NotificationName { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// 消息级别
        /// </summary>
        public virtual NotificationSeverity Severity { get; set; }
        /// <summary>
        /// 消息通知类型
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
