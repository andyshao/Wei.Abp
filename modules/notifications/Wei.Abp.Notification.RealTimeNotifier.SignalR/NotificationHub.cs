using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Users;

namespace Wei.Abp.Notification.RealTimeNotifier.SignalR
{
    public class NotificationHub : AbpHub
    {
        protected IExternalUserLookupServiceProvider UserLookupServiceProvider;
        protected ILookupNormalizer LookupNormalizer;

        public NotificationHub(IExternalUserLookupServiceProvider userLookupServiceProvider, ILookupNormalizer lookupNormalizer)
        {
            UserLookupServiceProvider = userLookupServiceProvider;
            LookupNormalizer = lookupNormalizer;
        }
    }
}
