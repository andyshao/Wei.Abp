using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationPublisher"/>.
    /// </summary>
    public class NotificationPublisher : ApplicationService, INotificationPublisher
    {
        public const int MaxUserCountToDirectlyDistributeANotification = 5;


        private readonly INotificationStore _store;
        private readonly IBackgroundJobManager _backgroundJobManager;

        protected NotificationOptions Options;
        // protected INotificationDistributer[] Distributers;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPublisher"/> class.
        /// </summary>
        public NotificationPublisher(
            IOptions<NotificationOptions> options,
            INotificationStore store,
            IBackgroundJobManager backgroundJobManager
        )
        {
            _store = store;
            _backgroundJobManager = backgroundJobManager;
            Options = options.Value;
        }
        
        public async Task PublishAsync(string notificationName, 
            Guid userId,
            NotificationData data = null,
            NotificationSeverity severity = NotificationSeverity.Info)
        {
            if (notificationName.IsNullOrEmpty())
            {
                throw new ArgumentException("NotificationName can not be null or whitespace!", "notificationName");
            }

            var notificationInfo = new NotificationInfo()
            {
                NotificationName = notificationName,
                Severity = severity,
                UserIds = new Guid[] { userId },
                TenantId = CurrentTenant.Id,
                Data = data == null ? null : System.Text.Json.JsonSerializer.Serialize(data),
            };

            await _store.InsertNotificationAsync(notificationInfo);


            var userNotificationInfo = new UserNotificationInfo();
            userNotificationInfo.NotificationName = notificationName;
            userNotificationInfo.UserId = userId;
            userNotificationInfo.NotificationId = notificationInfo.Id;
            userNotificationInfo.TenantId = CurrentTenant.Id;
            await _store.InsertUserNotificationAsync(userNotificationInfo);

            await CurrentUnitOfWork.SaveChangesAsync(); //To get Id of the notification

            var userNotification = userNotificationInfo.ToUserNotification();
            
            foreach (var distributerType in Options.Distributers)
            {
                var notificationDistributer = ServiceProvider.GetRequiredService(distributerType) as INotificationDistributer;
                await notificationDistributer.DistributeAsync(userNotificationInfo.Id);
                // await notificationDistributer.DistributeAsync(userNotification);
            }

            foreach (var realTimeType in Options.Notifiers)
            {
                var realTime = ServiceProvider.GetRequiredService(realTimeType) as IRealTimeNotifier;
                await realTime.SendNotificationsAsync(userNotification);
            }
        }
    }
}