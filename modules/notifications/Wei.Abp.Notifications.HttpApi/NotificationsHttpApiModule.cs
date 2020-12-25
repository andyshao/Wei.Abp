using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Modularity;

namespace Wei.Abp.Notifications
{

    [DependsOn(
        typeof(NotificationsApplicationModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAspNetCoreSignalRModule)
        )]

    public class NotificationsHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(NotificationsApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = "notifications";
                });
            });

        }
    }
}