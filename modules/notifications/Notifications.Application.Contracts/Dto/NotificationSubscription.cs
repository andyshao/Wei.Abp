using System;
using Volo.Abp.Auditing;
using Volo.Abp.Timing;

namespace Wei.Abp.Notifications.Dto
{
    /// <summary>
    /// Represents a user subscription to a notification.
    /// </summary>
    public class NotificationSubscription : IHasCreationTime
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

        /// <summary>
        /// Entity type.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Name of the entity type (including namespaces).
        /// </summary>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Entity Id.
        /// </summary>
        public object EntityId { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSubscription"/> class.
        /// </summary>
        public NotificationSubscription()
        {
            //CreationTime = Clock.Now;
        }
    }
}
