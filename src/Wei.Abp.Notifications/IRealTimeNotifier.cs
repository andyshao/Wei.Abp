using System;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Interface to send real time notifications to users.
    /// 用于向用户发送实时通知的接口。
    /// </summary>
    public interface IRealTimeNotifier : Volo.Abp.DependencyInjection.ITransientDependency
    {
        /// <summary>
        /// This method tries to deliver real time notifications to specified users.
        /// 此方法尝试将实时通知传递给指定的用户。
        /// If a user is not online, it should ignore him.
        /// 如果用户不在线，它应该忽略他。
        /// </summary>
        //Task SendNotificationsAsync(UserNotification[] userNotifications);

        /// <summary>
        /// This method tries to deliver real time notifications to specified users.
        /// 此方法尝试将实时通知传递给指定的用户。
        /// If a user is not online, it should ignore him.
        /// 如果用户不在线，它应该忽略他。
        /// </summary>
        Task SendNotificationsAsync(UserNotification userNotifications);

    }
}
