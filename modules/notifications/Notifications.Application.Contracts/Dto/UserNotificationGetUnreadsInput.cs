using System;
using System.Collections.Generic;
using System.Text;

namespace Wei.Abp.Notifications.Dto
{
    public class UserNotificationGetUnreadsInput
    {
        public int MaxResultCount { get; set; } = 10;
    }
}
