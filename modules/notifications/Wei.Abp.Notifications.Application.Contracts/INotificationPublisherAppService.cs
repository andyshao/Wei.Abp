using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Wei.Abp.Notifications
{
    public interface INotificationPublisherAppService: IApplicationService
    {
        Task PublishAsync(PublishNotificationInput input);
    }
}
