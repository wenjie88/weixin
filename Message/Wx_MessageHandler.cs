using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Menu;
using weixin.Message.Model;
using weixin.Utils;

namespace weixin.Message
{
    public class Wx_MessageHandler
    {
        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static string ReplyMessage(Stream inputStream)
        {
            RequestMessage message = XMLHelper.XMLDeserializer<RequestMessage>(inputStream);


            if (message.MsgType == MsgType.Text) //接受到文字信息
            {
                ResponeMessage send = Wx_RegisterReplyMessage.onTextMsssage(message);
                return XMLHelper.XMLSerializer<ResponeMessage>(send);
            }
            else if (message.MsgType == MsgType.Image)//接受到图片信息
            {
                ResponeMessage send = Wx_RegisterReplyMessage.onImageMessage(message);
                return XMLHelper.XMLSerializer<ResponeMessage>(send);
            }
            else if (message.MsgType == MsgType.Event) //接受到菜单点击
            {
                if (message.Event == EventType.click)//点击
                {
                    ResponeMessage send = Wx_RegisterMenuEvent.onMenuClick(message);
                    return XMLHelper.XMLSerializer<ResponeMessage>(send);
                }
                else if (message.Event == EventType.view)//跳转url
                {

                }
                return "";
            }
            else
            {
                return "";
            }

        }


        /// <summary>
        /// 推送消息
        /// </summary>
        public static void pushMessage(PushTemplateMessage pushModel)
        {
            //post 消息模版
            string access_token = Wx.GetAccessToken();

            string s = HttpRequsetHelper.Post("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token, pushModel);
        }
    }
}
