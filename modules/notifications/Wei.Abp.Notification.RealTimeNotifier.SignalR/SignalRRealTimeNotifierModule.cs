using System;
using Volo.Abp.Modularity;
using Wei.Abp.Notification.RealTimeNotifier.SignalR;

namespace Wei.Abp.Notifications
{
    [DependsOn(
        typeof(NotificationsModule),
        typeof(Volo.Abp.Users.AbpUsersAbstractionModule)
    )]
    public class SignalRRealTimeNotifierModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NotificationOptions>(options =>
            {
                options.Notifiers.Add<SignalRHubRealTimeNotifier>();
            });
        }
    }
}