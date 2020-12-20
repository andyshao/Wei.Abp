using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Wei.Abp.Notifications.Data
{
    public class NotificationsDbMigrationService : ITransientDependency
    {
        public ILogger<NotificationsDbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly INotificationsDbSchemaMigrator _dbSchemaMigrator;

        public NotificationsDbMigrationService(
            IDataSeeder dataSeeder,
            INotificationsDbSchemaMigrator dbSchemaMigrator)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrator = dbSchemaMigrator;

            Logger = NullLogger<NotificationsDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");

            Logger.LogInformation("Migrating database schema...");
            await _dbSchemaMigrator.MigrateAsync();

            Logger.LogInformation("Executing database seed...");
            await _dataSeeder.SeedAsync();

            Logger.LogInformation("Successfully completed database migrations.");
        }
    }
}