using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Emailing;
using Volo.Abp.Users;
using Volo.Abp.TextTemplating;
using Volo.Abp.Emailing.Templates;

namespace Wei.Abp.Notifications.RealTimeNotifier
{
    public class EmailRealTimeNotifier : IRealTimeNotifier, Volo.Abp.DependencyInjection.ITransientDependency
    {
        protected IExternalUserLookupServiceProvider UserLookupServiceProvider;
        protected IEmailSender EmailSender;
        protected ITemplateRenderer TemplateRenderer;

        public EmailRealTimeNotifier(IExternalUserLookupServiceProvider userLookupServiceProvider, IEmailSender emailSender, ITemplateRenderer templateRenderer)
        {
            UserLookupServiceProvider = userLookupServiceProvider;
            EmailSender = emailSender;
            TemplateRenderer = templateRenderer;
        }

        public virtual async Task SendNotificationsAsync(UserNotificationInfo userNotification)
        {
            var userData = await UserLookupServiceProvider.FindByIdAsync(userNotification.UserId);

            if (userData.EmailConfirmed && !userData.Email.IsNullOrWhiteSpace())
            {
                var body = await TemplateRenderer.RenderAsync(
                    StandardEmailTemplates.Message,
                    new
                    {
                        message = "This is email body..."
                    }
                );

                await EmailSender.SendAsync(
                    userData.Email,
                    userNotification.Message,
                    body
                );
                //await EmailSender.SendAsync(userData.Email,);
            }
        }
    }
}
