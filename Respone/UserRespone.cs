using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Respone
{
    public class UserRespone : PostResponeBase
    {
        //关注总人数
        public int total { get; set; }
        //拉取的人数，最大值1000
        public int count { get; set; }
        public UserData data { get; set; }
        //拉取列表的最后一个用户的OPENID
        public string next_openid { get; set; }
    }

    public class UserData
    {
        public List<string> openid { get; set; }
    }
}
