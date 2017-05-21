using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Message.Model
{
    public class PushTemplateMessage
    {
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }
        public dynamic data { get; set; }
    }
}
