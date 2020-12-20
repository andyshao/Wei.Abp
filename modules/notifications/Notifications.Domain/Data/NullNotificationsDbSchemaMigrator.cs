using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Wei.Abp.Notifications.Data
{
    /* This is used if database provider does't define
     * IAgentManagementDbSchemaMigrator implementation.
     */
    public class NullNotificationsDbSchemaMigrator : INotificationsDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}