using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using KyBll;
using KyBase;
using System.Threading;
namespace KangYiCollection
{
    static class Program
    {
        public static string CurrentLanguage = "zh-TW";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AssemblyName assemName = new AssemblyName();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CurrentLanguage);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            if (CurrentLanguage == "zh-CN")
            {
                Process[] updates = Process.GetProcessesByName("KangYiUpdate.exe");
                if (updates.Length == 0)
                {
                    //调用软件升级程序
                    System.Diagnostics.Process p = new Process();
                    p.StartInfo.FileName = Application.StartupPath + "\\KangYiUpdate.exe";
                    p.Start();
                    updates = Process.GetProcessesByName("KangYiUpdate");
                    p.WaitForExit();
                }
            }
            else
            {
                Process[] updates = Process.GetProcessesByName("WeiKeUpdate.exe");
                if (updates.Length == 0)
                {
                    //调用软件升级程序
                    System.Diagnostics.Process p = new Process();
                    p.StartInfo.FileName = Application.StartupPath + "\\WeiKeUpdate.exe";
                    p.Start();
                    updates = Process.GetProcessesByName("WeiKeUpdate");
                    p.WaitForExit();
                }
            }
            try
            {
                //判断该系统是否已打开
                Process[] ps = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

                Boolean IsOpen = false;
                for (int i = 0; i < ps.Length; i++)
                {
                    if (Process.GetCurrentProcess().MainModule.FileName == ps[i].MainModule.FileName
                        && Process.GetCurrentProcess().Id != ps[i].Id)
                    {
                        IsOpen = true;
                        break;
                    }
                }
                if (!IsOpen)//未打开，打开系统
                {
                    //清除日志
                    MyLog.CleanLogs();
                    MyLog.CleanFsnFloder();
                    //加载未导入的图像文件和Sql
                    if (Directory.Exists(KyDataOperation.PictureFileDir))
                    {
                        string[] pictureFiles = Directory.GetFiles(KyDataOperation.PictureFileDir).Take(KyDataOperation.MaxPictureFileCount).ToArray();
                        foreach (var pic in pictureFiles)
                        {
                            KyDataOperation.pictureQueue.Enqueue(pic);
                        }
                    }
                    string sqlFile = KyDataOperation.SqlDir + "\\UnUpload.sql";
                    List<string> sqls = FileOperation.ReadLines(KyDataOperation.SqlDir, sqlFile, KyDataOperation.MaxSqlCount);
                    if (sqls.Count > 0)
                    {
                        foreach (var sql in sqls)
                        {
                            KyDataOperation.sqlQueue.Enqueue(sql);
                        }
                    }
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new NodeManager());
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MyLog.UnHandleException(e.ToString(), e.ExceptionObject as Exception);
            AutoClosingMessageBox.Show(MyLog.GetExceptionMsg((Exception)e.ExceptionObject, clsMsg.getMsg("log_27")), clsMsg.getMsg("msg_Tip"), 10000);
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MyLog.UnHandleException(e.ToString(), e.Exception);
            AutoClosingMessageBox.Show(MyLog.GetExceptionMsg(e.Exception, clsMsg.getMsg("log_27")), clsMsg.getMsg("msg_Tip"), 10000);
        }
        

    }
}
