using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utility
{
    public class Log
    {
        /// <summary>
        /// 未经处理的错误日志  日期_unhandle.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void UnHandleException(string str)
        {
            string path = System.Environment.CurrentDirectory + "\\Err";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string time = DateTime.Now.ToString("yyyyMMdd");
            using (StreamWriter sw = new StreamWriter(path + "\\" + time + "_unhandle.log", true, Encoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss：")+str);
            }
        }
        /// <summary>
        /// 未经处理的错误日志  日期_database.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void DataBaseException(string str)
        {
            string path = System.Environment.CurrentDirectory + "\\Err";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string time = DateTime.Now.ToString("yyyyMMdd");
            using (StreamWriter sw = new StreamWriter(path + "\\" + time + "_database.log", true, Encoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss：") + str);
            }
        }
        /// <summary>
        /// 清除Err目录下的访问日期小于当前10天的日志文件
        /// </summary>
        public static void CleanLogs()
        {
            string path = System.Environment.CurrentDirectory + "\\Err";
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if ((DateTime.Now - fi.LastAccessTime).Days > 10)
                {
                    fi.Delete();
                }
            }
        }
    }
}
