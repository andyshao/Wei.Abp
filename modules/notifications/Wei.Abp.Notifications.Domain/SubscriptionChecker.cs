using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Wei.Abp.Notifications
{
    public class SubscriptionChecker : DomainService, ISubscriptionChecker, ITransientDependency
    {
        protected IRepository<NotificationSubscription, Guid> NotificationSubscriptionRepository;

        public SubscriptionChecker(IRepository<NotificationSubscription, Guid> notificationSubscriptionRepository)
        {
            NotificationSubscriptionRepository = notificationSubscriptionRepository;
        }

        /// <summary>
        /// 检查用户是否订阅了通知
        /// </summary>
        /// <param name="notificationName">通知名称</param>
        public Task<bool> IsSubscribedAsync(Guid user, string notificationName)
        {
            return AsyncExecuter.AnyAsync(NotificationSubscriptionRepository, c => c.NotificationName == notificationName && c.UserId == user);
        }
    }
}
