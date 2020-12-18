using System;
using System.Collections.Generic;
using System.Text;

namespace Wei.Abp.Ocelot.Provider.Consul
{
    public class ConsulOptions
    {
        public string ClientName { get; set; }
        public string ClientIP { get; set; }
        public string Server { get; set; }
        public string DataCenter { get; set; }
        public Uri Host { get; set; }
        ///// <summary>
        ///// 定义服务健康检查的url地址
        ///// </summary>
        //public string HealthCheckUrl { get; set; }
    }
}
