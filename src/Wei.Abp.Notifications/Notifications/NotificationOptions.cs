using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Collections;

namespace Wei.Abp.Notifications
{
    public class NotificationOptions
    {
        /// <summary>
        /// 消息定义提供
        /// </summary>
        public ITypeList<INotificationDefinitionProvider> DefinitionProviders { get; private set; }

        /// <summary>
        /// 消息分发者
        /// </summary>
        public ITypeList<INotificationDistributer> Distributers { get; private set; }
        /// <summary>
        /// 实时通知提供
        /// </summary>
        public ITypeList<IRealTimeNotifier> Notifiers { get; private set; }

        public NotificationOptions()
        {
            DefinitionProviders = new TypeList<INotificationDefinitionProvider>();
            Distributers = new TypeList<INotificationDistributer>();
            Notifiers = new TypeList<IRealTimeNotifier>();
        }
    }
}
