using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Wei.Abp.Notifications;

namespace Notifications.Dto
{
    public record PublishNotificationInput:Volo.Abp.Data.IHasExtraProperties
    {
        public PublishNotificationInput()
        {
            UserIds = new List<Guid>();
            ExcludedUserIds = new List<Guid>();
        }
        /// <summary>
        /// 订阅消息的名称
        /// </summary>
        [System.ComponentModel.DataAnnotations.Required]
        public string notificationName { get; set; }
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
