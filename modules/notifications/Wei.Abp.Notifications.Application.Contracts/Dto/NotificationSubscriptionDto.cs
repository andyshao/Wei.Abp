using System;
using Volo.Abp.Auditing;
using Volo.Abp.Localization;
using Volo.Abp.Timing;

namespace Wei.Abp.Notifications.Dto
{
    /// <summary>
    /// Represents a user subscription to a notification.
    /// </summary>
    public class NotificationSubscriptionDto 
    {
        /// <summary>
        /// Notification unique name.
        /// </summary>
        public string NotificationName { get; set; }

        /// <summary>
        /// 显示
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否已经订阅
        /// </summary>
        public bool Subscription { get; set; }
    }
}
