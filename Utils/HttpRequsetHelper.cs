using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Utils
{
    public class HttpRequsetHelper
    {
        public static string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

        public static string Post(string url, dynamic data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            //request.ContentType = "application/json";
            
            if(data != null)
            {
                string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                byte[] contentByte = Encoding.UTF8.GetBytes(jsonStr);

                request.ContentLength = contentByte.Length;
                request.ContentType = "application/json";

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(contentByte, 0, contentByte.Length);
                }
            }

            using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                return sr.ReadToEnd();
            }

        }
    }
}
