using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using KyBase;
namespace KyBll
{
    public class MyLog
    {
        /// <summary>
        /// 测试记录日志  日期_test.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void TestLog(string backStr)
        {
            WriteLog("test.log", backStr, null);

        }
        /// <summary>
        /// 未经处理的错误日志  日期_unhandle.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void UnHandleException(string backStr,Exception ex)
        {
            WriteLog("unhandle.log", backStr, ex);
        }
        /// <summary>
        /// 数据库的错误日志  日期_database.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void DataBaseException(string backStr,Exception ex)
        {
            WriteLog("database.log", backStr, ex);
        }
        /// <summary>
        /// 连接的错误日志  日期_connect.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void ConnectionException(string backStr,Exception ex )
        {
            WriteLog("connection.log", backStr, ex);
        }
        /// <summary>
        /// 连接的命令信息  日期_command.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void CommandLog(string backStr, Exception ex=null)
        {
            WriteLog("command.log", backStr, ex);
        }
        /// <summary>
        /// 导入日志  日期_import.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void ImportLog(string backStr="",Exception ex=null)
        {
            WriteLog("import.log", backStr, ex);
        }
        /// <summary>
        /// CRH日志  日期_CRH.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void CRHLog(string backStr = "",Exception ex = null)
        {
            WriteLog("CRH.log", backStr, ex);
          
        }
        /// <summary>
        /// 业务交易日志  日期_bussiness.log
        /// </summary>
        /// <param name="path"></param>
        /// <param name="str"></param>
        public static void BussinessLog(string backStr)
        {
            WriteLog("bussiness.log", backStr, null);
          
        }
        /// <summary>
        /// 清除Log目录下的访问日期小于当前5天的文件夹
        /// </summary>
        public static void CleanLogs()
        {
            int CleanDay = 5;
            string path = Application.StartupPath + "\\Log";
            if (Directory.Exists(path))
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    if ((DateTime.Now - di.CreationTime).Days > CleanDay)
                    {
                        di.Delete(true);
                    }
                }
            }
        }
        /// <summary>
        /// 清除FsnFloder目录下的访问日期小于当前30天的文件夹
        /// </summary>
        public static void CleanFsnFloder()
        {
            int CleanDay = 30;
            string path = Application.StartupPath + "\\FsnFloder";
            if (Directory.Exists(path))
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    if ((DateTime.Now - di.CreationTime).Days > CleanDay)
                    {
                        di.Delete(true);
                    }
                }
            }
        }
        public static void WriteLog(string fileName,string backStr, Exception ex=null)
        {
            string path = Application.StartupPath + "\\Log" + "\\" + DateTime.Now.ToString("yyyyMMdd");
            string str = "";
            if (ex != null)
                str = GetExceptionMsg(ex, backStr);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string time = DateTime.Now.ToString("yyyyMMdd");
            string fullName = path + "\\" + time + "_" + fileName;
            ShareWrite(DateTime.Now.ToString("[ yyyy-MM-dd HH:mm:ss.fff ] ")+ backStr, fullName);
            if (str != "")
                ShareWrite(str, fullName);
            
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
            sb.AppendLine(string.Format("****************************{0}****************************", clsMsg.getMsg("log_10")));
            sb.AppendLine(string.Format("【{0}】：{1}",clsMsg.getMsg("log_11"),DateTime.Now.ToString()));
            if (ex != null)
            {        
                sb.AppendLine(string.Format("【{0}】：{1}",clsMsg.getMsg("log_12"),ex.GetType().Name));
                sb.AppendLine(string.Format("【{0}】：{1}",clsMsg.getMsg("log_13"),ex.Message));
                sb.AppendLine(string.Format("【{0}】：{1}",clsMsg.getMsg("log_14"),ex.StackTrace));
                sb.AppendLine( string.Format("【{0}】：{1}",clsMsg.getMsg("log_15"),ex.TargetSite));
            }
            else
            {
                sb.AppendLine(string.Format("【{0}】：{1}", clsMsg.getMsg("log_16"), backStr));
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
        /// <summary>
        /// 共享写
        /// </summary>
        /// <param name="content"></param>
        /// <param name="file"></param>
        public static void ShareWrite(string content, string file)
        {
            FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            try
            {
                if (fs.CanWrite)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(content+Environment.NewLine);
                    if (buffer.Length > 0)
                    {
                        fs.Write(buffer, 0, buffer.Length);
                        fs.Flush();
                    }
                }
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
        }
 
    }
}
