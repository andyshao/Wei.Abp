using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used to store (persist) notifications.
    /// 已删除所有同步方法保留异步方法
    /// </summary>
    public interface INotificationStore : ITransientDependency
    {
        /// <summary>
        /// Inserts a notification subscription.
        /// </summary>
        Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription);

        /// <summary>
        /// Deletes a notification subscription.
        /// </summary>
        Task DeleteSubscriptionAsync(Guid userId, string notificationName);

        /// <summary>
        /// Inserts a notification.
        /// </summary>
        Task InsertNotificationAsync(NotificationInfo notification);

        /// <summary>
        /// Gets a notification by Id, or returns null if not found.
        /// </summary>
        Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId);

        /// <summary>
        /// Inserts a user notification.
        /// </summary>
        Task InsertUserNotificationAsync(UserNotificationInfo userNotification);

        /// <summary>
        /// Gets subscriptions for a notification.
        /// </summary>
        Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName);

        /// <summary>
        /// Gets subscriptions for a user.
        /// </summary>
        Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(Guid userId);

        /// <summary>
        /// Checks if a user subscribed for a notification
        /// </summary>
        Task<bool> IsSubscribedAsync(Guid id, string notificationName);

        /// <summary>
        /// Updates a user notification state.
        /// </summary>
        Task UpdateUserNotificationStateAsync(Guid userNotificationId, UserNotificationState state);

        /// <summary>
        /// Updates all notification states for a user.
        /// </summary>
        Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state);

        /// <summary>
        /// Deletes a user notification.
        /// </summary>
        Task DeleteUserNotificationAsync(Guid userNotificationId);

        /// <summary>
        /// Deletes all notifications of a user.
        /// </summary>
        Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets notifications of a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        //Task<List<UserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets user notification count.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">The state.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets a user notification.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="userNotificationId">Skip count.</param>
        //Task<UserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync(Guid userNotificationId);

        /// <summary>
        /// Inserts notification for a tenant.
        /// </summary>
        Task InsertTenantNotificationAsync(TenantNotificationInfo tenantNotificationInfo);

        Task DeleteNotificationAsync(Guid id);
    }
}