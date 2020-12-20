namespace Wei.Abp.Notifications.Features
{
    public static class NotificationsFeatures
    {
        public const string Group = "Notifications";

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
