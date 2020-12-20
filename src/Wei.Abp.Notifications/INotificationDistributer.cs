using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// 用于向用户分发通知。
    /// </summary>
    public interface INotificationDistributer : IDomainService
    {
        /// <summary>
        /// Distributes given notification to users.
        /// 将给定的通知分发给用户。
        /// </summary>
        /// <param name="notificationId">The notification id.</param>
        Task DistributeAsync(Guid notificationId);
    }
}
