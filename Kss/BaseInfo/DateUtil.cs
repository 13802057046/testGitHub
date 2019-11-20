using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BaseInfo
{
    public static class DateUtil
    {
        /// <summary>
        /// 获取日期时间
        /// 格式说明:
        /// yy yyyy : 年
        /// M MM    : 月
        /// d dd    : 日
        /// hh      : 12小时制
        /// HH      : 24小时制
        /// mm      : 分
        /// ss      : 秒
        /// fff     : 千分之一秒
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format"></param>
        /// <param name="rst"></param>
        /// <returns></returns>
        public static bool TryToDate(string str, string format, out DateTime rst)
        {
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.InvariantCulture;
            return DateTime.TryParseExact(str, format, cul, System.Globalization.DateTimeStyles.None, out rst);
        }

        /// <summary>
        /// 日期转为字符串
        /// 格式说明:
        /// yy yyyy : 年
        /// M MM    : 月
        /// d dd    : 日
        /// hh      : 12小时制
        /// HH      : 24小时制
        /// mm      : 分
        /// ss      : 秒
        /// fff     : 千分之一秒
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateToStr(DateTime dt, string format)
        {
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.InvariantCulture;
            return dt.ToString(format, cul);
        }

        /// <summary>
        /// 日期转为字符串
        /// 格式说明:
        /// yy yyyy : 年
        /// M MM    : 月
        /// d dd    : 日
        /// hh      : 12小时制
        /// HH      : 24小时制
        /// mm      : 分
        /// ss      : 秒
        /// fff     : 千分之一秒
        /// </summary>
        /// <param name="dateTimeStr"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime StrToDate(String dateTimeStr, string format)
        {
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.InvariantCulture;
            return DateTime.ParseExact(dateTimeStr, format, cul);
        }

        public static bool IsTimeOnly(string str, string format)
        {
            Regex reg = new Regex(@"^\d{1,2}:\d{1,2}:\d{1:2}$");
            if (!reg.IsMatch(str))
            {
                return false;
            }

            string[] hms = str.Split(':');

            int h = int.Parse(hms[0]);
            if (h > 23)
            {
                return false;
            }
            int m = int.Parse(hms[1]);
            if (m > 59)
            {
                return false;
            }
            int s = int.Parse(hms[2]);
            if (s > 59)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 一天的最开始
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime BeginOfDate(DateTime dt)
        {
            return dt.Date;
        }

        /// <summary>
        /// 一天的最后一秒
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime EndOfDate(DateTime dt)
        {
            return dt.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 该月的最开始
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime BeginOfMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        /// <summary>
        /// 该月的最后一秒
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime EndOfMonth(DateTime dt)
        {
            return BeginOfMonth(dt).AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 判断是否是周末
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsWeekEnd(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Saturday
                || dt.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否是正确的日期格式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDate(object obj)
        {
            try
            {
                DateTime.Parse(StrUtil.GetStr(obj));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断 两个日期哪个大: 如果 time2 >= time1 返回true;否则返回false
        /// </summary>
        /// <param name="input"></param>
        /// <returns>如果 time2 >= time1 返回true;否则返回false</returns>
        public static bool CheckDateBig(string time1, string time2)
        {
            try
            {
                if (DateTime.Compare(DateTime.Parse(time1), DateTime.Parse(time2)) <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断 两个日期哪个大: 
        /// </summary>
        /// <param name="input"></param>
        /// <returns> time2 > time1 的 天数 </returns>
        public static int DatesDiffer(string time1, string time2)
        {
            try
            {
                DateTime d1 = StrToDate(time1, "yyyy-MM-dd");
                DateTime d2 = StrToDate(time2, "yyyy-MM-dd");
                TimeSpan d3 = d1.Subtract(d2);

                return d3.Days;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 日期转为字符串
        /// </summary>
        /// <param name="dateTimeStr"></param>
        /// <returns></returns>
        public static DateTime ToDate(String dateTimeStr)
        {
            try
            {
                System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.InvariantCulture;
                return DateTime.Parse(dateTimeStr);
            }
            catch (Exception e)
            {
               return new DateTime();
            }
        }

        //timeSpan转换为DateTime
        public static DateTime? LongToDate(long? span)
        {
            if (span == null)
                return null;
            else
            {
                DateTime time = DateTime.MinValue;
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
                time = startTime.AddMilliseconds(span ?? 0);
                return time;
            }

        }
    }
}