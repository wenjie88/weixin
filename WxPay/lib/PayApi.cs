using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Utils;

namespace weixin.WxPay.lib
{
    public class PayApi
    {
        /// <summary>
        /// 调用统一下单，获得下单结果
        /// </summary>
        /// <returns></returns>
        public static WxPayData UnifiedOrder(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";


            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new Exception("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("body"))
            {
                throw new Exception("缺少统一支付接口必填参数body！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("缺少统一支付接口必填参数total_fee！");
            }
            else if (!inputObj.IsSet("trade_type"))
            {
                throw new Exception("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
            {
                throw new Exception("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
            {
                throw new Exception("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }


            inputObj.SetValue("appid", Wx.appid);
            inputObj.SetValue("mch_id", Wx.mch_id);
            inputObj.SetValue("notify_url", Wx.NOTIFY_URL);
            inputObj.SetValue("spbill_create_ip", Wx.IP);
            inputObj.SetValue("nonce_str", GenerateNonceStr());
            inputObj.SetValue("sign", inputObj.MakeSign());

            string xml = inputObj.ToXml();

            string respone = weixin.Utils.HttpService.Post(xml, url, false, 6);
            WxPayData result = new WxPayData();
            result.FromXml(respone);

            return result;
        }







        /**
        *    
        * 查询订单
        * @param WxPayData inputObj 提交给查询订单API的参数
        * @param int timeOut 超时时间
        * @throws WxPayException
        * @return 成功时返回订单查询结果，其他抛异常
        */
        public static WxPayData OrderQuery(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }


            inputObj.SetValue("appid", Wx.appid);
            inputObj.SetValue("mch_id", Wx.mch_id);
            inputObj.SetValue("nonce_str", GenerateNonceStr());
            inputObj.SetValue("sign", inputObj.MakeSign());

            string xml = inputObj.ToXml();

            string respone = HttpService.Post(xml, url, false, timeOut);

            WxPayData result = new WxPayData();
            result.FromXml(respone);

            return result;
        }








        /**
        * 
        * 申请退款
        * @param WxPayData inputObj 提交给申请退款API的参数
        * @param int timeOut 超时时间
        * @throws WxPayException
        * @return 成功时返回接口调用结果，其他抛异常
        */
        public static WxPayData Refund(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";

            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("退款申请接口中，out_trade_no、transaction_id至少填一个！");
            }
            else if (!inputObj.IsSet("out_refund_no"))
            {
                throw new Exception("退款申请接口中，缺少必填参数out_refund_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("退款申请接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("refund_fee"))
            {
                throw new Exception("退款申请接口中，缺少必填参数refund_fee！");
            }


            inputObj.SetValue("appid", Wx.appid);//公众账号ID
            inputObj.SetValue("mch_id", Wx.mch_id);//商户号
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名


            string xml = inputObj.ToXml();
            string response = HttpService.Post(xml, url, true, timeOut);//调用HTTP通信接口提交数据到API


            //将xml格式的结果转换为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

            return result;
        }








        /**
       * 
       * 查询退款
       * 提交退款申请后，通过该接口查询退款状态。退款有一定延时，
       * 用零钱支付的退款20分钟内到账，银行卡支付的退款3个工作日后重新查询退款状态。
       * out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个
       * @param WxPayData inputObj 提交给查询退款API的参数
       * @param int timeOut 接口超时时间
       * @throws WxPayException
       * @return 成功时返回，其他抛异常
       */
        public static WxPayData RefundQuery(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/refundquery";
            //检测必填参数
            if (!inputObj.IsSet("out_refund_no") && !inputObj.IsSet("out_trade_no") &&
                !inputObj.IsSet("transaction_id") && !inputObj.IsSet("refund_id"))
            {
                throw new Exception("退款查询接口中，out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个！");
            }


            inputObj.SetValue("appid", Wx.appid);//公众账号ID
            inputObj.SetValue("mch_id", Wx.mch_id);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名


            string xml = inputObj.ToXml();
            string response = HttpService.Post(xml, url, false, timeOut);//调用HTTP通信接口以提交数据到API

            //将xml格式的结果转换为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

            return result;
        }











        /// <summary>
        /// 关闭订单
        /// 注意：订单生成后不能马上调用关单接口，最短调用时间间隔为5分钟。
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxPayData CloseOrder(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/closeorder";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new Exception("关闭订单接口中，out_trade_no参数必填！");
            }

            inputObj.SetValue("appid", Wx.appid);//公众账号ID
            inputObj.SetValue("mch_id", Wx.mch_id);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名


            string xml = inputObj.ToXml();
            string response = HttpService.Post(xml, url, false, timeOut);//调用HTTP通信接口以提交数据到API

            //将xml格式的结果转换为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

            return result;
        }








        //========================================工具类方法======================================//
        //===================================================================================//



        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }




        /**
        * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
         * @return 时间戳
        */
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
