using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace weixin.Message.Model
{
    [XmlRoot("xml")]
    public class ResponeImageMessage : ResponeMessage
    {
        public string MsgType = Message.MsgType.Image;
        public resImg Image { get; set; }
    }

    [XmlRoot("Image")]
    public class resImg
    {
        public string MediaId { get; set; }
    }
}
