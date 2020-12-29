using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;

namespace Wei.Abp.Notifications
{
    [Serializable]
    public class PublishNotificationEto: IHasExtraProperties,Volo.Abp.MultiTenancy.IMultiTenant
    {
        public PublishNotificationEto()
        {
            ExtraProperties = new ExtraPropertyDictionary();
            ExcludedUserIds = new List<Guid>();
            UserIds = new List<Guid>();
        }

        public PublishNotificationEto([NotNull] string notificationName,Guid userId, string message = null, NotificationSeverity severity = NotificationSeverity.Info)
            : this()
        {
            NotificationName = notificationName;
            Message = message;
            Severity = severity;
            UserIds.Add(userId);
        }

        /// <summary>
        /// 订阅消息的名称
        /// </summary>
        public string NotificationName { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 消息的类型
        /// </summary>
        public NotificationSeverity Severity { get; set; } = NotificationSeverity.Info;
        /// <summary>
        /// 用户的集合
        /// </summary>
        public List<Guid> UserIds { get;private set; }

        /// <summary>
        /// 例外的用户
        /// </summary>
        public List<Guid> ExcludedUserIds { get;private set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Guid? TenantId { get; set; }
    }
}
