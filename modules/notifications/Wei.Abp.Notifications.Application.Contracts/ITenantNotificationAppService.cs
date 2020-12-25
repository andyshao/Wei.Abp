using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Wei.Abp.Notifications.Dto;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// 租户消息
    /// </summary>
    public interface ITenantNotificationAppService : IApplicationService
    {
        /// <summary>
        /// 获取当前租户的消息
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<TenantNotificationInfoOutput>> GetListAsync(UserNotificationGetListInput input);

        /// <summary>
        /// 获取当前租户的消息量
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync(UserNotificationGetListInput input);

        /// <summary>
        /// 获取消息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TenantNotificationInfoOutput> GetAsync(Guid id);

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
    }
}
