using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace weixin.Message.Model
{
    [XmlRoot("xml")]
    public class ReplyImageMessage : ReplyBaseMessage
    {
        public string MsgType = "image";
        public resImg Image { get; set; }
    }

    [XmlRoot("Image")]
    public class resImg
    {
        public string MediaId { get; set; }
    }
}
