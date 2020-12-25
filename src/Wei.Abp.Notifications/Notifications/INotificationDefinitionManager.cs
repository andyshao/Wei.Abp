using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.Abp.Notifications
{
    public interface INotificationDefinitionManager
    {
        

        /// <summary>
        /// Gets a notification definition by name.
        /// Throws exception if there is no notification definition with given name.
        /// </summary>
        NotificationDefinition Get(string name);

        /// <summary>
        /// Gets a notification definition by name.
        /// Returns null if there is no notification definition with given name.
        /// </summary>
        NotificationDefinition GetOrNull(string name);

        /// <summary>
        /// Gets all notification definitions.
        /// </summary>
        IReadOnlyList<NotificationDefinition> GetAll();

        /// <summary>
        /// Checks if given notification (<paramref name="name"/>) is available for given user.
        /// </summary>
        Task<bool> IsAvailableAsync(string name, Guid user);

        /// <summary>
        /// Gets all available notification definitions for given user.
        /// </summary>
        /// <param name="user">User.</param>
        Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(Guid user);
        ///// <summary>
        ///// Adds the specified notification definition.
        ///// </summary>
        //void Add(NotificationDefinition notificationDefinition);
        ///// <summary>
        ///// Remove notification with given name
        ///// </summary>
        ///// <param name="name"></param>
        //void Remove(string name);
    }
}
