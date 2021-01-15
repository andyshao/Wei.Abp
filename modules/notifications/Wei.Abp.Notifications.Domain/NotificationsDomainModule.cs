using System;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Wei.Abp.Notifications
{
    [DependsOn(
        typeof(AbpEntityFrameworkCoreModule),
        typeof(NotificationsModule),
        typeof(AbpMultiTenancyModule)
        )]
    public class NotificationsDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
