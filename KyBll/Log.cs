using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace KyBll
{
    public class Log
    {
        /// <summary>
        /// 未经处理的错误日志  日期_unhandle.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void UnHandleException(Exception ex,string backStr)
        {
            string str = GetExceptionMsg(ex, backStr);
            string path = System.Environment.CurrentDirectory + "\\Err" + "\\" + DateTime.Now.ToString("yyyyMMdd");
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
        /// 数据库的错误日志  日期_database.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void DataBaseException(Exception ex,string backStr)
        {
            string str = GetExceptionMsg(ex, backStr);
            string path = System.Environment.CurrentDirectory + "\\Err" + "\\" + DateTime.Now.ToString("yyyyMMdd");
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
        /// 连接的错误日志  日期_connect.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void ConnectionException(Exception ex, string backStr)
        {
            string str = GetExceptionMsg(ex, backStr);
            string path = System.Environment.CurrentDirectory + "\\Err" + "\\" + DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string time = DateTime.Now.ToString("yyyyMMdd");
            using (StreamWriter sw = new StreamWriter(path + "\\" + time + "_connect.log", true, Encoding.Default))
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
        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        public static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【呈现时候】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
                sb.AppendLine("【异常办法】：" + ex.TargetSite);
            }
            else
            {
                sb.AppendLine("【未处理惩罚异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
