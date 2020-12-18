# Wei.Abp.BlobStoring.TencentCloudCos

abp vnext blob 腾讯云的支持

使用方法 在 `public override void ConfigureServices(ServiceConfigurationContext context)` 中

```
var configuration = context.Services.GetConfiguration();
            var appid = configuration["TencentCloud:AppId"];
            var secretKey = configuration["TencentCloud:SecretKey"];
            var secretId = configuration["TencentCloud:SecretId"];
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.ConfigureDefault(action =>
                {
                    action.UseTencentCloudCos(cos =>
                    {
                        cos.AppId = appid;
                        cos.SecretKey = secretKey;
                        cos.SecretId = secretId;
                        //区域建议手动配置
                        cos.Region = EnumUtils.GetValue(CosRegion.AP_Guangzhou);
                    });
                });
            });
```

# Wei.Abp.Sms.TencentCloud

### 在应用程序中设置

如上一节所述，ConfigurationSettingValueProvider 从 IConfiguration 服务中读取设置 appsettings.json，默认情况下，该服务可以从中读取值。因此，最简单的方法是配置设置值以在 appsettings.json 文件中定义它们。

例如，您可以配置 ISmsSender 设置，如下所示：

```json
{
  "Settings": {
    "Tencent.Sms.AppId": "应用ID",
    "Tencent.Sms.SecretId": "密钥ID",
    "Tencent.Sms.SecretKey": "密钥Key",
    "Tencent.Sms.Region": "区域",
    "Tencent.Sms.Sign": "签名"
  }
}
```

发送方法

```
await _sms.SendAsync(new TencentCloudSmsMessage("+8618588688087", "广州安华磨具") { Sign = "广州安华磨具", TemplateID = "656176", TemplateParamSet = "德邦物流;20234234" });
```
