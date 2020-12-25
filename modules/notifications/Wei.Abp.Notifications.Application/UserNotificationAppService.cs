using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;
using Wei.Abp.Notifications.Dto;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// 用户消息
    /// </summary>
    public class UserNotificationAppService : ApplicationService, IUserNotificationAppService
    {
        private readonly INotificationStore _store;
        private IRepository<UserNotification, Guid> _userNotificationInfoRepository;

        public UserNotificationAppService(IRepository<UserNotification, Guid> uerNotificationInfoRepository, INotificationStore store)
        {
            _store = store;
            _userNotificationInfoRepository = uerNotificationInfoRepository;
        }
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteAsync(Guid id)
        {
            return _store.DeleteUserNotificationAsync(id);
        }

        /// <summary>
        /// 获取消息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserNotificationOutput> GetAsync(Guid id)
        {
            var entity = await _userNotificationInfoRepository.GetAsync(id);
            return ObjectMapper.Map<UserNotification, UserNotificationOutput>(entity);
        }
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(UserNotificationGetListInput input)
        {
            return await _userNotificationInfoRepository.WhereIf(input.State.HasValue,c=>c.State==input.State.Value).WhereIf(input.StartTime.HasValue,c=>c.CreationTime>=input.StartTime.Value).WhereIf(input.EndTime.HasValue,c=>c.CreationTime<=input.EndTime.Value).Where(c => c.UserId == CurrentUser.Id.Value).CountAsync();
        }
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<UserNotificationOutput>> GetListAsync(UserNotificationGetListInput input)
        {
            var query = _userNotificationInfoRepository
                .WhereIf(input.StartTime.HasValue, c => c.CreationTime >= input.StartTime.Value)
                .WhereIf(input.EndTime.HasValue, c => c.CreationTime <= input.EndTime.Value)
                .Where(c => input.State == c.State)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            var list = await query
                .Select(entity => ObjectMapper.Map<UserNotification, UserNotificationOutput>(entity)).ToListAsync();

            return new ListResultDto<UserNotificationOutput>
            {
                Items = list
            };
        }
        /// <summary>
        /// 获取未读消息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<UserNotificationOutput>> GetUnreadsAsync(UserNotificationGetUnreadsInput input)
        {
            var query = _userNotificationInfoRepository
                .Include(c => c.Notification)
                .Where(c => c.State == UserNotificationState.Unread)
                .OrderByDescending(c => c.CreationTime);

            var list = await AsyncExecuter.ToListAsync(query.PageBy(input).Select(entity => ObjectMapper.
            Map<UserNotification, UserNotificationOutput>(entity)));
            var count = await AsyncExecuter.CountAsync(query);

            return new PagedResultDto<UserNotificationOutput>
            {
                TotalCount = count,
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
            var entity = await _userNotificationInfoRepository.GetAsync(id);
            entity.State = state;
            await _userNotificationInfoRepository.UpdateAsync(entity);

        }
    }
}