using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Menu.Model;
using weixin.Respone;
using weixin.Utils;

namespace weixin
{
    public class WxMenu
    {
        public static PostResponeBase CreatMenu(MenuButton MuenButton)
        {
            string access_token = Wx.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token;
            string responeStr = HttpRequsetHelper.Post(url, MuenButton);
            PostResponeBase res = Newtonsoft.Json.JsonConvert.DeserializeObject<PostResponeBase>(responeStr);
            return res;
            
        }
    }
}
