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
        public static PostResponeBase CreatMenu(List<WxMenuButton> MuenButton)
        {
            string access_token = Wx.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token;
            string responeStr = HttpRequsetHelper.Post(url, new { button= MuenButton });
            PostResponeBase res = Newtonsoft.Json.JsonConvert.DeserializeObject<PostResponeBase>(responeStr);
            return res;
            
        }


        public static bool DeleteMenu()
        {
            string access_token = Wx.GetAccessToken();
            string url = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=" + access_token;
            string responeStr = HttpRequsetHelper.Get(url);
            PostResponeBase res = Newtonsoft.Json.JsonConvert.DeserializeObject<PostResponeBase>(responeStr);
            return res.errcode == 0;
        }
    }
}
