﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using KyModel.Models;
using System.Linq;
using KyBll;
using KyBll.DBUtility;
namespace NodeServerAndManager.BaseWinform
{
    public partial class ServerSettings : MaterialSkin.Controls.MaterialForm
    {
        public string ServerIp="";
        public string PictureIp="";
        public string DeviceIp ="";
        public string LocalIp = "";
        public string PushIp = "";
        /// <summary>
        /// 等待窗体
        /// </summary>
        WaitingForm waitingForm = new WaitingForm();
        /// <summary>
        /// 委托关闭等待窗体
        /// </summary>
        public delegate void delloading();
        public void CloseLoading(IAsyncResult ar)
        {
            this.Invoke(new delloading(() => { waitingForm.Close(); }));
        }
        public ServerSettings()
        {
            InitializeComponent();
           
            
        }

        private void ServerSettings_Load(object sender, EventArgs e)
        {

            ServerIp = Properties.Settings.Default.ServerIp;
            PictureIp = Properties.Settings.Default.PictureIp;
            DeviceIp = Properties.Settings.Default.DeviceIp;
            LocalIp = Properties.Settings.Default.LocalIp;
            PushIp = Properties.Settings.Default.PushIp;
            if(LocalIp==""||LocalIp=="192.168.1.1")
            {
                LocalIp = GetLocalIP();
            }
            ipControl_Local.Text = LocalIp;
            ipControl_Server.Text = ServerIp;
            ipControl_Device.Text = DeviceIp;
            ipControl_Push.Text = PushIp;


            //启动服务后关闭等待窗体
            waitingForm.SetText("正在初始化，请稍候...");
            new Action(ServerSettingInit).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            cmb_imageServer.Text = PictureIp;

            txb_SphinxPort.Text = Properties.Settings.Default.ServerDbPort.ToString();
            txb_DevicePort.Text = Properties.Settings.Default.DeviceDbPort.ToString();
            txb_ImagePort.Text = Properties.Settings.Default.PicturtDbPort.ToString();
            txb_LocalPort.Text = Properties.Settings.Default.Port.ToString();
            txb_PushPort.Text = Properties.Settings.Default.PushPort.ToString();
            
        }

        /// <summary>
        /// 设置初始化
        /// </summary>
        private void ServerSettingInit()
        {
            if (DeviceIp != "0.0.0.0" && DeviceIp != "")
            {
                DbHelperMySQL.SetConnectionString(Properties.Settings.Default.DeviceIp, Properties.Settings.Default.DeviceDbPort, DbHelperMySQL.DataBaseServer.Device);
                bool result = KyDataOperation.TestConnectDevice();
                if (result)
                {
                    List<ky_imgserver> dt = KyDataOperation.GetAllImageServer();
                    cmb_imageServer.DataSource = dt;
                    cmb_imageServer.DisplayMember = "kIpAddress";

                    List<ky_node> dtNode = KyDataOperation.GetAllNode();
                    chkList_Node.DataSource = dtNode;
                    chkList_Node.DisplayMember = "kNodeName";
                    chkList_Node.ValueMember = "kId";
                    if (LocalIp != "" && LocalIp != "0.0.0.0")
                    {
                        string strMessage = "";
                        for (var i = 0; i < dtNode.Count; i++)
                        {
                            if (LocalIp == dtNode[i].kBindIpAddress)
                            {
                                chkList_Node.SetItemChecked(i, true);
                                strMessage += dtNode[i].kNodeName + ";";
                            }
                        }
                        txb_BindNode.Text = strMessage;
                    }
                }
            }
        }
        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            //本地IP 端口号
            Properties.Settings.Default.LocalIp = ipControl_Local.Text;
            Properties.Settings.Default.Port = int.Parse(txb_LocalPort.Text);
            //sphinx IP 端口号
            Properties.Settings.Default.ServerIp = ipControl_Server.Text;
            Properties.Settings.Default.ServerDbPort = int.Parse(txb_SphinxPort.Text);
            //设备服务器IP 端口号
            Properties.Settings.Default.DeviceIp = ipControl_Device.Text;
            Properties.Settings.Default.DeviceDbPort = int.Parse(txb_DevicePort.Text);
            //推送服务器IP 端口号
            Properties.Settings.Default.PushIp = ipControl_Push.Text;
            Properties.Settings.Default.PushPort = int.Parse(txb_PushPort.Text);
            //图片服务器IP 端口号
            Properties.Settings.Default.PictureIp = cmb_imageServer.Text;
            Properties.Settings.Default.PicturtDbPort = int.Parse(txb_ImagePort.Text);
            //修改自动更新配置文件
            IniFile g = new IniFile(Environment.CurrentDirectory + "/config.ini");
            string url = g.ReadString("NETWORK", "URL", "");
            g.WriteString("NETWORK", "URL", "http://"+Properties.Settings.Default.DeviceIp.ToString()+":8888/update/");
            Properties.Settings.Default.Save();
            //先清除已绑定的项
            //List<ky_node> nodes = KyDataOperation.GetNodeWithBindIp(ipControl_Local.Text);
            //int[] ids = (from node in nodes select node.kId).ToArray();
            //KyDataOperation.UpdateNodeTable(ids, "");
            //获取被选中的项
            List<int> ids=new List<int>();
            foreach (ky_node dr in chkList_Node.CheckedItems)
            {
                ids.Add(Convert.ToInt32(dr.kId));
            }
            if(ids.Count>0)
            {
                List<ky_node> selectNodes = KyDataOperation.GetNodeWithIds(ids);
                string strMessage = "";
                List<int> IDS = new List<int>();
                foreach (ky_node selectNode in selectNodes)
                {
                    string bindIp = selectNode.kBindIpAddress;
                    if (bindIp != "" && bindIp != ipControl_Local.Text)
                    {
                        strMessage += string.Format("{0} 已经绑定了IP：{1}", selectNode.kNodeName, bindIp) + Environment.NewLine;
                    }
                    else
                    {
                        IDS.Add(Convert.ToInt32(selectNode.kId));
                    }
                }
                if (strMessage != "")
                {
                    strMessage += "是否继续？";
                    if (DialogResult.Yes == MessageBox.Show(strMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        bool success = KyDataOperation.UpdateNodeTable(ids, ipControl_Local.Text);
                        if (success)
                            MessageBox.Show("绑定成功！");
                        else
                            MessageBox.Show("绑定失败！");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    bool success = KyDataOperation.UpdateNodeTable(ids, ipControl_Local.Text);
                    if (success)
                        MessageBox.Show("绑定成功！");
                    else
                        MessageBox.Show("绑定失败！");
                }
            }
            this.Close();
        }

        /// <summary>
        /// 测试Sphinx服务器的连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lab_ServerTest_Click(object sender, EventArgs e)
        {
            waitingForm.SetText("正在测试，请稍等...");
            new Action(SeverTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
        }
        private void SeverTest()
        {
            if (txb_SphinxPort.Text.Trim() == "")
            {
                MessageBox.Show("请设置端口号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            Properties.Settings.Default.ServerIp = ipControl_Server.Text;
            Properties.Settings.Default.ServerDbPort = int.Parse(txb_SphinxPort.Text);
            Properties.Settings.Default.Save();
            DbHelperMySQL.SetConnectionString(Properties.Settings.Default.ServerIp, Properties.Settings.Default.ServerDbPort, DbHelperMySQL.DataBaseServer.Sphinx);

            bool result = KyDataOperation.TestConnectServer();
            if (result)
                MessageBox.Show("数据库服务器连接成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            else
                MessageBox.Show("数据库服务器连接失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
        /// <summary>
        /// 测试设备服务器的连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lab_DeviceTest_Click(object sender, EventArgs e)
        {
            waitingForm.SetText("正在测试，请稍等...");
            new Action(DeviceTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            
        }
        private void DeviceTest()
        {
            if (txb_DevicePort.Text.Trim() == "")
            {
                MessageBox.Show("请设置端口号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            Properties.Settings.Default.DeviceIp = ipControl_Device.Text;
            Properties.Settings.Default.DeviceDbPort = int.Parse(txb_DevicePort.Text);
            Properties.Settings.Default.Save();
            DbHelperMySQL.SetConnectionString(Properties.Settings.Default.DeviceIp, Properties.Settings.Default.DeviceDbPort, DbHelperMySQL.DataBaseServer.Device);
            bool result = KyDataOperation.TestConnectDevice();
            if (result)
            {
                MessageBox.Show("设备服务器连接成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                List<ky_imgserver> dt = KyDataOperation.GetAllImageServer();
                cmb_imageServer.DataSource = dt;
                cmb_imageServer.DisplayMember = "kIpAddress";
                List<ky_node> dtNode = KyDataOperation.GetAllNode();
                chkList_Node.DataSource = dtNode;
                chkList_Node.DisplayMember = "kNodeName";
                chkList_Node.ValueMember = "kId";
            }
            else
                MessageBox.Show("设备服务器连接失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
        /// <summary>
        /// 测试推送服务器的连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lab_PushTest_Click(object sender, EventArgs e)
        {
            waitingForm.SetText("正在测试，请稍等...");
            new Action(PushTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
        }
        private void PushTest()
        {
            if (txb_PushPort.Text.Trim() == "")
            {
                MessageBox.Show("请设置端口号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            string PushIp = ipControl_Push.Text;
            int PushPort = int.Parse(txb_PushPort.Text);
            bool result = KyDataOperation.TestConnectPush(PushIp, PushPort);
            if (result)
            {
                MessageBox.Show("推送服务器连接成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.PushIp = PushIp;
                Properties.Settings.Default.PushPort = PushPort;
                Properties.Settings.Default.Save();
            }
            else
                MessageBox.Show("推送服务器连接失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
        /// <summary>
        /// 测试图像服务器的连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lab_PictureTest_Click(object sender, EventArgs e)
        {
            waitingForm.SetText("正在测试，请稍等...");
            new Action(PictureTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
         }
        private void PictureTest() 
        {
            Properties.Settings.Default.PictureIp = cmb_imageServer.Text;
            Properties.Settings.Default.PicturtDbPort = int.Parse(txb_ImagePort.Text);
            Properties.Settings.Default.Save();
            DbHelperMySQL.SetConnectionString(Properties.Settings.Default.PictureIp, Properties.Settings.Default.PicturtDbPort, DbHelperMySQL.DataBaseServer.Image);
            bool result = KyDataOperation.TestConnectImage();
            if (result)
                MessageBox.Show("图像服务器连接成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            else
                MessageBox.Show("图像服务器连接失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        
        }

        private void cmb_imageServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)cmb_imageServer.DataSource;
            string pictureIp = cmb_imageServer.Text;
            if(dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["kIpAddress"].ToString()==pictureIp)
                    {
                        txb_ImagePort.Text = dt.Rows[i]["kPort"].ToString();
                    }
                }
            }
        }

        private void txb_SphinxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;  //不为数字就移除
            }
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        private static string GetLocalIP() //获取本地IP
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            for (int i = 0; i < ipHost.AddressList.Length; i++)
            {
                if (ipHost.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddr = ipHost.AddressList[i];
                }
            }
            //IPAddress ipAddr = ipHost.AddressList[0];
            return ipAddr.ToString();
        }

        private void chkList_Node_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkList_Node.CheckedItems.Count > 0)
            {
                string txt = "";
                foreach (ky_node dr in chkList_Node.CheckedItems)
                {
                    txt += dr.kNodeName.ToString() + ";";
                }
                txb_BindNode.Text = txt;
            }
            else
            {
                txb_BindNode.Text = "";
            }
        }

        private void chkList_Node_Click(object sender, EventArgs e)
        {
            
        }

       

    }
}
