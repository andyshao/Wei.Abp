using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Wei.Abp.Notifications.EntityFrameworkCore
{
    [ConnectionStringName(NotificationsDbProperties.ConnectionStringName)]
    public interface INotificationsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<NotificationInfo> NotificationInfos { get; }

        DbSet<NotificationSubscriptionInfo> NotificationSubscriptionInfos { get; }

        DbSet<TenantNotificationInfo> TenantNotificationInfos { get; }

        DbSet<UserNotificationInfo> UserNotificationInfos { get; }


    }
}