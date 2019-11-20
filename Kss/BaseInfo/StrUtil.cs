using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BaseInfo
{
    public class StrUtil
    {
        public const char HalfCaseSpace = ' ';
        public const char FullCaseSpace = '　';

        /// <summary>test a char is full case.</summary>
        /// <param name="intChrCode">the char's unicode.(must be a number that subjects to unicode rule)</param>
        /// <returns>true-is full case char, false-otherwise.</returns>
        /// <remarks></remarks>
        public static bool IsSBCChar(char intChrCode)
        {
            if (intChrCode <= 0xFF) { return false; }
            if ((intChrCode >> 8) == 0xFF) { return false; }
            return true;
        }

        /// <summary>trim out left charater</summary>
        /// <param name="str">string</param>
        /// <param name="chrs">trimed charater</param>
        /// <returns>string after trimed</returns>
        /// <remarks></remarks>
        public static string TrimLeftChar(string str, string chrs)
        {
            if (chrs == null || chrs.Length == 0 || str == null || str.Length == 0) { return str; }
            char[] chars = chrs.ToCharArray();
            return str.TrimStart(chars);
        }

        /// <function>StrUtil.TrimLeft</function>
        /// <summary>trim left spaces.</summary>
        /// <param name="str">string.</param>
        /// <param name="blnIncludeFullSpace">
        ///             true   ,include full case spaces.
        ///             false  ,do not include full case spaces.(default)
        ///</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string TrimLeft(string str, bool blnIncludeFullSpace)
        {
            if (str == null) { return ""; }
            if (blnIncludeFullSpace) { return str.TrimStart(HalfCaseSpace, FullCaseSpace); }
            return str.TrimStart(HalfCaseSpace);
        }

        public static string TrimLeft(string str)
        {
            return TrimLeft(str, false);
        }

        /// <function>StrUtil.TrimRightChar</function>
        /// <summary>trim out right charater</summary>
        /// <param name="str">string</param>
        /// <param name="chrs">trimed charater</param>
        /// <returns>string after trimed</returns>
        /// <remarks></remarks>
        public static string TrimRightChar(string str, string chrs)
        {
            if (chrs == null || chrs.Length == 0 || str == null || str.Length == 0) { return str; }
            char[] chars = chrs.ToCharArray();
            return str.TrimEnd(chars);
        }

        /// <function>StrUtil.TrimRight</function>
        /// <summary>trim right spaces.</summary>
        /// <param name="str">string.</param>
        /// <param name="blnIncludeFullSpace">
        ///             true   ,include full case spaces.
        ///             false  ,do not include full case spaces.(default)
        /// </param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string TrimRight(string str, bool blnIncludeFullSpace)
        {
            if (str == null) { return ""; }
            if (blnIncludeFullSpace) { return str.TrimEnd(HalfCaseSpace, FullCaseSpace); }
            return str.TrimEnd(HalfCaseSpace);
        }

        public static string TrimRight(string str)
        {
            return TrimRight(str, false);
        }

        /// <function>StrUtil.TrimChar</function>
        /// <summary>trim out left and right charater</summary>
        /// <param name="str">string</param>
        /// <param name="chrs">trimed charater</param>
        /// <returns>string after trimed</returns>
        /// <remarks></remarks>
        public static string TrimChar(string str, string chrs)
        {
            if (chrs == null || chrs.Length == 0 || str == null || str.Length == 0) { return str; }
            char[] chars = chrs.ToCharArray();
            return str.Trim(chars);
        }

        /// <function>StrUtil.Trim</function>
        /// <summary>trim left and right spaces.</summary>
        /// <param name="str">string.</param>
        /// <param name="blnIncludeFullSpace">
        ///             true   ,include full case spaces.
        ///             false  ,do not include full case spaces.(default)
        /// </param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string Trim(string str, bool blnIncludeFullSpace)
        {
            if (str == null) { return ""; }
            if (blnIncludeFullSpace) { return str.Trim(HalfCaseSpace, FullCaseSpace); }
            return str.Trim(HalfCaseSpace);
        }

        public static string Trim(string str)
        {
            return Trim(str, true);
        }

        public static string ToUpper(string str)
        {
            if (str == null) { return ""; }
            return str.ToUpper();
        }

        public static string ToLower(string str)
        {
            if (str == null) { return ""; }
            return str.ToLower();
        }

        /// <function>StrUtil.GetStrByteLen</function>
        /// <summary>get byte length of a string.</summary>
        /// <param name="str">tested string</param>
        /// <returns>byte number.</returns>
        /// <remarks></remarks>
        public static int GetStrByteLen(string str)
        {
            return System.Text.Encoding.Default.GetByteCount(str);
        }

        /// <function>StrUtil.PadLeft</function>
        /// <summary>pad left with char to a length</summary>
        /// <param name="strOri">string will be padded</param>
        /// <param name="strPad">padding char</param>
        /// <param name="intTotalLen">total length of the string after padding.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string PadLeft(string strOri, char strPad, int intTotalLen)
        {
            if (strOri == null) { strOri = ""; }
            return strOri.PadLeft(intTotalLen, strPad);
        }

        /// <function>StrUtil.PadRight</function>
        /// <summary>pad right with char to a length</summary>
        /// <param name="strOri">string will be padded</param>
        /// <param name="strPad">padding char</param>
        /// <param name="intTotalLen">total length of the string after padding.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string PadRight(string strOri, char strPad, int intTotalLen)
        {
            if (strOri == null) { strOri = ""; }
            return strOri.PadRight(intTotalLen, strPad);
        }

        /// <function>StrUtil.Replace</function>
        /// <summary>replace string</summary>
        /// <param name="strTarget"></param>
        /// <param name="strOld"></param>
        /// <param name="strNew"></param>
        /// <returns>string after replaced</returns>
        /// <remarks></remarks>
        public static string Replace(string strTarget, string strOld, string strNew)
        {
            if (strTarget == null) { return ""; }
            return strTarget.Replace(strOld, strNew);
        }

        /// <function>StrUtil.IsEmptyStr</function>
        /// <summary>test if a value is a empty string</summary>
        /// <param name="str">tested value</param>
        /// <param name="intIgnoreSpacePattern">
        ///             0,do not ignore space(defalut).
        ///             1,ignore half case spaces.
        ///             2,ignore full/half case spaces.
        /// </param>
        /// <returns>true-empty, false-not empty.</returns>
        /// <remarks></remarks>
        public static bool IsEmptyStr(string str, int intIgnoreSpacePattern)
        {
            if (str == null || str.Length == 0) { return true; }
            switch (intIgnoreSpacePattern)
            {
                case 1:
                    return Trim(str, false).Length == 0 ? true : false;
                case 2:
                    return Trim(str, true).Length == 0 ? true : false;
                default:
                    return false;
            }
        }

        public static bool IsEmptyStr(string str)
        {
            return IsEmptyStr(str, 2);
        }

        /// <summary>
        /// 正则表达式check
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regExp"></param>
        /// <returns></returns>
        public static bool IsMatchRegex(string str, string regExp)
        {
            Regex reg = new Regex(regExp);
            return reg.IsMatch(str);
        }

        /// <summary>
        /// 大写A-Z
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUpperEn(string str)
        {
            return IsMatchRegex(str, @"^[A-Z]+$");
        }

        /// <summary>
        /// 小写a-z
        /// </summary>
        public static bool IsLowerEn(string str)
        {
            return IsMatchRegex(str, @"^[a-z]+$");
        }

        /// <summary>
        /// A-Z，a-z
        /// </summary>
        public static bool IsUpperLowerEn(string str)
        {
            return IsMatchRegex(str, @"^[A-Za-z]+$");
        }

        /// <summary>
        /// 0-9
        /// </summary>
        public static bool IsNum(string str)
        {
            return IsMatchRegex(str, @"^[0-9]+$");
        }

        /// <summary>
        /// A-Z，0-9
        /// </summary>
        public static bool IsUpperEnNum(string str)
        {
            return IsMatchRegex(str, @"^[A-Z0-9]+$");
        }

        /// <summary>
        /// a-z，0-9
        /// </summary>
        public static bool IsLowerEnNum(string str)
        {
            return IsMatchRegex(str, @"^[a-z0-9]+$");
        }

        /// <summary>
        /// A-Z，a-z，0-9
        /// </summary>
        public static bool IsEnNum(string str)
        {
            return IsMatchRegex(str, @"^[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 所有半角字符
        /// </summary>
        public static bool IsHalfCase(string str)
        {
            if (GetStrByteLen(str) == str.Length)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Url地址
        /// </summary>
        public static bool IsUrl(string str)
        {
            return IsMatchRegex(str, @"^http:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\""\""])*$");
        }

        /// <summary>
        /// Email地址
        /// </summary>
        public static bool IsEmail(string str)
        {
            return IsMatchRegex(str, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HalfToFull(string input)
        {
            if (input == null) return "";

            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127) c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FullToHalf(string input)
        {
            if (input == null) return "";

            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 字符串取前几位
        /// </summary>
        /// <param name="val"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(string val, int len)
        {
            if (val == null || val.Length <= len)
            {
                return val;
            }
            return val.Substring(0, len);
        }

        /// <summary>
        /// Encodes non-US-ASCII characters in a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// ConvToStr
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvToStr(System.Object obj)
        {
            string str = "";
            try
            {
                str = Convert.ToString(obj);
            }
            catch
            {
            }
            return str;
        }

        /// <summary>
        /// Determines if the character needs to be encoded.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";

            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;

            return true;
        }

        /// <summary>
        /// Encodes a non-US-ASCII character.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Get string of an object
        /// if object is null, return "", else return o.ToString()
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetStr(object o)
        {
            if (o == null) { return ""; }
            return o.ToString();
        }

        /// <summary>
        /// test if string is in array
        /// </summary>
        /// <param name="s"></param>
        /// <param name="arry"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool IsStrInArry(string s, string[] arry, bool ignoreCase)
        {
            if (GetStrIdxInArry(s, arry, ignoreCase) < 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// get index of string in string array
        /// </summary>
        /// <param name="s"></param>
        /// <param name="arry"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static int GetStrIdxInArry(string s, string[] arry, bool ignoreCase)
        {
            if (arry == null || arry.Length == 0)
            {
                return -1;
            }

            for (int i = 0; i < arry.Length; i++)
            {
                if (string.Compare(s, arry[i], ignoreCase) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// String 转换为指定数据类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object CustomedConvert(Type type, string input)
        {
            object result = DBNull.Value;
            try
            {
                switch (type.ToString())
                {
                    case "System.Boolean":
                        if ("1".Equals(input) || "0".Equals(input))
                        {
                            if ("1".Equals(input))
                            {
                                input = "True";
                            }
                            else
                            {
                                input = "False";
                            }
                        }
                        break;
                    default:
                        break;
                }

                result = System.ComponentModel.TypeDescriptor.GetConverter(type).ConvertFrom(input);
            }
            catch
            {
            }
            finally
            {
            }
            return result;
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format"></param>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static object StrFormat(String format, Object objValue)
        {
            try
            {
                return string.Format("{0:" + format + "}", objValue);
            }
            catch
            {
                return objValue;
            }
        }

        /// <summary>
        /// 将以逗号分隔的字符串转换成字符串数组
        /// </summary>
        /// <param name="ValStr">原字符串</param>
        /// <returns>string[]</returns>
        public static string[] StrList(string ValStr)
        {
            int i = 0;
            string TempStr = ValStr;
            string[] returnStr = new string[ValStr.Length + 1 - TempStr.Replace(",", "").Length];
            ValStr = ValStr + ",";
            while (ValStr.IndexOf(',') > 0)
            {
                returnStr[i] = ValStr.Substring(0, ValStr.IndexOf(','));
                ValStr = ValStr.Substring(ValStr.IndexOf(',') + 1, ValStr.Length - ValStr.IndexOf(',') - 1);
                i++;
            }
            return returnStr;
        }

        /// <summary>
        /// 半角单引号（Single quotation mark）改全角
        /// </summary>
        /// <param name="ValStr"></param>
        /// <returns></returns>
        public static string StrChangeSQM(string ValStr)
        {
            string returnStr = "";

            if (ValStr == null)
            {
            }
            else
            {
                returnStr = ValStr.Replace("'", "’");
            }
            return returnStr;
        }
    }
}