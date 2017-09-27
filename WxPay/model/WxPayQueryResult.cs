using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.WxPay.lib;

namespace weixin.WxPay.model
{
    public class WxPayQueryResult
    {
        //返回状态码
        public string return_code { get; set; }

        //返回信息
        public string return_msg { get; set; }


        //==========以下字段在return_code为SUCCESS的时候有返回==========//
        public string appid { get; set; }

        public string mch_id { get; set; }

        public string nonce_str { get; set; }

        public string sign { get; set; }

        public string result_code { get; set; }

        public string err_code { get; set; }

        public string err_code_des { get; set; }


        //===========以下字段在return_code 、result_code、trade_state都为SUCCESS时有返回 ，如trade_state不为 SUCCESS，则只返回out_trade_no（必传）和attach（选传）。====//
        public string device_info { get; set; }

        public string openid { get; set; }

        public string is_subscribe { get; set; }//用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效

        public string trade_type { get; set; }//调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，MICROPAY

        public string trade_state { get; set; }

        public string bank_type { get; set; }

        public int total_fee { get; set; }

        public int cash_fee { get; set; }

        public string fee_type { get; set; }

        public string cash_fee_type { get; set; }

        public string transaction_id { get; set; }

        public string out_trade_no { get; set; }

        public string attach { get; set; }

        public string time_end { get; set; }//20141030133525



        public WxPayQueryResult(WxPayData Data)
        {
            return_code = Data.GetValue("return_code")?.ToString();
            return_msg = Data.GetValue("return_msg")?.ToString();

            //==========以下字段在return_code为SUCCESS的时候有返回==========//
            if (return_code == "SUCCESS")
            {
                appid = Data.GetValue("appid")?.ToString();
                mch_id = Data.GetValue("mch_id")?.ToString();
                nonce_str = Data.GetValue("nonce_str")?.ToString();
                sign = Data.GetValue("sign")?.ToString();
                result_code = Data.GetValue("result_code")?.ToString();
                err_code = Data.GetValue("err_code")?.ToString();
                err_code_des = Data.GetValue("err_code_des")?.ToString();

                //===========以下字段在return_code 、result_code、trade_state都为SUCCESS时有返回 ，如trade_state不为 SUCCESS，则只返回out_trade_no（必传）和attach（选传）。====//
                if (result_code == "SUCCESS")
                {
                    device_info = Data.GetValue("device_info")?.ToString();
                    openid = Data.GetValue("openid")?.ToString();
                    is_subscribe = Data.GetValue("is_subscribe")?.ToString();
                    trade_type = Data.GetValue("trade_type")?.ToString();
                    trade_state = Data.GetValue("trade_state")?.ToString();
                    bank_type = Data.GetValue("bank_type")?.ToString();
                    fee_type = Data.GetValue("fee_type")?.ToString();
                    cash_fee_type = Data.GetValue("cash_fee_type")?.ToString();
                    total_fee = Convert.ToInt32(Data.GetValue("total_fee") ?? 0);
                    cash_fee = Convert.ToInt32(Data.GetValue("cash_fee") ?? 0);
                    out_trade_no = Data.GetValue("out_trade_no").ToString();
                    transaction_id = Data.GetValue("transaction_id")?.ToString();
                    time_end = Data.GetValue("time_end")?.ToString();
                    attach = Data.GetValue("attach")?.ToString();
                }
            }






        }
    }
}
