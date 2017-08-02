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

        private static string access_token;
        private static DateTime expires_in;

        /// <summary>
        /// 配置微信
        /// </summary>
        /// <param name="_token"></param>
        /// <param name="_appid"></param>
        /// <param name="_secret"></param>
        public static void WxConfig(string _token, string _appid, string _secret,string _EncodingAESKey = "")
        {
            token = _token;
            appid = _appid;
            secret = _secret;
            EncodingAESKey = _EncodingAESKey;
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



        public static List<string> GetUserList()
        {
            string res = HttpRequsetHelper.Get($"https://api.weixin.qq.com/cgi-bin/user/get?access_token={GetAccessToken()}");
            UserRespone result = Newtonsoft.Json.JsonConvert.DeserializeObject<UserRespone>(res);

            if (result.count < 0)
                return null;

            List<string> openidList = new List<string>();
            openidList.AddRange(result.data.openid);

            bool hasnext = result.next_openid != null;
            while (hasnext)
            {
                string _re = HttpRequsetHelper.Get($"https://api.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN&next_openid={result.next_openid}");
                UserRespone _result = Newtonsoft.Json.JsonConvert.DeserializeObject<UserRespone>(res);
                openidList.AddRange(_result.data.openid);
                hasnext = _result.next_openid != null;
            }

            return openidList;
        }


    }


    class AccessToken
    {
        public string access_token { get; set; }
        public long expires_in { get; set; }
    }
}
