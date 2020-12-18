using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace Wei.Abp.Ocelot.Provider.Consul
{
    [DependsOn(typeof(AbpAspNetCoreModule))]
    public class AbpOcelotProviderConsulModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 读取 Consul 配置，注入服务
            Configure<ConsulOptions>(context.Services.GetConfiguration().GetSection("Consul"));
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var clientConfig = context.ServiceProvider.GetService<IOptions<ConsulOptions>>()?.Value;

            ConsulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(clientConfig.Server);
                config.Datacenter = clientConfig.DataCenter;
            });

            AgentServiceCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(7), // 服务启动 7 秒后注册服务
                Interval = TimeSpan.FromSeconds(9), // 健康检查的间隔时间为：9秒
                HTTP = $"{clientConfig.Host.Scheme}://{clientConfig.Host.Host}:{clientConfig.Host.Port}{HEALTH_CHECK_URI}"
            };

            AgentServiceRegistration = new AgentServiceRegistration()
            {
                Checks = new[] { AgentServiceCheck },
                Address = clientConfig.Host.Host,
                ID = $"{clientConfig.ClientName}-{clientConfig.Host.Host}-{clientConfig.Host.Port}",
                Name = clientConfig.ClientName,
                Port = clientConfig.Host.Port
            };


            ConsulClient.Agent.ServiceRegister(AgentServiceRegistration).GetAwaiter().GetResult();

            MapHealthCheck(context.GetApplicationBuilder());
        }

        /// <summary>
        ///  实现健康检查输出，无需另行定义
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        protected virtual IApplicationBuilder MapHealthCheck(IApplicationBuilder app)
        {
            app.Map(HEALTH_CHECK_URI, s =>
            {
                s.Run(async context =>
                {
                    await context.Response.WriteAsync("ok");
                });
            });
            return app;
        }

        protected virtual ConsulClient ConsulClient { get; set; }
        protected virtual AgentServiceCheck AgentServiceCheck { get; set; }

        private const string HEALTH_CHECK_URI = "/consul/health/check";

        /// <summary>
        /// 客户端服务注册
        /// </summary>
        protected virtual AgentServiceRegistration AgentServiceRegistration { get; set; }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            ConsulClient.Agent.ServiceRegister(AgentServiceRegistration);
            
        }
    }
}