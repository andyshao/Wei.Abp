using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.Abp.Notifications
{
    public interface INotificationDefinitionProvider
    {
        void Define(INotificationDefinitionContext context);
    }
}
