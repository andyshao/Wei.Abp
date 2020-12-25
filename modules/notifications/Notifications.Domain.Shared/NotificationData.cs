using System;
using System.Collections.Generic;
using System.Text.Json;
using Volo.Abp.Data;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used to store data for a notification.
    /// It can be directly used or can be derived.
    /// <see cref="https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/Notifications/NotificationData.cs"/>
    /// </summary>
    [Serializable]
    public class NotificationData:IHasExtraProperties
    {
        /// <summary>
        /// Gets notification data type name.
        /// It returns the full class name by default.
        /// </summary>
        public virtual string Type => GetType().FullName;

        /// <summary>
        /// Shortcut to set/get <see cref="Properties"/>.
        /// </summary>
        public object this[string key]
        {
            get { return this.GetProperty<object>(key); }
            set { this.SetProperty(key,value); }
        }
        
        
        public  ExtraPropertyDictionary ExtraProperties { get; private set; }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize<NotificationData>(this);
        }
    }
}
