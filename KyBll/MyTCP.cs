using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using KyModel;
using System.Net.Sockets;
using System.Threading;
namespace KyBll
{
    public class MyTCP
    {
        /// <summary>
        /// 将命令byte数组格式化十六进制和ASCII
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToStringX2(byte[] bytes)
        {
            string all = "";
            string tmp1 = "";
            string tmp2 = "";
            int count = 0;
            foreach (byte b in bytes)
            {
                tmp1 += b.ToString("X2") + " ";
                var ascii = System.Text.Encoding.ASCII.GetString(new byte[] { b });
                //var regex = new Regex(@"([^a-zA-z0-9\s])");
                //ascii = regex.Replace(ascii, ".")+" ";
                if (b < 32 || b > 126)
                    ascii = "_";

                tmp2 += ascii + " ";
                count++;
                if (count % 20 == 0)
                {
                    tmp1 = tmp1.PadRight(60, ' ');
                    all += tmp1 + " : " + tmp2.Replace('\n', ' ').Replace('\f', ' ') + Environment.NewLine;
                    tmp1 = tmp2 = "";
                }

            }
            if (tmp1 != "" || tmp2 != "")
            {
                tmp1 = tmp1.PadRight(60, ' ');
                all += tmp1 + " : " + tmp2.Replace('\n', ' ').Replace('\f', ' ');
            }
            return all;
        }
        /// <summary>
        /// 命令格式化
        /// </summary>
        /// <param name="TCPMessage"></param>
        /// <returns></returns>
        public static string TCPMessageFormat(TCPMessage TCPMessage)
        {
            string message = "";
            string CommandFormat = "";
            if (TCPMessage.Command != null)
            {
                CommandFormat = " CMD Length " + TCPMessage.Command.Length + Environment.NewLine + MyTCP.ByteToStringX2(TCPMessage.Command);
            }
            if (TCPMessage.MessageType == TCPMessageType.NewConnection)
            {
                message += " new connection " + TCPMessage.IpAndPort;
            }
            if (TCPMessage.MessageType == TCPMessageType.NoMachineIp)
            {
                message += TCPMessage.IpAndPort + "Machine Ip not exists ";
            }
            if (TCPMessage.MessageType == TCPMessageType.ExistConnection)
            {
                message += TCPMessage.IpAndPort + "Connection exists ";
            }
            if (TCPMessage.MessageType == TCPMessageType.UnknownCommand)
            {
                message += TCPMessage.IpAndPort + "Unknown Command " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_SIMPLE)
            {
                message += TCPMessage.IpAndPort + ", Request upload Command   " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_UP)
            {
                int SignLength = 0;
                if(TCPMessage.Command.Length>64)
                    SignLength=BitConverter.ToInt16(TCPMessage.Command, 62);
                message += TCPMessage.IpAndPort + ", Send Data Command  Sign Length "+SignLength + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_CLOSE)
            {
                message += TCPMessage.IpAndPort + ", Close connection Command  " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_TIME)
            {
                message += TCPMessage.IpAndPort + ",Time Synchronization Command  " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_CONTINUE)
            {
                message += TCPMessage.IpAndPort + ",Heart Beat Command  " + CommandFormat;
            }
            message += Environment.NewLine;
            return message;
        }

        #region 委托、事件定义
        //委托
        public delegate void CmdEventHandler(Object sender, CmdEventArgs e);
        //事件
        public event EventHandler<CmdEventArgs> CmdEvent;
        protected virtual void OnCmd(Object sender, CmdEventArgs e)
        {
            if (CmdEvent != null)    // 如果有对象注册
            {
                CmdEvent(this.CmdEvent, e);  // 调用所有注册对象的方法
            }
        }
        //时间参数定义
        public class CmdEventArgs : EventArgs
        {
            public readonly string IP;
            public readonly KYDataLayer1.Amount Amount;
            public CmdEventArgs(string ip, KYDataLayer1.Amount amount)
            {
                IP = ip;
                Amount = amount;
            }

        }
        public void OnAmountCmd( CmdEventArgs e)
        {
            try
            {
                OnCmd(this, e);
            }
            catch (Exception ex)
            {
                Log.UnHandleException("记录Amount日志出错", ex);
            }
        }
        //日志参数定义
        public class LogEventArgs : EventArgs
        {
            public LogType Type;
            public string Message;
            public LogEventArgs(LogType type,string message)
            {
                Type = type;
                Message = DateTime.Now.ToString("[ yyyy-MM-dd HH:mm:ss ] ")+message+Environment.NewLine;
            }

        }
        public delegate void LogEventHandler(Object sender, LogEventArgs e);
        //事件
        public event EventHandler<LogEventArgs> LogEvent;
        public void ClearLogEvent()
        {
            LogEvent = null;
        }
        protected virtual void OnLogEvent(Object sender, LogEventArgs e)
        {
            try
            {
                if (LogEvent != null)    // 如果有对象注册
                {
                    LogEvent(this.LogEvent, e);  // 调用所有注册对象的方法
                }

            }
            finally
            {

            }

        }
        public void OnCommandLog(TCPMessage TCPMessage)
        {
            try
            {
                string message = MyTCP.TCPMessageFormat(TCPMessage);
                OnLogEvent(this, new LogEventArgs(LogType.Command,message));
                Log.CommandLog(message);
            }
            catch (Exception e)
            {
                Log.UnHandleException("记录command日志出错", e);
            }
        }
        public void OnBussninessLog(string Message)
        {
            try
            {
                OnLogEvent(this, new LogEventArgs(LogType.Bussiness, Message));
                Log.BussinessLog(Message);
            }
            catch (Exception e)
            {
                Log.UnHandleException("记录bussiness日志出错", e);
            }
        }
        public void OnFSNImportLog(string Message, Exception ex = null)
        {
            try
            {
                if(ex!=null)
                    Message += Log.GetExceptionMsg(ex, "");
                OnLogEvent(this, new LogEventArgs(LogType.FSNImport, Message));
                Log.ImportLog(Message);  
            }
            catch (Exception e)
            {
                Log.UnHandleException("记录fsn日志出错", e);
            }
        }
        
        #endregion


        /// <summary>
        /// 接收命令
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ReceiveBytes"></param>
        /// <param name="length"></param>
        public static void ReceiveMessage(Socket user, out byte[] ReceiveBytes, int length)
        {
            try
            {
                byte[] readbuf = new byte[length];
                int bytesRead = user.Receive(readbuf, 0, length, SocketFlags.None);
                ReceiveBytes = new byte[bytesRead];
                Array.Copy(readbuf, ReceiveBytes, bytesRead);
            }
            catch
            {
                ReceiveBytes = null;
            }
        }
        /// <summary>
        /// 异步接收命令
        /// </summary>
        /// <param name="user"></param>
        /// <param name="length"></param>
        /// <param name="ReceiveBytes"></param>
        /// <returns></returns>
        public static int AsyncReceiveFromClient(Socket user, int length, out byte[] ReceiveBytes)
        {
            ReceiveMessageDelegate d = new ReceiveMessageDelegate(ReceiveMessage);
            IAsyncResult result = d.BeginInvoke(user, out ReceiveBytes, length, null, null);
            //使用轮询方式来判断异步操作是否完成
            while (result.IsCompleted == false)
            {
                Thread.Sleep(250);
            }
            //获取Begin方法的返回值和所有输入/输出参数
            d.EndInvoke(out ReceiveBytes, result);
            if (ReceiveBytes != null)
                return ReceiveBytes.Length;
            else
                return 0;
        }
        delegate void ReceiveMessageDelegate(Socket user, out byte[] ReceiveBytes, int length);

        /// <summary>
        /// 异步发送命令
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        public static void AsyncSendToClient(Socket user, byte[] message)
        {
            SendToClientDelegate d = new SendToClientDelegate(SendToClient);
            IAsyncResult result = d.BeginInvoke(user, message, null, null);
            while (result.IsCompleted == false)
            {
                Thread.Sleep(100);
            }
            d.EndInvoke(result);
        }
        private delegate void SendToClientDelegate(Socket user, byte[] message);
       /// <summary>
       /// 发送命令
       /// </summary>
       /// <param name="user"></param>
       /// <param name="message"></param>
        public static void SendToClient(Socket user, byte[] message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                user.Send(message, 0, message.Length, SocketFlags.None);
            }
            catch (Exception e)
            {
                Log.ConnectionException("发送命令失败!", e);
            }
        }
    }
   
}
