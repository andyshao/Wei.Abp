﻿using System;
namespace Wei.Abp.Notifications.Dto
{
    public class TenantNotificationInfoOutput
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 消息通知名称
        /// </summary>
        public virtual string NotificationName { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get { return Data.Title; } }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message
        {
            get { return Data.Message; }
        }


        public string CreationTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }

        /// <summary>
        /// Notification Id.
        /// </summary>
        public virtual Guid NotificationId { get; set; }

        /// <summary>
        /// Current state of the user notification.
        /// </summary>
        public virtual UserNotificationState State { get; set; } = UserNotificationState.Unread;

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public virtual MessageNotificationData Data { get; set; }
    }
}
