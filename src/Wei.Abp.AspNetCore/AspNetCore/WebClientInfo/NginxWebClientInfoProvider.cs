using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Volo.Abp.AspNetCore.WebClientInfo;
using Volo.Abp.DependencyInjection;

namespace Wei.Abp.AspNetCore.WebClientInfo
{
    /// <summary>
    /// 新增nginx提供商解决无法获取用户IP的问题
    /// </summary>
    [Dependency(ReplaceServices =true)]
    public class NginxWebClientInfoProvider : HttpContextWebClientInfoProvider, IWebClientInfoProvider, ITransientDependency
    {
        public NginxWebClientInfoProvider(ILogger<HttpContextWebClientInfoProvider> logger, IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {

        }
        protected override string GetClientIpAddress()
        {
            foreach (var item in HttpContextAccessor.HttpContext?.Request?.Headers)
            {
                Logger.LogInformation($"{item.Key}:{item.Value}");
            }
            
            StringValues? xForwardedfor = HttpContextAccessor.HttpContext?.Request?.Headers?["X-Forwarded-For"];
            string clientIpAddress = xForwardedfor.HasValue ? xForwardedfor.Value.ToString() : base.GetClientIpAddress();
            return clientIpAddress;
        }
    }
}
