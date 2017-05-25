using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace weixin.Message.Model
{
    [XmlRoot("xml")]
    public class ReplyNewsMessage:ReplyMessage
    {
        public string MsgType = "news";
        public int ArticleCount { get; set; }
        [XmlArrayItem("item")]
        public List<Articles> Articles { get; set; }
    }
    
    public class Articles
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
    }

}
