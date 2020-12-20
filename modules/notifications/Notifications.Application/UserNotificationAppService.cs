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
        private IRepository<UserNotificationInfo, Guid> _userNotificationInfoRepository;

        public UserNotificationAppService(IRepository<UserNotificationInfo, Guid> uerNotificationInfoRepository, INotificationStore store)
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
        public async Task<UserNotificationInfoOutput> GetAsync(Guid id)
        {
            var entity = await _userNotificationInfoRepository.GetAsync(id);
            return ObjectMapper.Map<UserNotificationInfo, UserNotificationInfoOutput>(entity);
        }
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(UserNotificationGetListInput input)
        {
            return await _userNotificationInfoRepository.Where(c => c.UserId == CurrentUser.Id.Value).CountAsync();
        }
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<UserNotificationInfoOutput>> GetListAsync(UserNotificationGetListInput input)
        {
            var query = _userNotificationInfoRepository
                .WhereIf(input.StartTime.HasValue, c => c.CreationTime >= input.StartTime.Value)
                .WhereIf(input.EndDate.HasValue, c => c.CreationTime <= input.EndDate.Value)
                .Where(c => input.State == c.State)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            var list = await query
                .Select(entity => ObjectMapper.Map<UserNotificationInfo, UserNotificationInfoOutput>(entity)).ToListAsync();

            return new ListResultDto<UserNotificationInfoOutput>
            {
                Items = list
            };
        }
        /// <summary>
        /// 获取未读消息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<UserNotificationInfoOutput>> GetUnreadsAsync(UserNotificationGetUnreadsInput input)
        {
            var query = _userNotificationInfoRepository
                .Include(c => c.Notification)
                .Where(c => c.State == UserNotificationState.Unread)
                .OrderByDescending(c => c.CreationTime)
                .Take(input.MaxResultCount);
            var list = await query.Select(entity => ObjectMapper.
            Map<UserNotificationInfo, UserNotificationInfoOutput>(entity)).ToListAsync();

            return new ListResultDto<UserNotificationInfoOutput>
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
            var entity = await _userNotificationInfoRepository.GetAsync(id);
            entity.State = state;
            await _userNotificationInfoRepository.UpdateAsync(entity);

        }
    }
}