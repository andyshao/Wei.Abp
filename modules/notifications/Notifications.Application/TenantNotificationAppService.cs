using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Wei.Abp.Notifications.Dto;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// 租户消息
    /// </summary>
    public class TenantNotificationAppService : ApplicationService, ITenantNotificationAppService
    {
        private readonly INotificationStore _store;
        private IRepository<TenantNotificationInfo, Guid> _notificationInfoRepository;

        public TenantNotificationAppService(IRepository<TenantNotificationInfo, Guid> uerNotificationInfoRepository, INotificationStore store)
        {
            _store = store;
            _notificationInfoRepository = uerNotificationInfoRepository;
        }
        /// <summary>
        /// 删除消息        
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _store.DeleteUserNotificationAsync(id);
        }

        /// <summary>
        /// 获取消息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TenantNotificationInfoOutput> GetAsync(Guid id)
        {
            var entity = await _notificationInfoRepository.GetAsync(id);
            return ObjectMapper.Map<TenantNotificationInfo, TenantNotificationInfoOutput>(entity);
        }
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(UserNotificationGetListInput input)
        {
            return await _notificationInfoRepository.WhereIf(input.State.HasValue, c => input.State == c.State).CountAsync();
        }
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<TenantNotificationInfoOutput>> GetListAsync(UserNotificationGetListInput input)
        {
            var query = _notificationInfoRepository
                .Include(c => c.Notification)
                .WhereIf(input.StartTime.HasValue, c => c.CreationTime >= input.StartTime.Value)
                .WhereIf(input.EndDate.HasValue, c => c.CreationTime <= input.EndDate.Value)
                .WhereIf(input.State.HasValue, c => input.State == c.State)
                .OrderByDescending(c => c.CreationTime)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            var list = await query.Select(entity => ObjectMapper.
            Map<TenantNotificationInfo, TenantNotificationInfoOutput>(entity)).ToListAsync();

            return new ListResultDto<TenantNotificationInfoOutput>
            {
                Items = list
            };
        }

        /// <summary>
        /// 获取未读消息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<TenantNotificationInfoOutput>> GetUnreadsAsync(UserNotificationGetUnreadsInput input)
        {
            var query = _notificationInfoRepository
                .Include(c => c.Notification)
                .Where(c => c.State == UserNotificationState.Unread)
                .OrderByDescending(c => c.CreationTime)
                .Take(input.MaxResultCount);
            var list = await query.Select(entity => ObjectMapper.
            Map<TenantNotificationInfo, TenantNotificationInfoOutput>(entity)).ToListAsync();

            return new ListResultDto<TenantNotificationInfoOutput>
            {
                Items = list
            };
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task UpdateStateAsync(Guid id, UserNotificationState state)
        {
            var entity = await _notificationInfoRepository.GetAsync(id);
            entity.State = state;
            await _notificationInfoRepository.UpdateAsync(entity);

        }
    }
}
