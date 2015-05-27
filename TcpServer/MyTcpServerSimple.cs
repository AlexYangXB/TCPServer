using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using KyModel;
namespace MyTcpServer
{
    public class MyTcpServerSimple
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
            public readonly int Cmd;
            public readonly string IP;
            public readonly string Machine;
            public CmdEventArgs(int cmd, string ip, string machine)
            {
                Cmd = cmd;
                IP = ip;
                Machine = machine;
            }
            //public readonly string IP;
            //public readonly KyData.DataBase.KYDataLayer1.Amount Amount;
            //public CmdEventArgs(string ip, KYDataLayer1.Amount amount)
            //{
            //    IP = ip;
            //    Amount = amount;
            //}

        }
        #endregion
        //start_work
        public static byte[] start_work = new byte[] { 0x40, 0x40, 0x4A, 0x4C };
        public static byte[] msg_lenght = new byte[] { 0x2E, 0x00, 0x00, 0x00 };
        public static byte[] msg_len_TimeSync = new byte[] { 0x3A, 0x00, 0x00, 0x00 };
        public static byte[] version = new byte[] { 0x0A, 0x00 };
        public static byte[] success = new byte[] { 0xF0, 0x00 };
        public string DataSaveFolder = "";
        //public int PictureServerId = 0;
        public bool IsRunning = false;

        //IP地址列表
        private List<string> ipList=new List<string>();
       
        public List<string> IpList
        {
            set { ipList = value; }
            get { return ipList; }
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
            catch (SocketException se)
            {
                //MessageBox.Show("异常：" + se.Message);
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

                //IP地址在数据库中，才会接收数据
                if (ipList.Contains(ipPort[0]))
                {
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
                    sokConnection.Close();
                }

            }
        }
        void RecMsg(object sokConnectionparn)
        {
            Socket sokClient = sokConnectionparn as Socket;
            string ipAndPort = sokClient.RemoteEndPoint.ToString();
            string[] ip = ipAndPort.Split(":".ToCharArray());
            while (true)
            {
                try
                {
                    byte[] readBuf = new byte[4];
                    int length = -1;
                    length = sokClient.Receive(readBuf);
                    if (length == 4 && BitConverter.ToInt32(readBuf, 0) == 0x4C4A4040)
                    {
                        length = sokClient.Receive(readBuf);
                        if (length == 4)
                        {
                            int msgLen = BitConverter.ToInt32(readBuf, 0);
                            byte[] bBuffer = new byte[msgLen - 8];
                            readBuf = new byte[1024 * 1024];
                            for (int i = 0; i < msgLen - 8; )
                            {
                                length = sokClient.Receive(readBuf);
                                Array.Copy(readBuf, 0, bBuffer, i, length);
                                i += length;
                                if (length == 0)
                                    throw new Exception("网速过慢");
                            }
                            Int16 cmd = BitConverter.ToInt16(bBuffer, 2);
                            string machineNo = "";
                            machineNo = Encoding.ASCII.GetString(bBuffer, 4, 28).Replace("\0", "");
                            if (cmd == 0x00A1)//请求上传
                            {
                                //发送回复信息
                                byte[] returnBytes = new byte[46];
                                Array.Copy(start_work, 0, returnBytes, 0, start_work.Length);
                                Array.Copy(msg_lenght, 0, returnBytes, 4, msg_lenght.Length);
                                Array.Copy(bBuffer, 0, returnBytes, 8, 2);
                                Array.Copy(success, 0, returnBytes, 12, success.Length);
                                Array.Copy(bBuffer, 4, returnBytes, 14, 28);
                                sokClient.Send(returnBytes);
                            }
                            else if (cmd > 0 && cmd < 10)
                            {
                                //发送回复信息
                                byte[] returnBytes = new byte[46];
                                Array.Copy(start_work, 0, returnBytes, 0, start_work.Length);
                                Array.Copy(msg_lenght, 0, returnBytes, 4, msg_lenght.Length);
                                Array.Copy(bBuffer, 0, returnBytes, 8, 4);
                                Array.Copy(success, 0, returnBytes, 12, success.Length);
                                Array.Copy(bBuffer, 4, returnBytes, 14, 28);
                                sokClient.Send(returnBytes);

                                //machineNo = Encoding.ASCII.GetString(bBuffer,4,28).Replace("\0","");
                                int bodyLen = msgLen - 84 - 2;
                                byte[] Fsn = new byte[bodyLen];
                                Array.Copy(bBuffer, 76, Fsn, 0, bodyLen);
                                //获取文件的第一个点钞时间,用于对文件命名
                                DateTime fileTime = KyDataLayer2.GetDateTime(Fsn);
                                string fileName = string.Format("{0}\\{1}-{2}.FSN", DataSaveFolder, fileTime.ToString("yyyyMMddHHmmssfff"), machineNo);
                                //string fileName = DataSaveFolder + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") +
                                //                  "-" + machineNo + ".FSN";
                                //保存FSN文件
                                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                                {
                                    fs.Write(Fsn, 0, Fsn.Length);
                                    fs.Close();
                                }
                            }
                            else if (cmd == 0x0010)//时间同步
                            {
                                //发送回复信息
                                byte[] returnBytes = new byte[58];
                                Array.Copy(start_work, 0, returnBytes, 0, start_work.Length);
                                Array.Copy(msg_len_TimeSync, 0, returnBytes, 4, msg_lenght.Length);
                                Array.Copy(bBuffer, 0, returnBytes, 8, 4);
                                Array.Copy(success, 0, returnBytes, 12, success.Length);
                                Array.Copy(bBuffer, 4, returnBytes, 14, 28);
                                string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                                byte[] timeBytes = Encoding.ASCII.GetBytes(time);
                                Array.Copy(timeBytes, 0, returnBytes, 42, timeBytes.Length);
                                sokClient.Send(returnBytes);
                            }
                            else if (cmd == 0x00A2)//关闭连接
                            {

                                dict[ipAndPort].Close();
                                if (dict.ContainsKey(ipAndPort))
                                    dict.Remove(ipAndPort);
                                // 从通信线程集合中删除被中断连接的通信线程对象； 
                                try
                                {
                                    dictThread[ipAndPort].Abort();//Thread.Abort一定会抛出异常。所以要try catch掉
                                }
                                catch (ThreadAbortException)
                                {
                                }
                                finally
                                {
                                    if (dictThread.ContainsKey(ipAndPort))
                                    {
                                        dictThread.Remove(ipAndPort);
                                    }
                                    CmdEventArgs ex = new CmdEventArgs(cmd, ip[0], machineNo);
                                    OnCmd(this, ex);
                                }
                            }
                            CmdEventArgs e = new CmdEventArgs(cmd, ip[0], machineNo);
                            OnCmd(this, e);
                        }
                    }
                }
                catch (SocketException se)
                {
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
                catch (Exception e)
                {
                    //ShowMsg("异常：" + e.Message);
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
    }
}
