using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Wei.Abp.Notifications.EntityFrameworkCore
{
    [ConnectionStringName(NotificationsDbProperties.ConnectionStringName)]
    public class NotificationsDbContext : AbpDbContext<NotificationsDbContext>, INotificationsDbContext
    {
        public static string TablePrefix { get; set; } = NotificationsDbProperties.DbTablePrefix;

        public static string Schema { get; set; } = NotificationsDbProperties.DbSchema;

        public DbSet<Notification> NotificationInfos { get; set; }

        public DbSet<NotificationSubscription> NotificationSubscriptionInfos { get; set; }


        public DbSet<UserNotification> UserNotificationInfos { get; set; }



        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureNotifycations(options =>
            {
                options.TablePrefix = TablePrefix;
                options.Schema = Schema;
            });
        }
    }
}