using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.WxPay.lib;

namespace weixin.WxPay
{
    public class ResultNotify
    {

        public static WxPayData ReceiveNofifyData(Stream inputStream,out WxPayData ReturnData)
        {
            WxPayData notifyData = GetNotifyData(inputStream);

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                ReturnData = new WxPayData();
                ReturnData.SetValue("return_code", "FAIL");
                ReturnData.SetValue("return_msg", "支付结果中微信订单号不存在");
                return notifyData;
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                ReturnData = new WxPayData();
                ReturnData.SetValue("return_code", "FAIL");
                ReturnData.SetValue("return_msg", "订单查询失败");
                return notifyData;
            }
            //查询订单成功
            else
            {
                ReturnData = new WxPayData();
                ReturnData.SetValue("return_code", "SUCCESS");
                ReturnData.SetValue("return_msg", "OK");
            }

            return notifyData;

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






        //查询订单
        private static bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = PayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
