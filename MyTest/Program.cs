using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace MyTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string sign = string.Format("{0,-12}\r\n", "1234567890");
            byte[] sss=Encoding.ASCII.GetBytes(sign);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TCPClient());
        }
    }
}
