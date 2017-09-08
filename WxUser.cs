using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Model;
using weixin.Utils;

namespace weixin
{
    public class WxUser
    {
        public static UserInfo GetWxUserInfo(string OpenId)
        {
            string access_token = Wx.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/user/info?access_token={access_token}&openid={OpenId}&lang=zh_CN";
            string responeStr = HttpRequsetHelper.Get(url);
            UserInfo res = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfo>(responeStr);
            return res;
        }
        
    }
}
