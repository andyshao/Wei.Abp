using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Wei.Abp.Notifications
{
    [DependsOn(
        typeof(NotificationsContractsApplicationModule),
        typeof(AbpHttpClientModule)
    )]
    public class NotificationsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Notifications";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(NotificationsContractsApplicationModule).Assembly,
                RemoteServiceName
            );
        }
    }
}