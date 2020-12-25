using System;
using AutoMapper;
using Notifications.Dto;
using Wei.Abp.Notifications.Dto;

namespace Wei.Abp.Notifications
{

    public class NotificationsApplicationAutoMapperProfile : Profile
    {
        public NotificationsApplicationAutoMapperProfile()
        {
            //CreateMap<Notification, MessageNotificationData>()
            //    .ConvertUsing(c => Newtonsoft.Json.JsonConvert.DeserializeObject<MessageNotificationData>(c.Data));

            CreateMap<UserNotification, UserNotificationOutput>(MemberList.None)
                .ForMember(c => c.NotificationType, c => c.MapFrom(d => d.Notification.NotificationType))
            .ForMember(c => c.NotificationName, c => c.MapFrom(d => d.Notification.NotificationName))
            .ForMember(c => c.Severity, c => c.MapFrom(d => d.Notification.Severity))
            .ForMember(c => c.Message, c => c.MapFrom(d => d.Notification.Message));

            CreateMap<PublishNotificationInput, Notification>(MemberList.Source);

        }
    }
}
