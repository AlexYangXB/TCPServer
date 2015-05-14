using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KyData.DbTable;
using MyTcpServer;
using Utility.DBUtility;

namespace MyTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
            myTcpServer.CmdEvent += new EventHandler<MyTcpServerClass.CmdEventArgs>(myTcpServer_CmdEvent);
        }

        void myTcpServer_CmdEvent(object sender, MyTcpServerClass.CmdEventArgs e)
        {
            //string message = "";
            //message = string.Format("命令:{0}\tIP:{1}\t机具编号:{2}\r\n\r\n",e.Cmd.ToString("X4"),e.IP,e.Machine);
            //txb_message.Text += message;
            //if(e.Cmd==0x00A1)
            //{
            //    lbOnline.Items.Add(e.IP);
            //}
            //else if(e.Cmd==0x00A2)
            //{
            //    if(lbOnline.Items.Contains(e.IP))
            //        lbOnline.Items.Remove(e.IP);
            //}
        }
        private MyTcpServerClass  myTcpServer=new MyTcpServerClass();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBeginListen_Click(object sender, EventArgs e)
        {
            myTcpServer.StartListenling(txtIp.Text.Trim(), int.Parse(txtPort.Text.Trim()));
            btnBeginListen.Enabled = false;
            btn_Close.Enabled = true;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            myTcpServer.Stop();
            btn_Close.Enabled = false;
            btnBeginListen.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "纸币冠字号码文件(*.DAT;*.FSN;*.KY0)|*.DAT;*.FSN;*.KY0|DAT|*.DAT|FSN|*.FSN|KY0|*.KY0";
                if(DialogResult.OK==ofd.ShowDialog())
                {
                    string fileName = ofd.FileName;
                    Utility.DBUtility.DbHelperMySQL.SetConnectionString("localhost",DbHelperMySQL.DataBaseServer.Sphinx);
                    Utility.DBUtility.DbHelperMySQL.SetConnectionString("localhost", DbHelperMySQL.DataBaseServer.Device);
                    Utility.DBUtility.DbHelperMySQL.SetConnectionString("localhost", DbHelperMySQL.DataBaseServer.Image);
                    List<KyData.DataBase.KYDataLayer1.SignTypeL2> signs=KyData.KyDataLayer2.ReadFromFSN(fileName);

                    Int64 batchId = KyData.KyDataLayer2.GuidToLongID();
                    bool result = Utility.KyDataOperation.InsertSign(batchId,1, signs);
                    if (result)
                        MessageBox.Show("成功");
                    else
                        MessageBox.Show("失败");
                    KyData.DbTable.ky_batch_sphinx batch=new ky_batch_sphinx();
                    batch.id = batchId;
                    batch.date = KyData.DateTimeAndTimeStamp.ConvertDateTimeInt(signs[0].Date);
                    batch.factory = 1;
                    batch.machine=new uint[1];
                    batch.machine[0] = 1;
                    batch.node = 1;
                    batch.type = "signrecord";
                    batch.recorduser = 1;
                    batch.totalnumber = 900;
                    batch.totalvalue = 9000;
                    batch.imgipaddress = 1;
                    batch.hjson = "";
                    result = Utility.KyDataOperation.InsertSignBatch(batch);
                    if (result)
                        MessageBox.Show("成功");
                    else
                        MessageBox.Show("失败");
                }
        }
    }
}
