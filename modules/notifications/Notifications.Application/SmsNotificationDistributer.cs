using System;
using System.Threading.Tasks;
using Volo.Abp.Sms;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class SmsNotificationDistributer : INotificationDistributer, Volo.Abp.DependencyInjection.ITransientDependency
    {
        protected INotificationStore NotificationStore;
        protected ISmsSender smsSender;

        public SmsNotificationDistributer(INotificationStore notificationStore)
        {
            NotificationStore = notificationStore;
        }
        //短信分发消息
        public async Task DistributeAsync(Guid notificationId)
        {
            // NotificationStore.
            // smsSender.SendAsync();
        }
    }
}