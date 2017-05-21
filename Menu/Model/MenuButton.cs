using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Menu.Model
{
    public class MenuButton
    {
        public List<Button> button { get; set; }
    }

    public class Button
    {
        public string type { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string url { get; set; }
        public List<subButton> sub_button { get; set; }
    }

    public class subButton
    {
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string key { get; set; }
    }
}
