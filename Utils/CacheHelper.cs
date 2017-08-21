using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace weixin.Utils
{
    public class CacheHelper
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="msgid"></param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            Cache c = HttpRuntime.Cache;
            if (c[key] != null)
            {
                return c[key];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="msgid"></param>
        public static void SetCache(string key, object value, TimeSpan ts)
        {
            Cache c = HttpRuntime.Cache;
            c.Insert(key, value, null, DateTime.MaxValue, ts, CacheItemPriority.NotRemovable, null);
        }
    }
}
