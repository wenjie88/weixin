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
        string _msg_signature;
        string _timestamp;
        string _nonce;
        bool _isSecurity;

        string _token = Wx.token;
        string _EncodingAESKey = Wx.EncodingAESKey;
        string _appId = Wx.appid;

        Utils.Tencent.WXBizMsgCrypt wxcpt = null;
        


        public WxMessage(string msg_signature, string timestamp, string nonce, bool isSecurity)
        {
            
            _msg_signature = msg_signature;
            _timestamp = timestamp;
            _nonce = nonce;
            _isSecurity = isSecurity;
            //微信消息解密类
            wxcpt = new Utils.Tencent.WXBizMsgCrypt(_token, _EncodingAESKey, _appId);
        }
        

        /// <summary>
        /// 获取收到的消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public ReceiveMessage GetReceiveMsg(Stream inputStream)
        {
            string postData = "";
            using (StreamReader sr = new StreamReader(inputStream))
            {
                postData = sr.ReadToEnd();
            }

            //是否安全模式？
            if (_isSecurity)
            {
                string str = "";
                //解密
                int i = wxcpt.DecryptMsg(_msg_signature, _timestamp, _nonce, postData, ref str);
                if (i == 0)
                {
                    ReceiveMessage message = XMLHelper.XMLDeserializer<ReceiveMessage>(new MemoryStream(Encoding.UTF8.GetBytes(str)));
                    return message;
                }
            }
            else
            {
                ReceiveMessage message = XMLHelper.XMLDeserializer<ReceiveMessage>(new MemoryStream(Encoding.UTF8.GetBytes(postData)));
                return message;
            }

            return null;
        }




        /// <summary>
        /// 获取回复消息xml
        /// </summary>
        /// <param name="replyMsg"></param>
        /// <returns></returns>
        public string GetReplyMessage(ReplyMessage replyMsg)
        {
            if (replyMsg == null) return "";

            if (_isSecurity)
            {
                string relMsg = XMLHelper.XMLSerializer<ReplyMessage>(replyMsg);
                string EncryptMsg = "";
                //加密
                wxcpt.EncryptMsg(relMsg, _timestamp, _nonce, ref EncryptMsg);
                return EncryptMsg;
            }
            else
            {
                return XMLHelper.XMLSerializer<ReplyMessage>(replyMsg);
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
