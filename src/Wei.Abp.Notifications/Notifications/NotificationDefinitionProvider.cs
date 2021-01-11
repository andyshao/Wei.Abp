using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Wei.Abp.Notifications
{
    public abstract class NotificationDefinitionProvider : INotificationDefinitionProvider, ITransientDependency
    {
        public abstract void Define(INotificationDefinitionContext context);
    }
}
