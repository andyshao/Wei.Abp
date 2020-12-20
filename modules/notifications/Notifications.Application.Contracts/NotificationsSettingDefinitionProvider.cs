using Volo.Abp.Settings;

namespace Wei.Abp.Notifications
{
    /* These setting definitions will be visible to clients that has a OrderManagement.Application.Contracts
     * reference. Settings those should be hidden from clients should be defined in the OrderManagement.Application
     * package.
     */
    public class NotificationsSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //context.Add (
            //    new SettingDefinition (
            //        NotificationsSettings.UserEnablePush,
            //        true.ToString(),
            //        isVisibleToClients : true
            //    )
            //);


        }
    }
}