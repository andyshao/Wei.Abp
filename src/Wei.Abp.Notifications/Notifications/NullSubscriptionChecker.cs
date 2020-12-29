using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Features;

namespace Wei.Abp.Notifications
{
    [Dependency(TryRegister = true)]
    public class NullSubscriptionChecker : ISubscriptionChecker, ISingletonDependency
    {
        public Task<bool> IsSubscribedAsync(Guid user, string notificationName)
        {
            return Task.FromResult(true);
        }
    }
}
