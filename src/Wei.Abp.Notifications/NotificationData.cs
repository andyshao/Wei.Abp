using System;
using System.Collections.Generic;
using Volo.Abp.Data;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used to store data for a notification.
    /// It can be directly used or can be derived.
    /// </summary>
    [Serializable]
    public class NotificationData : IHasExtraProperties
    {
        /// <summary>
        /// Gets notification data type name.
        /// It returns the full class name by default.
        /// </summary>
        public virtual string Type => GetType().FullName;

        public ExtraPropertyDictionary ExtraProperties { get; set; }


    }
}
