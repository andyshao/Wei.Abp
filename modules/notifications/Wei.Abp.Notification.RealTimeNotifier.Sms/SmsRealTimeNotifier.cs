using System;
using System.Threading.Tasks;
using Volo.Abp.Sms;
using Volo.Abp.Users;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class SmsRealTimeNotifier : IRealTimeNotifier, Volo.Abp.DependencyInjection.ITransientDependency
    {
        protected ISmsSender smsSender;
        protected IExternalUserLookupServiceProvider UserLookupServiceProvider;

        public SmsRealTimeNotifier(ISmsSender smsSender, IExternalUserLookupServiceProvider userLookupServiceProvider)
        {
            this.smsSender = smsSender;
            UserLookupServiceProvider = userLookupServiceProvider;
        }

        public async Task SendNotificationsAsync(UserNotificationInfo userNotification)
        {
            var userData = await UserLookupServiceProvider.FindByIdAsync(userNotification.UserId);

            if (userData.PhoneNumberConfirmed&&!userData.PhoneNumber.IsNullOrWhiteSpace())
            {
                await smsSender.SendAsync(userData.PhoneNumber, userNotification.Message);
            }
        }
    }
}