using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KyBll;
namespace KangYiCollection
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
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
            MyLog.UnHandleException( e.ToString(),e.ExceptionObject as Exception);
            MessageBox.Show(null, MyLog.GetExceptionMsg((Exception)e.ExceptionObject,"未知错误！"), "错误:", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MyLog.UnHandleException( e.ToString(),e.Exception);
            MessageBox.Show(null, MyLog.GetExceptionMsg(e.Exception, "未知错误！"), "错误:", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
       
    }
}
