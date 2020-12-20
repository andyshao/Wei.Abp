using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace Wei.Abp.Notifications
{
    [Serializable]
    public class UserNotification : IHasExtraProperties
    {        /// <summary>
             /// Tenant Id.
             /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// User Id.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 消息通知唯一名称
        /// </summary>
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// Notification severity.
        /// </summary>
        public virtual NotificationSeverity Severity { get; set; }
        /// <summary>
        /// 消息通知类型
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
