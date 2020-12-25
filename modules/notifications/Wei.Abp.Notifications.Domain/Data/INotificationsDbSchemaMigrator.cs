using System.Threading.Tasks;

namespace Wei.Abp.Notifications.Data
{
    public interface INotificationsDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
