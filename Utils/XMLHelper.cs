using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace weixin.Utils
{
    public class XMLHelper
    {
        public static T XMLDeserializer<T>(Stream stream)
        {
            T obj;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                obj = (T)serializer.Deserialize(stream);
            }
            catch (Exception x)
            {
                return default(T);
            }

            stream.Close();
            stream.Dispose();

            return obj;
        }

        public static string XMLSerializer<T>(T obj)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = " ",
                NewLineChars = "\r\n",
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true //不生成声明头
            };

            var xns = new XmlSerializerNamespaces();
            xns.Add("", "");

            using (Stream stream2 = new MemoryStream())
            {

                using (XmlWriter writer = XmlWriter.Create(stream2, settings))
                {
                    var xml = new XmlSerializer(obj.GetType());
                    xml.Serialize(writer, obj, xns);
                }


                stream2.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(stream2))
                {
                    string sendStr2 = sr.ReadToEnd();
                    return sendStr2;
                }

            }
        }


    }
}
