using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace KssApi.Common
{
    /// <summary>
    /// XML序列化公共处理类
    /// </summary>
    public static class XmlSerializeHelper
    {
        public static string Serialize<T>(T serializeClass)
        {
            string xmlString = string.Empty;
            try
            {
                if (serializeClass != null)
                {
                    //Create our own namespaces for the output
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    //Add an empty namespace and empty value 
                    ns.Add("", "");


                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    StringBuilder xmlStringBuilder = new StringBuilder();
                    XmlWriterSettings setting = new XmlWriterSettings()
                    {
                        OmitXmlDeclaration = true,
                        Encoding = Encoding.UTF8
                    };
                    using (XmlWriter writer = XmlWriter.Create(xmlStringBuilder, setting))
                    {
                        serializer.Serialize(writer, serializeClass,ns);
                        xmlString = xmlStringBuilder.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                xmlString = string.Empty;
            }
            return xmlString;
        }


        

        public class StringUTF8Writer : System.IO.StringWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        /// <summary>
        /// 将实体对象转换成XML
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体对象</param>
        public static string XmlSerialize<T>(T obj)
        {
            try
            {
                using (StringUTF8Writer sw = new StringUTF8Writer())
                {
                    XmlSerializer xz = new XmlSerializer(obj.GetType());
                    xz.Serialize(sw, obj);
                    return sw.ToString();
                }

                //using (StringWriter sw = new StringWriter())
                //{
                //    Type t = obj.GetType();
                //    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                //    serializer.Serialize(sw, obj);
                //    sw.Close();
                //    return sw.ToString();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }

        /// <summary>
        /// 将XML转换成实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="strXML">XML</param>
        public static T DESerializer<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将XML转换成实体对象异常", ex);
            }
        }
    }
}