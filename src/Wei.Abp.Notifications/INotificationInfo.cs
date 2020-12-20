using System;
using Volo.Abp.MultiTenancy;

namespace Wei.Abp.Notifications
{
    public interface INotificationInfo : IMultiTenant
    {
        string NotificationName { get; }

        /// <summary>
        /// Notification data as JSON string.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Notification severity.
        /// </summary>
        NotificationSeverity Severity { get; }

        /// <summary>
        /// Target users of the notification.
        /// If this is set, it overrides subscribed users.
        /// If this is null/empty, then notification is sent to all subscribed users.
        /// </summary>
        Guid[] UserIds { get; }

        /// <summary>
        /// Excluded users.
        /// This can be set to exclude some users while publishing notifications to subscribed users.
        /// It's not normally used if <see cref="UserIds"/> is not null.
        /// </summary>
        Guid[] ExcludedUserIds { get; }

        /// <summary>
        /// Target tenants of the notification.
        /// Used to send notification to subscribed users of specific tenant(s).
        /// This is valid only if UserIds is null.
        /// If it's "0", then indicates to all tenants.
        /// 前面为0发布所有 现在为null 发布给所有租户
        /// </summary>
        //[StringLength(MaxTenantIdsLength)]
        Guid? TenantId { get; }

    }
}
