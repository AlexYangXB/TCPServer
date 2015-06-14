using System;
using KyBll;
using KyModel;
using MyTcpServer;
namespace KangYiCollection.BaseWinform
{
    public partial class LogForm : MaterialSkin.Controls.MaterialForm
    {
        public string LogText = "";
        public LogForm()
        {
            InitializeComponent();
            
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
                string message=MyTCP.TCPMessageFormat(TCPMessage);
                LogRichTextBox.AppendText(message);
            }
            catch (Exception ex)
            {
                Log.ConnectionException(ex, "日志输出异常!");
            }
        }
    }
}
