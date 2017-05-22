using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.WebOauth
{
    public class WebOauthHandler
    {
        /// <summary>
        /// 转换url为需要授权后跳转的url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="state">授权后 重定向后会带上state参数</param>
        /// <returns></returns>
        public static string CreateOathUrl(string url, string state = "")
        {
            url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Wx.appid + "&redirect_uri=" + url + "&response_type=code&scope=snsapi_userinfo&state=" + state + "#wechat_redirect";

            return url;
        }

        /// <summary>
        /// 通过code换取网页授权access_token
        /// </summary>
        public static Model.Oauth2 GetWebAccess_Token(string code)
        {
            string url = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={Wx.appid}&secret={Wx.secret}&code={code}&grant_type=authorization_code";
            string respone = weixin.Utils.HttpRequsetHelper.Get(url);
            Model.Oauth2 oa = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Oauth2>(respone);
            return oa;
        }

        /// <summary>
        /// 拉取用户信息
        /// </summary>
        /// <param name="oa"></param>
        /// <returns></returns>
        public static Model.WxUser GetWx_user(Model.Oauth2 WebAccess_Token_Oauth2)
        {
            //拉取用户信息
            string getUserUrl = $"https://api.weixin.qq.com/sns/userinfo?access_token={WebAccess_Token_Oauth2.access_token}&openid={WebAccess_Token_Oauth2.openid}&lang=zh_CN";
            string responeUser = weixin.Utils.HttpRequsetHelper.Get(getUserUrl);
            Model.WxUser user = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.WxUser>(responeUser);
            return user;
        }
    }
}
