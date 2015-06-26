﻿using System;
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


        public string DataSaveFolder = "";
        public int PictureServerId = 0;
        public bool IsRunning = false;
        //机器信息集合，key为IP地址，Value为机具信息
        public Dictionary<string, ky_machine> machine = new Dictionary<string, ky_machine>();
        //machine表
        private List<ky_machine> dt;
        public MyTCP TCPEvent;
        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);
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
                //新增的机具
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
            //新增的机具
            foreach (var m in dt)
            {
                if (!machine.ContainsKey(m.kIpAddress))
                    machine.Add(m.kIpAddress, m);
            }
            //被删除的机具
            for (var i = 0; i < machine.Count; i++)
            {
                var key = machine.Keys.ElementAt(i);
                if (dt.Find(p => p.kIpAddress == key) == null)
                {
                    machine.Remove(key);
                }
            }
        }


        Thread threadWatch = null; // 负责监听客户端连接请求的 线程
        Socket socketWatch = null;

        private Dictionary<TCPReceive, Thread> dictThread = new Dictionary<TCPReceive, Thread>();
        private volatile bool _shouldStop;
        public void StartListenling(string ip, int port)
        {
            // 创建负责监听的套接字，注意其中的参数
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 获得文本框中的IP对象；  
            IPAddress address = IPAddress.Parse(ip);
            // 创建包含ip和端口号的网络节点对象
            IPEndPoint endPoint = new IPEndPoint(address, port);
            _shouldStop = false;
            try
            {
                // 将负责监听的套接字绑定到唯一的ip和端口上；  
                socketWatch.Bind(endPoint);
            }
            catch (SocketException e)
            {
                Log.ConnectionException("启动监听服务异常", e);
                return;
            }
            // 设置监听队列的长度
            socketWatch.Listen(10);
            // 创建负责监听的线程
            threadWatch = new Thread(WatchConnecting);
            threadWatch.IsBackground = true;
            threadWatch.Start();
            IsRunning = true;

        }
        public void WatchConnecting()
        {
            while (!_shouldStop)
            {
                DateTime start = DateTime.Now;
                //结束状态_shouldStop为True的线程
                Dictionary<TCPReceive, Thread> tmpDict = new Dictionary<TCPReceive, Thread>();
                foreach(var dict in dictThread)
                {
                    if (dict.Key._shouldStop)
                    {
                        if (dict.Key.sokClient != null)
                            dict.Key.sokClient.Close();
                        dictThread[dict.Key].Join();
                    }
                    else
                        tmpDict.Add(dict.Key,dict.Value);
                }
                dictThread = tmpDict;
                TimeSpan span = DateTime.Now - start;
                KyBll.Log.TestLog("close thread take about " + span.TotalMilliseconds + "ms. total " + dictThread.Count + " threads.");
                if (dictThread.Count >= 30)
                {
                    TCPEvent.OnCommandLog(new TCPMessage
                    {
                        MessageType = TCPMessageType.Reach_Max_Connection
                    });
                    continue;
                }
                allDone.Reset();
                socketWatch.BeginAccept(new AsyncCallback(AcceptCallback), socketWatch);
                allDone.WaitOne();
            }
        }
        public void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();
            if (!_shouldStop)
            {
                Socket sokConnection = null;
                Socket listener = (Socket)ar.AsyncState;
                sokConnection = listener.EndAccept(ar);
                if (sokConnection != null)
                {
                    string[] ipPort = sokConnection.RemoteEndPoint.ToString().Split(":".ToCharArray());
                    TCPEvent.OnCommandLog(new TCPMessage
                    {
                        IpAndPort = sokConnection.RemoteEndPoint.ToString(),
                        MessageType = TCPMessageType.NewConnection
                    });
                    bool RecFlag = false;
                    //IP地址在数据库中，才会接收数据
                    if (machine.ContainsKey(ipPort[0]))
                    {
                        RecFlag = true;
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
                    if (RecFlag)
                    {
                        TCPReceive tcpr = new TCPReceive(DataSaveFolder, PictureServerId, TCPEvent, machine);
                        Thread thr = new Thread(tcpr.RecMsg);
                        thr.IsBackground = true;

                        //超时设置  15秒
                        sokConnection.ReceiveTimeout = 15000;
                        sokConnection.SendTimeout = 15000;
                        thr.Start(sokConnection);
                        dictThread.Add(tcpr, thr);
                    }
                }
            }
        }


        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            foreach (var dict in dictThread)
            {
                dict.Key._shouldStop = true;
                dict.Key.sokClient.Shutdown(SocketShutdown.Both);
                dict.Key.sokClient.Close();
                dict.Value.Join();
            }
            dictThread.Clear();
            _shouldStop = true;
            allDone.Set();
            if (threadWatch != null)
            {
                threadWatch.Join();
            }
            if (socketWatch != null)
            {
                socketWatch.Close();
            }
            IsRunning = false;
        }

        public enum MyBusinessStatus
        {
            Start = 0,
            End = 1,
            Cancel = 2,
        };
        /// <summary>
        /// 交易控制
        /// </summary>
        /// <param name="myBusinessStatus"></param>
        /// <param name="bControl"></param>
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
                                        string saveFile = tmpPath + "\\" + bundle + ".FSN";
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
