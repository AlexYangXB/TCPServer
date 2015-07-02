using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialSkin;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using KyBll;
using KyModel;
namespace MyTest
{
    
    public partial class TCPClient : MaterialSkin.Controls.MaterialForm
    {
        public class StateObject
        {
            public Socket workSocket = null;
            public TCPMessageType MessageType = TCPMessageType.NET_CONTINUE;

        }
        private readonly MaterialSkinManager materialSkinManager;
        public TCPClient()
        {
            materialSkinManager = MaterialSkinManager.Instance;
            //蓝色
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
            InitializeComponent();
        }
        public static byte[] start_work = new byte[] { 0x40, 0x40, 0x4A, 0x4C };
        public static byte[] msg_length = new byte[] { 0x2E, 0x00, 0x00, 0x00 };
        public static byte[] msg_len_TimeSync = new byte[] { 0x3A, 0x00, 0x00, 0x00 };
        public static byte[] version = new byte[] { 0x0F, 0x00 };
        public static byte[] success = new byte[] { 0xF0, 0x00 };
        public static byte[] NET_CONTINUE = new byte[] { 0x0B, 0x00 };
        public static byte[] NET_SIMPLE = new byte[] { 0xA1, 0x00 };
        public static byte[] NET_UP = new byte[] { 0x06, 0x00 };
        public static byte[] NET_CLOSE = new byte[] { 0xA2, 0x00 };
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
           
            
        }
        private static ManualResetEvent connectDone =new ManualResetEvent(false);
        private static ManualResetEvent sendDone =new ManualResetEvent(false);
        private DateTime start;
        private void StartClient()
        {
            try
            {
                int CycleTime = Convert.ToInt32(MyTest.Properties.Settings.Default.CycleTime);
                for (int i = 0; i < CycleTime; i++)
                {
                    SetButton();
                    connectDone.Reset();
                    var ipAddress = IPAddress.Parse(MyTest.Properties.Settings.Default.ServerIp);
                    var port = Convert.ToInt32(MyTest.Properties.Settings.Default.ServerPort);
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                    Socket client = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);

                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(KyBll.Log.GetExceptionMsg(e, "连接服务器异常"));
            }
        }
        private int FileCnt = 0;
        private  void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                client.EndConnect(ar);
               
                sendDone.Reset();
                StateObject StaObject = new StateObject() { MessageType=TCPMessageType.NET_SIMPLE,workSocket=client};
                Send(new StateObject() { MessageType=TCPMessageType.NET_SIMPLE,workSocket=client}, NetSimpleCmd());
                sendDone.WaitOne();
                FileCnt = Convert.ToInt32(MyTest.Properties.Settings.Default.FileCnt);
                
                for (int i = 0; i < FileCnt; i++)
                {
                    sendDone.Reset();
                    int Value = Convert.ToInt32(MyTest.Properties.Settings.Default.Value);
                    int PerFileCount = Convert.ToInt32(MyTest.Properties.Settings.Default.PerFileCount);
                    byte[] fsn = MakeFsn.ExportFSN(Value, PerFileCount, MyTest.Properties.Settings.Default.MachineNumber);
                    Send(new StateObject() { MessageType=TCPMessageType.NET_UP,workSocket=client}, NetUpCmd(fsn));
                    sendDone.WaitOne();
                }
                sendDone.Reset();
                Send(new StateObject() { MessageType = TCPMessageType.NET_CLOSE, workSocket = client }, NetCloseCmd());
                //client.Shutdown(SocketShutdown.Both);
                //client.Close();
                sendDone.WaitOne();
                SetButton();
                connectDone.Set();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(KyBll.Log.GetExceptionMsg(e, "连接服务器异常"));
            }
        }
        private void Send(StateObject StaObject, byte[] data)
        {

            StaObject.workSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), StaObject);
        }
        private  void SendCallback(IAsyncResult ar)
        {
            try
            {
                StateObject StaObject = (StateObject)ar.AsyncState;

                int bytesSent = StaObject.workSocket.EndSend(ar);

                if (StaObject.MessageType == TCPMessageType.NET_SIMPLE)
                {
                    LogStr("发送传输数据请求命令");
                }
                if (StaObject.MessageType == TCPMessageType.NET_UP)
                {
                    LogStr("发送纸币信息发送命令");
                    
                }
                if (StaObject.MessageType == TCPMessageType.NET_CLOSE)
                {
                    LogStr("发送数据传输结束命令");
                }
                var sc = (DateTime.Now - start).TotalMilliseconds;
                LogStr(string.Format("发送 {0} 字节,用时{1}ms ", bytesSent,sc));
                start = DateTime.Now;
                sendDone.Set();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public  byte[] NetSimpleCmd()
        {
            byte[] returnbytes = new byte[54];
            byte[] machineByte = System.Text.Encoding.ASCII.GetBytes(MyTest.Properties.Settings.Default.MachineNumber);
            Array.Copy(start_work, 0, returnbytes, 0, 4);
            Array.Copy(BitConverter.GetBytes(returnbytes.Length), 0, returnbytes, 4, 4);
            Array.Copy(version, 0, returnbytes, 8, 2);
            Array.Copy(NET_SIMPLE, 0, returnbytes, 10, 2);
            Array.Copy(machineByte, 0, returnbytes, 12, machineByte.Length);
            return returnbytes;
        }
        public  byte[] NetCloseCmd()
        {
            byte[] returnbytes = new byte[44];
            byte[] machineByte = System.Text.Encoding.ASCII.GetBytes(MyTest.Properties.Settings.Default.MachineNumber);
            Array.Copy(start_work, 0, returnbytes, 0, 4);
            Array.Copy(BitConverter.GetBytes(returnbytes.Length), 0, returnbytes, 4, 4);
            Array.Copy(version, 0, returnbytes, 8, 2);
            Array.Copy(NET_CLOSE, 0, returnbytes, 10, 2);
            Array.Copy(machineByte, 0, returnbytes, 12, machineByte.Length);
            return returnbytes;
        }
        public  byte[] NetUpCmd(byte[] fsn)
        {
            byte[] returnbytes = new byte[fsn.Length+86];
            byte[] machineByte = System.Text.Encoding.ASCII.GetBytes(MyTest.Properties.Settings.Default.MachineNumber);
            Array.Copy(start_work, 0, returnbytes, 0, 4);
            Array.Copy(BitConverter.GetBytes(returnbytes.Length), 0, returnbytes, 4, 4);
            Array.Copy(version, 0, returnbytes, 8, 2);
            Array.Copy(NET_UP, 0, returnbytes, 10, 2);
            Array.Copy(machineByte, 0, returnbytes, 12, machineByte.Length);
            Array.Copy(BitConverter.GetBytes(1000), 0, returnbytes, 62,2 );
            Array.Copy(fsn, 0, returnbytes, 84, fsn.Length);
            return returnbytes;
        }
        public  byte[] HeartBeatCmd()
        {
            byte[] returnbytes = new byte[44];
            byte[] machineByte=System.Text.Encoding.ASCII.GetBytes(MyTest.Properties.Settings.Default.MachineNumber);
            Array.Copy(start_work, 0, returnbytes, 0, 4);
            Array.Copy(BitConverter.GetBytes(returnbytes.Length), 0, returnbytes, 4, 4);
            Array.Copy(version, 0, returnbytes, 8, 2);
            Array.Copy(NET_CONTINUE, 0, returnbytes, 10, 2);
            Array.Copy(machineByte, 0, returnbytes, 12, machineByte.Length);
            return returnbytes;
        }
        private void TCPClient_Load(object sender, EventArgs e)
        {
            tf_FileCnt.Text = MyTest.Properties.Settings.Default.FileCnt;
            tf_PerFileCount.Text = MyTest.Properties.Settings.Default.PerFileCount;
            tf_MachineNumber.Text = MyTest.Properties.Settings.Default.MachineNumber;
            tf_Value.Text = MyTest.Properties.Settings.Default.Value;
            tf_ServerIp.Text = MyTest.Properties.Settings.Default.ServerIp;
            tf_ServerPort.Text = MyTest.Properties.Settings.Default.ServerPort;
            tf_CycleTime.Text = MyTest.Properties.Settings.Default.CycleTime;
        }
        public delegate void LogInvoke(string str);
        private void LogStr(string str)
        {
            //判断控件是否在本线程内
            if (this.rb_Log.InvokeRequired)
            {
                LogInvoke _myInvoke = new LogInvoke(LogStr);
                this.Invoke(_myInvoke, new object[] { str });
            }
            else
            {
                rb_Log.AppendText(DateTime.Now.ToString("[ yyyy-MM-dd HH:mm:ss.fff ] ")+str+Environment.NewLine);
                rb_Log.Focus();
            }
        }
        public delegate void ButtonInvoke();
        private void SetButton()
        {
            //判断控件是否在本线程内
            if (this.btn_Start.InvokeRequired)
            {
                ButtonInvoke _myInvoke = new ButtonInvoke(SetButton);
                this.Invoke(_myInvoke);
            }
            else
            {
                if (btn_Start.Enabled)
                    btn_Start.Enabled = false;
                else
                    btn_Start.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToInt32(1024*1024)+"");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MyTest.Properties.Settings.Default.FileCnt = tf_FileCnt.Text;
            MyTest.Properties.Settings.Default.PerFileCount = tf_PerFileCount.Text;
            MyTest.Properties.Settings.Default.MachineNumber = tf_MachineNumber.Text;
            MyTest.Properties.Settings.Default.Value = tf_Value.Text;
            MyTest.Properties.Settings.Default.ServerIp = tf_ServerIp.Text;
            MyTest.Properties.Settings.Default.ServerPort = tf_ServerPort.Text;
            MyTest.Properties.Settings.Default.CycleTime = tf_CycleTime.Text;
            MyTest.Properties.Settings.Default.Save();
            Thread client = new Thread(StartClient);
            client.IsBackground = true;
            client.Start();
        }
    }
}
