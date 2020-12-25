using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.Abp.Notifications
{
    public interface INotificationPublisherAppService
    {
        Task PublishAsync(PublishNotificationInput input);
    }
}
