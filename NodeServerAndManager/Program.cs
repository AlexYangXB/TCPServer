﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NodeServerAndManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int SW_SHOWMAXIMIZED = 3;
        //列举所有窗口
        [DllImport("user32.dll", EntryPoint = "EnumWindows")]
        public static extern int EnumWindows(EnumWindowsCallback callback, int lParam);
        public delegate bool EnumWindowsCallback(int hWnd, int lParam);//EnumWindows的回调函数

        //判断窗口句柄是否有效
        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        public static extern bool IsWindow(int hWnd);

        //判断窗口是否可见
        [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
        public static extern bool IsWindowVisible(int hwnd);
      
        //static System.Threading.Timer gcScheduler = new System.Threading.Timer(state => GC.Collect(), null, 0, 30000);
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //调用软件升级程序
            System.Diagnostics.Process p = new Process();
            p.StartInfo.FileName = Application.StartupPath + "\\KangYiUpdate.exe";
            p.Start();
            p.WaitForExit();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

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
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new NodeManager());
                }
                else
                {
                    //int windowHandler = FindWindow(null, ky_database.SysTitle + "    " + ky_database.Version);
                    //if (!IsWindowVisible(windowHandler))
                    //{
                    //    if (windowHandler > 0)
                    //    {
                    //        ShowWindowAsync((IntPtr)windowHandler, SW_SHOWMAXIMIZED);
                    //        SetForegroundWindow((IntPtr)windowHandler);
                    //    }
                    //}
                    //else
                    //{
                    //    if (windowHandler > 0)
                    //    {
                    //        ShowWindowAsync((IntPtr)windowHandler, SW_SHOWMAXIMIZED);
                    //        SetForegroundWindow((IntPtr)windowHandler);
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
                
                throw;
            }


            
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(null, "未知错误: " + ((Exception)e.ExceptionObject).ToString(), "错误:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            //DataTypeSDK.FileOperation.WriteLog(Application.StartupPath + "\\Err", str);
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(null, "未知错误: " + e.Exception, "错误:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //string str = GetExceptionMsg(e.Exception, e.ToString());
            //DataTypeSDK.FileOperation.WriteLog(Application.StartupPath + "\\Err", str);
        }
    }
}