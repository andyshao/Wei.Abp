using System;
using System.Collections.Generic;
using System.Text;

namespace Wei.Abp.Sms.Settings
{
    public static class TencentCloudSmsSettingNames
    {
        const string Group = "Tencent.Sms";
        /// <summary>
        /// 应用ID
        /// </summary>
        public const string AppId = Group+".AppId";
        /// <summary>
        /// 秘钥ID
        /// </summary>
        public const string SecretId = Group + ".SecretId";
        /// <summary>
        /// 秘钥key
        /// </summary>
        public const string SecretKey = Group + ".SecretKey";

        /// <summary>
        /// 所在区域
        /// </summary>
        public const string Region = Group + ".Region";
        /// <summary>
        /// 默认签名
        /// </summary>
        public const string Sign = Group + ".Sign";
    }
}
