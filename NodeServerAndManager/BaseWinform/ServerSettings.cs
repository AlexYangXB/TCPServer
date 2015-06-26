using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using KyBll;
using KyBll.DBUtility;
using KyModel.Models;
using Newtonsoft.Json;
using System.Threading;
namespace KangYiCollection.BaseWinform
{
    public partial class ServerSettings : MaterialSkin.Controls.MaterialForm
    {
        public List<int> bindNodeId = new List<int>();
        private bool rServerTest = false;
        private bool rDeviceTest = false;
        private bool rPushTest = false;
        private bool rImageTest = false;
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
            this.Invoke(new delloading(() =>
            {
                waitingForm.Close();
            }));
        }

        public ServerSettings()
        {
            InitializeComponent();
        }

        private void ServerSettings_Load(object sender, EventArgs e)
        {


            if (KangYiCollection.Properties.Settings.Default.LocalIp == "" || KangYiCollection.Properties.Settings.Default.LocalIp == "0.0.0.0")
            {
                KangYiCollection.Properties.Settings.Default.LocalIp = GetLocalIp();
            }
            ipControl_Local.Text = KangYiCollection.Properties.Settings.Default.LocalIp;
            ipControl_Server.Text = KangYiCollection.Properties.Settings.Default.ServerIp;
            ipControl_Device.Text = KangYiCollection.Properties.Settings.Default.DeviceIp;
            ipControl_Push.Text = KangYiCollection.Properties.Settings.Default.PushIp;


            txb_SphinxPort.Text = KangYiCollection.Properties.Settings.Default.ServerDbPort.ToString();
            txb_DevicePort.Text = KangYiCollection.Properties.Settings.Default.DeviceDbPort.ToString();
            txb_ImagePort.Text = KangYiCollection.Properties.Settings.Default.PicturtDbPort.ToString();
            txb_LocalPort.Text = KangYiCollection.Properties.Settings.Default.Port.ToString();
            txb_PushPort.Text = KangYiCollection.Properties.Settings.Default.PushPort.ToString();

            cmb_imageServer.Text = KangYiCollection.Properties.Settings.Default.PictureIp;
            Application.DoEvents();

            KangYiCollection.Properties.Settings.Default.DeviceIp = ipControl_Device.Text;
            KangYiCollection.Properties.Settings.Default.DeviceDbPort = int.Parse(txb_DevicePort.Text);
            KangYiCollection.Properties.Settings.Default.Save();
            waitingForm.SetText("正在初始化...");
            new Action(DeviceTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            SettingInit();
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            SaveSettings();
            //修改自动更新配置文件
            IniFile g = new IniFile(Application.StartupPath + "/config.ini");
            string url = g.ReadString("NETWORK", "URL", "");
            g.WriteString("NETWORK", "URL", "http://" + KangYiCollection.Properties.Settings.Default.DeviceIp.ToString() + ":8888/update/");

            //获取被选中的项
            List<int> ids = new List<int>();
            foreach (ky_node dr in chkList_Node.CheckedItems)
            {
                ids.Add(Convert.ToInt32(dr.kId));
            }
            if (ids.Count > 0)
            {
                if (rDeviceTest)
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
                    bool flag = true;
                    if (strMessage != "")
                    {
                        strMessage += "是否继续？";
                        if (DialogResult.No == MessageBox.Show(strMessage, "提示", MessageBoxButtons.YesNo))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        bool success = KyDataOperation.UpdateNodeTable(ids, ipControl_Local.Text);
                        if (success)
                        {
                            bindNodeId = ids;
                            MessageBox.Show("绑定成功！", "提示", MessageBoxButtons.OK);
                        }
                        else
                            MessageBox.Show("绑定失败！", "提示", MessageBoxButtons.OK);
                    }
                    else
                        return;
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
            if (txb_SphinxPort.Text.Trim() == "")
            {
                MessageBox.Show("请设置端口号", "提示", MessageBoxButtons.OK);
                return;
            }
            KangYiCollection.Properties.Settings.Default.ServerIp = ipControl_Server.Text;
            KangYiCollection.Properties.Settings.Default.ServerDbPort = int.Parse(txb_SphinxPort.Text);
            KangYiCollection.Properties.Settings.Default.Save();
            waitingForm = new WaitingForm();
            waitingForm.SetText("正在测试数据服务器...");
            new Action(SeverTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            if (rServerTest)
                MessageBox.Show("数据服务器连接成功!", "提示", MessageBoxButtons.OK);
            else
                MessageBox.Show("数据服务器连接失败!", "警告", MessageBoxButtons.OK);
        }
        /// <summary>
        /// 测试数据服务器的连接
        /// </summary>
        private void SeverTest()
        {
            try
            {
                DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.ServerIp, KangYiCollection.Properties.Settings.Default.ServerDbPort, DbHelperMySQL.DataBaseServer.Sphinx);
                rServerTest = KyDataOperation.TestConnectServer();
            }
            catch (Exception e)
            {
                Log.TestLog(Log.GetExceptionMsg(e, "连接数据服务器异常"));
            }

        }
        /// <summary>
        /// 测试设备服务器的连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lab_DeviceTest_Click(object sender, EventArgs e)
        {
            if (txb_DevicePort.Text.Trim() == "")
            {
                MessageBox.Show("请设置端口号", "提示", MessageBoxButtons.OK);
                return;
            }
            waitingForm.SetText("正在测试设备服务器...");
            KangYiCollection.Properties.Settings.Default.DeviceIp = ipControl_Device.Text;
            KangYiCollection.Properties.Settings.Default.DeviceDbPort = int.Parse(txb_DevicePort.Text);
            KangYiCollection.Properties.Settings.Default.Save();
            new Action(DeviceTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            if (rDeviceTest)
            {
                MessageBox.Show("设备服务器连接成功!", "提示", MessageBoxButtons.OK);

            }
            else
                MessageBox.Show("设备服务器连接失败!", "警告", MessageBoxButtons.OK);


        }
        /// <summary>
        /// 测试设备服务器的连接
        /// </summary>
        private void DeviceTest()
        {
            try
            {
                DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.DeviceIp, KangYiCollection.Properties.Settings.Default.DeviceDbPort, DbHelperMySQL.DataBaseServer.Device);
                rDeviceTest = KyDataOperation.TestConnectDevice();

            }
            catch (Exception e)
            {
                Log.TestLog(Log.GetExceptionMsg(e, "连接设备服务器异常"));
            }

        }
        /// <summary>
        /// 图像数据库、绑定网点初始化
        /// </summary>
        private void SettingInit()
        {
            if (rDeviceTest)
            {
                List<ky_imgserver> dt = KyDataOperation.GetAllImageServer();
                cmb_imageServer.DataSource = dt;
                cmb_imageServer.DisplayMember = "kIpAddress";

                List<ky_node> dtNode = KyDataOperation.GetAllNode();
                chkList_Node.DataSource = dtNode;
                chkList_Node.DisplayMember = "kNodeName";
                chkList_Node.ValueMember = "kId";

                if (KangYiCollection.Properties.Settings.Default.LocalIp != "" && KangYiCollection.Properties.Settings.Default.LocalIp != "0.0.0.0")
                {
                    string strMessage = "";
                    for (var i = 0; i < dtNode.Count; i++)
                    {
                        if (KangYiCollection.Properties.Settings.Default.LocalIp == dtNode[i].kBindIpAddress)
                        {
                            chkList_Node.SetItemChecked(i, true);
                            strMessage += dtNode[i].kNodeName + ";";
                        }
                    }
                    txb_BindNode.Text = strMessage;
                }
            }
            else
            {
                cmb_imageServer.DataSource = null;
                chkList_Node.DataSource = null;
                txb_BindNode.Text = "";
            }
        }
        /// <summary>
        /// 测试推送服务器的连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lab_PushTest_Click(object sender, EventArgs e)
        {
            if (txb_PushPort.Text.Trim() == "")
            {
                MessageBox.Show("请设置端口号", "提示", MessageBoxButtons.OK);
                return;
            }
            KangYiCollection.Properties.Settings.Default.PushIp = ipControl_Push.Text;
            KangYiCollection.Properties.Settings.Default.PushPort = int.Parse(txb_PushPort.Text);
            KangYiCollection.Properties.Settings.Default.Save();
            waitingForm.SetText("正在测试推送服务器...");
            new Action(PushTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            if (rPushTest)
            {
                MessageBox.Show("推送服务器连接成功!", "提示", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("推送服务器连接失败!", "警告", MessageBoxButtons.OK);
        }
        /// <summary>
        /// 测试推送服务器的连接
        /// </summary>
        private void PushTest()
        {
            try
            {
                rPushTest = KyDataOperation.TestConnectPush(KangYiCollection.Properties.Settings.Default.PushIp, KangYiCollection.Properties.Settings.Default.PushPort);
            }
            catch (Exception e)
            {
                Log.TestLog(Log.GetExceptionMsg(e, "连接推送服务器异常"));
            }
        }
        /// <summary>
        /// 测试图像服务器的连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lab_PictureTest_Click(object sender, EventArgs e)
        {
            if (txb_ImagePort.Text.Trim() == "")
            {
                MessageBox.Show("请设置端口号", "提示", MessageBoxButtons.OK);
                return;
            }
            KangYiCollection.Properties.Settings.Default.PictureIp = cmb_imageServer.Text;
            KangYiCollection.Properties.Settings.Default.PicturtDbPort = int.Parse(txb_ImagePort.Text);
            KangYiCollection.Properties.Settings.Default.Save();
            waitingForm.SetText("正在测试图像服务器...");
            new Action(PictureTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            if (rImageTest)
                MessageBox.Show("图像服务器连接成功!", "提示", MessageBoxButtons.OK);
            else
                MessageBox.Show("图像服务器连接失败!", "警告", MessageBoxButtons.OK);
        }
        /// <summary>
        /// 测试图像服务器的连接
        /// </summary>
        private void PictureTest()
        {

            try
            {
                DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.PictureIp, KangYiCollection.Properties.Settings.Default.PicturtDbPort, DbHelperMySQL.DataBaseServer.Image);
                rImageTest = KyDataOperation.TestConnectImage();
            }
            catch (Exception e)
            {
                Log.TestLog(Log.GetExceptionMsg(e, "连接图像服务器异常"));
            }



        }

        private void cmb_imageServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ky_imgserver> imgservers = (List<ky_imgserver>)cmb_imageServer.DataSource;
            string PictureIp = cmb_imageServer.Text;
            if (imgservers.Count > 0)
            {
                foreach (var imgserver in imgservers)
                {
                    if (imgserver.kIpAddress == PictureIp)
                    {
                        txb_ImagePort.Text = imgserver.kPort.ToString();
                    }
                }
            }
        }

        private void txb_Port_KeyPress(object sender, KeyPressEventArgs e)
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
        private static string GetLocalIp() //获取本地IP
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
        private void ipControl_Server_Leave(object sender, EventArgs e)
        {
            ipControl_Device.Value = ipControl_Server.Value;
            ipControl_Push.Value = ipControl_Server.Value;
            KangYiCollection.Properties.Settings.Default.DeviceIp = ipControl_Device.Text;
            KangYiCollection.Properties.Settings.Default.DeviceDbPort = int.Parse(txb_DevicePort.Text);
            KangYiCollection.Properties.Settings.Default.Save();
            waitingForm.SetText("正在载入数据...");
            new Action(DeviceTest).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            SettingInit();
            SaveSettings();
        }

        private void SaveSettings()
        {
            //本地IP 端口号
            KangYiCollection.Properties.Settings.Default.LocalIp = ipControl_Local.Text;
            KangYiCollection.Properties.Settings.Default.Port = int.Parse(txb_LocalPort.Text);
            //sphinx IP 端口号
            KangYiCollection.Properties.Settings.Default.ServerIp = ipControl_Server.Text;
            KangYiCollection.Properties.Settings.Default.ServerDbPort = int.Parse(txb_SphinxPort.Text);
            //设备服务器IP 端口号
            KangYiCollection.Properties.Settings.Default.DeviceIp = ipControl_Device.Text;
            KangYiCollection.Properties.Settings.Default.DeviceDbPort = int.Parse(txb_DevicePort.Text);
            //推送服务器IP 端口号
            KangYiCollection.Properties.Settings.Default.PushIp = ipControl_Push.Text;
            KangYiCollection.Properties.Settings.Default.PushPort = int.Parse(txb_PushPort.Text);
            //图片服务器IP 端口号
            KangYiCollection.Properties.Settings.Default.PictureIp = cmb_imageServer.Text;
            KangYiCollection.Properties.Settings.Default.PicturtDbPort = int.Parse(txb_ImagePort.Text);
            KangYiCollection.Properties.Settings.Default.Save();
        }

    }
}
