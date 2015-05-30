using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyTcpServer;
using KyModel;
using KyBll;
using MaterialSkin;
namespace NodeServerAndManager.BaseWinform
{
    public partial class LogForm : MaterialSkin.Controls.MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public string LogText = "";
        public LogForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
        }
        public void myTcpServer_LogEvent(object sender, TcpServer.LogEventArgs e)
        {
            try
            {
                if (LogRichTextBox.Lines.Length > 100)
                {
                    LogRichTextBox.Clear();

                }
                TCPMessage TCPMessage = e.TCPMessage;
                string message=TCP.TCPMessageFormat(TCPMessage);
                LogRichTextBox.AppendText(message);
            }
            catch (Exception ex)
            {
                Log.ConnectionException(ex, "日志输出异常!");
            }
        }
    }
}
