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
namespace NodeServerAndManager.BaseWinform
{
    public partial class LogForm : Form
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
                string date = DateTime.Now.ToString("[ yyyy-MM-dd HH:mm:ss ] ");
                string message = date;
                if (TCPMessage.MessageType == TCPMessageType.NewConnection)
                {
                    message += " new connection " + TCPMessage.IpAndPort;
                }
                if (TCPMessage.MessageType == TCPMessageType.NoMachineIp)
                {
                    message += TCPMessage.IpAndPort + "Machine Ip not exists ";
                }
                if (TCPMessage.MessageType == TCPMessageType.NET_SIMPLE)
                {
                    message += TCPMessage.IpAndPort + ", Request upload Command  " + Environment.NewLine+TCP.ByteToStringX2(TCPMessage.Command.Take(54).ToArray());
                }
                if (TCPMessage.MessageType == TCPMessageType.NET_UP)
                {
                    message += TCPMessage.IpAndPort + ", Send Data Command  " + Environment.NewLine + TCP.ByteToStringX2(TCPMessage.Command.Take(86).ToArray());
                }
                if (TCPMessage.MessageType == TCPMessageType.NET_CLOSE)
                {
                    message += TCPMessage.IpAndPort + ", Close connection Command  " + Environment.NewLine + TCP.ByteToStringX2(TCPMessage.Command.Take(44).ToArray());
                }
                if (TCPMessage.MessageType == TCPMessageType.NET_TIME)
                {
                    message += TCPMessage.IpAndPort + ",Time Synchronization Command  " + Environment.NewLine + TCP.ByteToStringX2(TCPMessage.Command.Take(42).ToArray());
                }
                message += Environment.NewLine;
                LogRichTextBox.AppendText(message);
            }
            catch (Exception ex)
            {
                Log.ConnectionException(ex, "日志输出异常!");
            }
        }
    }
}
