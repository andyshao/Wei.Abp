using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;

namespace Wei.Abp.AspNetCore.AspNetCore
{
    [DependsOn(typeof(AbpAspNetCoreModule))]
    public class WeiAbpAspNetCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //配置
            //修复.net core使用nginx做反向代理获取客户端ip的问题
            Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

        }
    }
}
