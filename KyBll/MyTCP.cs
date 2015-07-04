using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using KyModel;
using System.Net.Sockets;
using System.Threading;
using System.Management;
namespace KyBll
{
    public class MyTCP
    {
        private static int BufferSize = 8192;
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        public class StateObject
        {
            public Socket workSocket = null;
            public byte[] buffer = new byte[BufferSize];
            public int bytesRead = 0;
        }
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
                if (b < 32 || b > 126)
                    ascii = "_";

                tmp2 += ascii + " ";
                count++;
                if (count % 20 == 0)
                {
                    tmp1 = tmp1.PadRight(60, ' ');
                    all += tmp1 + " : " + tmp2 + Environment.NewLine;
                    tmp1 = tmp2 = "";
                }

            }
            if (tmp1 != "" || tmp2 != "")
            {
                tmp1 = tmp1.PadRight(60, ' ');
                all += tmp1 + " : " + tmp2;
            }
            return all;
        }
        /// <summary>
        /// 命令格式化
        /// </summary>
        /// <param name="TCPMessage"></param>
        /// <returns></returns>
        public static string TCPMessageFormat(TCPMessage TCPMessage,bool Command)
        {
            string message = "";
            string CommandFormat = "";
            if (TCPMessage.Command != null)
            {
                CommandFormat = " 命令长度 " + TCPMessage.Command.Length ;
                if (Command)
                    CommandFormat += Environment.NewLine+MyTCP.ByteToStringX2(TCPMessage.Command);
            }
            if (TCPMessage.MessageType == TCPMessageType.NewConnection)
            {
                message += " 新连接 " + TCPMessage.IpAndPort;
            }
            if (TCPMessage.MessageType == TCPMessageType.NoMachineIp)
            {
                message += TCPMessage.IpAndPort + " IP地址未知,请确认已添加IP地址，再重启客户端或等待5分钟自动更新！";
            }
            if (TCPMessage.MessageType == TCPMessageType.ExistConnection)
            {
                message += TCPMessage.IpAndPort + " 连接已存在！";
            }
            if (TCPMessage.MessageType == TCPMessageType.UnknownCommand)
            {
                message += TCPMessage.IpAndPort + " 未知命令" + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_SIMPLE)
            {
                message += TCPMessage.IpAndPort + ", 传输数据请求命令   " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_UP)
            {
                int SignLength = 0;
                int MsgLength = 0;
                if (TCPMessage.Command.Length > 64)
                {
                    SignLength = BitConverter.ToInt16(TCPMessage.Command, 62);
                    MsgLength = BitConverter.ToInt32(TCPMessage.Command, 4);
                }
                message += TCPMessage.IpAndPort + ", 纸币信息发送命令 数据包长度 " + MsgLength + "  纸币张数 " + SignLength + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_CLOSE)
            {
                message += TCPMessage.IpAndPort + ", 传输数据结束命令 " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_TIME)
            {
                message += TCPMessage.IpAndPort + ",时间同步命令 " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_CONTINUE)
            {
                message += TCPMessage.IpAndPort + ",心跳命令  " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.FILE_TOO_BIG)
            {
                int MsgLength = 0;
                if (TCPMessage.Command.Length > 64)
                {
                    MsgLength = BitConverter.ToInt32(TCPMessage.Command, 4);
                }
                message += TCPMessage.IpAndPort + ",文件大小" + (MsgLength - 86) + "字节,文件大小超过5M,将关闭连接！ " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.Reach_Max_Connection)
            {
                message += "达到最大连接数30的限制！";
            }
            if (TCPMessage.MessageType == TCPMessageType.Reach_Max_File)
            {
                message += TCPMessage.IpAndPort + ",一次请求文件数超过30个，将关闭连接！";
            }
            if (TCPMessage.MessageType == TCPMessageType.Exception)
            {
                message += TCPMessage.IpAndPort +Environment.NewLine+TCPMessage.Message;
            }
            if (TCPMessage.MessageType == TCPMessageType.Out_Of_Date)
            {
                message += TCPMessage.IpAndPort + " " + TCPMessage.Message;
            }
            if (TCPMessage.MessageType == TCPMessageType.Thread_Close)
            {
                message += TCPMessage.IpAndPort + " 关闭线程!";
            }
            if (TCPMessage.MessageType == TCPMessageType.Common_Message)
            {
                message += TCPMessage.Message;
            }
            message += Environment.NewLine;
            return message;
        }
        /// <summary>
        /// 获取本机IP且过滤非真实网卡
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIp()
        {
             List<string> listIP = new List<string>();
            ManagementClass mcNetworkAdapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc_NetworkAdapterConfig = mcNetworkAdapterConfig.GetInstances();
            foreach (ManagementObject mo in moc_NetworkAdapterConfig)
            {
                string mServiceName = mo["ServiceName"] as string;

                //过滤非真实的网卡
                if (!(bool)mo["IPEnabled"])
                { continue; }
                if (mServiceName.ToLower().Contains("vmnetadapter")
                 || mServiceName.ToLower().Contains("ppoe")
                 || mServiceName.ToLower().Contains("bthpan")
                 || mServiceName.ToLower().Contains("tapvpn")
                 || mServiceName.ToLower().Contains("ndisip")
                 || mServiceName.ToLower().Contains("sinforvnic"))
                { continue; }

                string[] mIPAddress = mo["IPAddress"] as string[];
                if (mIPAddress != null)
                {

                    foreach (string ip in mIPAddress)
                    {
                        if (ip != "0.0.0.0")
                        {
                            listIP.Add(ip);
                        }
                    }
                }
                mo.Dispose();
            }
            if (listIP.Count > 0)
                return listIP[0];
            else
                return "";
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
        public void OnAmountCmd(CmdEventArgs e)
        {
            try
            {
                OnCmd(this, e);
            }
            catch (Exception ex)
            {
                MyLog.UnHandleException("记录Amount日志出错", ex);
            }
        }
        //日志参数定义
        public class LogEventArgs : EventArgs
        {
            public LogType Type;
            public string Message;
            public LogEventArgs(LogType type, string message)
            {
                Type = type;
                Message = DateTime.Now.ToString("[ yyyy-MM-dd HH:mm:ss.fff ] ") + message + Environment.NewLine;
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
                string message = MyTCP.TCPMessageFormat(TCPMessage,false);
                OnLogEvent(this, new LogEventArgs(LogType.Command, message));
                message = MyTCP.TCPMessageFormat(TCPMessage, true);
                MyLog.CommandLog(message);
            }
            catch (Exception e)
            {
                MyLog.UnHandleException("记录command日志出错", e);
            }
        }
        public void OnBussninessLog(string Message)
        {
            try
            {
                OnLogEvent(this, new LogEventArgs(LogType.Bussiness, Message));
                MyLog.BussinessLog(Message);
            }
            catch (Exception e)
            {
                MyLog.UnHandleException("记录bussiness日志出错", e);
            }
        }
        public void OnFSNImportLog(string Message, Exception ex = null)
        {
            try
            {
                if (ex != null)
                    Message += MyLog.GetExceptionMsg(ex, "");
                OnLogEvent(this, new LogEventArgs(LogType.FSNImport, Message));
                MyLog.ImportLog(Message);
            }
            catch (Exception e)
            {
                MyLog.UnHandleException("记录fsn日志出错", e);
            }
        }

        #endregion

        /// <summary>
        /// 异步接收命令
        /// </summary>
        /// <param name="user"></param>
        /// <param name="length"></param>
        /// <param name="ReceiveBytes"></param>
        /// <returns></returns>
        public static int AsyncReceiveFromClient(Socket socket, int length, out byte[] receiveBuffers)
        {
            receiveDone.Reset();
            //接收数据大小不超过BufferSize
            int len = BufferSize;
            if (length < BufferSize)
                len = length;
            StateObject staObject = new StateObject() { workSocket = socket };
            IAsyncResult ar = socket.BeginReceive(staObject.buffer, 0, len, 0, new AsyncCallback(ReadCallback), staObject);
            receiveDone.WaitOne();
            receiveBuffers = staObject.buffer;
            return staObject.bytesRead;
        }
        public static void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            try
            {
                int bytesRead = state.workSocket.EndReceive(ar);
                byte[] realBuffers = new byte[bytesRead];
                Array.Copy(state.buffer, 0, realBuffers, 0, bytesRead);
                state.buffer = realBuffers;
                state.bytesRead = bytesRead;
                
            }
            catch (Exception e)
            {
                MyLog.ConnectionException("异步接收命令异常", e);
                state.bytesRead = -1;
                state.buffer = null;
            }
            receiveDone.Set();
        }
        /// <summary>
        /// 异步发送命令
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        public static void AsyncSendToClient(Socket socket, byte[] message)
        {
            sendDone.Reset();
            socket.BeginSend(message, 0, message.Length, 0, new AsyncCallback(SendCallback), socket);
            sendDone.WaitOne();
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                
            }
            catch (Exception e)
            {
                MyLog.ConnectionException("异步发送命令异常", e);
            }
            sendDone.Set();
        }




    }

}
