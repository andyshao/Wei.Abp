using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Wei.Abp.Notifications;

namespace Wei.Abp.Notifications
{
    public record PublishNotificationInput: IHasExtraProperties
    {

        public PublishNotificationInput([NotNull] string notificationName, string message=null, NotificationSeverity severity=NotificationSeverity.Info, List<Guid> userIds=null, List<Guid> excludedUserIds=null)
        {
            NotificationName = notificationName;
            Message = message;
            Severity = severity;
            ExcludedUserIds = excludedUserIds == null ? new List<Guid>() : excludedUserIds; ;
            UserIds = userIds == null ? new List<Guid>() : userIds;
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
        public List<Guid> UserIds { get; set; }
        /// <summary>
        /// 例外的用户
        /// </summary>
        public List<Guid> ExcludedUserIds { get; set; }
        /// <summary>
        /// 扩展属性
        /// </summary>
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}
