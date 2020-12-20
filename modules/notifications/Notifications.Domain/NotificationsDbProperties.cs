using System;
using Volo.Abp.Data;

namespace Wei.Abp.Notifications
{
    public static class NotificationsDbProperties
    {
        public static string DbTablePrefix { get; set; } = AbpCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = AbpCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "Notifications";
    }
}
