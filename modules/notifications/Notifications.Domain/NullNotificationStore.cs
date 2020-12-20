using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Wei.Abp.Notifications
{
    public class NullNotificationStore
    //: INotificationStore, ITransientDependency
    {

        public Task DeleteAllUserNotificationsAsync(Guid user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return Task.FromResult(0);
        }

        public Task DeleteNotificationAsync(NotificationInfo notification)
        {
            return Task.FromResult(0);
        }

        public Task DeleteSubscriptionAsync(Guid user, string notificationName)
        {
            return Task.FromResult(0);
        }


        public Task DeleteUserNotificationAsync(Guid? tenantId, Guid userNotificationId)
        {
            return Task.FromResult(0);
        }

        public Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId)
        {
            return Task.FromResult(default(NotificationInfo));
        }

        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName)
        {
            return Task.FromResult(default(List<NotificationSubscriptionInfo>));
        }

        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(Guid[] tenantIds, string notificationName)
        {
            return Task.FromResult(default(List<NotificationSubscriptionInfo>));
        }

        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(Guid user)
        {
            return Task.FromResult(default(List<NotificationSubscriptionInfo>));
        }

        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, string entityTypeName, Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(Guid[] tenantIds, string notificationName, string entityTypeName, Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUserNotificationCountAsync(Guid user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return Task.FromResult(0);
        }

        //public Task<List<UserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync (UserIdentifier user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null) {
        //    return Task.FromResult (default (List<UserNotificationInfoWithNotificationInfo>));
        //}

        //public Task<UserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync (Guid? tenantId, Guid userNotificationId) {
        //    return Task.FromResult (default (UserNotificationInfoWithNotificationInfo));
        //}

        public Task InsertNotificationAsync(NotificationInfo notification)
        {
            return Task.FromResult(0);
        }

        public Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription)
        {
            return Task.FromResult(0);
        }

        public Task InsertTenantNotificationAsync(TenantNotificationInfo tenantNotificationInfo)
        {
            return Task.FromResult(0);
        }

        public Task InsertUserNotificationAsync(UserNotificationInfo userNotification)
        {
            return Task.FromResult(0);
        }

        public Task<bool> IsSubscribedAsync(Guid user, string notificationName)
        {
            return Task.FromResult(false);
        }

        public Task<bool> IsSubscribedAsync(Guid user, string notificationName, string entityTypeName, Guid entityId)
        {
            return Task.FromResult(false);
        }

        public Task UpdateAllUserNotificationStatesAsync(Guid user, UserNotificationState state)
        {
            return Task.FromResult(0);
        }

        public Task UpdateUserNotificationStateAsync(Guid? tenantId, Guid userNotificationId, UserNotificationState state)
        {
            return Task.FromResult(0);
        }
    }
}