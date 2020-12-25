using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// 用于向用户分发通知。
    /// </summary>
    public interface INotificationDistributer 
    {
        /// <summary>
        /// 将给定的通知分发给用户。
        /// </summary>
        Task DistributeAsync(Guid userNotificationId);
    }
}
