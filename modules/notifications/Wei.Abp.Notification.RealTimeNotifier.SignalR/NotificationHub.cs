using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Users;

namespace Wei.Abp.Notification.RealTimeNotifier.SignalR
{
    [HubRoute("/notification-hub")]
    public class NotificationHub : AbpHub
    {
        protected IExternalUserLookupServiceProvider UserLookupServiceProvider;

        public NotificationHub(IExternalUserLookupServiceProvider userLookupServiceProvider)
        {
            UserLookupServiceProvider = userLookupServiceProvider;
        }

    }
}
