using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Wei.Abp.Notifications
{
    public class NotificationSubscription : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// Tenant id of the subscribed user.
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Notification unique name.
        /// </summary>
        public string NotificationName { get; set; }

    }
}
