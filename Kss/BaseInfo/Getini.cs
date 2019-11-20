using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BaseInfo
{
    //以下两个方法 用来将枚举类型 的返回值变为字符串
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;
        public string Description { get { return description; } }

        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }

    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
    }

    /// <summary>
    /// 配置文件定义
    /// </summary>
    public enum IniInfo
    {
        //主节点 
        [EnumDescription("log")]
        Log,
        //子节点
        [EnumDescription("Log_dir")]
        Log_dir,
    }

    public class G_INI
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

        private static string sPath = System.AppDomain.CurrentDomain.BaseDirectory + "set.ini";

        //public void Ini(string path)
        //{
        //    this.sPath = path;
        //}

        public static void Writue(string section, string key, string value)
        {
            // section=配置节，key=键名，value=键值，path=路径
            WritePrivateProfileString(section, key, value, sPath);
        }

        public static string ReadValue(string section, string key)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);

            // section=配置节，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 255, sPath);

            return temp.ToString();
        }

        /// <summary>
        /// 参数为枚举类型   读取路径 自动加\\
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadValue(IniInfo section, IniInfo key)
        {
            // 每次从ini中读取多少字节 
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
            // section=配置节，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section.ToString(), key.ToString(), "", temp, 255, sPath);
            if (temp.ToString().EndsWith("\\"))
            {
                return temp.ToString();
            }
            else
            {
                return temp.ToString() + "\\";
            }
        }
    }
}