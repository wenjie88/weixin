using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.QRCode;
using weixin.Utils;

namespace weixin
{
    public class WxQRCode
    {

        public class QRCodeTicket
        {
            /// <summary>
            /// 获取的二维码ticket，凭借此ticket可以在有效时间内换取二维码。
            /// </summary>
            public string ticket { get; set; }

            /// <summary>
            /// 该二维码有效时间，以秒为单位。 最大不超过2592000（即30天）
            /// </summary>
            public int expire_seconds { get; set; }

            /// <summary>
            /// 二维码图片解析后的地址，开发者可根据该地址自行生成需要的二维码图片
            /// </summary>
            public string url { get; set; }
        }


        /// <summary>
        /// 创建二维码ticket
        /// </summary>
        /// <param name="scene_str"></param>
        /// <param name="action_name"></param>
        /// <param name="expire_seconds"></param>
        /// <returns></returns>
        public static QRCodeTicket CreateQRCode(string scene_str, QRCode_ActionName action_name, TimeSpan expire_seconds)
        {
            string access_token = Wx.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={access_token}";


            var data = CreateData(action_name, expire_seconds, null, scene_str);
            string responeStr = HttpRequsetHelper.Post(url, data);

            QRCodeTicket res = Newtonsoft.Json.JsonConvert.DeserializeObject<QRCodeTicket>(responeStr);
            return res;
            

        }



        /// <summary>
        /// 创建二维码ticket
        /// </summary>
        /// <param name="scene_id"></param>
        /// <param name="action_name"></param>
        /// <param name="expire_seconds"></param>
        /// <returns></returns>
        public static QRCodeTicket CreateQRCode(int scene_id, QRCode_ActionName action_name, TimeSpan expire_seconds)
        {
            string access_token = Wx.GetAccessToken();
            string url = $"https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={access_token}";

            var data = CreateData(action_name, expire_seconds, scene_id);
            string responeStr = HttpRequsetHelper.Post(url, data);
            
            QRCodeTicket res = Newtonsoft.Json.JsonConvert.DeserializeObject<QRCodeTicket>(responeStr);
            return res;
        }




        /// <summary>
        /// 生成二维码图片的url
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static string CreateQRCodeImgURL(string ticket)
        {
            return $"https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={ticket}";
        }




        private static object CreateData(QRCode_ActionName action_name, TimeSpan expire_seconds, int? scene_id, string scene_str = null)
        {
            switch (action_name)
            {
                //临时 id类型
                case QRCode_ActionName.QR_SCENE:
                    return new
                    {
                        expire_seconds = expire_seconds.Seconds,
                        action_name = action_name.ToString(),
                        action_info = new
                        {
                            scene = new
                            {
                                scene_id = scene_id
                            }
                        }
                    };
                //临时 str类型
                case QRCode_ActionName.QR_STR_SCENE:
                    return new
                    {
                        expire_seconds = expire_seconds.Seconds,
                        action_name = action_name.ToString(),
                        action_info = new
                        {
                            scene = new
                            {
                                scene_str = scene_str
                            }
                        }
                    };
                //永久 id类型
                case QRCode_ActionName.QR_LIMIT_SCENE:
                    return new
                    {
                        action_name = action_name.ToString(),
                        action_info = new
                        {
                            scene = new
                            {
                                scene_id = scene_id
                            }
                        }
                    };
                //永久 str类型
                case QRCode_ActionName.QR_LIMIT_STR_SCENE:
                    return new
                    {
                        action_name = action_name.ToString(),
                        action_info = new
                        {
                            scene = new
                            {
                                scene_str = scene_str
                            }
                        }
                    };
                default:
                    return null;
            }

        }
    }
}
