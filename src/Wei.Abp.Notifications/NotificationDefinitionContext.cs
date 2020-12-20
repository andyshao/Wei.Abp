using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.Abp.Notifications
{
    public class NotificationDefinitionContext : INotificationDefinitionContext
    {
        public INotificationDefinitionManager Manager { get; private set; }

        public NotificationDefinitionContext(INotificationDefinitionManager manager)
        {
            Manager = manager;
        }
    }
}
