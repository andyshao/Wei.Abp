namespace Wei.Abp.Notifications
{
    public static class NotificationsFeatures
    {
        public const string Group = "Notifications";
        /// <summary>
        /// 是否开启消息通知
        /// </summary>
        public const string Enable = Group + ".Enable";
        /// <summary>
        /// 开启短信通知
        /// </summary>
        public const string EnableForSMS = Group + ".EnableForSMS";
        /// <summary>
        /// 开启微信通知
        /// </summary>
        public const string EnableForWeChat = Group + ".EnableForWeChat";

    }
}
