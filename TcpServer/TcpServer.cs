using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using KyBll;
using KyModel;
using KyModel.Models;
namespace MyTcpServer
{
    public class TcpServer
    {
      
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
        public MyTCP TCPEvent;
        public TcpServer()
        {
            TCPEvent = new MyTCP();
        }
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
            foreach (var m in dt)
            {
                if (!machine.ContainsKey(m.kIpAddress))
                    machine.Add(m.kIpAddress, m);
            }
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
                Log.ConnectionException("启动监听服务异常",e);
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
                TCPEvent.OnCommandLog(new TCPMessage
                    {
                        IpAndPort = sokConnection.RemoteEndPoint.ToString(),
                        MessageType = TCPMessageType.NewConnection
                    });
                //IP地址在数据库中，才会接收数据
                if (machine.ContainsKey(ipPort[0]))
                {
                    // 将与客户端连接的 套接字 对象添加到集合中
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
                        else
                        {
                            TCPEvent.OnCommandLog(new TCPMessage
                           {
                               IpAndPort = sokConnection.RemoteEndPoint.ToString(),
                               MessageType = TCPMessageType.ExistConnection
                           });
                        }
                        //ShowMsg("客户端连接成功！");
                        Thread thr = new Thread(RecMsg);
                        thr.IsBackground = true;
                        if (!dictThread.ContainsKey(sokConnection.RemoteEndPoint.ToString()))
                            dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程 添加 到线程的集合中去。  
                        else
                        {
                            TCPEvent.OnCommandLog(new TCPMessage
                            {
                                IpAndPort = sokConnection.RemoteEndPoint.ToString(),
                                MessageType = TCPMessageType.ExistConnection
                            });
                        }
                        thr.Start(sokConnection);

                    }
                    else
                    {
                        TCPEvent.OnCommandLog(new TCPMessage
                            {
                                IpAndPort = sokConnection.RemoteEndPoint.ToString(),
                                MessageType = TCPMessageType.NoMachineIp
                            });
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
                                    machine[ip].alive = DateTime.Now;
                                    TCPEvent.OnCommandLog(new TCPMessage
                                        {
                                            IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                            Command = bBuffer.Take(54).ToArray(),
                                            MessageType = TCPMessageType.NET_SIMPLE
                                        });
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
                                    TCPEvent.OnCommandLog(new TCPMessage
                                       {
                                           IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                           Command = bBuffer.Take(200).ToArray(),
                                           MessageType = TCPMessageType.NET_UP
                                       });
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
                                    DateTime date = DateTime.Now;
                                    string filePath = DataSaveFolder + "\\" + date.ToString("yyyyMMdd") + "\\" + date.ToString("HH") + "\\";
                                    if (!Directory.Exists(filePath))
                                        Directory.CreateDirectory(filePath);
                                    string fileName = filePath + date.ToString("mmssfff") +
                                                      "-" + machineNo + ".FSN";
                                    //进行了交易控制，即指定了交易类型（如果收到前一天的数据是否应该排除呢？？2015.03.12）
                                    DateTime fileTime = FSNFormat.GetDateTime(Fsn);
                                    TCPEvent.OnBussninessLog("收到文件的时间是" + fileTime.ToString("yyyy-MM-dd HH:mm:ss") + ",机具" + ip + "的开始标志是" + machine[ip].startBusinessCtl);
                                    if (machine[ip].startBusinessCtl && machine[ip].dateTime.AddHours(-2) < fileTime)
                                    {
                                        if (machine[ip].fileName == null || machine[ip].fileName == "")
                                            machine[ip].fileName = fileName;
                                        else
                                            fileName = machine[ip].fileName;
                                        string tmpPath = machine[ip].tmpPath;
                                        
                                        if (machine[ip].business == BussinessType.KHDK)
                                        {

                                            if (!Directory.Exists(tmpPath))
                                            {
                                                Directory.CreateDirectory(tmpPath);
                                            }
                                            List<string> modifyFiles = GZHImport.MergeLastFile(Fsn, tmpPath, machine[ip].bundleCount);
                                            foreach (string file in modifyFiles)
                                            {
                                                KYDataLayer1.Amount amount;
                                                KyDataLayer2.GetTotalValueFromFSN(file, out amount);
                                                amount.BundleNumber = Convert.ToInt32(Path.GetFileNameWithoutExtension(file));
                                                MyTCP.CmdEventArgs e = new MyTCP.CmdEventArgs(ip, amount);
                                                TCPEvent.OnAmountCmd(e);
                                            }
                                        }
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
                                        if (machine[ip].business != BussinessType.KHDK)
                                        {
                                            KYDataLayer1.Amount amount;
                                            KyDataLayer2.GetTotalValueFromFSN(fileName, out amount);
                                            amount.BundleNumber = 999999;
                                            MyTCP.CmdEventArgs e = new MyTCP.CmdEventArgs(ip, amount);
                                            TCPEvent.OnAmountCmd(e);
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
                                        FSNImport.SaveFsn(fileName, machine[ip]);
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
                                        //if (File.Exists(fileName))
                                        //    File.Delete(fileName);
                                    }


                                }
                                else if (cmd == 0x0010)//时间同步
                                {
                                    //发送回复信息
                                    TCPEvent.OnCommandLog(new TCPMessage
                                       {
                                           IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                           Command = bBuffer.Take(42).ToArray(),
                                           MessageType = TCPMessageType.NET_TIME
                                       });
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
                                    CloseThread(ipAndPort);
                                }
                                else if (cmd == 0x000B)//心跳命令
                                {
                                    TCPEvent.OnCommandLog(new TCPMessage
                                    {
                                        IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                        Command = bBuffer,
                                        MessageType = TCPMessageType.NET_CONTINUE
                                    });
                                    machine[ip].alive = DateTime.Now;
                                    CloseThread(ipAndPort);
                                }
                                else if (cmd == 0x00A2)//关闭连接
                                {
                                    TCPEvent.OnCommandLog(new TCPMessage
                                       {
                                           IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                           Command = bBuffer.Take(44).ToArray(),
                                           MessageType = TCPMessageType.NET_CLOSE
                                       });
                                    CloseThread(ipAndPort);
                                }
                            }
                        }
                        else
                        {
                            TCPEvent.OnCommandLog(new TCPMessage
                                       {
                                           IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                           Command = startBuf,
                                           MessageType = TCPMessageType.UnknownCommand
                                       });
                        }
                    }
                }
                catch (SocketException se)
                {
                    Log.ConnectionException("通信异常",se );
                    CloseThread(ipAndPort);
                    break;
                }
            }
        }
        /// <summary>
        /// 关闭收数据线程
        /// </summary>
        /// <param name="ipAndPort"></param>
        private void CloseThread(string ipAndPort)
        {
            //dict[ipAndPort].Shutdown(SocketShutdown.Both);
            dict[ipAndPort].Close();
            if (dict.ContainsKey(ipAndPort))
                dict.Remove(ipAndPort);
            //从通信线程集合中删除被中断连接的通信线程对象； 
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
                    TCPEvent.OnBussninessLog(bControl.business + "业务开始,机具ip为" + bControl.ip);
                    if (machine.ContainsKey(bControl.ip))
                    {
                        currentMachine.startBusinessCtl = true;
                        currentMachine.fileName = "";
                        currentMachine.dateTime = bControl.dateTime;
                        currentMachine.business = bControl.business;
                        currentMachine.bundleCount = bControl.bundleCount;//每捆的张数  2015.05.05
                        currentMachine.userId = bControl.userId;//2015.05.08
                        if (currentMachine.business == BussinessType.KHDK)
                            currentMachine.tmpPath = DataSaveFolder + "\\tmp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    }
                    break;
                case MyBusinessStatus.End:
                    TCPEvent.OnBussninessLog(currentMachine.business + "业务结束,机具ip为" + bControl.ip);
                    if (machine.ContainsKey(bControl.ip))
                    {
                        if (currentMachine.business == BussinessType.CK || currentMachine.business == BussinessType.QK)
                            currentMachine.bussinessNumber = bControl.bussinessNumber;
                        if (currentMachine.business == BussinessType.ATMP || currentMachine.business == BussinessType.ATMQ)
                        {
                            currentMachine.atmId = Convert.ToInt32(bControl.atmId);
                            currentMachine.cashBoxId = Convert.ToInt32(bControl.cashBoxId);
                        }
                        if (currentMachine.business == BussinessType.KHDK)
                        {
                            currentMachine.isClearCenter = bControl.isClearCenter;
                            currentMachine.packageNumber = bControl.packageNumber;
                        }
                        currentMachine.imgServerId = PictureServerId;
                        if (!bControl.cancel)
                        {
                            if (currentMachine.fileName != "" && File.Exists(currentMachine.fileName))
                            {
                                FSNImport.SaveFsn(currentMachine.fileName, currentMachine);
                            }
                            string tmpPath = currentMachine.tmpPath;
                            if (currentMachine.business == BussinessType.KHDK)
                            {
                                if (Directory.Exists(tmpPath))
                                {
                                    string[] bundleNumbers = bControl.bundleNumbers.Split(',');
                                    List<string> saveFiles = new List<string>();
                                    foreach (var bundle in bundleNumbers)
                                    {
                                        string saveFile=tmpPath+"\\"+bundle+".FSN";
                                        if (File.Exists(saveFile))
                                            saveFiles.Add(saveFile);
                                    }
                                    GZHImport.SaveKHDK(saveFiles, currentMachine);
                                }
                                else
                                    TCPEvent.OnBussninessLog("KHDK中临时路径" + tmpPath + "不存在！");
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
