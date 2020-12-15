using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Wei.Abp.Sms.Settings
{
    public class TencentCloudSmsSettingProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(new SettingDefinition(TencentCloudSmsSettingNames.AppId, "",L("应用ID"),L("您短信服务的应用ID"), isVisibleToClients: false));
            context.Add(new SettingDefinition(TencentCloudSmsSettingNames.SecretId, "", L("密钥ID"), L("在腾讯云中新建密钥获取"), isVisibleToClients: false));
            context.Add(new SettingDefinition(TencentCloudSmsSettingNames.SecretKey, "", L("密钥Key"), L("在腾讯云中新建密钥获取"), isVisibleToClients: false));
            context.Add(new SettingDefinition(TencentCloudSmsSettingNames.Region, "", L("区域"), L("服务区域"), isVisibleToClients: false));
            context.Add(new SettingDefinition(TencentCloudSmsSettingNames.Sign, "", L("签名"), L("短信的签名"), isVisibleToClients: false));
        }

        private static ILocalizableString L(string name)
        {
            return new FixedLocalizableString(name);
        }
    }
}
