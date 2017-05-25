using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace weixin.Message.Model
{
    [XmlRoot("xml")]
    public class ReplyTextMessage : ReplyMessage
    {
        public string MsgType = Message.MsgType.text.ToString();
        public string Content { get; set; }
    }
}
