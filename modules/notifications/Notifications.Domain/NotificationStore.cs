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
        private readonly IRepository<NotificationInfo, Guid> _notificationRepository;
        //private readonly IRepository<TenantNotificationInfo, Guid> _tenantNotificationRepository;
        private readonly IRepository<UserNotificationInfo, Guid> _userNotificationRepository;
        private readonly IRepository<NotificationSubscriptionInfo, Guid> _notificationSubscriptionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationStore"/> class.
        /// </summary>
        public NotificationStore(
            IRepository<NotificationInfo, Guid> notificationRepository,
            //IRepository<TenantNotificationInfo, Guid> tenantNotificationRepository,
            IRepository<UserNotificationInfo, Guid> userNotificationRepository,
            IRepository<NotificationSubscriptionInfo, Guid> notificationSubscriptionRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _notificationRepository = notificationRepository;
            //_tenantNotificationRepository = tenantNotificationRepository;
            _userNotificationRepository = userNotificationRepository;
            _notificationSubscriptionRepository = notificationSubscriptionRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public virtual async Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription)
        {
            using (CurrentTenant.Change(subscription.TenantId))
            {
                await _notificationSubscriptionRepository.InsertAsync(subscription);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }

        [UnitOfWork]
        public virtual async Task DeleteSubscriptionAsync(Guid userId, string notificationName)
        {
    
            await _notificationSubscriptionRepository.DeleteAsync(s =>
                s.UserId == userId &&
                s.NotificationName == notificationName
                );
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        [UnitOfWork]
        public virtual async Task InsertNotificationAsync(NotificationInfo notification)
        {
            using (CurrentTenant.Change(null))
            {
                await _notificationRepository.InsertAsync(notification);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }


        [UnitOfWork]
        public virtual async Task<NotificationInfo> GetNotificationOrNullAsync(Guid notificationId)
        {
            using (CurrentTenant.Change(null))
            {
                return await _notificationRepository.Where(c => c.Id == notificationId).FirstOrDefaultAsync();
            }
        }


        [UnitOfWork]
        public virtual async Task InsertUserNotificationAsync(UserNotificationInfo userNotification)
        {
            using (CurrentTenant.Change(userNotification.TenantId))
            {
                await _userNotificationRepository.InsertAsync(userNotification);
                await _unitOfWorkManager.Current.SaveChangesAsync();
            }
        }

        [UnitOfWork]
        public virtual Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName)
        {
            //using (_dataFilter.Disable<Volo.Abp.MultiTenancy.IMultiTenant>())
            //{
            return _notificationSubscriptionRepository.Where(s => s.NotificationName == notificationName).ToListAsync();
            //}
        }

        [UnitOfWork]
        public virtual async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(Guid userId)
        {
            return await _notificationSubscriptionRepository.Where(s => s.UserId == userId).ToListAsync();
        }


        [UnitOfWork]
        public virtual async Task<bool> IsSubscribedAsync(Guid userId, string notificationName)
        {
            return await _notificationSubscriptionRepository.CountAsync(s =>
                s.UserId == userId &&
                s.NotificationName == notificationName
                ) > 0;
        }

        [UnitOfWork]
        public virtual async Task UpdateUserNotificationStateAsync(Guid userNotificationId, UserNotificationState state)
        {
            var userNotification = await _userNotificationRepository.Where(c => c.UserId == userNotificationId).FirstOrDefaultAsync();
            if (userNotification == null)
            {
                return;
            }

            userNotification.State = state;
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        [UnitOfWork]
        public virtual async Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state)
        {
            var userNotifications = await _userNotificationRepository.Where(un => un.UserId == userId).ToListAsync();

            foreach (var userNotification in userNotifications)
            {
                userNotification.State = state;
            }

            await _unitOfWorkManager.Current.SaveChangesAsync();
        }


        [UnitOfWork]
        public virtual async Task DeleteUserNotificationAsync(Guid userNotificationId)
        {

            await _userNotificationRepository.DeleteAsync(userNotificationId);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        [UnitOfWork]
        public virtual async Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {

            var predicate = CreateNotificationFilterPredicate(userId, state, startDate, endDate);

            await _userNotificationRepository.DeleteAsync(predicate);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }


        private Expression<Func<UserNotificationInfo, bool>> CreateNotificationFilterPredicate(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var predicate = PredicateBuilder.New<UserNotificationInfo>();
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

        [UnitOfWork]
        public virtual Task<List<UserNotificationInfo>> GetUserNotificationsAsync(Guid user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null)
        {

            var query = _userNotificationRepository.Where(CreateNotificationFilterPredicate(user, state, startDate, endDate)).PageBy(skipCount, maxResultCount);

            return AsyncExecuter.ToListAsync(query);
        }

        [UnitOfWork]
        public virtual async Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {

            var predicate = CreateNotificationFilterPredicate(userId, state, startDate, endDate);
            return await _userNotificationRepository.CountAsync(predicate);
        }


        //[UnitOfWork]
        //public virtual async Task InsertTenantNotificationAsync(TenantNotificationInfo tenantNotificationInfo)
        //{
        //    await _tenantNotificationRepository.InsertAsync(tenantNotificationInfo);
        //}

        public Task DeleteNotificationAsync(Guid id)
        {
            return _notificationRepository.DeleteAsync(id);
        }
    }
}
