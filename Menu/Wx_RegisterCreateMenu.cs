using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Menu.Model;
using weixin.Utils;

namespace weixin.Menu
{
    public class Wx_RegisterCreateMenu
    {
        public static void CreatMenu(MenuButton MuenButton)
        {
            string access_token = Wx.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token;
            string responeStr = HttpRequsetHelper.Post(url, MuenButton);
            var s = 0;
        }
    }
}
