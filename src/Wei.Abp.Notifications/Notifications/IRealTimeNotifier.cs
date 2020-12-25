using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wei.Abp.Notifications;

namespace Wei.Abp.Notifications
{
    public interface IRealTimeNotifier
    {
        /// <summary>
        /// 此方法尝试将实时通知传递给指定的用户。
        /// </summary>
        Task SendNotificationsAsync(UserNotificationInfo userNotification);
    }
}
