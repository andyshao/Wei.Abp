using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Wei.Abp.Notifications;

namespace PriceManagement {
    [DependsOn(
        typeof(NotificationsContractsApplicationModule),
        typeof(NotificationsHttpApiModule),
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