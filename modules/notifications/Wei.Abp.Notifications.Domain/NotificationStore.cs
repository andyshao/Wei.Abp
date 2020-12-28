using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Wei.Abp.Notifications.Domain
{
    /// <summary>
    /// Implements <see cref="INotificationStore"/> using repositories.
    /// </summary>
    public class NotificationStore : DomainService, INotificationStore, ITransientDependency
    {
        private readonly IRepository<Notification, Guid> _notificationRepository;
        private readonly IRepository<UserNotification, Guid> _userNotificationRepository;
        private readonly IRepository<NotificationSubscription, Guid> _notificationSubscriptionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationStore"/> class.
        /// </summary>
        public NotificationStore(
            IRepository<Notification, Guid> notificationRepository,
            IRepository<UserNotification, Guid> userNotificationRepository,
            IRepository<NotificationSubscription, Guid> notificationSubscriptionRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _notificationRepository = notificationRepository;
            _userNotificationRepository = userNotificationRepository;
            _notificationSubscriptionRepository = notificationSubscriptionRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public virtual Task InsertSubscriptionAsync(NotificationSubscription subscription)
        {
            return _notificationSubscriptionRepository.InsertAsync(subscription);
        }

        public virtual async Task DeleteSubscriptionAsync(Guid userId, string notificationName)
        {
    
            await _notificationSubscriptionRepository.DeleteAsync(s =>
                s.UserId == userId &&
                s.NotificationName == notificationName
                );
        }

        public virtual Task InsertNotificationAsync(Notification notification)
        {
              return  _notificationRepository.InsertAsync(notification);
        }


        public virtual Task<Notification> GetNotificationOrNullAsync(Guid notificationId)
        {

            return  _notificationRepository.GetAsync(notificationId); ;
        }


        public virtual async Task InsertUserNotificationAsync(UserNotification userNotification)
        {
                await  _userNotificationRepository.InsertAsync(userNotification);
        }

        public virtual Task<List<NotificationSubscription>> GetSubscriptionsAsync(string notificationName)
        {
            return AsyncExecuter.ToListAsync(_notificationSubscriptionRepository.Where(s => s.NotificationName == notificationName));
        }

        public virtual Task<List<NotificationSubscription>> GetSubscriptionsAsync(Guid userId)
        {
            return AsyncExecuter.ToListAsync(_notificationSubscriptionRepository.Where(s => s.UserId == userId));
        }


        public virtual async Task<bool> IsSubscribedAsync(Guid userId, string notificationName)
        {
            return await AsyncExecuter.CountAsync(_notificationSubscriptionRepository.Where(s =>
                s.UserId == userId &&
                s.NotificationName == notificationName
                )) > 0;
        }

        public virtual async Task UpdateUserNotificationStateAsync(Guid userNotificationId, UserNotificationState state)
        {
            var userNotification = await AsyncExecuter.FirstOrDefaultAsync(_userNotificationRepository.Where(c => c.UserId == userNotificationId));
            if (userNotification == null)
            {
                return;
            }

            userNotification.State = state;
            await _userNotificationRepository.UpdateAsync(userNotification);
        }

        public virtual async Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state)
        {
            var userNotifications = await AsyncExecuter.ToListAsync(_userNotificationRepository.Where(un => un.UserId == userId));

            foreach (var userNotification in userNotifications)
            {
                userNotification.State = state;
                await _userNotificationRepository.UpdateAsync(userNotification);
            }
        }


        public virtual Task DeleteUserNotificationAsync(Guid userNotificationId)
        {

            return _userNotificationRepository.DeleteAsync(userNotificationId);
        }

        [UnitOfWork]
        public virtual Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {

            var predicate = CreateNotificationFilterPredicate(userId, state, startDate, endDate);

            return _userNotificationRepository.DeleteAsync(predicate);
        }


        private Expression<Func<UserNotification, bool>> CreateNotificationFilterPredicate(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var predicate = PredicateBuilder.New<UserNotification>();
            predicate = predicate.And(p => p.UserId == userId);

            if (startDate.HasValue)
            {
                predicate = predicate.And(p => p.CreationTime >= startDate);
            }

            if (endDate.HasValue)
            {
                predicate = predicate.And(p => p.CreationTime <= endDate);
            }

            if (state.HasValue)
            {
                predicate = predicate.And(p => p.State == state);
            }

            return predicate;
        }

        public virtual Task<List<UserNotification>> GetUserNotificationsAsync(Guid user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null)
        {

            var query = _userNotificationRepository.Where(CreateNotificationFilterPredicate(user, state, startDate, endDate)).PageBy(skipCount, maxResultCount);

            return AsyncExecuter.ToListAsync(query);
        }

        public virtual async Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {

            var predicate = CreateNotificationFilterPredicate(userId, state, startDate, endDate);
            return await _userNotificationRepository.CountAsync(predicate);
        }

        public virtual async Task<List<UserNotification>> GetUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {

            var predicate = CreateNotificationFilterPredicate(userId, state, startDate, endDate);
            return await AsyncExecuter.ToListAsync(_userNotificationRepository.Where(predicate));
        }

        public Task DeleteNotificationAsync(Guid id)
        {
            return _notificationRepository.DeleteAsync(id);
        }
    }
}
