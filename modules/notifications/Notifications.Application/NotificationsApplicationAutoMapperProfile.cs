using System;
using AutoMapper;
using Wei.Abp.Notifications.Dto;

namespace Wei.Abp.Notifications
{

    public class NotificationsApplicationAutoMapperProfile : Profile
    {
        public NotificationsApplicationAutoMapperProfile()
        {
            CreateMap<NotificationInfo, MessageNotificationData>()
                .ConvertUsing(c => Newtonsoft.Json.JsonConvert.DeserializeObject<MessageNotificationData>(c.Data));

            CreateMap<UserNotificationInfo, UserNotificationInfoOutput>(MemberList.None)
                .ForMember(c => c.Data, c => c.MapFrom(d => d.Notification));

            CreateMap<TenantNotificationInfo, TenantNotificationInfoOutput>(MemberList.None)
                .ForMember(c => c.Data, c => c.MapFrom(d => d.Notification));
        }
    }
}
