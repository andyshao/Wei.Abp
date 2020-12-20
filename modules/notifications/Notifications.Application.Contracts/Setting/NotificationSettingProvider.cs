using System;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Wei.Abp.Notifications
{
    public class NotificationSettingProvider : SettingDefinitionProvider
    {

        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(NotificationSettingNames.ReceiveNotifications, "true",
                  L("ReceiveNotifications"), isVisibleToClients: true)
            );
        }
        //public NotificationSettingProvider(ISettingDefinitionManager settingDefinitionManager,
        //    ISettingEncryptionService settingEncryptionService,
        //    ISettingValueProviderManager settingValueProviderManager)
        //    : base(settingDefinitionManager, settingEncryptionService, settingValueProviderManager)
        //{

        //}

        //public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        //{
        //    return new[]
        //    {
        //        new SettingDefinition(
        //            NotificationSettingNames.ReceiveNotifications,
        //            "true",
        //            L("ReceiveNotifications"),
        //            scopes: SettingScopes.User,
        //            clientVisibilityProvider: new VisibleSettingClientVisibilityProvider())
        //    };
        //}

        private static ILocalizableString L(string name)
        {
            //return new LocalizedString(Value, Value);
            return new FixedLocalizableString(name);
        }
    }
}
