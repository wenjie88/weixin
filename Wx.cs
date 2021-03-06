﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using weixin.Message;
using weixin.Message.Model;
using weixin.Respone;
using weixin.Utils;

namespace weixin
{
    public static class Wx
    {
        public static string token = "";
        public static string appid = "";
        public static string secret = "";
        public static string EncodingAESKey = "";

        //WxPay
        public static string KEY = "";
        public static string mch_id = "";
        public static string IP = "0.0.0.0";
        public static string NOTIFY_URL = "";
        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public static string SSLCERT_PATH = "";
        public static string SSLCERT_PASSWORD = "";


        private static string access_token;
        private static DateTime expires_in;

        /// <summary>
        /// 配置微信
        /// </summary>
        /// <param name="_token"></param>
        /// <param name="_appid"></param>
        /// <param name="_secret"></param>
        public static void WxConfig(string _token, string _appid, string _secret, string _EncodingAESKey = "")
        {
            token = _token;
            appid = _appid;
            secret = _secret;
            EncodingAESKey = _EncodingAESKey;
        }


        public static void WxConfigWxPay(string _KEY, string _mch_id, string _IP, string _NOTIFY_URL, string _SSLCERT_PATH, string _SSLCERT_PASSWORD)
        {
            KEY = _KEY;
            mch_id = _mch_id;
            IP = _IP;
            NOTIFY_URL = _NOTIFY_URL;
            SSLCERT_PATH = _SSLCERT_PATH;
            SSLCERT_PASSWORD = _SSLCERT_PASSWORD;
        }


        /// <summary>
        /// 验证微信服务器消息
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static bool validateSignature(string signature, string timestamp, string nonce)
        {
            string[] strArr = { timestamp, nonce, token };
            Array.Sort(strArr);
            string str = string.Join("", strArr);

            //sha1 加密
            string sha1Str = Security.sha1(str);

            return sha1Str == signature;
        }

        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken()
        {
            if (DateTime.Now < expires_in)
            {
                return access_token;
            }
            else
            {
                string res = HttpRequsetHelper.Get("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret);

                AccessToken token = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(res);
                access_token = token.access_token;
                //提前10分钟过期
                expires_in = DateTime.Now.AddSeconds(token.expires_in - 60 * 10);

                return access_token;
            }

        }


    }


    class AccessToken
    {
        public string access_token { get; set; }
        public long expires_in { get; set; }
    }
}
