using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wei.Abp.Notifications
{
    public interface ISubscriptionChecker:Volo.Abp.DependencyInjection.ITransientDependency
    {
        /// <summary>
        /// Checks if a user subscribed for a notification.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task<bool> IsSubscribedAsync(Guid user, string notificationName);
    }
}
