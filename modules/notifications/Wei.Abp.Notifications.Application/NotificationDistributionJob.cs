using System;
using Volo.Abp.BackgroundJobs;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Options;
using Notifications;
using Volo.Abp.MultiTenancy;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// This background job distributes notifications to users.
    /// </summary>
    public class NotificationDistributionJob : BackgroundJob<NotificationDistributionJobArgs>, ITransientDependency
    {
        protected NotificationOptions Options;
        protected IServiceProvider ServiceProvider;
        protected ICurrentTenant CurrentTenant { get; set; }

        public NotificationDistributionJob(
            IOptions<NotificationOptions> notificationOption, IServiceProvider serviceProvider)
        {
            Options = notificationOption.Value;
            ServiceProvider = serviceProvider;
        }


        public override void Execute(NotificationDistributionJobArgs args)
        {
            foreach (var notificationDistributorType in Options.Distributers)
            {
                using (CurrentTenant.Change(args.TenantId))
                {
                    var notificationDistributer = ServiceProvider.GetRequiredService(notificationDistributorType) as INotificationDistributer;
                    notificationDistributer.DistributeAsync(args.NotificationId);
                }
            }
        }
    }
}
