using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using KyBll;
using KyData.DbTable;
using MyTcpServer;
using Newtonsoft.Json.Linq;
using NodeServerAndManager.BaseWinform;
using System.Runtime.InteropServices;
using Quobject.EngineIoClientDotNet.Modules;
using Quobject.SocketIoClientDotNet.Client;
using KyModel;
using KyModel.Models;
using System.Linq;
using Newtonsoft.Json;
using Utility;
namespace NodeServerAndManager
{
    public partial class NodeManager : Form
    {
        public NodeManager()
        {
            InitializeComponent();
        }
        //用于收数据的server端
        private TcpServer myTcpServer = new TcpServer();
        private string path = Application.StartupPath + "\\FsnFloder";

        //当前网点ID，登录的时候获取的用户所在网点
        private List<int> bindNodeId = new List<int>();  //绑定的网点ID
        private Dictionary<int, string> idIp = new Dictionary<int, string>();
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
            //记录本地IP与端口号，当本地IP与端口号发生改变时，重启TcpServer端
            string localIp = Properties.Settings.Default.LocalIp;
            int port = Properties.Settings.Default.Port;

            //记录推送IP与端口号，当推送IP与端口号发生改变时，
            string pushIp = Properties.Settings.Default.PushIp;
            int pushPort = Properties.Settings.Default.PushPort;
            //显示设置界面
            BaseWinform.ServerSettings frm = new ServerSettings();
            frm.ShowDialog();

            //设置数据库连接字符串
            DbHelperMySQL.SetConnectionString(Properties.Settings.Default.ServerIp, Properties.Settings.Default.ServerDbPort, DbHelperMySQL.DataBaseServer.Sphinx);
            DbHelperMySQL.SetConnectionString(Properties.Settings.Default.DeviceIp, Properties.Settings.Default.DeviceDbPort, DbHelperMySQL.DataBaseServer.Device);
            DbHelperMySQL.SetConnectionString(Properties.Settings.Default.PictureIp, Properties.Settings.Default.PicturtDbPort, DbHelperMySQL.DataBaseServer.Image);
            //本地IP或者端口号改过之后，要重新启动 TcpServer端
            if (localIp != Properties.Settings.Default.LocalIp || port != Properties.Settings.Default.Port)
            {
                if (myTcpServer.IsRunning)
                {
                    myTcpServer.Stop();
                    StartTcpServer();
                }
                else
                {
                    StartTcpServer();
                }
            }

            //当推送IP或端口号发生改变时
            if (pushIp != Properties.Settings.Default.PushIp || pushPort != Properties.Settings.Default.PushPort)
            {
                //停止socket
                if (socket != null)
                    socket.Close();
                //重现连接
                SocketIoConnect();
            }
        }

        //启动TcpServer 接收数据
        private bool StartTcpServer()
        {
            bool result = false;
            if (Properties.Settings.Default.ServerIp != "" && Properties.Settings.Default.DeviceIp != "" && Properties.Settings.Default.PictureIp != "" && Properties.Settings.Default.LocalIp != "")
            {
                //SPHINX数据服务器连接
                result = KyDataOperation.TestConnectServer();
                if (!result)
                {
                    MessageBox.Show("无法连接‘数据服务器’，请查看‘服务器设置’是否正确？", "提示", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    return false;
                }
                result = KyDataOperation.TestConnectDevice();
                //Device数据库连接
                if (!result)
                {
                    MessageBox.Show("无法连接‘设备服务器’，请查看‘服务器设置’是否正确？", "提示", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    return false;
                }
                //图像数据库
                result = KyDataOperation.TestConnectImage();
                if (!result)
                {
                    MessageBox.Show("无法连接‘图像数据库’，请查看‘服务器设置’是否正确？", "提示", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    return false;
                }
                try
                {
                    //获取绑定的网点ID
                    bindNodeId.Clear();
                    List<ky_node> nodes = KyDataOperation.GetNodeWithBindIp(Properties.Settings.Default.LocalIp);
                    bindNodeId = (from node in nodes select node.kId).ToList();
                    List<ky_machine> machineDt = new List<ky_machine>();
                    //获取绑定网点内的机器
                    if (bindNodeId.Count > 0)
                    {
                        int[] nodeIds = bindNodeId.ToArray();
                        machineDt = KyDataOperation.GetMachineWithNodeIds(nodeIds);
                    }
                    idIp.Clear();
                    foreach (ky_machine machine in machineDt)
                    {
                        int machineId = Convert.ToInt32(machine.kId);
                        string ip = machine.kIpAddress.Trim();
                        if (!idIp.ContainsKey(machineId))
                        {
                            idIp.Add(machineId, ip);
                        }
                    }
                    //启动TcpServer线程接收数据

                    if (result)
                    {
                        myTcpServer.DTable = machineDt;
                        myTcpServer.DataSaveFolder = path;
                        int pictureServerId = KyDataOperation.GetPictureServerId(Properties.Settings.Default.PictureIp);
                        myTcpServer.PictureServerId = pictureServerId;
                        myTcpServer.StartListenling(Properties.Settings.Default.LocalIp, Properties.Settings.Default.Port);
                        result = true;
                    }
                }
                catch (Exception e)
                {
                    Log.ConnectionException(e, "启动连接服务器异常！");
                    return false;
                }
            }
            return true;
        }

        //登录
        private void btn_Login_Click(object sender, EventArgs e)
        {
            string user = txb_User.Text.Trim();
            string passWord = txb_PassWord.Text.Trim();
            //加密
            passWord = KyDataOperation.Md5(passWord);

            ky_user dt = KyDataOperation.GetUser(user);
            if (dt != null)
            {
                if (dt.kPassWord == passWord)
                {
                    //localNodeId = Convert.ToInt32( dt.Rows[0]["kNodeId"]);
                    userNumber = user;
                    userId = Convert.ToInt32(dt.kId);
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
            myTcpServer.CmdEvent += new EventHandler<TcpServer.CmdEventArgs>(myTcpServer_CmdEvent);

            //打开定时器
            timer_UpdateMachine.Start();


            //绘制最小化、退出按钮
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(1, 1, 33, 33);
            gp.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            btn_Exit.Region = new Region(gp);
            btn_Minimize.Region = new Region(gp);

            //设置数据库连接字符串
            DbHelperMySQL.SetConnectionString(Properties.Settings.Default.ServerIp, Properties.Settings.Default.ServerDbPort, DbHelperMySQL.DataBaseServer.Sphinx);
            DbHelperMySQL.SetConnectionString(Properties.Settings.Default.DeviceIp, Properties.Settings.Default.DeviceDbPort, DbHelperMySQL.DataBaseServer.Device);
            DbHelperMySQL.SetConnectionString(Properties.Settings.Default.PictureIp, Properties.Settings.Default.PicturtDbPort, DbHelperMySQL.DataBaseServer.Image);
            //判断路径是否存在，不存在就建立
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //设置收数据的Server端
            bool success = StartTcpServer();
            //if(!success)
            //{
            //    MessageBox.Show("false");
            //}
            //连接socket.IO
            SocketIoConnect();
        }

        void myTcpServer_CmdEvent(object sender, TcpServer.CmdEventArgs e)
        {
            int id = 0;
            foreach (var item in idIp)
            {
                if (item.Value == e.IP)
                {
                    id = item.Key;
                    break;
                }
            }
            if (id != 0)
            {
                var obj = new JObject();
                obj["MachineId"] = id;       //机具ID
                obj["Fake"] = e.Amount.TotalErr;            //假币张数
                obj["Real"] = e.Amount.TotalTrue;            //真币张数
                obj["Total"] = e.Amount.TotalValue;           //总金额
                obj["First"] = e.Amount.FirstSign; //首张冠字号码
                obj["Last"] = e.Amount.LastSign;   //末张冠字号码
                socket.Emit("SetCount", obj);
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
                    lab_Tital.Text = "冠字号文件上传";
                    //所属厂家
                    List<ky_factory> dtFactory = KyDataOperation.GetAllFactory();
                    cmb_Factory.Items.Clear();
                    foreach (var factory in dtFactory)
                    {
                        string str = string.Format("{0},{1}", factory.kId,
                                                   factory.kFactoryName.Trim());
                        cmb_Factory.Items.Add(str);
                    }
                    //所属网点
                    List<ky_node> dtNode = KyDataOperation.GetNodeWithIds(bindNodeId);
                    cmb_Node.Items.Clear();
                    foreach (var node in dtNode)
                    {
                        string str = string.Format("{0},{1}", node.kId, node.kNodeName.Trim());
                        cmb_Node.Items.Add(str);
                    }
                    //ATM编号
                    List<ky_atm> dtATM2 = KyDataOperation.GetAtmWithNodeId(bindNodeId);
                    cmb_ATM2.Items.Clear();
                    foreach (var atm in dtATM2)
                    {
                        string str = string.Format("{0},{1}", atm.kId, atm.kATMNumber.Trim());
                        cmb_ATM2.Items.Add(str);
                    }
                    //钞箱编号
                    List<ky_cashbox> dtCashBox2 = KyDataOperation.GetCashBoxWithNodeId(bindNodeId);
                    cmb_CashBox2.Items.Clear();
                    foreach (var cashbox in dtCashBox2)
                    {
                        string str = string.Format("{0},{1}", cashbox.kId, cashbox.kCashBoxNumber.Trim());
                        cmb_CashBox2.Items.Add(str);
                    }
                    break;
                case 3:
                    lab_Tital.Text = "设备监控";
                    List<ky_machine> dtMachine = KyDataOperation.GetMachineStatus(bindNodeId);
                    if (dtMachine != null)
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
                        this.Close(); ;
                    break;
                case 1:
                    if (DialogResult.Yes == MessageBox.Show("退出软件后将无法接收纸币数据，是否退出软件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        this.Close();
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
            //localNodeId = 0;
        }
        //交易控制
        private void btn_BusinessControl_Click(object sender, EventArgs e)
        {
        }
        //冠字号码文件上传
        private void btn_InsertData_Click(object sender, EventArgs e)
        {
            txb_FilePath.Text = "";
            txb_Message.Text = "";
            tabControl1.SelectedIndex = 2;
        }
        //设备监控
        private void btn_MachineMonitoring_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }
        #endregion

        #region 冠字号文件上传
        //选择文件
        private string[] uploadFiles;
        private void btn_Scan_Click(object sender, EventArgs e)
        {
            if (rad_FSN.Checked)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "纸币冠字号码文件*.FSN|*.FSN";
                ofd.Multiselect = true;
                if (DialogResult.OK == ofd.ShowDialog())
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
            else if (rad_GZH.Checked)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "GZH压缩文件*.ZIP|*.ZIP";
                if (DialogResult.OK == ofd.ShowDialog())
                {
                    string file = ofd.FileName;
                    txb_FilePath.Text = file;
                    uploadFiles = new string[1];
                    uploadFiles[0] = file;
                }
            }

        }

        //交易类型
        private void cmb_BusinessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_BusinessType.Text.IndexOf("ATM") != -1)
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
            if (rad_FSN.Checked)
            {
                if (cmb_Factory.Text == "")
                {
                    MessageBox.Show("请选择文件所属厂家", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cmb_Node.Text == "")
                {
                    MessageBox.Show("请选择所属网点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cmb_BusinessType.Text == "")
                {
                    MessageBox.Show("请选择交易类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cmb_BusinessType.Text.IndexOf("ATM") != -1)
                {
                    if (cmb_ATM2.Text == "" || cmb_CashBox2.Text == "")
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

                int AtmId = 0, cashBoxId = 0;

                //ATM Id
                strTmp = cmb_ATM2.Text.Trim().Split(",".ToCharArray());
                if (strTmp.Length > 1)
                    AtmId = Convert.ToInt32(strTmp[0]);
                //钞箱Id
                strTmp = cmb_CashBox2.Text.Trim().Split(",".ToCharArray());
                if (strTmp.Length > 1)
                    cashBoxId = Convert.ToInt32(strTmp[0]);

                //交易类型
                string business = "";
                switch (cmb_BusinessType.SelectedIndex)
                {
                    case 0:
                        business = "HM";
                        break;
                    case 1:
                        business = "QK";
                        break;
                    case 2:
                        business = "CK";
                        break;
                    case 3:
                        business = "ATMP";
                        break;
                    case 4:
                        business = "ATMQ";
                        break;
                }

                foreach (var uploadFile in uploadFiles)
                {
                    txb_Message.Text = "";
                    string machineModel = "";
                    string[] str = KyDataLayer2.GetMachineNumberFromFSN(uploadFile, out machineModel).Split("/".ToCharArray());
                    string machineMac = str[str.Length - 1];
                    if (machineMac == "")
                        continue;

                    int machineId = 0;
                    int machineId2 = 0;
                    machineId = KyDataOperation.GetMachineId(machineMac);
                    if (machineId == 0)//未在机具列表中找到该机具编号
                    {
                        //获取数据库内的上传文件的机具列表
                        machineId2 = KyDataOperation.GetMachineIdFromImportMachine(machineMac);
                        if (machineId2 == 0)//未在上传文件的机具列表中找到该机具编号
                        {
                            int id = KyDataOperation.InsertMachineToImportMachine(machineMac, nodeId, factoryId);
                            if (id > 0)
                                machineId2 = id;
                        }
                    }
                    ky_machine machineTmp = new ky_machine();
                    machineTmp.kMachineNumber = machineMac;
                    machineTmp.kNodeId = nodeId;
                    machineTmp.kFactoryId = factoryId;
                    machineTmp.business = business;
                    machineTmp.kId = machineId;
                    machineTmp.atmId = AtmId;
                    machineTmp.cashBoxId = cashBoxId;
                    machineTmp.userId = userId;
                    machineTmp.imgServerId = KyDataOperation.GetPictureServerId(Properties.Settings.Default.PictureIp);
                    machineTmp.importMachineId = machineId2;
                    bool result = SaveDataToDB.UploadFsn(uploadFile, machineTmp);
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
                txb_FilePath.Text = "";
            }
            else if (rad_GZH.Checked)
            {
                if (uploadFiles.Length > 0)
                {
                    GzhInput bll = new GzhInput();
                    int pictureServerId = KyDataOperation.GetPictureServerId(Properties.Settings.Default.PictureIp);
                    bool success = bll.UploadGzhFile(uploadFiles[0], Application.StartupPath + "\\GZH", pictureServerId, userId);
                    string strMessage = "";
                    if (success)
                    {
                        //删除文件
                        if (Directory.Exists(Application.StartupPath + "\\GZH"))
                        {
                            string[] files = Directory.GetFiles(Application.StartupPath + "\\GZH");
                            foreach (var file in files)
                            {
                                File.Delete(file);
                            }
                        }
                        strMessage = string.Format("{0} 导入成功\n", Path.GetFileName(uploadFiles[0]));
                        txb_FilePath.Text = "";
                    }
                    else
                    {
                        strMessage = string.Format("{0} 导入失败\n", Path.GetFileName(uploadFiles[0]));
                    }
                    txb_Message.Text = strMessage;
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
                List<int> nodeId = new List<int>();
                nodeId.Add(int.Parse(strNode[0]));
                //ATM编号
                List<ky_atm> dtATM2 = KyDataOperation.GetAtmWithNodeId(nodeId);
                cmb_ATM2.Items.Clear();
                foreach (var atm in dtATM2)
                {
                    string str = string.Format("{0},{1}", atm.kId,
                                              atm.kATMNumber.Trim());
                    cmb_ATM2.Items.Add(str);
                }
            }
        }

        private void rad_FSN_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_FSN.Checked)
            {
                groupBox1.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
            }
        }

        private void rad_GZH_CheckedChanged(object sender, EventArgs e)
        {
            if (rad_GZH.Checked)
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
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
            if (Properties.Settings.Default.DeviceIp != "" && KyDataOperation.TestConnectDevice())
            {
                //获取绑定网点内的机器
                int[] nodeIds = new int[bindNodeId.Count];
                bindNodeId.CopyTo(nodeIds);
                List<ky_machine> machineDt = KyDataOperation.GetMachineWithNodeIds(nodeIds);
                myTcpServer.UpdateMachineTable(machineDt);
                idIp.Clear();
                foreach (var m in machineDt)
                {
                    int machineId = Convert.ToInt32(m.kId);
                    string ip = m.kIpAddress.Trim();
                    if (!idIp.ContainsKey(machineId))
                    {
                        idIp.Add(machineId, ip);
                    }
                }
            }
        }


        #region 设备监控
        #endregion

        #region socket.IO
        protected IO.Options CreateOptions()
        {
            var log = LogManager.GetLogger(Global.CallerName());

            var options = new IO.Options();
            options.Port = Properties.Settings.Default.PushPort;
            options.Hostname = Properties.Settings.Default.PushIp;
            options.ForceNew = true;

            return options;
        }

        protected string CreateUri()
        {
            var options = CreateOptions();
            var uri = string.Format("{0}://{1}:{2}", options.Secure ? "https" : "http", options.Hostname, options.Port);
            return uri;
        }
        private Socket socket;

        //连接Socket.Io
        private void SocketIoConnect()
        {
            try
            {
                string node = JsonConvert.SerializeObject(bindNodeId);
                if (node != "")
                {
                    var options = CreateOptions();
                    var uri = CreateUri();
                    socket = IO.Socket(uri, options);
                    socket.On(Socket.EVENT_CONNECT, () =>
                    {
                        socket.Emit("SetNodeId", node);
                    });
                    socket.On("SetStart",
                       (data) =>
                       {
                           var d = (JObject)data;
                           int machineId = Convert.ToInt32(d["MachineId"]);
                           if (idIp.ContainsKey(machineId))
                           {
                               KyData.DbTable.businessControl bControl = new businessControl();
                               bControl.dateTime = DateTime.Now;
                               bControl.ip = idIp[machineId];
                               if (d["Type"] != null)
                                   bControl.business = d["Type"].ToString();
                               else
                                   bControl.business = "";
                               if (d["UserId"] != null)
                                   bControl.userId = Convert.ToInt32(d["UserId"]);
                               else
                                   bControl.userId = 0;
                               if (bControl.business == "KHDK")
                               {
                                   int bundleCount = 100;
                                   if (d["BundleCount"] != null)
                                       bundleCount = Convert.ToInt32(d["BundleCount"]);
                                   bControl.bundleCount = bundleCount;
                               }
                               myTcpServer.BusinessControl(TcpServer.MyBusinessStatus.Start, bControl);
                           }
                       });
                    socket.On("SetEnd",
                                  (data) =>
                                  {
                                      var d = (JObject)data;
                                      int machineId = Convert.ToInt32(d["MachineId"]);
                                      if (idIp.ContainsKey(machineId))
                                      {
                                          KyData.DbTable.businessControl bControl = new businessControl();
                                          bControl.ip = idIp[machineId];
                                          if (d["BussinessNumber"] != null)
                                              bControl.bussinessNumber = d["BussinessNumber"].ToString();
                                          else
                                              bControl.bussinessNumber = "";
                                          if (d["ATMId"] != null)
                                              bControl.atmId = d["ATMId"].ToString();
                                          else
                                              bControl.atmId = "";
                                          if (d["CashBoxId"] != null)
                                              bControl.cashBoxId = d["CashBoxId"].ToString();
                                          else
                                              bControl.cashBoxId = "";
                                          bool isClearCenter = false;
                                          if (d["ClearCenter"] != null)
                                          {
                                              isClearCenter = Convert.ToBoolean(d["ClearCenter"]);
                                              if (isClearCenter)
                                                  bControl.isClearCenter = "T";
                                              else
                                                  bControl.isClearCenter = "F";
                                          }
                                          else
                                              bControl.isClearCenter = "";
                                          if (d["PackageNumber"] != null)
                                              bControl.packageNumber = d["PackageNumber"].ToString();
                                          else
                                              bControl.packageNumber = "";
                                          myTcpServer.BusinessControl(TcpServer.MyBusinessStatus.End, bControl);
                                      }
                                  });
                    socket.On("NoMachine", (data) => { });
                }
                else
                {
                    MessageBox.Show("该客户端未绑定网点，请先绑定网点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception)
            {
            }
        }

        //关闭Socket.Io
        private void SocketIoStop()
        {
            socket.Close();
        }


        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                var options = CreateOptions();
                var uri = CreateUri();
                socket = IO.Socket(uri, options);
                socket.On(Socket.EVENT_CONNECT, () =>
                {
                    socket.Emit("SetNodeId", "[1,2,3]");
                });
                socket.On("SetStart",
                   (data) =>
                   {
                       //AddMessage(Convert.ToString(data));
                       var d = (JObject)data;
                       MessageBox.Show(d["MachineId"].ToString());
                   });
                socket.On("SetEnd",
                              (data) =>
                              {
                                  var d = (JObject)data;
                                  //if(d["Type"]==null)

                                  MessageBox.Show("点钞结束了,机具id和业务流水号为" + d["MachineId"] + "," + d["BussinessNumber"]);
                              });
                socket.On("NoMachine", (data) => { });
            }
            catch (Exception)
            {
            }

        }
        private void ben_Send_Click(object sender, EventArgs e)
        {
            var obj = new JObject();
            obj["MachineId"] = 2;       //机具ID
            obj["Fake"] = 2;            //假币张数
            obj["Real"] = 1;            //真币张数
            obj["Total"] = 3;           //总金额
            obj["First"] = "1234567DF"; //首张冠字号码
            obj["Last"] = "SDF23434";   //末张冠字号码
            socket.Emit("SetCount", obj);
            //socket.Emit("SetCount", "[2,3,100,\"ADFDFJDF\",0]");//机具Id，可疑币总数，总张数，首张冠字号码（不存在填 0）、末张冠字号码（不存在填 0）
            //socket.Emit("SetCount", "[2,3,100,0,\"ADFDFJDF\"]");//机具Id，可疑币总数，总张数，首张冠字号码（不存在填 0）、末张冠字号码（不存在填 0）
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            socket.Close();
        }


        #endregion

        //SetEnd :
        //obj["MachineId"];
        //obj["BussinessNumber"];
        //obj["Type"];
        //obj["ATMId"];
        //obj["CashBoxId"];                         不存在的项为null d["MachineId1"]==null







    }
}
