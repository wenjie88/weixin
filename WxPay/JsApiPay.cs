using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.WxPay.lib;
using weixin.WxPay.model;

namespace weixin.WxPay
{
    public class JsApiPay
    {

        public WxPayData unifiedOrderResult { get; set; }



        /// <summary>
        /// 调用统一下单，获得下单结果
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="out_trade_no"></param>
        /// <param name="total_fee">单位 分</param>
        /// <param name="time_expire">超时时间  yyyyMMddHHmmss</param>
        /// <param name="attach">自定义数据包 </param>
        /// <returns></returns>
        public WxPayData GetUnifiedOrderResult(string OpenId, string out_trade_no, int total_fee, string body, string time_expire, string attach = null)
        {
            WxPayData data = new WxPayData();
            data.SetValue("body", body);
            data.SetValue("attach", attach);
            data.SetValue("out_trade_no", out_trade_no);
            data.SetValue("total_fee", total_fee);
            data.SetValue("trade_type", "JSAPI");
            data.SetValue("openid", OpenId);
            data.SetValue("device_info", "WEB");
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", time_expire);
            //data.SetValue("product_id", ProductId);


            //调用接口
            WxPayData result = PayApi.UnifiedOrder(data);



            this.unifiedOrderResult = result;
            return result;
        }



        /**
        *  
        * 从统一下单成功返回的数据中获取微信浏览器调起jsapi支付所需的参数，
        * 微信浏览器调起JSAPI时的输入参数格式如下：
        * {
        *   "appId" : "wx2421b1c4370ec43b",     //公众号名称，由商户传入     
        *   "timeStamp":" 1395712654",         //时间戳，自1970年以来的秒数     
        *   "nonceStr" : "e61463f8efa94090b1f366cccfbbb444", //随机串     
        *   "package" : "prepay_id=u802345jgfjsdfgsdg888",     
        *   "signType" : "MD5",         //微信签名方式:    
        *   "paySign" : "70EA570631E4BB79628FBCA90534C63FF7FADD89" //微信签名 
        * }
        * @return string 微信浏览器调起JSAPI时的输入参数，json格式可以直接做参数用
        * 更详细的说明请参考网页端调起支付API：http://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_7
        * 
        */
        public string GetJsApiParameters()
        {
            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", this.unifiedOrderResult.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", PayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", PayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + this.unifiedOrderResult.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            return Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                appId = jsApiParam.GetValue("appId"),
                timeStamp = jsApiParam.GetValue("timeStamp"),
                nonceStr = jsApiParam.GetValue("nonceStr"),
                package = jsApiParam.GetValue("package"),
                signType = jsApiParam.GetValue("signType"),
                paySign = jsApiParam.GetValue("paySign")
            });
        }





        /// <summary>
        /// 支付结果通知
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="ReturnData"></param>
        /// <returns></returns>
        public static WxPayQueryResult ReceiveNofifyData(Stream inputStream, out WxPayData ReturnData)
        {
            WxPayData notifyData = GetNotifyData(inputStream);

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                ReturnData = new WxPayData();
                ReturnData.SetValue("return_code", "FAIL");
                ReturnData.SetValue("return_msg", "支付结果中微信订单号不存在");

                WxPayQueryResult result = new WxPayQueryResult(notifyData);
                return result;
            }
            else
            {
                string transaction_id = notifyData.GetValue("transaction_id").ToString();

                //查询订单，判断订单真实性
                WxPayQueryResult result = QueryOrder(transaction_id: transaction_id);
                if (String.IsNullOrEmpty(result.transaction_id))
                {
                    //若订单查询失败，则立即返回结果给微信支付后台
                    ReturnData = new WxPayData();
                    ReturnData.SetValue("return_code", "FAIL");
                    ReturnData.SetValue("return_msg", "订单查询失败");
                }
                //查询订单成功
                else
                {
                    ReturnData = new WxPayData();
                    ReturnData.SetValue("return_code", "SUCCESS");
                    ReturnData.SetValue("return_msg", "OK");
                }

                return result;
            }

        }




        private static WxPayData GetNotifyData(Stream inputStream)
        {
            string resultXml = "";
            using (StreamReader sr = new StreamReader(inputStream))
            {
                resultXml = sr.ReadToEnd();
            }

            try
            {
                WxPayData res = new WxPayData();
                res.FromXml(resultXml);
                return res;
            }
            catch (Exception ex)
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                return res;
            }
        }









        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="transaction_id"></param>
        ///  /// <param name="transaction_id"></param>
        /// <returns></returns>
        public static WxPayQueryResult QueryOrder(string transaction_id = null, string out_trade_no = null)
        {
            WxPayData req = new WxPayData();
            if (!String.IsNullOrEmpty(transaction_id))
                req.SetValue("transaction_id", transaction_id);
            if (!String.IsNullOrEmpty(out_trade_no))
                req.SetValue("out_trade_no", out_trade_no);

            WxPayData _res = PayApi.OrderQuery(req);
            WxPayQueryResult result = new WxPayQueryResult(_res);
            /*
             res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS"
             */
            return result;
        }










        /***
         * 申请退款完整业务流程逻辑
         * @param transaction_id 微信订单号（优先使用）
         * @param out_trade_no 商户订单号
         * @param total_fee 订单总金额
         * @param refund_fee 退款金额
         * @param OutTradeNo 商户退款单号
         * @param refund_desc 退款原因 (80个字符)
         * @return 退款结果（）
         */
        public static WxPayData Refund(string transaction_id, string out_trade_no, int total_fee, int refund_fee, string OutTradeNo, string refund_desc)
        {
            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", out_trade_no);
            }

            data.SetValue("total_fee", total_fee);//订单总金额
            data.SetValue("refund_fee", refund_fee);//退款金额
            data.SetValue("out_refund_no", OutTradeNo);//随机生成商户退款单号
            //data.SetValue("op_user_id", Wx.mch_id);//操作员，默认为商户号

            WxPayData result = PayApi.Refund(data);//提交退款申请给API，接收返回数据

            return result;

        }








        /***
        * 退款查询完整业务流程逻辑
        * @param refund_id 微信退款单号（优先使用）
        * @param out_refund_no 商户退款单号
        * @param transaction_id 微信订单号
        * @param out_trade_no 商户订单号
        * @return 退款查询结果（xml格式）
        */
        public static WxPayData QueryRefundResult(string refund_id, string out_refund_no, string transaction_id, string out_trade_no)
        {
            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(refund_id))
            {
                data.SetValue("refund_id", refund_id);//微信退款单号，优先级最高
            }
            else if (!string.IsNullOrEmpty(out_refund_no))
            {
                data.SetValue("out_refund_no", out_refund_no);//商户退款单号，优先级第二
            }
            else if (!string.IsNullOrEmpty(transaction_id))
            {
                data.SetValue("transaction_id", transaction_id);//微信订单号，优先级第三
            }
            else
            {
                data.SetValue("out_trade_no", out_trade_no);//商户订单号，优先级最低
            }

            WxPayData result = PayApi.RefundQuery(data);//提交退款查询给API，接收返回数据


            return result;
        }








        /***
       * 关闭订单
       * 订单生成后不能马上调用关单接口，最短调用时间间隔为5分钟。
       * @param out_trade_no 商户订单号
       * @return 退款查询结果（xml格式）
       */
        public static WxPayCloseOrderResult CloseOrder(string out_trade_no)
        {
            WxPayData data = new WxPayData();
            data.SetValue("out_trade_no", out_trade_no);//商户订单号去退款
            
            WxPayData result = PayApi.CloseOrder(data);
            WxPayCloseOrderResult CloseResult = new WxPayCloseOrderResult(result);
            return CloseResult;
        }
    }
}
