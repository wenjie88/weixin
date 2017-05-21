using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Message.Model;

namespace weixin.Message
{
    public class Wx_RegisterReplyMessage
    {
        /// <summary>
        /// 收到文字信息
        /// </summary>
        public static Func<RequestMessage, ResponeMessage> onTextMsssage { get; set; }

        /// <summary>
        /// 收到图片信息
        /// </summary>
        public static Func<RequestMessage, ResponeMessage> onImageMessage { get; set; }

       
    }
}
