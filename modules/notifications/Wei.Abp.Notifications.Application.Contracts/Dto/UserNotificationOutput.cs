using System;
using Volo.Abp.Data;

namespace Wei.Abp.Notifications.Dto
{
    public class UserNotificationOutput:Volo.Abp.Data.IHasExtraProperties
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 消息通知名称
        /// </summary>
        public virtual string NotificationName { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }
        /// <summary>
        /// 消息级别
        /// </summary>
        public virtual NotificationSeverity Severity { get; set; }

        /// <summary>
        /// Notification Id.
        /// </summary>
        public virtual Guid NotificationId { get; set; }

        /// <summary>
        /// Current state of the user notification.
        /// </summary>
        public virtual UserNotificationState State { get; set; } = UserNotificationState.Unread;



        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
