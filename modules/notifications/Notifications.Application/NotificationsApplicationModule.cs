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
            /* Using `true` for the `validate` parameter to
             * validate the profile on application startup.
             * See http://docs.automapper.org/en/stable/Configuration-validation.html for more info
             * about the configuration validation. */
                options.AddProfile<NotificationsApplicationAutoMapperProfile>(validate: true);
                options.Configurators.Add(c =>
                {
                //统一显示年月日时分
                //c.MapperConfiguration.CreateMap<DateTime, string>().ConvertUsing(c =>c.ToString("yyyy年MM月dd HH:mm"));
                c.MapperConfiguration.CreateMap<DateTime, string>().ConvertUsing(d => d.ToString("YYYY-MM-DD HH:mm"));

                    c.MapperConfiguration.CreateMap<DateTime?, string>().ConvertUsing(d => d.HasValue ? d.Value.ToString("YYYY-MM-DD HH:mm") : "");
                });
            });
            //NotificationsApplicationAutoMapperProfile
        }
    }
}