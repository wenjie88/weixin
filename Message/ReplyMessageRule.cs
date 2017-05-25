using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Message.Model;

namespace weixin.Message
{
    public class ReplyMessageRule
    {
        private static Dictionary<string, Func<ReceiveMessage, ReplyMessage>> msgRule = new Dictionary<string, Func<ReceiveMessage, ReplyMessage>>();

        private static Dictionary<string, Func<ReceiveMessage, ReplyMessage>> eventRule = new Dictionary<string, Func<ReceiveMessage, ReplyMessage>>();


        /// <summary>
        /// 添加回复消息
        /// </summary>
        /// <param name="ReceiveMsgType"></param>
        /// <param name="HowToReply"></param>
        public static void AddRule(MsgType ReceiveMsgType, Func<ReceiveMessage, ReplyMessage> HowToReply)
        {
            msgRule[ReceiveMsgType.ToString().ToLower()] = HowToReply;
        }

        public static void AddRule(EventType ReceiveEnentType, Func<ReceiveMessage, ReplyMessage> HowToReply)
        {
            eventRule[ReceiveEnentType.ToString().ToLower()] = HowToReply;
        }


        /// <summary>
        /// 执行规则，返回 回复消息
        /// </summary>
        /// <param name="receiveMessage"></param>
        /// <returns></returns>
        public static ReplyMessage ExcuteRule(ReceiveMessage receiveMessage)
        {
            string msgtype = receiveMessage.MsgType.ToLower();
            if(msgtype == "event")
            {
                string Event = receiveMessage.Event.ToLower();
                return eventRule.ContainsKey(Event) ? eventRule[Event](receiveMessage) : null;
            }
            else
            {
                return msgRule.ContainsKey(msgtype) ? msgRule[msgtype](receiveMessage) : null;
            }
            
        }
    }
}
