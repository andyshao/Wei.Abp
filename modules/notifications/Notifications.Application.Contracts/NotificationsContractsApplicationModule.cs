
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Wei.Abp.Notifications
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(NotificationsDomainModule)
        )]
    public class NotificationsContractsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        }
    }
}
