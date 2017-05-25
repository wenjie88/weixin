using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Message.Model
{
    public abstract class ReplyMessage
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public long CreateTime = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }
}
