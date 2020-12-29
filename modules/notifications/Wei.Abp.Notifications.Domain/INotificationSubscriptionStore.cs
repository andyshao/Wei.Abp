using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used to manage subscriptions for notifications.
    /// </summary>
    public interface INotificationSubscriptionStore :ITransientDependency
    {
        /// <summary>
        /// Subscribes to a notification for given user and notification informations.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task SubscribeAsync(Guid user, string notificationName);

        /// <summary>
        /// Subscribes to all available notifications for given user.
        /// It does not subscribe entity related notifications.
        /// </summary>
        /// <param name="user">User.</param>
        Task SubscribeToAllAvailableNotificationsAsync(Guid user);


        /// <summary>
        /// Unsubscribes from a notification.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task UnsubscribeAsync(Guid user, string notificationName);


        /// <summary>
        /// Gets all subscribtions for given notification (including all tenants).
        /// </summary>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task<List<NotificationSubscription>> GetSubscriptionsAsync(string notificationName);


        /// <summary>
        /// Gets subscribed notifications for a user.
        /// </summary>
        /// <param name="user">User.</param>
        Task<List<NotificationSubscription>> GetSubscribedNotificationsAsync(Guid user);

    }
}
