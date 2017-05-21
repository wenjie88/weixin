using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Utils
{
    public class Security
    {
        public static string sha1(string str)
        {
            using (SHA1 sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] str1 = Encoding.UTF8.GetBytes(str);
                str1 = sha1.ComputeHash(str1);

                //注意， 不要用这个，生成出来会有问题
                //return Convert.ToBase64String(str1);

                string outPut = BitConverter.ToString(str1).Replace("-", "").ToLower();
                return outPut;
            }
        }
    }
}
