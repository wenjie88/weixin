using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.WebOauth.Model
{
    public class Oauth2
    {
        //网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }//用户刷新access_token
        public string openid { get; set; }
        public string scope { get; set; }
    }
}
