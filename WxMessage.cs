using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using weixin.Message;
using weixin.Message.Model;
using weixin.Respone;
using weixin.Utils;

namespace weixin
{
    public class WxMessage
    {
        //string _msg_signature;
        //string _timestamp;
        //string _nonce;
        //bool _isSecurity;

        string _token = Wx.token;
        string _EncodingAESKey = Wx.EncodingAESKey;
        string _appId = Wx.appid;

        WxMessageBaseHandler _MessageHandler;

        //Utils.Tencent.WXBizMsgCrypt wxcpt = null;



        //public WxMessage(string msg_signature, string timestamp, string nonce, bool isSecurity)
        //{

        //    _msg_signature = msg_signature;
        //    _timestamp = timestamp;
        //    _nonce = nonce;
        //    _isSecurity = isSecurity;
        //    //微信消息解密类
        //    wxcpt = new Utils.Tencent.WXBizMsgCrypt(_token, _EncodingAESKey, _appId);
        //}

        public WxMessage(WxMessageBaseHandler MessageHandler)
        {
            this._MessageHandler = MessageHandler;
        }
        
        /// <summary>
        /// 执行消息回复
        /// </summary>
        /// <param name="MessageHandler"></param>
        /// <returns></returns>
        public string Excute()
        {
            var Request = HttpContext.Current.Request;

           
            string signature = Request["signature"];
            string timestamp = Request["timestamp"];
            string nonce = Request["nonce"];
            string echostr = Request["echostr"];

            string encrypt_type = Request["encrypt_type"];// aes 是安全模式。  null是明文模式
            string msg_signature = Request["msg_signature"];  //消息加密后 会有这个参数

            //解加密类
            Utils.Tencent.WXBizMsgCrypt wxcpt = new Utils.Tencent.WXBizMsgCrypt(_token, _EncodingAESKey, _appId);

            //获取xml
            Stream xmlStream = Request.InputStream;
            string postData = "";
            using (StreamReader sr = new StreamReader(xmlStream))
            {
                postData = sr.ReadToEnd();
            }

            //收到的消息
            ReceiveMessage receiveMsg = null;

            //是否安全模式？
            if (encrypt_type == "aes")
            {
                string str = "";
                int i = wxcpt.DecryptMsg(msg_signature, timestamp, nonce, postData, ref str);
                if (i == 0)
                {
                    receiveMsg = XMLHelper.XMLDeserializer<ReceiveMessage>(new MemoryStream(Encoding.UTF8.GetBytes(str)));
                }
            }
            else
            {
                receiveMsg = XMLHelper.XMLDeserializer<ReceiveMessage>(new MemoryStream(Encoding.UTF8.GetBytes(postData)));
            }


            //避免重复接受
            if (receiveMsg.MsgId != null)
            {
                if (CacheHelper.GetCache(receiveMsg.MsgId) != null)
                    return "";
                CacheHelper.SetCache(receiveMsg.MsgId, "xx", new TimeSpan(0, 0, 1, 0));
            }



            //执行MessageHandler
            ReplyBaseMessage replyMsg = ExcuteMessageHandler(receiveMsg);
            if (replyMsg == null)
                return "";

            //是否安全模式？
            if (encrypt_type == "aes")
            {
                string relMsg = XMLHelper.XMLSerializer<ReplyBaseMessage>(replyMsg);
                string EncryptMsg = "";
                //加密
                wxcpt.EncryptMsg(relMsg, timestamp, nonce, ref EncryptMsg);
                return EncryptMsg;
            }
            else
            {
                return XMLHelper.XMLSerializer<ReplyBaseMessage>(replyMsg);
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



        private ReplyBaseMessage ExcuteMessageHandler(ReceiveMessage ReceiveMessage)
        {
            ReplyBaseMessage Reply = null;

            switch (ReceiveMessage.MsgType)
            {
                case "text":
                    Reply = _MessageHandler.On_TextEvent(ReceiveMessage);
                    break;
                case "image":
                    Reply = _MessageHandler.On_ImageEvent(ReceiveMessage);
                    break;
                case "voice":
                    Reply = _MessageHandler.On_VoiceEvent(ReceiveMessage);
                    break;
                case "video":
                    Reply = _MessageHandler.On_VideoEvent(ReceiveMessage);
                    break;
                case "shortvideo":
                    Reply = _MessageHandler.On_ShortVideoEvent(ReceiveMessage);
                    break;
                case "location":
                    Reply = _MessageHandler.On_LocationEvent(ReceiveMessage);
                    break;
                case "link":
                    Reply = _MessageHandler.On_LinkEvent(ReceiveMessage);
                    break;
                case "event":
                    switch (ReceiveMessage.Event)
                    {
                        case "subscribe":
                            Reply = _MessageHandler.On_SubscribeEvent(ReceiveMessage);
                            break;
                        case "unsubscribe":
                            Reply = _MessageHandler.On_UnSubscribeEvent(ReceiveMessage);
                            break;
                        case "CLICK":
                            Reply = _MessageHandler.On_CLICKEvent(ReceiveMessage);
                            break;
                        case "VIEW":
                            Reply = _MessageHandler.On_VIEWEvent(ReceiveMessage);
                            break;
                        case "scancode_push":
                            Reply = _MessageHandler.On_Scancode_pushEvent(ReceiveMessage);
                            break;
                        case "scancode_waitmsg":
                            Reply = _MessageHandler.On_Scancode_waitmsgEvent(ReceiveMessage);
                            break;
                        case "pic_sysphoto":
                            Reply = _MessageHandler.On_Pic_sysphotoEvent(ReceiveMessage);
                            break;
                        case "pic_photo_or_album":
                            Reply = _MessageHandler.On_Pic_photo_or_albumEvent(ReceiveMessage);
                            break;
                        case "pic_weixin":
                            Reply = _MessageHandler.On_Pic_weixinEvent(ReceiveMessage);
                            break;
                        case "location_select":
                            Reply = _MessageHandler.On_Location_selectEvent(ReceiveMessage);
                            break;
                        default:
                            Reply = _MessageHandler.On_DefaultEvent(ReceiveMessage);
                            break;
                    }
                    break;
                default:
                    Reply = _MessageHandler.On_DefaultEvent(ReceiveMessage);
                    break;
            }

            if(Reply != null)
            {
                Reply.FromUserName = ReceiveMessage.ToUserName;
                Reply.ToUserName = ReceiveMessage.FromUserName;
            }
            

            return Reply;
        }

        /// <summary>
        /// 获取收到的消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        //private ReceiveMessage GetReceiveMsg(Stream inputStream)
        //{
        //    string postData = "";
        //    using (StreamReader sr = new StreamReader(inputStream))
        //    {
        //        postData = sr.ReadToEnd();
        //    }

        //    //是否安全模式？
        //    if (_isSecurity)
        //    {
        //        string str = "";
        //        //解密
        //        int i = wxcpt.DecryptMsg(_msg_signature, _timestamp, _nonce, postData, ref str);
        //        if (i == 0)
        //        {
        //            ReceiveMessage message = XMLHelper.XMLDeserializer<ReceiveMessage>(new MemoryStream(Encoding.UTF8.GetBytes(str)));
        //            return message;
        //        }
        //    }
        //    else
        //    {
        //        ReceiveMessage message = XMLHelper.XMLDeserializer<ReceiveMessage>(new MemoryStream(Encoding.UTF8.GetBytes(postData)));
        //        return message;
        //    }

        //    return null;
        //}




        /// <summary>
        /// 获取回复消息xml
        /// </summary>
        /// <param name="replyMsg"></param>
        /// <returns></returns>
        //private string GetReplyMessage(ReplyMessage replyMsg)
        //{
        //    if (replyMsg == null) return "";

        //    if (_isSecurity)
        //    {
        //        string relMsg = XMLHelper.XMLSerializer<ReplyMessage>(replyMsg);
        //        string EncryptMsg = "";
        //        //加密
        //        wxcpt.EncryptMsg(relMsg, _timestamp, _nonce, ref EncryptMsg);
        //        return EncryptMsg;
        //    }
        //    else
        //    {
        //        return XMLHelper.XMLSerializer<ReplyMessage>(replyMsg);
        //    }
        //}





 
        
    }
}
