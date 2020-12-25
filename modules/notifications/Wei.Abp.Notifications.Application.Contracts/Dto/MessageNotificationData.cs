//using System;
//using Volo.Abp.Data;

//namespace Wei.Abp.Notifications
//{
//    /// <summary>
//    /// 消息通知内容
//    /// </summary>
//    [Serializable]
//    public class MessageNotificationData : NotificationData
//    {
//        /// <summary>
//        /// 消息标题
//        /// </summary>
//        public string Title
//        {
//            get { return this.GetProperty<string>(nameof(Title)); }
//            set
//            {
//                this.SetProperty(nameof(Title), value);
//            }
//        }

//        /// <summary>
//        /// 消息内容
//        /// </summary>
//        public string Message
//        {
//            get { return this.GetProperty<string>(nameof(Message)); }
//            set
//            {
//                this.SetProperty(nameof(Message), value);
//            }
//        }

//        /// <summary>
//        /// React 后台地址
//        /// </summary>
//        public string ReactPath
//        {
//            get { return this.GetProperty<string>(nameof(ReactPath)); }
//            set
//            {
//                this.SetProperty(nameof(ReactPath), value);
//            }
//        }

//        /// <summary>
//        /// 微信小程序地址
//        /// </summary>
//        public string WeChatMiniPath
//        {
//            get { return this.GetProperty<string>(nameof(WeChatMiniPath)); }
//            set
//            {
//                this.SetProperty(nameof(WeChatMiniPath), value);
//            }
//        }


//        public MessageNotificationData(string message)
//        {
//            //Title = title;
//            Message = message;
//        }
//    }
//}
