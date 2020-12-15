# Wei.Abp.Sms.TencentCloud
abp vnext 腾讯云短信发送支持

### 在应用程序中设置
如上一节所述，ConfigurationSettingValueProvider从IConfiguration服务中读取设置appsettings.json，默认情况下，该服务可以从中读取值。因此，最简单的方法是配置设置值以在appsettings.json文件中定义它们。

例如，您可以配置 ISmsSender设置，如下所示：

````json
{
  "Settings": {
    "Tencent.Sms.AppId": "应用ID",
    "Tencent.Sms.SecretId": "密钥ID",
    "Tencent.Sms.SecretKey": "密钥Key",
    "Tencent.Sms.Region": "区域",
    "Tencent.Sms.Sign": "签名",
  }
}

发送方法
```
 await _sms.SendAsync(new TencentCloudSmsMessage("+8618588688087", "广州安华磨具") { Sign = "广州安华磨具", TemplateID = "656176", TemplateParamSet = "德邦物流;20234234" });
 ```