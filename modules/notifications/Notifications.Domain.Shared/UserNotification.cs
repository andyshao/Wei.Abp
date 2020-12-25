using System;
using Volo.Abp.Data;

namespace Wei.Abp.Notifications
{
    public class UserNotification:Volo.Abp.Data.IHasExtraProperties
    {
        /// <summary>
        /// User Id.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 消息通知名称
        /// </summary>
        public virtual string NotificationName { get; set; }

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
