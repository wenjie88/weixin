using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Model
{
    public class UserInfo
    {
        //用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        public string subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public string language { get; set; }

        //用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        public int sex { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public decimal subscribe_time { get; set; }
        public string remark { get; set; }

        //只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        public string unionid { get; set; }

        //错误
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}
