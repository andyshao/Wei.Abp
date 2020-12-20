using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Wei.Abp.Notifications
{

    /// <summary>
    /// A notification distributed to it's related tenant.
    /// </summary>
    //[Table("AbpTenantNotifications")]
    public class TenantNotificationInfo : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public TenantNotificationInfo(Guid id)
        {
            Id = id;
        }
        /// <summary>
        /// Unique notification name.
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

        /// <summary>
        /// Tenant Id.
        /// </summary>
        public Guid? TenantId { get; set; }


    }
}
