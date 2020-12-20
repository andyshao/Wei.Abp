using System;
using Volo.Abp.BackgroundJobs;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// This background job distributes notifications to users.
    /// </summary>
    public class NotificationDistributionJob : BackgroundJob<NotificationDistributionJobArgs>, ITransientDependency
    {
        protected NotificationOptions NotificationOption;
        protected IServiceProvider ServiceProvider;
        protected INotificationDistributer[] Distributers;

        public NotificationDistributionJob(INotificationDistributer[] distributers,
            IOptions<NotificationOptions> notificationOption, IServiceProvider serviceProvider)
        {
            NotificationOption = notificationOption.Value;
            ServiceProvider = serviceProvider;
            Distributers = distributers;
        }


        public override void Execute(NotificationDistributionJobArgs args)
        {
            foreach (var notificationDistributor in Distributers)
            {
                //var notificationDistributer = ServiceProvider.GetRequiredService(notificationDistributorType) as INotificationDistributer;
                notificationDistributor.DistributeAsync(args.NotificationId);
            }
        }
    }
}
