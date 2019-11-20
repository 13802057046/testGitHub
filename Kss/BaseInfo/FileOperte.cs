using System;
using System.IO;
using System.Collections.Generic;

namespace BaseInfo
{
    public class FileOperte
    {
        #region 文件操作

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件的完整路径</param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 判断路径是否存在，不存在，创建 
        /// </summary>
        /// <param name="path"></param>
        public static void PathIsExsit(string path)
        {
            if (Directory.Exists(path))//判断是否存在
            {
            }
            else
            {
                Directory.CreateDirectory(path);//创建新路径

            }
        }

        /// <summary>
        /// 判断文件是否存在，不存在，创建 
        /// </summary>
        /// <param name="path"></param>
        public static void FileIsExsit(string file)
        {
            if (File.Exists(file))//判断是否存在
            {
            }
            else
            {
                File.Create(file).Close();
            }
        }

        #endregion
    }
}