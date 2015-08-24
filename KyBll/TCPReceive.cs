using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using KyBase;
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
        private int ReceiveFileCnt = 0;
        private string ipAndPort = "";
        private int FakeVer = 0;
        private List<string> FakeList = new List<string>();
        public void RecMsg(object sokConnectionparn)
        {
            sokClient = sokConnectionparn as Socket;
            ipAndPort = sokClient.RemoteEndPoint.ToString();
            var ipEndPoint = ((IPEndPoint)sokClient.RemoteEndPoint);
            var ip = ipEndPoint.Address.ToString();
            while (!_shouldStop)
            {
                try
                {
                    byte[] startBuf = new byte[40];
                    int length = -1;
                    length = MyTCP.AsyncReceiveFromClient(sokClient, startBuf.Length, out startBuf);
                    if (length > 0)
                    {
                        if (length == 40 && BitConverter.ToInt32(startBuf, 0) == 0x4C4A4040)
                        {
                            machine[ip].alive = DateTime.Now;
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
                                length = MyTCP.AsyncReceiveFromClient(sokClient, msgLen - i, out readBuf);
                                if (length == 0)
                                    throw new Exception(clsMsg.getMsg("log_6"));
                                if (length == -1)
                                {
                                    CloseThread();
                                    break;
                                }
                                Array.Copy(readBuf, 0, bBuffer, i, length);
                                i += length;
                            }

                            if (cmd == 0x00A1)//传输数据请求命令 54字节
                            {
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = ipAndPort,
                                    Command = bBuffer.Take(54).ToArray(),
                                    MessageType = TCPMessageType.NET_SIMPLE
                                });
                                //请求回复
                                SendNetReply(0, success, machineByte, 0);
                            }
                            else if (cmd > 0 && cmd <= 10)//纸币信息发送命令 86+1644*N字节
                            {
                                ReceiveFileCnt++;
                                //接收文件数超过30将关闭连接
                                if (ReceiveFileCnt > 30)
                                {
                                    TCPEvent.OnCommandLog(new TCPMessage
                                    {
                                        IpAndPort = ipAndPort,
                                        MessageType = TCPMessageType.Reach_Max_File
                                    });
                                    CloseThread();
                                    break;
                                }
                                //报文体长度
                                int bodyLen = msgLen - 84 - 2;
                                //文件长度大于等于5M,将关闭连接
                                if (bodyLen / 1024 / 1024 >= 5)
                                {
                                    TCPEvent.OnCommandLog(new TCPMessage
                                    {
                                        IpAndPort = ipAndPort,
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
                                    IpAndPort = ipAndPort,
                                    Command = bBuffer.Take(200).ToArray(),
                                    MessageType = TCPMessageType.NET_UP
                                });
                                SendNetReply(cmd, success, machineByte, 0);
                                byte[] Fsn = new byte[bodyLen];
                                Array.Copy(bBuffer, 84, Fsn, 0, bodyLen);

                                //获取保留字reserve首字节，作为图像标志位
                                int imageType = bBuffer[40];
                                CopeWithNetUp(Fsn, ip, machineNo, imageType);


                            }
                            else if (cmd == 0x0010)//时间同步 42字节
                            {
                                //发送回复信息
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = ipAndPort,
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
                                    IpAndPort = ipAndPort,
                                    Command = bBuffer,
                                    MessageType = TCPMessageType.NET_CONTINUE
                                });
                                CloseThread();
                            }
                            else if (cmd == 0x00A2)//传输数据结束命令 44字节
                            {
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = ipAndPort,
                                    Command = bBuffer.Take(44).ToArray(),
                                    MessageType = TCPMessageType.NET_CLOSE
                                });
                                CloseThread();
                            }
                            else if (cmd == 0X000D) //假币预警版本请求命令
                            {
                                FakeVer = BitConverter.ToInt32(bBuffer, 40);
                                FakeVer++;
                                int FakeCnt = 1;
                                string FakeTime=DateTime.Now.ToString("yyyyMMddHHmmss");
                                SendNetFakeReply(cmd, success, machineByte, FakeVer, FakeCnt,FakeTime);
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = ipAndPort,
                                    Command = bBuffer.Take(46).ToArray(),
                                    MessageType = TCPMessageType.NET_FAKE_VER
                                });
                                CloseThread();
                            }
                            else if (cmd == 0X000E) //假币预警下载请求命令
                            {
                                FakeVer = BitConverter.ToInt32(bBuffer, 40);
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = ipAndPort,
                                    Command = bBuffer.Take(46).ToArray(),
                                    MessageType = TCPMessageType.NET_FAKE_DWN
                                });
                                for (int kk = 0; kk < 1000; kk++)
                                {
                                    Guid g = Guid.NewGuid();
                                    string sign = g.ToString().Replace("-", "").ToUpper().Substring(0, 10);
                                    FakeList.Add(sign);
                                }
                                SendNetFakeDwnReply(cmd, success, machineByte, FakeVer, FakeList);
                                CloseThread();
                            }
                            else if (cmd == 0X000F) //假币预警下载完成请求命令
                            {
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = ipAndPort,
                                    Command = bBuffer.Take(48).ToArray(),
                                    MessageType = TCPMessageType.NET_FAKE_CMP
                                });
                                CloseThread();
                            }
                            else
                            {
                                TCPEvent.OnCommandLog(new TCPMessage
                                {
                                    IpAndPort = ipAndPort,
                                    Command = startBuf,
                                    MessageType = TCPMessageType.UnknownCommand
                                });
                            }
                        }
                        else
                        {
                            TCPEvent.OnCommandLog(new TCPMessage
                            {
                                IpAndPort = ipAndPort,
                                Command = startBuf,
                                MessageType = TCPMessageType.UnknownCommand
                            });
                        }
                    }
                    else
                    {
                        CloseThread();
                        break;
                    }
                }
                catch (Exception se)
                {
                    TCPEvent.OnCommandLog(new TCPMessage
                    {
                        IpAndPort = ipAndPort,
                        Message = MyLog.GetExceptionMsg(se, clsMsg.getMsg("log_7")),
                        MessageType = TCPMessageType.Exception
                    });
                    CloseThread();
                    break;
                }
                Thread.Sleep(100);
                if ((DateTime.Now - machine[ip].alive).TotalMinutes > 5)
                {
                    TCPEvent.OnCommandLog(new TCPMessage
                    {
                        IpAndPort = ipAndPort,
                        Message = string.Format(clsMsg.getMsg("log_8"), ipAndPort, machine[ip].alive.ToString("yyyy-MM-dd HH:mm:ss")),
                        MessageType = TCPMessageType.Out_Of_Date
                    });
                    CloseThread();
                }
            }
        }
        /// <summary>
        /// 关闭收数据线程
        /// </summary>
        private void CloseThread()
        {
            TCPEvent.OnCommandLog(new TCPMessage
            {
                IpAndPort = ipAndPort,
                MessageType = TCPMessageType.Thread_Close
            });
            _shouldStop = true;
            if (sokClient.Connected)
            {
                sokClient.Shutdown(SocketShutdown.Both);
                sokClient.Close();
            }
        }
        /// <summary>
        /// 处理纸币信息发送命令
        /// </summary>
        /// <param name="Fsn"></param>
        /// <param name="ip"></param>
        /// <param name="machineNo"></param>
        public void CopeWithNetUp(byte[] Fsn, string ip, string machineNo,int imageType)
        {
            DateTime date = DateTime.Now;
            string filePath = DataSaveFolder + "\\" + date.ToString("yyyyMMdd") + "\\" + date.ToString("HH") + "\\";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string fileName = "";
            if (imageType == 0)
            {
                fileName=filePath + date.ToString("mmssfff") +"-" + machineNo + ".FSN";
            }
            else
            {
                fileName = filePath + date.ToString("mmssfff") + "-" + machineNo + ".KY0";
            }
            
            //进行了交易控制，即指定了交易类型（如果收到前一天的数据是否应该排除呢？？2015.03.12）
            DateTime fileTime = FSNFormat.GetDateTime(Fsn);
            TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_22"), fileTime.ToString("yyyy-MM-dd HH:mm:ss")));
            if (machine[ip].startBusinessCtl)
                TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_23"), ip));
            if (machine[ip].startBusinessCtl && machine[ip].dateTime.AddHours(-2) < fileTime)
            {
                if (machine[ip].fileName == null || machine[ip].fileName == "")
                    machine[ip].fileName = fileName;
                else
                    fileName = machine[ip].fileName;

                if (machine[ip].business == BussinessType.KHDK)
                {
                    string tmpPath = machine[ip].tmpPath;
                    if (!Directory.Exists(tmpPath))
                    {
                        Directory.CreateDirectory(tmpPath);
                    }
                    List<string> modifyFiles = GZHImport.MergeLastFile(Fsn, tmpPath, machine[ip].bundleCount);
                    fileName = modifyFiles.FirstOrDefault();
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
                else if (!File.Exists(fileName))
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


                //删除文件
                if (!MySetting.GetProgramValue("SaveFile"))
                {
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                }
            }
            if (File.Exists(fileName))
            {
                //更新机具编号和机型
                string machineNumber = "", machineModel = "";
                string[] str = KyDataLayer2.GetMachineNumberFromFSN(fileName, out machineModel).Split("/".ToCharArray());
                if (str.Length == 3)
                    machineNumber = str[2];
                if (machine[ip].kMachineNumber != machineNumber && machineNumber != "" || machine[ip].kMachineModel != machineModel && machineModel != "")
                {
                    KyDataOperation.UpdateMachine(machine[ip].kId, machineNumber, machineModel);
                    machine[ip].kMachineNumber = machineNumber;
                    machine[ip].kMachineModel = machineModel;
                }

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
            Array.Copy(version, 0, returnBytes, 8, 2);//协议版本
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
        /// <summary>
        /// 假币请求回复命令
        /// </summary>
        /// <param name="requestCmd">请求命令</param>
        /// <param name="retCode">状态</param>
        /// <param name="MachineNo">机具编号</param>
        /// <param name="packageIndex">文件分次发送时序号</param>
        public void SendNetFakeReply(short requestCmd, byte[] retCode, byte[] MachineNo, int FakeVer,int FakeCnt,string FakeTime)
        {
            byte[] returnBytes = new byte[66];
            Array.Copy(start_work, 0, returnBytes, 0, 4);
            Array.Copy(BitConverter.GetBytes(returnBytes.Length), 0, returnBytes, 4, 4);
            Array.Copy(version, 0, returnBytes, 8, 2);//协议版本
            Array.Copy(BitConverter.GetBytes(requestCmd), 0, returnBytes, 10, 2);//requesCmd
            Array.Copy(retCode, 0, returnBytes, 12, 2);//retCode
            Array.Copy(MachineNo, 0, returnBytes, 14, 28);
            //保留字 2字节 （42~43）
            Array.Copy(BitConverter.GetBytes(FakeVer), 0, returnBytes, 44, 4);
            Array.Copy(BitConverter.GetBytes(FakeCnt), 0, returnBytes, 48, 4);
            byte[] timeBytes = Encoding.ASCII.GetBytes(FakeTime);
            Array.Copy(timeBytes, 0, returnBytes, 52, 14);
            MyTCP.AsyncSendToClient(sokClient, returnBytes);
        }
        /// <summary>
        /// 假币下载请求回复回复命令
        /// </summary>
        /// <param name="requestCmd">请求命令</param>
        /// <param name="retCode">状态</param>
        /// <param name="MachineNo">机具编号</param>
        /// <param name="packageIndex">文件分次发送时序号</param>
        public void SendNetFakeDwnReply(short requestCmd, byte[] retCode, byte[] MachineNo, int FakeVer, List<string> FakeList)
        {
            byte[] returnBytes = new byte[56 + FakeList.Count * 14];
            Array.Copy(start_work, 0, returnBytes, 0, 4);
            Array.Copy(BitConverter.GetBytes(returnBytes.Length), 0, returnBytes, 4, 4);
            Array.Copy(version, 0, returnBytes, 8, 2);//协议版本
            Array.Copy(BitConverter.GetBytes(requestCmd), 0, returnBytes, 10, 2);//requesCmd
            Array.Copy(retCode, 0, returnBytes, 12, 2);//retCode
            Array.Copy(MachineNo, 0, returnBytes, 14, 28);
            //保留字 2字节 （42~43）
            Array.Copy(BitConverter.GetBytes(FakeVer), 0, returnBytes, 44, 4);
            Array.Copy(BitConverter.GetBytes(FakeList.Count), 0, returnBytes, 48, 4);//黑名单条数
            Array.Copy(BitConverter.GetBytes(0), 0, returnBytes, 52, 4);//通配符个数
            for (int i = 0; i < FakeList.Count; i++)
            {
                string sign = string.Format("{0,-12}\r\n", FakeList[i]); 
                Array.Copy(Encoding.ASCII.GetBytes(sign), 0, returnBytes, 56 + i * 14, 14);//黑名单条数
            }
            MyTCP.AsyncSendToClient(sokClient, returnBytes);
        }
    }
}
