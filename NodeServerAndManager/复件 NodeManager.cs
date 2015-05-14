using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using KyData.DbTable;
using MyTcpServer;
using NodeServerAndManager.BaseWinform;
using System.Runtime.InteropServices;
using Utility.DBUtility;

namespace NodeServerAndManager
{
    public partial class NodeManager : Form
    {
        public NodeManager()
        {
            InitializeComponent();
        }
        //用于收数据的server端
        private MyTcpServerClass myTcpServer = new MyTcpServerClass();
        private string path = Application.StartupPath + "\\FsnFloder";

        //当前网点ID，登录的时候获取的用户所在网点
        private int localNodeId = 0;
        private string userNumber = "";//当前登录的用户编号
        private int userId = 0;//当前登录的用户的ID

        #region 无边框窗体   移动  添加右键菜单 
        //http://blog.csdn.net/ku_cha_cha/article/details/6697131 详情参考该网址
        //移动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0); 
        }

        //添加右键菜单
        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        #endregion

        #region 登录界面
        private void lab_ServerSettings_Click(object sender, EventArgs e)
        {
            BaseWinform.ServerSettings frm = new ServerSettings();

            frm.ServerIp = Properties.Settings.Default.ServerIp;
            frm.PictureIp = Properties.Settings.Default.PictureIp;
            frm.DeviceIp = Properties.Settings.Default.DeviceIp;
            frm.ShowDialog();
            //设置数据库连接字符串
            Utility.DBUtility.DbHelperMySQL.SetConnectionString(Properties.Settings.Default.ServerIp,Properties.Settings.Default.ServerDbPort, DbHelperMySQL.DataBaseServer.Sphinx);
            Utility.DBUtility.DbHelperMySQL.SetConnectionString(Properties.Settings.Default.DeviceIp,Properties.Settings.Default.DeviceDbPort, DbHelperMySQL.DataBaseServer.Device);
            Utility.DBUtility.DbHelperMySQL.SetConnectionString(Properties.Settings.Default.PictureIp,Properties.Settings.Default.PicturtDbPort, DbHelperMySQL.DataBaseServer.Image);
        }
        //基本设置
        private void lab_Setting_Click(object sender, EventArgs e)
        {
            string localIp = Properties.Settings.Default.LocalIp;
            int port = Properties.Settings.Default.Port;

            BaseWinform.Setting frm=new Setting();
            frm.LocalIp = Properties.Settings.Default.LocalIp;
            frm.Port = Properties.Settings.Default.Port;
            frm.ShowDialog();
            //本地IP或者端口号改过之后，要重新启动 TcpServer端
            if(localIp!=Properties.Settings.Default.LocalIp||port!=Properties.Settings.Default.Port)
            {
                if(myTcpServer.IsRunning)
                {
                    myTcpServer.Stop();
                    DataTable dt = Utility.KyDataOperation.GetAllMachine();
                    myTcpServer.DTable = dt;
                    myTcpServer.DataSaveFolder = path;
                    int pictureServerId = Utility.KyDataOperation.GetPictureServerId(Properties.Settings.Default.PictureIp);
                    myTcpServer.PictureServerId = pictureServerId;
                    myTcpServer.StartListenling(Properties.Settings.Default.LocalIp, Properties.Settings.Default.Port);
                }
            }
        }

        //登录
        private void btn_Login_Click(object sender, EventArgs e)
        {
            string user = txb_User.Text.Trim();
            string passWord = txb_PassWord.Text.Trim(); 
            //加密
            passWord=Utility.KyDataOperation.Md5(passWord);

            DataTable dt = Utility.KyDataOperation.GetUser(user);
            if(dt.Rows.Count>0)
            {
                if (dt.Rows[0]["kPassWord"].ToString() == passWord)
                {
                    localNodeId = (int) dt.Rows[0]["kNodeId"];
                    userNumber = user;
                    userId = (int) dt.Rows[0]["kId"];
                    txb_User.Text = "";
                    txb_PassWord.Text = "";
                    tabControl1.SelectedIndex = 1;
                }
                else
                {
                    MessageBox.Show("密码错误,请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txb_PassWord.Focus();
                }
            }
            else
            {
                MessageBox.Show("用户不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txb_User.Focus();
            }

        }
        #endregion

        #region 公共部分
        private void NodeManager_Load(object sender, EventArgs e)
        {
            //无边框窗体添加右键菜单
            //int WS_SYSMENU = 0x00080000;
            //int WS_MINIMIZEBOX = 0x20000; // 最大最小化按钮
            //int windowLong = (GetWindowLong(new HandleRef(this, this.Handle), -16));
            //SetWindowLong(new HandleRef(this, this.Handle), -16, windowLong | WS_SYSMENU | WS_MINIMIZEBOX);
            
            CheckForIllegalCrossThreadCalls = false;
            myTcpServer.CmdEvent += new EventHandler<MyTcpServerClass.CmdEventArgs>(myTcpServer_CmdEvent);

            //打开定时器
            timer_UpdateMachine.Start();


            //绘制最小化、退出按钮
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(1, 1, 33, 33);
            gp.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            btn_Exit.Region = new Region(gp);
            btn_Minimize.Region = new Region(gp);

            Utility.DBUtility.DbHelperMySQL.SetConnectionString(Properties.Settings.Default.ServerIp,Properties.Settings.Default.ServerDbPort,DbHelperMySQL.DataBaseServer.Sphinx);
            Utility.DBUtility.DbHelperMySQL.SetConnectionString(Properties.Settings.Default.DeviceIp,Properties.Settings.Default.DeviceDbPort,DbHelperMySQL.DataBaseServer.Device);
            Utility.DBUtility.DbHelperMySQL.SetConnectionString(Properties.Settings.Default.PictureIp,Properties.Settings.Default.PicturtDbPort, DbHelperMySQL.DataBaseServer.Image);
            
            //设置收数据的Server端
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                if (Properties.Settings.Default.ServerIp != "" && Properties.Settings.Default.DeviceIp != "" && Properties.Settings.Default.PictureIp != "" && Properties.Settings.Default.LocalIp != "")
                {
                    DataTable dt = Utility.KyDataOperation.GetAllMachine();
                    myTcpServer.DTable = dt;
                    myTcpServer.DataSaveFolder = path;
                    int pictureServerId = Utility.KyDataOperation.GetPictureServerId(Properties.Settings.Default.PictureIp);
                    myTcpServer.PictureServerId = pictureServerId;
                    myTcpServer.StartListenling(Properties.Settings.Default.LocalIp, Properties.Settings.Default.Port);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("服务器未正常启动，请查看‘基本设置’‘服务器设置’是否正确？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            
        }
        private void NodeManager_Activated(object sender, EventArgs e)
        {
            txb_User.Focus();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    lab_Tital.Text = "登录";
                    break;
                case 1:
                    lab_Tital.Text = "主界面";
                    break;
                case 2:
                    lab_Tital.Text = "交易控制";
                    //设备IP
                    DataTable dt;
                    if (localNodeId != 0)
                        dt = Utility.KyDataOperation.GetMachineWithNodeId(localNodeId);
                    else
                        dt = Utility.KyDataOperation.GetAllMachine();
                    cmb_MachineIp.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmb_MachineIp.Items.Add(dt.Rows[i]["kIpAddress"].ToString());
                    }
                    //ATM编号
                    DataTable dtATM;
                    if (localNodeId != 0)
                        dtATM = Utility.KyDataOperation.GetAtmWithNodeId(localNodeId);
                    else
                        dtATM = Utility.KyDataOperation.GetAllAtm();
                    cmb_ATM.Items.Clear();
                    for (int i = 0; i < dtATM.Rows.Count;i++ )
                    {
                        string str = string.Format("{0},{1}", dtATM.Rows[i]["kId"].ToString(),
                                                   dtATM.Rows[i]["kATMNumber"].ToString());
                        cmb_ATM.Items.Add(str);
                    }
                    //钞箱编号
                    DataTable dtCashBox = Utility.KyDataOperation.GetAllCashBox();
                    cmb_CashBox.Items.Clear();
                    for (int i = 0; i < dtCashBox.Rows.Count; i++)
                    {
                        string str = string.Format("{0},{1}", dtCashBox.Rows[i]["kId"].ToString(),
                                                   dtCashBox.Rows[i]["kCashBoxNumber"].ToString());
                        cmb_CashBox.Items.Add(str);
                    }

                    break;
                case 3:
                    lab_Tital.Text = "冠字号文件上传";
                    //所属厂家
                    DataTable dtFactory = Utility.KyDataOperation.GetAllFactory();
                    cmb_Factory.Items.Clear();
                    for(int i=0;i<dtFactory.Rows.Count;i++)
                    {
                        string str = string.Format("{0},{1}", dtFactory.Rows[i]["kId"].ToString(),
                                                   dtFactory.Rows[i]["kFactoryName"].ToString());
                        cmb_Factory.Items.Add(str);
                    }
                    //所属网点
                    DataTable dtNode = Utility.KyDataOperation.GetAllNode();
                    cmb_Node.Items.Clear();
                    for(int i=0;i<dtNode.Rows.Count;i++)
                    {
                        string str = string.Format("{0},{1}", dtNode.Rows[i]["kId"].ToString(),
                                                   dtNode.Rows[i]["kNodeName"].ToString());
                        cmb_Node.Items.Add(str);
                    }
                    //ATM编号
                    DataTable dtATM2 = Utility.KyDataOperation.GetAllAtm();
                    cmb_ATM2.Items.Clear();
                    for (int i = 0; i < dtATM2.Rows.Count;i++ )
                    {
                        string str = string.Format("{0},{1}", dtATM2.Rows[i]["kId"].ToString(),
                                                   dtATM2.Rows[i]["kATMNumber"].ToString());
                        cmb_ATM2.Items.Add(str);
                    }
                    //钞箱编号
                    DataTable dtCashBox2 = Utility.KyDataOperation.GetAllCashBox();
                    cmb_CashBox2.Items.Clear();
                    for (int i = 0; i < dtCashBox2.Rows.Count;i++ )
                    {
                        string str = string.Format("{0},{1}", dtCashBox2.Rows[i]["kId"].ToString(),
                                                   dtCashBox2.Rows[i]["kCashBoxNumber"].ToString());
                        cmb_CashBox2.Items.Add(str);
                    }
                    break;
                case 4:
                    lab_Tital.Text = "设备监控";
                    DataTable dtMachine = Utility.KyDataOperation.GetMachineStatus();
                    if(dtMachine.Rows.Count>0)
                    {
                        dgv_machine.DataSource = dtMachine;
                    }
                    break;
            }
        }

        //退出按钮
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    if (DialogResult.Yes == MessageBox.Show("退出软件后将无法接收纸币数据，是否退出软件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        this.Close();;
                    break;
                case 1:
                    if (DialogResult.Yes == MessageBox.Show("退出软件后将无法接收纸币数据，是否退出软件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        this.Close();
                    break;
                case 2:
                    if(!btn_Start.Enabled)
                    {
                        MessageBox.Show("交易未完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        tabControl1.SelectedIndex = 1;
                    break;
                default: tabControl1.SelectedIndex = 1;
                    break;
            }
        }
        //最小化按钮
        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion


        #region 主界面
        //注销
        private void btn_Logout_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            userId = 0;
            userNumber = "";
            localNodeId = 0;
        }
        //交易控制
        private void btn_BusinessControl_Click(object sender, EventArgs e)
        {
            lab_TotalAmount.Text = "0";
            lab_RealNote.Text = "0";
            lab_CounterfeitMoney.Text = "0";
            tabControl1.SelectedIndex = 2;
        }
        //冠字号码文件上传
        private void btn_InsertData_Click(object sender, EventArgs e)
        {
            txb_FilePath.Text = "";
            txb_Message.Text = "";
            tabControl1.SelectedIndex = 3;
        }
        //设备监控
        private void btn_MachineMonitoring_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }
        #endregion

        #region 交易控制
        void myTcpServer_CmdEvent(object sender, MyTcpServerClass.CmdEventArgs e)
        {
            lab_TotalAmount.Text = e.Amount.TotalValue.ToString();
            lab_RealNote.Text = e.Amount.TotalTrue.ToString();
            lab_CounterfeitMoney.Text = e.Amount.TotalErr.ToString();
        }

        private void cmb_Business_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Business.Text.IndexOf("ATM") != -1 || cmb_Business.Text.IndexOf("柜面") != -1)
            {
                
                if(cmb_Business.Text.IndexOf("ATM") != -1)
                {
                    label8.Text = "    ATM编号";
                    label9.Text = " 钞箱编号";
                    cmb_CashBox.Enabled = true;
                    cmb_ATM.Enabled = true;
                    cmb_ATM.Visible = true;
                    txb_BusinessSerial.Visible = false;
                }
                else if(cmb_Business.Text.IndexOf("柜面") != -1)
                {
                    label8.Text = " 业务流水号";
                    cmb_CashBox.Enabled = false;
                    cmb_ATM.Visible = false;
                    txb_BusinessSerial.Visible = true;
                }
            }
            else
            {
                label8.Text = "    ATM编号";
                cmb_ATM.Enabled = false;
                cmb_CashBox.Enabled = false;
                cmb_ATM.Visible = true;
                txb_BusinessSerial.Visible = false;
            }
        }
        //开始
        private void btn_Start_Click(object sender, EventArgs e)
        {
            lab_TotalAmount.Text ="0";
            lab_RealNote.Text = "0";
            lab_CounterfeitMoney.Text = "0";

            if(cmb_MachineIp.Text==""||cmb_Business.Text=="")
            {
                MessageBox.Show("请选择设备IP和交易类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //else if (cmb_Business.Text.IndexOf("ATM")!=-1&&ATM)
            //{
            //}
            else
            {
                if (cmb_Business.Text.IndexOf("ATM") != -1)
                {
                    if(cmb_ATM.Text==""||cmb_CashBox.Text=="")
                    {
                        MessageBox.Show("请选择ATM编号和钞箱编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                btn_End.Enabled = true;
                btn_Cancel.Enabled = true;
                btn_Start.Enabled = false;
                if (myTcpServer.IsRunning)
                {
                    KyData.DbTable.businessControl bControl = new businessControl();
                    bControl.dateTime = DateTime.Now;
                    bControl.userId = userId;
                    bControl.ip = cmb_MachineIp.Text;
                    switch (cmb_Business.SelectedIndex)
                    {
                        case 0:
                            bControl.business = "FK";
                            break;
                        case 1:
                            bControl.business = "SK";
                            break;
                        case 2:
                            bControl.business = "QK";
                            break;
                        case 3:
                            bControl.business = "CK";
                            break;
                        case 4:
                            bControl.business = "ATM";
                            string[] strATM = cmb_ATM.Text.Trim().Split(",".ToCharArray());
                            bControl.atmNumber = strATM[0];
                            string[] strCashBox=cmb_CashBox.Text.Trim().Split(",".ToCharArray());
                            bControl.cashBox = strCashBox[0];
                            break;
                        case 5:
                            bControl.business = "CACK";
                            string[] strATM2 = cmb_ATM.Text.Trim().Split(",".ToCharArray());
                            bControl.atmNumber = strATM2[0];
                            string[] strCashBox2=cmb_CashBox.Text.Trim().Split(",".ToCharArray());
                            bControl.cashBox = strCashBox2[0];
                            break;
                    }
                    myTcpServer.BusinessControl(MyTcpServerClass.MyBusinessStatus.Start, bControl);
                }
            }
            
        }
        //停止
        private void btn_End_Click(object sender, EventArgs e)
        {
            KyData.DbTable.businessControl bControl = new businessControl();
            bControl.ip = cmb_MachineIp.Text;
            myTcpServer.BusinessControl(MyTcpServerClass.MyBusinessStatus.End, bControl);
            btn_End.Enabled = false;
            btn_Cancel.Enabled = false;
            btn_Start.Enabled = true;
        }
        //取消
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            KyData.DbTable.businessControl bControl = new businessControl();
            bControl.ip = cmb_MachineIp.Text;
            myTcpServer.BusinessControl(MyTcpServerClass.MyBusinessStatus.Cancel, bControl);
            btn_End.Enabled = false;
            btn_Cancel.Enabled = false;
            btn_Start.Enabled = true;
        }
        #endregion

        #region 冠字号文件上传
        //选择文件
        private string[] uploadFiles;
        private void btn_Scan_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd=new OpenFileDialog();
            ofd.Filter = "纸币冠字号码文件*.FSN)|*.FSN";
            ofd.Multiselect = true;
            if(DialogResult.OK==ofd.ShowDialog())
            {
                string[] files = ofd.FileNames;
                uploadFiles = files;
                txb_FilePath.Text = "";
                foreach (var file in files)
                {
                    txb_FilePath.Text += file + ";";
                }
            }
        }
        
        //交易类型
        private void cmb_BusinessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_BusinessType.Text.IndexOf("ATM")!=-1)
            {
                cmb_ATM2.Enabled = true;
                cmb_CashBox2.Enabled = true;
            }
            else
            {
                cmb_ATM2.Enabled = false;
                cmb_CashBox2.Enabled = false;
            }
        }
        //确定
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (cmb_Factory.Text == "")
            {
                MessageBox.Show("请选择文件所属厂家", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(cmb_Node.Text=="")
            {
                MessageBox.Show("请选择所属网点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cmb_BusinessType.Text == "")
            {
                MessageBox.Show("请选择交易类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(cmb_BusinessType.Text.IndexOf("ATM") !=-1)
            {
                if(cmb_ATM2.Text==""||cmb_CashBox2.Text=="")
                {
                    MessageBox.Show("请选择ATM编号和钞箱编号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            string[] strTmp = cmb_Factory.Text.Trim().Split(",".ToCharArray());
            //所属厂家Id
            int factoryId = int.Parse(strTmp[0]);
            strTmp = cmb_Node.Text.Split(",".ToCharArray());
            //所属网点Id
            int nodeId = int.Parse(strTmp[0]);

            string AtmId = "", cashBoxId = "";

            //ATM Id
            strTmp = cmb_ATM2.Text.Trim().Split(",".ToCharArray());
            if(strTmp.Length>1)
                AtmId = strTmp[0];
            //钞箱Id
            strTmp = cmb_CashBox2.Text.Trim().Split(",".ToCharArray());
            if (strTmp.Length > 1)
                cashBoxId = strTmp[0];

            //交易类型
            string business = "";
            switch(cmb_BusinessType.SelectedIndex)
            {
                case 0:
                    business = "HM";
                    break;
                case 1:
                    business = "FK";
                    break;
                case 2:
                    business = "SK";
                    break;
                case 3:
                    business = "QK";
                    break;
                case 4:
                    business = "CK";
                    break;
                case 5:
                    business = "ATM";
                    break;
                case 6:
                    business = "CACK";
                    break;
            }

            foreach (var uploadFile in uploadFiles)
            {
                txb_Message.Text = "";
                string[] str=KyData.KyDataLayer2.GetMachineNumberFromFSN(uploadFile).Split("/".ToCharArray());
                string machineMac = str[str.Length - 1];
                if(machineMac=="")
                    continue;

                int machineId = 0;
                int machineId2 = 0;
                //获取数据库内的机具列表
                DataTable dtMachine = Utility.KyDataOperation.GetAllMachine();
                for(int i=0;i<dtMachine.Rows.Count;i++)
                {
                    if (machineMac == dtMachine.Rows[i]["kMachineNumber"].ToString())
                    {
                        machineId = (int)dtMachine.Rows[i]["kId"];
                        break;
                    }
                }
                if(machineId==0)//未在机具列表中找到该机具编号
                {
                    //获取数据库内的上传文件的机具列表
                    DataTable dtImportMachine = Utility.KyDataOperation.GetAllImportMachine();
                    for (int i = 0; i < dtImportMachine.Rows.Count; i++)
                    {
                        if(machineMac==dtImportMachine.Rows[i]["kMachineNumber"].ToString())
                        {
                            machineId2 = (int)dtImportMachine.Rows[i]["kId"];
                            break;
                        }
                    }
                    if (machineId2 == 0)//未在上传文件的机具列表中找到该机具编号
                    {
                       bool success= Utility.KyDataOperation.InsertMachineToImportMachine(machineMac, nodeId, factoryId);
                        if(success)
                        {
                            dtImportMachine = Utility.KyDataOperation.GetAllImportMachine();
                            for (int i = 0; i < dtImportMachine.Rows.Count; i++)
                            {
                                if (machineMac == dtImportMachine.Rows[i]["kMachineNumber"].ToString())
                                {
                                    machineId2 = (int)dtImportMachine.Rows[i]["kId"];
                                    break;
                                }
                            }
                        }
                    } 
                }
                machineData machineDataTmp = new machineData();
                machineDataTmp.machineNumber = machineMac;
                machineDataTmp.nodeId = nodeId;
                machineDataTmp.factoryId = factoryId;
                machineDataTmp.business = business;
                machineDataTmp.id = machineId;
                machineDataTmp.atmNumber = AtmId;
                machineDataTmp.cashBox = cashBoxId;
                machineDataTmp.userId = userId;
                int serverId = Utility.KyDataOperation.GetPictureServerId(Properties.Settings.Default.PictureIp);
                //string machineNum = "";
                bool result = Utility.SaveDataToDB.UploadFsn(uploadFile, serverId, machineDataTmp, machineId2);
                if (result)
                {
                    string strMessage = string.Format("{0},{1}导入成功\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                 Path.GetFileName(uploadFile));
                    txb_Message.Text += strMessage;
                }
                else
                {
                    string strMessage = string.Format("{0},{1}导入失败\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                 Path.GetFileName(uploadFile));
                    txb_Message.Text += strMessage;
                }
            }
        }

        /// <summary>
        /// 根据选择的网点，加载相应的ATM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_Node_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Node.Text != "")
            {
                string[] strNode = cmb_Node.Text.Split(",".ToCharArray());
                int nodeId = int.Parse(strNode[0]);
                //ATM编号
                DataTable dtATM2 = Utility.KyDataOperation.GetAtmWithNodeId(nodeId);
                cmb_ATM2.Items.Clear();
                for (int i = 0; i < dtATM2.Rows.Count; i++)
                {
                    string str = string.Format("{0},{1}", dtATM2.Rows[i]["kId"].ToString(),
                                               dtATM2.Rows[i]["kATMNumber"].ToString());
                    cmb_ATM2.Items.Add(str);
                }
            }
        }
        #endregion

        /// <summary>
        /// 每10分钟更新一次 从数据库中查询一次机具信息，并更新到Server端中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_UpdateMachine_Tick(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.DeviceIp!=""&&Utility.KyDataOperation.TestConnectDevice())
            {
                DataTable dt = Utility.KyDataOperation.GetAllMachine();
                myTcpServer.UpdateMachineTable(dt);
            }
        }

        private void txb_BusinessSerial_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            if ((e.KeyChar < 48 || (e.KeyChar > 57 && e.KeyChar < 65) || (e.KeyChar > 90 && e.KeyChar < 97) || e.KeyChar > 122) && e.KeyChar != 8)
            {
                e.Handled = true;  //不为数字或字母就移除
            }
        }

        #region 设备监控
        #endregion

    }
}
