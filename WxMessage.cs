using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Message;
using weixin.Message.Model;
using weixin.Respone;
using weixin.Utils;

namespace weixin
{
    public class WxMessage
    {
        /// <summary>
        /// 注册回复消息
        /// </summary>
        /// <param name="onMsgType"></param>
        /// <param name="HowToReply"></param>
        public static void RegisterReplyMessage(MsgType onMsgType, Func<ReceiveMessage, ReplyMessage> HowToReply)
        {
            ReplyMessageRule.AddRule(onMsgType, HowToReply);
        }
        public static void RegisterReplyMessage(EventType onEventType, Func<ReceiveMessage, ReplyMessage> HowToReply)
        {
            ReplyMessageRule.AddRule(onEventType, HowToReply);
        }


        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static string ReplyMessage(Stream inputStream)
        {
            ReceiveMessage message = XMLHelper.XMLDeserializer<ReceiveMessage>(inputStream);

            ReplyMessage replyMessage = ReplyMessageRule.ExcuteRule(message);
            if (replyMessage == null)
            {
                return "";
            }
            else
            {
                return XMLHelper.XMLSerializer<ReplyMessage>(replyMessage);
            }


        }


        /// <summary>
        /// 推送消息
        /// </summary>
        public static PushTemplateMsgRespone pushMessage(PushTemplateMessage pushModel)
        {
            //post 消息模版
            string access_token = Wx.GetAccessToken();

            string responeStr = HttpRequsetHelper.Post("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token, pushModel);

            PushTemplateMsgRespone res = Newtonsoft.Json.JsonConvert.DeserializeObject<PushTemplateMsgRespone>(responeStr);

            return res;
        }
    }
}
