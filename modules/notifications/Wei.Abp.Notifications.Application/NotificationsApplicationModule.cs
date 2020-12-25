using System;
using AutoMapper;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Wei.Abp.Notifications
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(NotificationsDomainModule),
        typeof(NotificationsContractsApplicationModule)
    )]
    public class NotificationsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<NotificationsApplicationAutoMapperProfile>(validate: true);
            });

            Configure<NotificationOptions>(options =>
            {
                options.Distributers.Add<DefaultNotificationDistributer>();
            });
        }
    }
}