using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Menu.Model
{

    public class WxMenuButton
    {

        public string type { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string url { get; set; }
        public List<WxMenu_subButton> sub_button { get; set; }
    }


    public class WxMenu_subButton
    {
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string key { get; set; }
    }

}
