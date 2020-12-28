using System;
using Volo.Abp.Modularity;

namespace Wei.Abp.Notifications
{
    [DependsOn(
        typeof(NotificationsModule),
        typeof(Volo.Abp.TextTemplating.AbpTextTemplatingModule),
        typeof(Volo.Abp.Users.AbpUsersAbstractionModule)
    )]
    public class SmsRealTimeNotifierModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NotificationOptions>(options =>
            {
                options.Notifiers.Add<SmsRealTimeNotifier>();
            });
        }
    }
}