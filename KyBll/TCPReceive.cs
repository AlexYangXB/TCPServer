using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using KyModel;
using KyModel.Models;

namespace KyBll
{
    public class TCPReceive
    {
        public volatile bool _shouldStop;
        //start_work
        public static byte[] start_work = new byte[] { 0x40, 0x40, 0x4A, 0x4C };
        public static byte[] msg_length = new byte[] { 0x2E, 0x00, 0x00, 0x00 };
        public static byte[] msg_len_TimeSync = new byte[] { 0x3A, 0x00, 0x00, 0x00 };
        public static byte[] version = new byte[] { 0x0F, 0x00 };
        public static byte[] success = new byte[] { 0xF0, 0x00 };
        public static byte[] FILE_TOO_BIG = new byte[] { 0x2E, 0x00 };
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dataSaveFolder">数据保存路径</param>
        /// <param name="pictureServerId">图像数据id</param>
        /// <param name="tcpEvent">TCPEvent日志记录</param>
        /// <param name="machine">机具列表</param>
        public TCPReceive(string dataSaveFolder, int pictureServerId, MyTCP tcpEvent, Dictionary<string, ky_machine> machine)
        {
            this.DataSaveFolder = dataSaveFolder;
            this.PictureServerId = pictureServerId;
            this.TCPEvent = tcpEvent;
            this.machine = machine;
        }
        public string DataSaveFolder = "";
        public int PictureServerId = 0;
        public MyTCP TCPEvent;
        //机器信息集合，key为IP地址，Value为机具信息
        public Dictionary<string, ky_machine> machine = new Dictionary<string, ky_machine>();
        public Socket sokClient;
        public void RecMsg(object sokConnectionparn)
        {
            sokClient = sokConnectionparn as Socket;
            var ipAndPort = sokClient.RemoteEndPoint.ToString();
            var ipEndPoint = ((IPEndPoint)sokClient.RemoteEndPoint);
            var ip = ipEndPoint.Address.ToString();
            while (!_shouldStop)
            {
                Thread.Sleep(300);
                try
                {
                    byte[] startBuf = new byte[40];
                    int length = -1;
                    length = MyTCP.AsyncReceiveFromClient(sokClient, startBuf.Length, out startBuf);
                    if (length > 0)
                    {
                        if (length == 40 && BitConverter.ToInt32(startBuf, 0) == 0x4C4A4040)
                        {
                            int msgLen = BitConverter.ToInt32(startBuf, 4);
                            Int16 cmd = BitConverter.ToInt16(startBuf, 10);
                            string machineNo = "";
                            byte[] machineByte = startBuf.Skip(12).Take(28).ToArray();
                            machineNo = Encoding.ASCII.GetString(machineByte).Replace("\0", "");
                            byte[] bBuffer = new byte[msgLen];
                            Array.Copy(startBuf, 0, bBuffer, 0, 40);
                            byte[] readBuf = new byte[1024 * 8];
                            for (int i = 40; i < msgLen; )
                            {
                                length = MyTCP.AsyncReceiveFromClient(sokClient, readBuf.Length, out readBuf);
                                if (length == 0)
                                    throw new Exception("网速过慢");
                                Array.Copy(readBuf, 0, bBuffer, i, length);
                                i += length;
                            }
                            if (cmd == 0x00A1)//传输数据请求命令 54字节
                            {
                                machine[ip].alive = DateTime.Now;
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                    Command = bBuffer.Take(54).ToArray(),
                                    MessageType = TCPMessageType.NET_SIMPLE
                                });
                                //请求回复
                                SendNetReply(0, success, machineByte, 0);
                            }
                            else if (cmd > 0 && cmd <= 10)//纸币信息发送命令 86+1644*N字节
                            {
                                //报文体长度
                                int bodyLen = msgLen - 84 - 2;
                                //文件长度大于等于5M,将关闭连接
                                if (bodyLen / 1024 / 1024 >= 5)
                                {
                                    TCPEvent.OnCommandLog(new TCPMessage
                                    {
                                        IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                        Command = bBuffer.Take(200).ToArray(),
                                        MessageType = TCPMessageType.FILE_TOO_BIG
                                    });
                                    SendNetReply(cmd, FILE_TOO_BIG, machineByte, 0);
                                    CloseThread();
                                    break;
                                }
                                //应答回复
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                    Command = bBuffer.Take(200).ToArray(),
                                    MessageType = TCPMessageType.NET_UP
                                });
                                SendNetReply(cmd, success, machineByte, 0);
                                byte[] Fsn = new byte[bodyLen];
                                Array.Copy(bBuffer, 84, Fsn, 0, bodyLen);
                                CopeWithNetUp(Fsn, ip, machineNo);


                            }
                            else if (cmd == 0x0010)//时间同步 42字节
                            {
                                //发送回复信息
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                    Command = bBuffer.Take(42).ToArray(),
                                    MessageType = TCPMessageType.NET_TIME
                                });
                                SendNetTimeReply(cmd, success, machineByte);
                                CloseThread();
                            }
                            else if (cmd == 0x000B)//心跳命令 44字节
                            {
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                    Command = bBuffer,
                                    MessageType = TCPMessageType.NET_CONTINUE
                                });
                                machine[ip].alive = DateTime.Now;
                                CloseThread();
                            }
                            else if (cmd == 0x00A2)//传输数据结束命令 44字节
                            {
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = sokClient.RemoteEndPoint.ToString(),
                                    Command = bBuffer.Take(44).ToArray(),
                                    MessageType = TCPMessageType.NET_CLOSE
                                });
                                CloseThread();
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
                catch (Exception se)
                {
                    Log.ConnectionException("通信异常", se);
                    CloseThread();
                    break;
                }
                if ((DateTime.Now - machine[ip].alive).TotalMinutes > 5)
                {
                    Log.ConnectionException("机具编号为" + machine[ip].kMachineNumber + "上次连接时间为" + machine[ip].alive.ToString("yyyy-MM-dd HH:mm:ss") + ",大于当前5分钟，将关闭线程!", null);
                    CloseThread();
                }
            }
        }
        /// <summary>
        /// 关闭收数据线程
        /// </summary>
        private void CloseThread()
        {
            _shouldStop = true;
            sokClient.Close();
        }
        /// <summary>
        /// 处理纸币信息发送命令
        /// </summary>
        /// <param name="Fsn"></param>
        /// <param name="ip"></param>
        /// <param name="machineNo"></param>
        public void CopeWithNetUp(byte[] Fsn, string ip, string machineNo)
        {
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
            KyDataOperation.UpdateMachineTime(machine[ip].kId, DateTime.Now);
        }
        /// <summary>
        /// 回复命令
        /// </summary>
        /// <param name="requestCmd">请求命令</param>
        /// <param name="retCode">状态</param>
        /// <param name="MachineNo">机具编号</param>
        /// <param name="packageIndex">文件分次发送时序号</param>
        public void SendNetReply(short requestCmd, byte[] retCode, byte[] MachineNo, short packageIndex)
        {
            byte[] returnBytes = new byte[46];
            Array.Copy(start_work, 0, returnBytes, 0, 4);
            Array.Copy(msg_length, 0, returnBytes, 4, 4);
            Array.Copy(version, 0, returnBytes, 8, 2);//协议版本15
            Array.Copy(BitConverter.GetBytes(requestCmd), 0, returnBytes, 10, 2);//requesCmd
            Array.Copy(retCode, 0, returnBytes, 12, 2);//retCode
            Array.Copy(MachineNo, 0, returnBytes, 14, 28);
            Array.Copy(BitConverter.GetBytes(packageIndex), 0, returnBytes, 42, 2);
            MyTCP.AsyncSendToClient(sokClient, returnBytes);
        }
        /// <summary>
        /// 时间同步回复命令
        /// </summary>
        /// <param name="requestCmd">请求命令</param>
        /// <param name="retCode">状态</param>
        /// <param name="MachineNo">机具编号</param>
        public void SendNetTimeReply(short requestCmd, byte[] retCode, byte[] MachineNo)
        {
            byte[] returnBytes = new byte[58];
            Array.Copy(start_work, 0, returnBytes, 0, 4);
            Array.Copy(msg_len_TimeSync, 0, returnBytes, 4, 4);
            Array.Copy(version, 0, returnBytes, 8, 2);
            Array.Copy(BitConverter.GetBytes(requestCmd), 0, returnBytes, 10, 2);
            Array.Copy(retCode, 0, returnBytes, 12, 2);
            Array.Copy(MachineNo, 0, returnBytes, 14, 28);
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            byte[] timeBytes = Encoding.ASCII.GetBytes(time);
            Array.Copy(timeBytes, 0, returnBytes, 42, 14);
            MyTCP.AsyncSendToClient(sokClient, returnBytes);
        }
    }
}
