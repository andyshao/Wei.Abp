using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Settings;
using Volo.Abp.Validation.StringValues;

namespace Wei.Abp.Notifications
{
    public class NotificationsFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        //see: https://docs.abp.io/en/abp/latest/Features
        public override void Define(IFeatureDefinitionContext context)
        {
            var group = context.AddGroup(NotificationsFeatures.Group, L("通知"));

            group.AddFeature(NotificationsFeatures.EnableForSMS,
                defaultValue: "true",
                displayName: L("消息通知"),
                description: L("启用后将接受并存储所有的消息通知"),
                valueType: new ToggleStringValueType());

            group.AddFeature(NotificationsFeatures.EnableForSMS,
                defaultValue: "true",
                displayName: L("短信通知"),
                description: L("启用后将发送短信至用户预留的手机号"),
                valueType: new ToggleStringValueType());

            group.AddFeature(NotificationsFeatures.EnableForWeChat,
                defaultValue: "true",
                displayName: L("微信通知"),
                description: L("启用后将发送微信消息至已绑定的微信"),
                valueType: new ToggleStringValueType());
        }

        private static ILocalizableString L(string name)
        {
            return new FixedLocalizableString(name);
        }
    }
}
