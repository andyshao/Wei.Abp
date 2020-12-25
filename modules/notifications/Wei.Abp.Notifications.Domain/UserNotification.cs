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
    public class UserNotification : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// Tenant Id.
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// User Id.
        /// </summary>
        public Guid UserId { get; set; }

        public Guid NotificationId { get; set; }

        [ForeignKey(nameof(NotificationId))]
        public Notification Notification { get; set; }
        /// <summary>
        ///当前用户通知状态
        /// </summary>
        public virtual UserNotificationState State { get; set; } = UserNotificationState.Unread;

        public UserNotificationInfo ToUserNotificationInfo()
        {
            return new UserNotificationInfo
            {
                ExtraProperties = Notification.ExtraProperties,
                Message = Notification.Message,
                NotificationName = Notification.NotificationName,
                NotificationType = Notification.NotificationType,
                Severity = Notification.Severity,
                TenantId = TenantId,
                UserId = UserId
            };
        }
    }
}
