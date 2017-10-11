using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.WxPay.lib;

namespace weixin.WxPay.model
{
    public class WxPayCloseOrderResult
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
        public string result_msg { get; set; }

        public string err_code { get; set; }

        public string err_code_des { get; set; }



        public WxPayCloseOrderResult() { }

        public WxPayCloseOrderResult(WxPayData Data)
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
                result_msg = Data.GetValue("result_msg")?.ToString();
                err_code = Data.GetValue("err_code")?.ToString();
                err_code_des = Data.GetValue("err_code_des")?.ToString();
            }
        }
    }
}
