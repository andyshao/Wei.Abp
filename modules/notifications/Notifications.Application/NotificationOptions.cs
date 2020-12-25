using System;
using Volo.Abp.Collections;

namespace Wei.Abp.Notifications
{
    public class NotificationOptions
    {
        // /// <summary>
        // /// 消息通知提供
        // /// </summary>
        // public ITypeList<NotificationProvider> Providers { get; private set; }
        /// <summary>
        /// 消息分发提供者
        /// </summary>
        public ITypeList<INotificationDistributer> Distributers { get; private set; }
        /// <summary>
        /// 实时通知提供
        /// </summary>
        public ITypeList<IRealTimeNotifier> Notifiers { get; private set; }

        public NotificationOptions()
        {
            // Providers = new TypeList<NotificationProvider>();
            Distributers = new TypeList<INotificationDistributer>();
            Notifiers = new TypeList<IRealTimeNotifier>();
        }
    }
}
