using System;
namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Arguments for <see cref="NotificationDistributionJob"/>.
    /// </summary>
    [Serializable]
    public class NotificationDistributionJobArgs : Volo.Abp.MultiTenancy.IMultiTenant
    {
        /// <summary>
        /// Notification Id.
        /// </summary>
        public Guid NotificationId { get; set; }

        public Guid? TenantId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDistributionJobArgs"/> class.
        /// </summary>
        public NotificationDistributionJobArgs(Guid notificationId, Guid? tenantId)
        {
            TenantId = tenantId;
            NotificationId = notificationId;
        }
    }
}
