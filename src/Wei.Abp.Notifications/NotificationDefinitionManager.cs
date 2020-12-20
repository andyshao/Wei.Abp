using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Features;
using Volo.Abp.Users;

namespace Wei.Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationDefinitionManager"/>.
    /// </summary>
    internal class NotificationDefinitionManager : INotificationDefinitionManager, ISingletonDependency
    {
        public IFeatureChecker FeatureChecker { get; set; }

        private IServiceProvider ServiceProvider { get; set; }

        protected NotificationOptions Options { get; }

        protected IDictionary<string, NotificationDefinition> NotificationDefinitions;

        public NotificationDefinitionManager(IOptions<NotificationOptions> options)
        {
            Options = options.Value;
            NotificationDefinitions = new Dictionary<string, NotificationDefinition>();
        }

        protected virtual void CreateNotificationDefinitions()
        {
            var context = new NotificationDefinitionContext(this);

            //using (var scope = _serviceScopeFactory.CreateScope())
            //{
                var providers = Options
                    .Providers
                    .Select(p => ServiceProvider.GetRequiredService(p) as NotificationProvider)
                    .ToList();

                foreach (var provider in providers)
                {
                    provider.SetNotifications(context);
                }
            //}
        }

        public void Add(NotificationDefinition notificationDefinition)
        {
            if (NotificationDefinitions.ContainsKey(notificationDefinition.Name))
            {
                throw new UserFriendlyException("There is already a notification definition with given name: " + notificationDefinition.Name + ". Notification names must be unique!");
            }

            NotificationDefinitions[notificationDefinition.Name] = notificationDefinition;
        }

        public NotificationDefinition Get(string name)
        {
            var definition = GetOrNull(name);
            if (definition == null)
            {
                throw new UserFriendlyException("There is no notification definition with given name: " + name);
            }

            return definition;
        }

        public NotificationDefinition GetOrNull(string name)
        {
            return NotificationDefinitions.GetOrDefault(name);
        }

        public void Remove(string name)
        {
            NotificationDefinitions.Remove(name);
        }

        public IReadOnlyList<NotificationDefinition> GetAll()
        {
            return NotificationDefinitions.Values.ToImmutableList();
        }

        public async Task<bool> IsAvailableAsync(string name, Guid user)
        {
            var notificationDefinition = GetOrNull(name);
            if (notificationDefinition == null)
            {
                return true;
            }

            //这里检查用户的Feature
            //if (notificationDefinition.FeatureDependency != null)
            //{
            //    //eatureChecker.IsEnabledAsync(notificationDefinition.)
            //    //using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
            //    //{
            //    //    featureDependencyContext.Object.TenantId = user.TenantId;

            //    //    if (!await notificationDefinition.FeatureDependency.IsSatisfiedAsync(featureDependencyContext.Object))
            //    //    {
            //    //        return false;
            //    //    }
            //    //}
            //}

            //这里检查用的权限
            //if (notificationDefinition.PermissionDependency != null) {
            //    using (var permissionDependencyContext = _iocManager.ResolveAsDisposable<PermissionDependencyContext> ()) {
            //        permissionDependencyContext.Object.User = user;

            //        if (!await notificationDefinition.PermissionDependency.IsSatisfiedAsync (permissionDependencyContext.Object)) {
            //            return false;
            //        }
            //    }
            //}

            return true;
        }


        public async Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(Guid user)
        {
            var availableDefinitions = new List<NotificationDefinition>();

            foreach (var notificationDefinition in GetAll())
            {
                //FeatureChecker.IsEnabledAsync();
                availableDefinitions.Add(notificationDefinition);
            }

            //using (var permissionDependencyContext = _iocManager.ResolveAsDisposable<PermissionDependencyContext> ()) {
            //    permissionDependencyContext.Object.User = user;

            //    using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext> ()) {
            //        featureDependencyContext.Object.TenantId = user.TenantId;

            //        foreach (var notificationDefinition in GetAll ()) {
            //            if (notificationDefinition.PermissionDependency != null &&
            //                !await notificationDefinition.PermissionDependency.IsSatisfiedAsync (permissionDependencyContext.Object)) {
            //                continue;
            //            }

            //            if (user.TenantId.HasValue &&
            //                notificationDefinition.FeatureDependency != null &&
            //                !await notificationDefinition.FeatureDependency.IsSatisfiedAsync (featureDependencyContext.Object)) {
            //                continue;
            //            }

            //            availableDefinitions.Add (notificationDefinition);
            //        }
            //    }
            //}

            return availableDefinitions.ToImmutableList();
        }

    }
}
