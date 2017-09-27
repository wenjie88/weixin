using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.QRCode
{
    public enum QRCode_ActionName
    {
        /// <summary>
        /// 时的整型参数值
        /// </summary>
        QR_SCENE,

        /// <summary>
        /// 临时的字符串参数值
        /// </summary>
        QR_STR_SCENE,

        /// <summary>
        /// 永久的整型参数值
        /// </summary>
        QR_LIMIT_SCENE,

        /// <summary>
        /// 永久的字符串参数值
        /// </summary>
        QR_LIMIT_STR_SCENE
    }
}
