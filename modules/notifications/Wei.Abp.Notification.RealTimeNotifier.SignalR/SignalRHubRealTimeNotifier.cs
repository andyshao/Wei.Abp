using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.MultiTenancy;
using Wei.Abp.Notifications;

namespace Wei.Abp.Notification.RealTimeNotifier.SignalR
{
    /// <summary>
    /// SignalR 在线通知
    /// </summary>
    
    public class SignalRHubRealTimeNotifier : IRealTimeNotifier, Volo.Abp.DependencyInjection.ITransientDependency
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        //private readonly ICurrentTenant CurrentTenant;

        public SignalRHubRealTimeNotifier(IHubContext<NotificationHub> hubContext
            //, ICurrentTenant currentTenant
            )
        {
            _hubContext = hubContext;
            //CurrentTenant = currentTenant;
        }

        public Task SendNotificationsAsync(UserNotificationInfo userNotification)
        {
            //using (CurrentTenant.Change(userNotification.TenantId))
            //{
                return _hubContext.Clients
               .User(userNotification.UserId.ToString())
               .SendAsync(userNotification.NotificationName, userNotification.Message, userNotification.ExtraProperties);
            //}
        }
    }
}
