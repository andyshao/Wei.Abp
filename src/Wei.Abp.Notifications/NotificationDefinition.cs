using System;
using Volo.Abp.Localization;
using Volo.Abp.Domain;
using Volo.Abp.Data;
using System.Collections.Generic;
using Volo.Abp.Features;

namespace Wei.Abp.Notifications
{

    /// <summary>
    /// Definition for a notification.
    /// </summary>
    public class NotificationDefinition : IHasExtraProperties
    {
        /// <summary>
        /// Unique name of the notification.
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// Display name of the notification.
        /// Optional.
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// Description for the notification.
        /// Optional.
        /// </summary>
        public ILocalizableString Description { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }

        ///// <summary>
        ///// A permission dependency. This notification will be available to a user if this dependency is satisfied.
        ///// Optional.
        ///// </summary>
        //public IPermissionDependency PermissionDependency { get; set; }

        /// <summary>
        /// A feature dependency. This notification will be available to a tenant if this feature is enabled.
        /// Optional.
        /// </summary>
        public IFeatureStore FeatureDependency { get; set; }

    }
}
