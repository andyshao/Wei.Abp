using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Wei.Abp.Notifications.Dto;

namespace Wei.Abp.Notifications
{
    public interface IUserNotificationAppService : IApplicationService
    {

        /// <summary>
        /// 获取当前用户的消息
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<UserNotificationInfoOutput>> GetListAsync(UserNotificationGetListInput input);

        /// <summary>
        /// 获取当前用户的消息量
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync(UserNotificationGetListInput input);

        /// <summary>
        /// 获取消息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserNotificationInfoOutput> GetAsync(Guid id);

        /// <summary>
        /// 更新消息状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        Task UpdateStateAsync(Guid id, UserNotificationState state);

        /// <summary>
        /// 删除自己的消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);


        //Task DeleteAllUserNotificationsAsync(Guid user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

    }
}
