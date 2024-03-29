﻿using System;
using System.Windows.Forms;
using KyBase;
using KyBll;
using KyModel;
using System.Threading;
namespace KangYiCollection.BaseWinform
{
    public partial class LogForm : MaterialSkin.Controls.MaterialForm
    {
        public string LogText = "";
        public LogForm()
        {
            InitializeComponent();
            foreach (TabPage tabPage in materialTabControl1.TabPages)
            {
                tabPage.Text = clsMsg.getMsg(tabPage.Name);
            }

        }
        public void myTcpServer_LogEvent(object sender, MyTCP.LogEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Program.CurrentLanguage);
            AddLog(e);

        }
        private delegate void AddStatusDelegate(MyTCP.LogEventArgs e);
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="e"></param>
        private void AddLog(MyTCP.LogEventArgs e)
        {
            if (this.InvokeRequired)
            {
                AddStatusDelegate d = new AddStatusDelegate(AddLog);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                try
                {
                    //命令日志
                    if (CommandLogRichTextBox.Lines.Length > 200)
                    {
                        CommandLogRichTextBox.Clear();
                    }
                    if (e.Type == LogType.Command)
                    {
                        CommandLogRichTextBox.AppendText(e.Message);
                        CommandLogRichTextBox.Focus();
                        CommandLogRichTextBox.Select(CommandLogRichTextBox.TextLength, 0);
                        CommandLogRichTextBox.ScrollToCaret();
                    }
                    //业务日志
                    if (BussinessLogRichTextBox.Lines.Length > 200)
                    {
                        BussinessLogRichTextBox.Clear();
                    }
                    if (e.Type == LogType.Bussiness)
                    {
                        BussinessLogRichTextBox.AppendText(e.Message);
                        BussinessLogRichTextBox.Focus();
                        BussinessLogRichTextBox.Select(BussinessLogRichTextBox.TextLength, 0);
                        BussinessLogRichTextBox.ScrollToCaret();
                    }
                    //FSN导入日志
                    if (FSNImortLogRichTextBox.Lines.Length > 200)
                    {
                        FSNImortLogRichTextBox.Clear();
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
                    MyLog.ConnectionException(clsMsg.getMsg("log_26"), ex);
                }
            }
        }

    }
}
