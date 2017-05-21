using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Message.Model;

namespace weixin.Menu
{
    public class Wx_RegisterMenuEvent
    {
        /// <summary>
        /// 菜单点击事件
        /// </summary>
        public static Func<RequestMessage, ResponeMessage> onMenuClick { get; set; }
    }
}
