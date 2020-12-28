using System;
using Volo.Abp.Modularity;
using Wei.Abp.Notifications.RealTimeNotifier;

namespace Wei.Abp.Notifications
{
    [DependsOn(
        typeof(NotificationsModule),
        typeof(Volo.Abp.Emailing.AbpEmailingModule),
        typeof(Volo.Abp.TextTemplating.AbpTextTemplatingModule),
        typeof(Volo.Abp.Users.AbpUsersAbstractionModule)
    )]
    public class NotificationsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NotificationOptions>(options =>
            {
                options.Notifiers.Add<EmailRealTimeNotifier>();
            });
        }
    }
}