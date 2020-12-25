using System;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace Wei.Abp.Notifications
{

    /// <summary>
    /// 发布一个消息通知
    /// </summary>
    public interface INotificationPublisher : Volo.Abp.DependencyInjection.ITransientDependency
    {
        /// <summary>
        /// 发布通知事件
        /// </summary>
        /// <param name="notificationName">通知名称</param>
        /// <param name="userId">用户ID</param>
        /// <param name="data">通知内容</param>
        /// <param name="severity">通知的严重程度</param>
        /// <returns></returns>
        Task PublishAsync(
            string notificationName,
            Guid userId,
            NotificationData data,
            NotificationSeverity severity = NotificationSeverity.Info);
    }
}