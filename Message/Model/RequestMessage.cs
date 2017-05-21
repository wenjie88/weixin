using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace weixin.Message.Model
{
    [XmlRoot("xml")]
    public class RequestMessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public long CreateTime { get; set; }
        public string MsgType { get; set; }
        public string MsgId { get; set; }
        //文字
        public string Content { get; set; }
        //图片
        public string PicUrl { get; set; }
        public string MediaId { get; set; }
        //事件
        public string Event { get; set; }
        public string EventKey { get; set; }
    }
    
}
