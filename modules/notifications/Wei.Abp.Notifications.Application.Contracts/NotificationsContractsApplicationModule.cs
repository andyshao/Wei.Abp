
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Wei.Abp.Notifications
{
    [DependsOn(
        typeof(AbpDddApplicationModule)
        )]
    public class NotificationsContractsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        }
    }
}
