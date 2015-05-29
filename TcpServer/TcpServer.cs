using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using KyBll;
using KyModel;
using KyModel.Models;
using System.Linq;
namespace MyTcpServer
{
    public class TcpServer
    {
        #region 委托、事件定义
        //委托
        public delegate void CmdEventHandler(Object sender, CmdEventArgs e);
        //事件
        public event EventHandler<CmdEventArgs> CmdEvent;
        protected virtual void OnCmd(Object sender, CmdEventArgs e)
        {
            if (CmdEvent != null)    // 如果有对象注册
            {
                CmdEvent(this, e);  // 调用所有注册对象的方法
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
        public delegate void LogEventHandler(Object sender, LogEventArgs e);
        //事件
        public event EventHandler<LogEventArgs> LogEvent;
        public void ClearLogEvent()
        {
            LogEvent = null;
        }
        protected virtual void OnLog(Object sender, LogEventArgs e)
        {
            if (LogEvent != null)    // 如果有对象注册
            {
                LogEvent(this, e);  // 调用所有注册对象的方法
            }
            string message = TCP.TCPMessageFormat(e.TCPMessage);
            Log.CommandLog(message);
        }
        //日志参数定义
        public class LogEventArgs : EventArgs
        {
            public readonly TCPMessage TCPMessage;
            public LogEventArgs(TCPMessage m)
            {
                TCPMessage = m;
            }

        }
        #endregion
        //start_work
        public static byte[] start_work = new byte[] { 0x40, 0x40, 0x4A, 0x4C };
        public static byte[] msg_length = new byte[] { 0x2E, 0x00, 0x00, 0x00 };
        public static byte[] msg_len_TimeSync = new byte[] { 0x3A, 0x00, 0x00, 0x00 };
        public static byte[] version = new byte[] { 0x0A, 0x00 };
        public static byte[] success = new byte[] { 0xF0, 0x00 };
        public string DataSaveFolder = "";
        public int PictureServerId = 0;
        public bool IsRunning = false;
        //机器信息集合，key为IP地址，Value为机具信息
        public Dictionary<string, ky_machine> machine = new Dictionary<string, ky_machine>();
        //machine表
        private List<ky_machine> dt;

        public List<ky_machine> DTable
        {
            set
            {
                dt = value;
                machine.Clear();
                foreach (ky_machine m in dt)
                {
                    if (!machine.ContainsKey(m.kIpAddress))
                    {
                        machine.Add(m.kIpAddress, m);
                    }
                }
            }
            get { return dt; }
        }

        public void UpdateMachineTable(List<ky_machine> dt)
        {
            Dictionary<string, ky_machine> machineTmp = new Dictionary<string, ky_machine>();
            foreach (var m in dt)
            {
                machineTmp.Add(m.kIpAddress, m);
            }
            machine = machineTmp;
        }


        Thread threadWatch = null; // 负责监听客户端连接请求的 线程；  
        Socket socketWatch = null;

        Dictionary<string, Socket> dict = new Dictionary<string, Socket>();
        Dictionary<string, Thread> dictThread = new Dictionary<string, Thread>();
        public void StartListenling(string ip, int port)
        {
            // 创建负责监听的套接字，注意其中的参数；  
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 获得文本框中的IP对象；  
            IPAddress address = IPAddress.Parse(ip);
            // 创建包含ip和端口号的网络节点对象；  
            IPEndPoint endPoint = new IPEndPoint(address, port);
            try
            {
                // 将负责监听的套接字绑定到唯一的ip和端口上；  
                socketWatch.Bind(endPoint);
            }
            catch (SocketException e)
            {
                Log.ConnectionException(e, "启动监听服务异常");
                return;
            }
            // 设置监听队列的长度；  
            socketWatch.Listen(10);
            // 创建负责监听的线程；  
            threadWatch = new Thread(WatchConnecting);
            threadWatch.IsBackground = true;
            threadWatch.Start();
            //ShowMsg("服务器启动监听成功！");
            IsRunning = true;
        }

        void WatchConnecting()
        {
            while (true)  // 持续不断的监听客户端的连接请求；  
            {
                // 开始监听客户端连接请求，Accept方法会阻断当前的线程；  
                Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；  
                // 想列表控件中添加客户端的IP信息；  
                //lbOnline.Items.Add(sokConnection.RemoteEndPoint.ToString());
                string[] ipPort = sokConnection.RemoteEndPoint.ToString().Split(":".ToCharArray());
                OnLog(this, new LogEventArgs(
                    new TCPMessage
                    {
                        IpAndPort = sokConnection.RemoteEndPoint.ToString(),
                        MessageType = TCPMessageType.NewConnection
                    }));
                //IP地址在数据库中，才会接收数据
                if (machine.ContainsKey(ipPort[0]))
                {
                    // 将与客户端连接的 套接字 对象添加到集合中；
                    if (!dict.ContainsKey(sokConnection.RemoteEndPoint.ToString()))
                        dict.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);
                    Thread thr = new Thread(RecMsg);
                    thr.IsBackground = true;
                    thr.Start(sokConnection);
                    if (!dictThread.ContainsKey(sokConnection.RemoteEndPoint.ToString()))
                        dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程 添加 到线程的集合中去。  
                }
                else
                {
                    ky_machine newmachine = KyDataOperation.GetMachineWithIp(ipPort[0]);
                    if (newmachine != null)
                    {
                        machine.Add(newmachine.kIpAddress, newmachine);
                        // 将与客户端连接的 套接字 对象添加到集合中；
                        if (!dict.ContainsKey(sokConnection.RemoteEndPoint.ToString()))
                            dict.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);
                        //ShowMsg("客户端连接成功！");
                        Thread thr = new Thread(RecMsg);
                        thr.IsBackground = true;
                        thr.Start(sokConnection);
                        if (!dictThread.ContainsKey(sokConnection.RemoteEndPoint.ToString()))
                            dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程 添加 到线程的集合中去。  
                    }
                    else
                    {
                        OnLog(this, new LogEventArgs(
                            new TCPMessage
                            {
                                IpAndPort = sokConnection.RemoteEndPoint.ToString(),
                                MessageType = TCPMessageType.NoMachineIp
                            }));
                        sokConnection.Close();
                    }
                }

            }
        }
        void RecMsg(object sokConnectionparn)
        {
            Socket sokClient = sokConnectionparn as Socket;
            var ipAndPort = sokClient.RemoteEndPoint.ToString();
            var ipEndPoint = ((IPEndPoint)sokClient.RemoteEndPoint);
            var ip = ipEndPoint.Address.ToString();
            while (true)
            {
                Thread.Sleep(500);
                try
                {
                    byte[] startBuf = new byte[4];
                    int length = -1;
                    length = sokClient.Receive(startBuf);
                    if (length > 0)
                    {
                        if (length == 4 && BitConverter.ToInt32(startBuf, 0) == 0x4C4A4040)
                        {
                            byte[] readBuf = new byte[4];
                            length = sokClient.Receive(readBuf);
                            if (length == 4)
                            {
                                int msgLen = BitConverter.ToInt32(readBuf, 0);
                                byte[] bBuffer = new byte[msgLen];
                                Array.Copy(startBuf, 0, bBuffer, 0, 4);
                                Array.Copy(readBuf, 0, bBuffer, 4, 4);
                                readBuf = new byte[1024 * 1024];
                                for (int i = 8; i < msgLen; )
                                {
                                    length = sokClient.Receive(readBuf);
                                    Array.Copy(readBuf, 0, bBuffer, i, length);
                                    i += length;
                                    if (length == 0)
                                        throw new Exception("网速过慢");
                                }
                                Int16 cmd = BitConverter.ToInt16(bBuffer, 10);
                                string machineNo = "";
                                machineNo = Encoding.ASCII.GetString(bBuffer, 12, 28).Replace("\0", "");
                                if (cmd == 0x00A1)//传输数据请求命令
                                {
                                    OnLog(this, new LogEventArgs(
                                        new TCPMessage
                                        {
                                            IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                            Command = bBuffer.Take(54).ToArray(),
                                            MessageType = TCPMessageType.NET_SIMPLE
                                        }));
                                    //请求回复
                                    byte[] returnBytes = new byte[46];
                                    Array.Copy(start_work, 0, returnBytes, 0, start_work.Length);
                                    Array.Copy(msg_length, 0, returnBytes, 4, msg_length.Length);
                                    Array.Copy(bBuffer, 8, returnBytes, 8, 2);//协议版本、requestCmd
                                    Array.Copy(success, 0, returnBytes, 12, success.Length);
                                    Array.Copy(bBuffer, 12, returnBytes, 14, 28);
                                    sokClient.Send(returnBytes);
                                }
                                else if (cmd > 0 && cmd < 10)//纸币信息发送命令
                                {
                                    //应答回复
                                    OnLog(this, new LogEventArgs(
                                       new TCPMessage
                                       {
                                           IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                           Command = bBuffer.Take(200).ToArray(),
                                           MessageType = TCPMessageType.NET_UP
                                       }));
                                    byte[] returnBytes = new byte[46];
                                    Array.Copy(start_work, 0, returnBytes, 0, start_work.Length);
                                    Array.Copy(msg_length, 0, returnBytes, 4, msg_length.Length);
                                    Array.Copy(bBuffer, 8, returnBytes, 8, 4);//协议版本、requestCmd
                                    Array.Copy(success, 0, returnBytes, 12, success.Length);
                                    Array.Copy(bBuffer, 12, returnBytes, 14, 28);
                                    sokClient.Send(returnBytes);

                                    //machineNo = Encoding.ASCII.GetString(bBuffer,4,28).Replace("\0","");
                                    //报文体长度
                                    int bodyLen = msgLen - 84 - 2;
                                    byte[] Fsn = new byte[bodyLen];
                                    Array.Copy(bBuffer, 84, Fsn, 0, bodyLen);
                                    string fileName = DataSaveFolder + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") +
                                                      "-" + machineNo + ".FSN";
                                    //进行了交易控制，即指定了交易类型（如果收到前一天的数据是否应该排除呢？？2015.03.12）
                                    DateTime fileTime = KyDataLayer2.GetDateTime(Fsn);
                                    if (machine[ip].startBusinessCtl && machine[ip].dateTime.AddSeconds(-30) < fileTime)
                                    {
                                        if (machine[ip].fileName == null || machine[ip].fileName == "")
                                            machine[ip].fileName = fileName;
                                        else
                                            fileName = machine[ip].fileName;
                                        //文件不存在
                                        if (!File.Exists(fileName))
                                        {
                                            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                                            {
                                                fs.Write(Fsn, 0, Fsn.Length);
                                                fs.Close();
                                            }
                                        }
                                        else//文件已存在
                                        {
                                            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                                            {
                                                fs.Write(Fsn, 32, Fsn.Length - 32);
                                                fs.Close();
                                            }
                                            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Write))
                                            {
                                                FileInfo fileInf = new FileInfo(fileName);
                                                long Cnt = (fileInf.Length - 32) / 1644;
                                                byte[] tmp = BitConverter.GetBytes((UInt32)Cnt);
                                                fs.Seek(20, SeekOrigin.Begin);
                                                fs.Write(tmp, 0, tmp.Length);
                                                fs.Close();
                                            }
                                        }
                                        KYDataLayer1.Amount amount;
                                        KyDataLayer2.GetTotalValueFromFSN(fileName, out amount);
                                        CmdEventArgs e = new CmdEventArgs(ip, amount);
                                        OnCmd(this, e);

                                        //更新机具列表
                                        //机器最后上传时间和机具编号
                                        string machineNumber = "", machineModel = "";
                                        string[] str = KyDataLayer2.GetMachineNumberFromFSN(fileName, out machineModel).Split("/".ToCharArray());
                                        if (str.Length == 3)
                                            machineNumber = str[2];
                                        if (machine[ip].kMachineNumber != machineNumber && machineNumber != "" || machine[ip].kMachineModel != machineModel && machineModel != "")
                                            KyDataOperation.UpdateMachine(machine[ip].kId, machineNumber, machineModel);
                                        machine[ip].kMachineNumber = machineNumber;
                                        machine[ip].kMachineModel = machineModel;
                                    }
                                    else//没有进行交易控制
                                    {
                                        using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                                        {
                                            fs.Write(Fsn, 0, Fsn.Length);
                                            fs.Close();
                                        }
                                        machine[ip].imgServerId = PictureServerId;
                                        machine[ip].business = BussinessType.HM;
                                        SaveDataToDB.SaveFsn(fileName, machine[ip]);
                                        //更新机具列表
                                        //机器最后上传时间和机具编号
                                        string machineNumber = "", machineModel = "";
                                        string[] str = KyDataLayer2.GetMachineNumberFromFSN(fileName, out machineModel).Split("/".ToCharArray());
                                        if (str.Length == 3)
                                            machineNumber = str[2];
                                        if (machine[ip].kMachineNumber != machineNumber && machineNumber != "" || machine[ip].kMachineModel != machineModel && machineModel != "")
                                            KyDataOperation.UpdateMachine(machine[ip].kId, machineNumber, machineModel);
                                        machine[ip].kMachineNumber = machineNumber;
                                        machine[ip].kMachineModel = machineModel;

                                        //删除文件
                                        if (File.Exists(fileName))
                                            File.Delete(fileName);
                                    }


                                }
                                else if (cmd == 0x0010)//时间同步
                                {
                                    //发送回复信息
                                    OnLog(this, new LogEventArgs(
                                       new TCPMessage
                                       {
                                           IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                           Command = bBuffer.Take(42).ToArray(),
                                           MessageType = TCPMessageType.NET_TIME
                                       }));
                                    byte[] returnBytes = new byte[58];
                                    Array.Copy(start_work, 0, returnBytes, 0, start_work.Length);
                                    Array.Copy(msg_len_TimeSync, 0, returnBytes, 4, msg_length.Length);
                                    Array.Copy(bBuffer, 8, returnBytes, 8, 4);
                                    Array.Copy(success, 0, returnBytes, 12, success.Length);
                                    Array.Copy(bBuffer, 12, returnBytes, 14, 28);
                                    string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    byte[] timeBytes = Encoding.ASCII.GetBytes(time);
                                    Array.Copy(timeBytes, 0, returnBytes, 42, timeBytes.Length);
                                    sokClient.Send(returnBytes);
                                }
                                else if (cmd == 0x00A2)//关闭连接
                                {
                                    OnLog(this, new LogEventArgs(
                                       new TCPMessage
                                       {
                                           IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                           Command = bBuffer.Take(44).ToArray(),
                                           MessageType = TCPMessageType.NET_CLOSE
                                       }));
                                    dict[ipAndPort].Close();
                                    if (dict.ContainsKey(ipAndPort))
                                        dict.Remove(ipAndPort);
                                    // 从通信线程集合中删除被中断连接的通信线程对象； 
                                    try
                                    {
                                        dictThread[ipAndPort].Abort();//Thread.Abort一定会抛出异常。所以要try catch掉
                                    }
                                    catch (ThreadAbortException e)
                                    {
                                        //Log.ConnectionException(e, "关闭连接异常");
                                    }
                                    finally
                                    {
                                        if (dictThread.ContainsKey(ipAndPort))
                                        {
                                            dictThread.Remove(ipAndPort);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (SocketException se)
                {
                    Log.ConnectionException(se, "通信异常");
                    //ShowMsg("异常：" + se.Message);
                    // 从 通信套接字 集合中删除被中断连接的通信套接字；  
                    if (dict.ContainsKey(ipAndPort))
                        dict.Remove(ipAndPort);
                    // 从通信线程集合中删除被中断连接的通信线程对象；  
                    if (dictThread.ContainsKey(ipAndPort))
                        dictThread.Remove(ipAndPort);
                    // 从列表中移除被中断的连接IP  
                    //lbOnline.Items.Remove(ipAndPort);
                    break;
                }
            }
        }
        //停止服务
        public void Stop()
        {
            foreach (var sock in dict)
            {
                sock.Value.Close();
            }
            dict.Clear();
            foreach (var thr in dictThread)
            {
                thr.Value.Abort();
            }
            dictThread.Clear();
            if (socketWatch != null)
                socketWatch.Close();
            if (threadWatch != null)
                threadWatch.Abort();
            IsRunning = false;
        }

        public enum MyBusinessStatus
        {
            Start = 0,
            End = 1,
            Cancel = 2,
        };
        //交易控制
        public void BusinessControl(MyBusinessStatus myBusinessStatus, businessControl bControl)
        {
            ky_machine currentMachine = new ky_machine();
            if (machine.ContainsKey(bControl.ip))
            {
                currentMachine = machine[bControl.ip];
            }

            switch (myBusinessStatus)
            {
                case MyBusinessStatus.Start:
                    if (machine.ContainsKey(bControl.ip))
                    {
                        currentMachine.startBusinessCtl = true;
                        currentMachine.fileName = "";
                        currentMachine.dateTime = bControl.dateTime;
                        currentMachine.business = bControl.business;
                        currentMachine.bundleCount = bControl.bundleCount;//每捆的张数  2015.05.05
                        currentMachine.userId = bControl.userId;//2015.05.08
                    }
                    break;
                case MyBusinessStatus.End:

                    if (machine.ContainsKey(bControl.ip))
                    {
                        currentMachine.startBusinessCtl = false;
                        if (currentMachine.business == BussinessType.CK || currentMachine.business == BussinessType.QK)
                            currentMachine.bussinessNumber = bControl.bussinessNumber;
                        if (currentMachine.business == BussinessType.ATMP || currentMachine.business == BussinessType.ATMQ)
                            currentMachine.atmId = Convert.ToInt32(bControl.atmId);
                        if (currentMachine.business == BussinessType.ATMP)
                            currentMachine.cashBoxId = Convert.ToInt32(bControl.cashBoxId);
                        if (currentMachine.business == BussinessType.KHDK)
                        {
                            currentMachine.isClearCenter = bControl.isClearCenter;
                            currentMachine.packageNumber = bControl.packageNumber;
                        }
                        currentMachine.imgServerId = PictureServerId;
                        if (currentMachine.fileName != "" && File.Exists(currentMachine.fileName))
                        {
                            if (currentMachine.business == BussinessType.KHDK)
                            {
                                if (!Directory.Exists(DataSaveFolder + "\\tmp"))
                                {
                                    Directory.CreateDirectory(DataSaveFolder + "\\tmp");
                                }
                                KyDataLayer2.DecompositionFile(currentMachine.fileName, DataSaveFolder + "\\tmp",
                                                               currentMachine.bundleCount);
                                SaveDataToDB.SaveKHDK(DataSaveFolder + "\\tmp", currentMachine);
                                //删除文件
                                if (Directory.Exists(DataSaveFolder + "\\tmp"))
                                {
                                    string[] files = Directory.GetFiles(DataSaveFolder + "\\tmp");
                                    foreach (var file in files)
                                    {
                                        File.Delete(file);
                                    }
                                }
                            }
                            else
                            {
                                SaveDataToDB.SaveFsn(currentMachine.fileName, currentMachine);
                            }
                            //删除文件
                            if (File.Exists(currentMachine.fileName))
                            {
                                File.Delete(currentMachine.fileName);
                            }
                        }
                        currentMachine.business = BussinessType.HM;
                        currentMachine.startBusinessCtl = false;
                        currentMachine.bussinessNumber = "";
                        currentMachine.atmId = 0;
                        currentMachine.cashBoxId = 0;
                        currentMachine.fileName = "";
                        currentMachine.userId = 0;
                    }
                    break;
            }
            machine[bControl.ip] = currentMachine;
        }
    }
}
