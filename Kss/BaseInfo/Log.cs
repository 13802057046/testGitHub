using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BaseInfo
{
    public class Log
    {
        /// <summary>
        /// 正常
        /// </summary>
        public const int LogTypeInfo = 1;

        /// <summary>
        /// 错误
        /// </summary>
        public const int LogTypeErr = 0;

        public static void WriteLog(string LogStr, int type)
        {
            string strType = "Info";

            StreamWriter sw = null;
            try
            {
                // 读取log文件的路径 
                string log_dir = G_INI.ReadValue(IniInfo.Log, IniInfo.Log_dir);
                if (!log_dir.EndsWith("\\"))
                {
                    log_dir = log_dir + "\\";
                }
                FileOperte.PathIsExsit(log_dir);
                string file = DateTime.Now.ToString("yyyyMMdd") + ".log";
                string logFile = log_dir + file;
                FileOperte.FileIsExsit(logFile);

                // 类型
                switch (type)
                {
                    case LogTypeInfo:
                        strType = "Info:";
                        break;
                    case LogTypeErr:
                        strType = "Error:";
                        break;
                    default:
                        strType = "Info:";
                        break;
                }
                LogStr = DateTime.Now.ToLocalTime().ToString() + " 【" + strType + " 】" + LogStr;
                sw = new StreamWriter(logFile, true, Encoding.Unicode);
                sw.WriteLine(LogStr);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        public static string GetLogFileName()
        {
            // 读取log文件的路径 
            string log_dir = G_INI.ReadValue(IniInfo.Log, IniInfo.Log_dir);
            if (!log_dir.EndsWith("\\"))
            {
                log_dir = log_dir + "\\";
            }
            FileOperte.PathIsExsit(log_dir);
            string file = DateTime.Now.ToString("yyyyMMdd") + ".log";
            string logFile = log_dir + file;

            return logFile;
        }

        public static void WriteLog_Err(string LogStr, int type, string fileName)
        {
            string strType = "Info";

            StreamWriter sw = null;
            try
            {
                // 读取log文件的路径
                string log_dir = G_INI.ReadValue(IniInfo.Log, IniInfo.Log_dir);
                if (!log_dir.EndsWith("\\"))
                {
                    log_dir = log_dir + "\\";
                }
                FileOperte.PathIsExsit(log_dir);
                string file = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
                string logFile = log_dir + file;
                FileOperte.FileIsExsit(logFile);

                // 类型
                switch (type)
                {
                    case LogTypeInfo:
                        strType = "Info:";
                        break;
                    case LogTypeErr:
                        strType = "Error:";
                        break;
                    default:
                        strType = "Info:";
                        break;
                }
                LogStr = DateTime.Now.ToLocalTime().ToString() + " 【" + strType + " 】" + LogStr;
                sw = new StreamWriter(logFile, true, Encoding.Unicode);
                sw.WriteLine(LogStr);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}