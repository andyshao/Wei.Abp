using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Used as a context while defining notifications.
    /// </summary>
    public interface INotificationDefinitionContext
    {
        /// <summary>
        /// Gets the notification definition manager.
        /// </summary>
        INotificationDefinitionManager Manager { get; }
    }
}
