using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace weixin.Message.Model
{
    [XmlRoot("xml")]
    public class ResponeTextMessage : ResponeMessage
    {
        public string MsgType = Message.MsgType.Text;
        public string Content { get; set; }
    }
}
