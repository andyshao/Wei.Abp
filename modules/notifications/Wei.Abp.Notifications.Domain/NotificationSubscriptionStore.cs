using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Wei.Abp.Notifications.Domain
{
    public class NotificationSubscriptionStore : DomainService, INotificationSubscriptionStore
    {
        private readonly IRepository<NotificationSubscription, Guid> _notificationSubscriptionRepository;

        protected INotificationDefinitionManager NotificationDefinitionManager { get; private set; }

        public NotificationSubscriptionStore(IRepository<NotificationSubscription, Guid> notificationSubscriptionRepository,
            INotificationDefinitionManager notificationDefinitionManager)
        {
            _notificationSubscriptionRepository = notificationSubscriptionRepository;
            NotificationDefinitionManager = notificationDefinitionManager;
        }

        /// <summary>
        /// 获取用户所有订阅信息
        /// </summary>
        /// <param name="user">用户ID</param>
        public Task<List<NotificationSubscription>> GetSubscribedNotificationsAsync(Guid user)
        {
            return AsyncExecuter.ToListAsync(_notificationSubscriptionRepository.Where(c => c.UserId == user));
        }

        /// <summary>
        /// 获取通知名称所有订阅的用户
        /// </summary>
        /// <param name="notificationName">通知名称</param>
        public Task<List<NotificationSubscription>> GetSubscriptionsAsync(string notificationName)
        {
            return AsyncExecuter.ToListAsync(_notificationSubscriptionRepository.Where(c => c.NotificationName == notificationName));
        }

       

        public async Task SubscribeAsync(Guid user, string notificationName)
        {
            if (!await AsyncExecuter.AnyAsync(_notificationSubscriptionRepository, c => c.NotificationName == notificationName && c.UserId == user))
            {
                await _notificationSubscriptionRepository.InsertAsync(new NotificationSubscription()
                {
                    UserId = user,
                    NotificationName = notificationName
                });
            }
        }

        public async Task SubscribeToAllAvailableNotificationsAsync(Guid user)
        {
            var allAvailable =await NotificationDefinitionManager.GetAllAvailableAsync(user);
            foreach (var item in allAvailable)
            {
                await SubscribeAsync(user, item.Name);
            }
        }

        public Task UnsubscribeAsync(Guid user, string notificationName)
        {
            return _notificationSubscriptionRepository.DeleteAsync(c => c.UserId == user && c.NotificationName == notificationName);
        }
    }
}
