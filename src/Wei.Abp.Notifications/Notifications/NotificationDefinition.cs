using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Localization;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Definition for a notification.
    /// </summary>
    public class NotificationDefinition:Volo.Abp.Data.IHasExtraProperties
    {
        /// <summary>
        /// Unique name of the notification.
        /// </summary>
        [NotNull]
        public string Name { get; private set; }
        /// <summary>
        /// Display name of the notification.
        /// Optional.
        /// </summary>
        [NotNull]
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// Description for the notification.
        /// Optional.
        /// </summary>
        public ILocalizableString Description { get; set; }

        /// <summary>
        /// 权限依赖关系。如果满足此关系，则用户可以使用此通知。
        /// 可选的
        /// </summary>
        public string Permission { get; set; }
        /// <summary>
        /// 功能依赖。如果租户启用此功能，则租户可以使用此通知。
        /// 可选的.
        /// </summary>
        public List<string> FeatureNames { get; set; }
        /// <summary>
        /// 功能全部满足或者满足一个
        /// </summary>
        public bool FeaturesRequiresAll { get; set; } = false;


        public ExtraPropertyDictionary ExtraProperties { get; set; }
        ///// <summary>
        ///// Default value of the setting.
        ///// </summary>
        //[CanBeNull]
        //public string DefaultValue { get; set; }

        /// <summary>
        /// Sets a property in the <see cref="Properties"/> dictionary.
        /// This is a shortcut for nested calls on this object.
        /// </summary>
        public virtual NotificationDefinition WithProperty(string key, object value)
        {
            ExtraProperties[key] = value;
            return this;
        }

        ///// <summary>
        ///// Sets a property in the <see cref="Properties"/> dictionary.
        ///// This is a shortcut for nested calls on this object.
        ///// </summary>
        //public virtual NotificationDefinition WithProviders(params string[] providers)
        //{
        //    if (!providers.IsNullOrEmpty())
        //    {
        //        Providers.AddRange(providers);
        //    }

        //    return this;
        //}
    }
}
