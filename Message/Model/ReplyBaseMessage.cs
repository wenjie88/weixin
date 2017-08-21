using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Message.Model
{
    public abstract class ReplyBaseMessage
    {
        public string ToUserName = "";
        public string FromUserName = "";
        public long CreateTime = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }
}
