using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used to store a notification request.
    /// This notification is distributed to tenants and users by <see cref="INotificationDistributer"/>.
    /// <see cref="https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/Notifications/NotificationInfo.cs"/>
    /// </summary>
    [Serializable]
    //[Table("AbpNotifications")]
    //[MultiTenancySide(MultiTenancySides.Host)]
    public class NotificationInfo : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// Unique notification name.
        /// </summary>
        [Required]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// Notification data as JSON string.
        /// </summary>
        public virtual string Data { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Notification severity.
        /// </summary>
        public virtual NotificationSeverity Severity { get; set; }
        /// <summary>
        /// 消息通知类型
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }

        /// <summary>
        /// Target users of the notification.
        /// If this is set, it overrides subscribed users.
        /// If this is null/empty, then notification is sent to all subscribed users.
        /// </summary>
        public virtual Guid[] UserIds { get; set; }

        /// <summary>
        /// Excluded users.
        /// This can be set to exclude some users while publishing notifications to subscribed users.
        /// It's not normally used if <see cref="UserIds"/> is not null.
        /// </summary>
        public virtual Guid[] ExcludedUserIds { get; set; }

        public Guid? TenantId { get; set; }
    }
}
