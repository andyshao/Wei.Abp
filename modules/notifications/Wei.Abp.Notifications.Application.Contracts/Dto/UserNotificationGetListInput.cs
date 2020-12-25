using System;
using Volo.Abp.Application.Dtos;
namespace Wei.Abp.Notifications.Dto
{
    public class UserNotificationGetListInput : PagedResultRequestDto
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public UserNotificationState? State { get; set; }

    }
}
