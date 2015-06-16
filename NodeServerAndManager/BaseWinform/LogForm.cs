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
        public void myTcpServer_LogEvent(object sender, MyTCP.LogEventArgs e)
        {
            try
            {
                if (CommandLogRichTextBox.Lines.Length > 200)
                {
                    CommandLogRichTextBox.Clear();

                }
                if (e.Type==LogType.Command)
                {
                    CommandLogRichTextBox.AppendText(e.Message);
                    CommandLogRichTextBox.Focus();
                    CommandLogRichTextBox.Select(CommandLogRichTextBox.TextLength, 0);
                    CommandLogRichTextBox.ScrollToCaret();
                }
                if (e.Type == LogType.Bussiness)
                {
                    BussinessLogRichTextBox.AppendText(e.Message);
                    BussinessLogRichTextBox.Focus();
                    BussinessLogRichTextBox.Select(BussinessLogRichTextBox.TextLength, 0);
                    BussinessLogRichTextBox.ScrollToCaret();
                }
                if (e.Type == LogType.FSNImport)
                {
                    FSNImortLogRichTextBox.AppendText(e.Message);
                    FSNImortLogRichTextBox.Focus();
                    FSNImortLogRichTextBox.Select(FSNImortLogRichTextBox.TextLength, 0);
                    FSNImortLogRichTextBox.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                Log.ConnectionException("日志输出异常!",ex);
            }
            
        }

    }
}
