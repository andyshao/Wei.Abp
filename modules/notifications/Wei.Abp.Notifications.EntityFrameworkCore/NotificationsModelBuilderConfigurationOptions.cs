using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Wei.Abp.Notifications.EntityFrameworkCore
{
    public class NotificationsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public NotificationsModelBuilderConfigurationOptions(
           [NotNull] string tablePrefix,
           [CanBeNull] string schema)
           : base(
               tablePrefix,
               schema)
        {

        }
    }
}