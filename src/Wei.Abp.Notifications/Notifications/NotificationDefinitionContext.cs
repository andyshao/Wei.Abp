using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.Abp.Notifications
{
    public class NotificationDefinitionContext: INotificationDefinitionContext
    {
        protected Dictionary<string, NotificationDefinition> Settings { get; }

        public NotificationDefinitionContext(Dictionary<string, NotificationDefinition> settings)
        {
            Settings = settings;
        }

        public virtual NotificationDefinition GetOrNull(string name)
        {
            return Settings.GetOrDefault(name);
        }

        public virtual IReadOnlyList<NotificationDefinition> GetAll()
        {
            return Settings.Values.ToImmutableList();
        }

        public virtual void Add(params NotificationDefinition[] definitions)
        {
            if (definitions.IsNullOrEmpty())
            {
                return;
            }

            foreach (var definition in definitions)
            {
                Settings[definition.Name] = definition;
            }
        }
    }
}
