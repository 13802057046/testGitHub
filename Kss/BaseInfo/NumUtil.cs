using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BaseInfo
{
    public class NumUtil
    {
        /// <summary>
        /// ###,###,###.#########
        /// </summary>
        public const string NumberFormat_Comma = "###,###,###.#########";

        /// <summary>
        /// ###,###,##0.#########;
        /// </summary>
        public const string NumberFormat_Comma_Default_0 = "###,###,##0.#########";
        public const string NumberFormat_Comma_Default_Empty = "###,###,###.##";

        public const string NumberFormat_Money = "#,##0.00";
        public const string NumberFormat_Money2 = "###0.00";

        /// <function>NumUtil.AddComma</function>
        /// <summary>add thousand comm to a number string</summary>
        /// <param name="strNum"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string AddComma(string strNum)
        {
            if (strNum == null || strNum.Length == 0)
            {
                return strNum;
            }

            int intLength = 0;
            string num = "";
            string number = StrUtil.Trim(strNum);
            int dotIdx = number.IndexOf(".");
            if (dotIdx >= 0)
            {
                num = number.Substring(dotIdx);
                number = number.Substring(0, dotIdx);
            }
            intLength = number.Length;
            for (int i = 1; i <= intLength / 3; i++)
            {
                number = number.Insert(intLength - i * 3, ",");
            }
            if (number.Length > 1)
                if (number.Substring(0, 1).Equals(","))
                    number = number.Substring(1);
            return number + num;
        }

        /// <function>NumUtil.RemoveComma</function>
        /// <summary>remove thouands comma from a number string</summary>
        /// <param name="strNum">string</param>
        /// <returns>if it's not a number,do not convert,else return number value </returns>
        /// <remarks></remarks>
        public static string RemoveComma(string strNum)
        {
            return StrUtil.Replace(strNum, ",", "");
        }

        public static bool IsInteger(string str, string format)
        {
            Regex reg = new Regex(@"^[-\+]?\d+$");
            return reg.IsMatch(RemoveComma(str));
        }

        public static bool IsDemical(string str, string format)
        {
            Regex reg = new Regex(@"^[-\+]?\d*(\.\d+)?$");
            return reg.IsMatch(RemoveComma(str));
        }

        public static decimal GetVal(string str, string format)
        {
            string removeCommastr = RemoveComma(str);
            if (StrUtil.IsEmptyStr(str))
            {
                return 0;
            }
            return Convert.ToDecimal(removeCommastr);
        }

        public static decimal GetVal(object o)
        {
            try
            {
                string str = o == null ? "" : o.ToString();
                string removeCommastr = RemoveComma(str);
                removeCommastr = removeCommastr.Replace("(", "");
                removeCommastr = removeCommastr.Replace(")", "");
                if (StrUtil.IsEmptyStr(str))
                {
                    return 0;
                }
                return Convert.ToDecimal(removeCommastr);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int GetVal_Int(object o)
        {
            return (int)Math.Round(GetVal(o));
        }

        public static long GetVal_Long(object o)
        {
            return (long)Math.Round(GetVal(o));
        }

        public static string NumToStr(decimal val, string format)
        {
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.InvariantCulture;
            string s = val.ToString(format, cul);
            if (s.StartsWith(".")) { return "0" + s; }
            if (s.StartsWith("-.")) { return "-0" + s.Replace("-", ""); }
            return s;
        }

        /// <summary>
        /// 如果字符串不是数字，返回0
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal TryGetVal(object o)
        {
            string str = o == null ? "" : StrUtil.Trim(o.ToString().Replace("(", "").Replace(")", ""));
            if (str.Length == 0) { return 0; }
            if (!IsDemical(str, NumberFormat_Comma))
            {
                return 0;
            }
            return GetVal(str);
        }

        /// <summary>
        /// 如果字符串不是数字，返回0
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int TryGetVal_Int(object o)
        {
            string str = o == null ? "" : StrUtil.Trim(o.ToString());
            if (str.Length == 0) { return 0; }
            if (!IsDemical(str, NumberFormat_Comma))
            {
                return 0;
            }
            return GetVal_Int(str);
        }

        /// <summary>
        /// 如果字符串不是数字，返回0
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static long TryGetVal_Long(object o)
        {
            string str = o == null ? "" : StrUtil.Trim(o.ToString());
            if (str.Length == 0) { return 0; }
            if (!IsDemical(str, NumberFormat_Comma))
            {
                return 0;
            }
            return GetVal_Long(str);
        }

        /// <summary>
        /// 强制转化为整形，非数字时返回0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt(object input)
        {
            try
            {
                // 2012/12/07 edit 去除输入字符中的，号和.
                string str = input == null ? "" : input.ToString();
                string removeCommastr = RemoveComma(str);

                return Convert.ToInt32(removeCommastr);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 强制转化为长整形，非数字时返回0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long ToLong(object input)
        {
            try
            {
                // 2012/12/07 edit 去除输入字符中的，号和.
                string str = input == null ? "" : input.ToString();
                string removeCommastr = RemoveComma(str);

                return Convert.ToInt64(removeCommastr);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 强制转化为整形，非数字时返回99999
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt2(object input)
        {
            try
            {
                if (StrUtil.GetStr(input).Equals(string.Empty))
                {
                    return 99999;
                }
                return Convert.ToInt32(input);
            }
            catch
            {
                return 99999;
            }
        }

        /// <summary>
        /// 强制转换为Decimal型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object input)
        {
            try
            {
                return Convert.ToDecimal(input);
            }
            catch
            {
                return decimal.Zero;
            }
        }

        /// <summary>
        /// 强制转换为double型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ToDouble(object input)
        {
            try
            {
                return Convert.ToDouble(input);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// bool转int
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int BoolToInt(bool input)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将对象转换为bool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(object obj)
        {
            if (obj == null) return false;

            try
            {
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 小于1900-01-01的日期，则返回1900-01-01
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime dt1900(DateTime input)
        {
            try
            {
                if (input < Convert.ToDateTime("1900-01-01"))
                {
                    return Convert.ToDateTime("1900-01-01");
                }
                else
                {
                    return input;
                }
            }
            catch
            {
                return input;
            }
        }
    }
}