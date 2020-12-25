using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used to store a user notification.
    /// </summary>
    public class UserNotificationInfo : FullAuditedAggregateRoot<Guid>, IMultiTenant
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
        /// 消息通知名称
        /// </summary>
        [Required]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// Notification Id.
        /// </summary>
        [Required]
        public virtual Guid NotificationId { get; set; }

        [ForeignKey(nameof(NotificationId))]
        public virtual NotificationInfo Notification { get; set; }

        /// <summary>
        /// Current state of the user notification.
        /// </summary>
        public virtual UserNotificationState State { get; set; } = UserNotificationState.Unread;


        public UserNotification ToUserNotification()
        {
            return new UserNotification();
        }
    }
}
