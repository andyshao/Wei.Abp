using System;
namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Represents state of a <see cref="UserNotification"/>.
    /// <see cref="https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/src/Abp/Notifications/UserNotificationState.cs"/>
    /// </summary>
    public enum UserNotificationState
    {
        /// <summary>
        /// Notification is not read by user yet.
        /// </summary>
        Unread = 0,

        /// <summary>
        /// Notification is read by user.
        /// </summary>
        Read
    }
}
