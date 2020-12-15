using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Wei.Abp.Sms
{
    [DependsOn(typeof(Volo.Abp.Sms.AbpSmsModule),typeof(Volo.Abp.Settings.AbpSettingsModule))]
    public class TencentCloudSmsModule : AbpModule
    {

    }
}
