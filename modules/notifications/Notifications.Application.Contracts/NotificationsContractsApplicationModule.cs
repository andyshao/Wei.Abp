
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
            //Configure<AbpAutoMapperOptions>(options =>
            //{
            //    /* Using `true` for the `validate` parameter to
            //     * validate the profile on application startup.
            //     * See http://docs.automapper.org/en/stable/Configuration-validation.html for more info
            //     * about the configuration validation. */
            //    options.AddProfile<LeaseManagementApplicationAutoMapperProfile>(validate: true);
            //});
        }
    }
}
