using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using KangYiCollection.BaseWinform;
using KyBase;
using KyBll;
using KyBll.DBUtility;
using KyModel;
using KyModel.Models;
using MaterialSkin;
using MyTcpServer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.EngineIoClientDotNet.Modules;
using Quobject.SocketIoClientDotNet.Client;
namespace KangYiCollection
{
    public partial class NodeManager : MaterialSkin.Controls.MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        
        public NodeManager()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Program.CurrentLanguage);
            showMenu = true;
            InitializeComponent();
            //只显示绑定数据的信息
            this.dgv_machine.AutoGenerateColumns = false;
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            ChangeColorTheme();
            //蓝色
            // materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
            //黑色
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            //绿色
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
            foreach (TabPage tabPage in materialTabControl1.TabPages)
            {
                tabPage.Text = clsMsg.getMsg(tabPage.Name);
                foreach (var item in tabPage.Controls)
                {
                    if (item is Label)
                    {
                        Label lab = (Label)item;
                        lab.Text = clsMsg.getMsg(lab.Name);
                    }
                    if (item is RadioButton)
                    {
                        RadioButton rad = (RadioButton)item;
                        rad.Text = clsMsg.getMsg(rad.Name);
                    }
                    if (item is Button)
                    {
                        Button btn = (Button)item;
                        btn.Text = clsMsg.getMsg(btn.Name);
                    }
                    if (item is DataGridView)
                    {
                        DataGridView dataGrid = (DataGridView)item;
                        foreach (DataGridViewTextBoxColumn col in dataGrid.Columns)
                        {
                            col.HeaderText = clsMsg.getMsg(col.Name);
                        }
                    }
                    if (item is GroupBox)
                    {
                        GroupBox gb = (GroupBox)item;
                        foreach (var subitem in gb.Controls)
                        {
                            if (subitem is Label)
                            {
                                Label lab = (Label)subitem;
                                lab.Text = clsMsg.getMsg(lab.Name);
                            }
                        }
                    }
                }
            }
            foreach (ToolStripMenuItem menuItem in contextMenuStrip_Main.Items)
            {
                menuItem.Text = clsMsg.getMsg(menuItem.Name);
            }
            cmb_BusinessType.Items.Clear();
            cmb_BusinessType.Items.AddRange(clsMsg.getMsg("cmb_BusinessType").Split(',').Cast<object>().ToArray());

        }
        //用于收数据的server端
        private TcpServer myTcpServer = new TcpServer();
        private string path = Application.StartupPath + "\\FsnFloder";

        //当前网点ID，登录的时候获取的用户所在网点
        private List<int> bindNodeId = new List<int>();  //绑定的网点ID
        private Dictionary<int, string> idIp = new Dictionary<int, string>();
        private string userNumber = "";//当前登录的用户编号
        private int userId = 0;//当前登录的用户的ID
        private bool Exit = false;
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

        #region 登录界面

        /// <summary>
        /// 启动TcpServer、Socket.IO 
        /// </summary>
        private void StartTcpServer()
        {
            bool result = false;
            if (KangYiCollection.Properties.Settings.Default.ServerIp != "" && KangYiCollection.Properties.Settings.Default.DeviceIp != "" && KangYiCollection.Properties.Settings.Default.PictureIp != "" && KangYiCollection.Properties.Settings.Default.LocalIp != "")
            {
                result = KyDataOperation.TestConnectDevice();
                //Device数据库连接
                if (!result)
                {
                    AutoClosingMessageBox.Show(clsMsg.getMsg("msg_1"), clsMsg.getMsg("msg_Tip"), 5000);
                }
                else
                {
                    try
                    {
                        //获取绑定的网点ID
                        bindNodeId.Clear();
                        List<ky_node> nodes = KyDataOperation.GetNodeWithBindIp(KangYiCollection.Properties.Settings.Default.LocalIp);
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
                            int pictureServerId = KyDataOperation.GetPictureServerId(KangYiCollection.Properties.Settings.Default.PictureIp);
                            myTcpServer.PictureServerId = pictureServerId;
                            myTcpServer.StartListenling(KangYiCollection.Properties.Settings.Default.LocalIp, KangYiCollection.Properties.Settings.Default.Port);
                            result = true;
                        }

                    }
                    catch (Exception e)
                    {
                        MyLog.ConnectionException(clsMsg.getMsg("msg_2"), e);
                        AutoClosingMessageBox.Show(clsMsg.getMsg("msg_2"), clsMsg.getMsg("msg_Tip"), 5000);
                        return;
                    }
                }
                //推送服务
                result = KyDataOperation.TestConnectPush(KangYiCollection.Properties.Settings.Default.PushIp, KangYiCollection.Properties.Settings.Default.PushPort);
                if (!result)
                {
                    AutoClosingMessageBox.Show(clsMsg.getMsg("msg_3"), clsMsg.getMsg("msg_Tip"), 5000);
                }
                //数据服务
                result = KyDataOperation.TestConnectServer();
                if (!result)
                {
                    AutoClosingMessageBox.Show(clsMsg.getMsg("msg_4"), clsMsg.getMsg("msg_Tip"), 5000);
                }
                //图像服务
                result = KyDataOperation.TestConnectImage();
                if (!result)
                {
                    AutoClosingMessageBox.Show(clsMsg.getMsg("msg_5"), clsMsg.getMsg("msg_Tip"), 5000);
                }

            }
            else
                AutoClosingMessageBox.Show(clsMsg.getMsg("msg_6"), clsMsg.getMsg("msg_Tip"), 5000);
            return;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Login_Click(object sender, EventArgs e)
        {

            waitingForm.SetText(clsMsg.getMsg("wf_Login"));
            new Action(Login).BeginInvoke(new AsyncCallback(CloseLoading), null);
            Application.DoEvents();
            waitingForm.ShowDialog();
            if (userId != 0)
            {
                this.tab_FileUpload.Parent = materialTabControl1;
                this.tab_DeviceControl.Parent = materialTabControl1;
                this.tab_UserLogin.Parent = null;
            }

        }

        /// <summary>
        /// 登录
        /// </summary>
        private void Login()
        {
            string user = txb_User.Text.Trim();
            string passWord = txb_PassWord.Text.Trim();
            //加密
            passWord = KyDataOperation.Md5(passWord);

            ky_user dt = null; ;
            try
            {
                dt = KyDataOperation.GetUser(user);
                if (dt != null)
                {
                    if (dt.kPassWord == passWord)
                    {
                        userNumber = user;
                        userId = Convert.ToInt32(dt.kId);
                        txb_User.Text = "";
                        txb_PassWord.Text = "";

                    }
                    else
                    {
                        OnTopMessageBox.Show(clsMsg.getMsg("msg_7"), clsMsg.getMsg("msg_Tip"));
                        txb_PassWord.Focus();
                    }
                }
                else
                {
                    OnTopMessageBox.Show(clsMsg.getMsg("msg_8"), clsMsg.getMsg("msg_Tip"));
                    txb_User.Focus();
                }
            }
            catch (Exception ex)
            {
                MyLog.DataBaseException(clsMsg.getMsg("msg_9"), ex);
                OnTopMessageBox.Show(clsMsg.getMsg("msg_9"), clsMsg.getMsg("msg_Tip"));
            }
        }
        #endregion

        #region 公共部分
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeManager_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            myTcpServer.TCPEvent.CmdEvent += new EventHandler<MyTCP.CmdEventArgs>(myTcpServer_CmdEvent);

            //设置数据库连接字符串
            DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.ServerIp, KangYiCollection.Properties.Settings.Default.ServerDbPort, DbHelperMySQL.DataBaseServer.Sphinx);
            DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.DeviceIp, KangYiCollection.Properties.Settings.Default.DeviceDbPort, DbHelperMySQL.DataBaseServer.Device);
            DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.PictureIp, KangYiCollection.Properties.Settings.Default.PicturtDbPort, DbHelperMySQL.DataBaseServer.Image);
            //判断路径是否存在，不存在就建立
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Application.DoEvents();
            //启动TCP服务
            waitingForm.SetText(clsMsg.getMsg("wf_StartListen"));
            new Action(StartTcpServer).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            //连接到socket.IO
            waitingForm.SetText(clsMsg.getMsg("wf_StartPush"));
            new Action(SocketIoConnect).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            this.tab_FileUpload.Parent = null;
            this.tab_DeviceControl.Parent = null;
            //打开定时器
            timer_ExportCRH = new System.Threading.Timer(new TimerCallback(timer_ExportCRH_Tick), this, 1000, 1000);
            timer_ImportFSN = new System.Threading.Timer(new TimerCallback(timer_ImportFSN_Tick), this, 1000, 3000);
            timer_UpdateMachine = new System.Threading.Timer(new TimerCallback(timer_UpdateMachine_Tick), this, 1000, 180000);
            timer_UploadSql = new System.Threading.Timer(new TimerCallback(timer_UploadSql_Tick), this, 1000, 3000);
            timer_UploadPictures = new System.Threading.Timer(new TimerCallback(timer_UploadPictures_Tick), this, 1000, 3000);
        }
        /// <summary>
        /// 发送冠字号信息给SOCKET.IO服务端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myTcpServer_CmdEvent(object sender, MyTCP.CmdEventArgs e)
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
                obj["BundleNumber"] = e.Amount.BundleNumber;   //捆钞序号
                socket.Emit("SetCount", obj);
                myTcpServer.TCPEvent.OnBussninessLog( string.Format(clsMsg.getMsg("buss_27"), id, e.Amount.TotalValue, e.Amount.TotalErr, e.Amount.TotalTrue, e.Amount.FirstSign, e.Amount.LastSign, e.Amount.BundleNumber));
            }
           
        }
        /// <summary>
        /// 窗体激活事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeManager_Activated(object sender, EventArgs e)
        {
            txb_User.Focus();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userId != 0)
            {
                List<int> nodeIds = new List<int>();
                if (bindNodeId.Count == 0)
                {
                    List<ky_node> nodes = KyDataOperation.GetAllNode();
                    nodeIds = (from node in nodes select node.kId).ToList();
                }
                bool flag = true;
                foreach (TabPage tab in materialTabControl1.TabPages)
                {
                    if (tab.Text == clsMsg.getMsg(tab_UserLogin.Name))
                        flag = false;

                }
                if (flag&&nodeIds.Count>0)
                {
                    switch (materialTabControl1.SelectedIndex)
                    {
                        case 0:
                            //文件上传
                            //所属厂家
                            if (KyDataOperation.TestConnectDevice())
                            {
                                List<ky_factory> dtFactory = KyDataOperation.GetAllFactory();
                                cmb_Factory.Items.Clear();
                                foreach (var factory in dtFactory)
                                {
                                    string str = string.Format("{0},{1}", factory.kId,
                                                               factory.kFactoryName.Trim());
                                    cmb_Factory.Items.Add(str);
                                }
                                //所属网点
                                List<ky_node> dtNode = KyDataOperation.GetNodeWithIds(nodeIds);
                                cmb_Node.Items.Clear();
                                foreach (var node in dtNode)
                                {
                                    string str = string.Format("{0},{1}", node.kId, node.kNodeName.Trim());
                                    cmb_Node.Items.Add(str);
                                }
                                //ATM编号
                                List<ky_atm> dtATM2 = KyDataOperation.GetAtmWithNodeId(nodeIds);
                                cmb_ATM2.Items.Clear();
                                foreach (var atm in dtATM2)
                                {
                                    string str = string.Format("{0},{1}", atm.kId, atm.kATMNumber.Trim());
                                    cmb_ATM2.Items.Add(str);
                                }
                                //钞箱编号
                                List<ky_cashbox> dtCashBox2 = KyDataOperation.GetCashBoxWithNodeId(nodeIds);
                                cmb_CashBox2.Items.Clear();
                                foreach (var cashbox in dtCashBox2)
                                {
                                    string str = string.Format("{0},{1}", cashbox.kId, cashbox.kCashBoxNumber.Trim());
                                    cmb_CashBox2.Items.Add(str);
                                }
                            }
                            break;
                        case 1:
                            //设备监控
                            if (KyDataOperation.TestConnectDevice())
                            {
                                List<ky_machine> dtMachine = KyDataOperation.GetMachineStatus(nodeIds);
                                if (dtMachine != null)
                                {
                                    dgv_machine.DataSource = dtMachine;
                                }
                            }
                            break;
                        default: break;
                    }
                }
            }
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
                ofd.Filter = clsMsg.getMsg("Filter_1");
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
                ofd.Filter = clsMsg.getMsg("Filter_2");
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
            if ((BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.ATMP || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.ATMQ)
            {
                cmb_ATM2.Enabled = true;
                cmb_CashBox2.Enabled = true;
            }
            else
            {
                txb_BussinessNumber.Enabled = true;
                cmb_ATM2.Enabled = false;
            }
            if ((BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.CK || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.QK
                || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.CACK || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.CAQK)
            {
                txb_BussinessNumber.Enabled = true;
            }
            else
            {
                txb_BussinessNumber.Enabled = false;
            }
        }
        //确定
        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            if (rad_FSN.Checked)
            {
                if (cmb_Factory.Text == "")
                {
                    MessageBox.Show(clsMsg.getMsg("msg_11"), clsMsg.getMsg("msg_Tip"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cmb_Node.Text == "")
                {
                    MessageBox.Show(clsMsg.getMsg("msg_12"), clsMsg.getMsg("msg_Tip"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cmb_BusinessType.Text == "")
                {
                    MessageBox.Show(clsMsg.getMsg("msg_13"), clsMsg.getMsg("msg_Tip"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if ((BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.ATMP || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.ATMQ)
                {
                    if (cmb_ATM2.Text == "" || cmb_CashBox2.Text == "")
                    {
                        MessageBox.Show(clsMsg.getMsg("msg_14"), clsMsg.getMsg("msg_Tip"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if ((BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.CK || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.QK
               || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.CACK || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.CAQK)
                {
                    if (txb_BussinessNumber.Text.Trim() == "")
                    {
                        MessageBox.Show(clsMsg.getMsg("msg_15"), clsMsg.getMsg("msg_Tip"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                BussinessType bussiness = (BussinessType)cmb_BusinessType.SelectedIndex;


                foreach (var uploadFile in uploadFiles)
                {
                    txb_Message.Text = "";
                    ky_machine machine = FSNFormat.FindMachineByFsn(uploadFile, nodeId, factoryId);
                    machine.business = bussiness;
                    machine.atmId = AtmId;
                    machine.cashBoxId = cashBoxId;
                    machine.userId = userId;
                    machine.imgServerId = KyDataOperation.GetPictureServerId(KangYiCollection.Properties.Settings.Default.PictureIp);
                    machine.importMachineId = machine.importMachineId;
                    machine.bussinessNumber = txb_BussinessNumber.Text;
                    long batchId = FSNImport.UploadFsn(uploadFile, machine);
                    if (batchId > 0)
                    {
                        string strMessage = string.Format("{0},{1}{2}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                     Path.GetFileName(uploadFile), clsMsg.getMsg("str_ImportSuccess"));
                        txb_Message.Text += strMessage;
                    }
                    else
                    {
                        string strMessage = string.Format("{0},{1}{2}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                     Path.GetFileName(uploadFile), clsMsg.getMsg("str_ImportFail"));
                        txb_Message.Text += strMessage;
                    }
                }
                txb_FilePath.Text = "";
            }
            else if (rad_GZH.Checked)
            {
                if (uploadFiles.Length > 0)
                {
                    int pictureServerId = KyDataOperation.GetPictureServerId(KangYiCollection.Properties.Settings.Default.PictureIp);
                    bool success = GZHImport.UploadGzhFile(uploadFiles[0], Application.StartupPath + "\\GZH", pictureServerId, userId);
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
                        strMessage = string.Format("{0} {1}\n", Path.GetFileName(uploadFiles[0]), clsMsg.getMsg("str_ImportSuccess"));
                        txb_FilePath.Text = "";
                    }
                    else
                    {
                        strMessage = string.Format("{0} {1}\n", Path.GetFileName(uploadFiles[0]), clsMsg.getMsg("str_ImportFail"));
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




        #region socket.IO
        protected IO.Options CreateOptions()
        {
            var log = LogManager.GetLogger(Global.CallerName());

            var options = new IO.Options();
            options.Port = KangYiCollection.Properties.Settings.Default.PushPort;
            options.Hostname = KangYiCollection.Properties.Settings.Default.PushIp;
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
                if (node != "[]")
                {
                    var options = CreateOptions();
                    var uri = CreateUri();
                    socket = IO.Socket(uri, options);
                    socket.On(Socket.EVENT_CONNECT, () =>
                    {
                        socket.Emit("SetNodeId", node);
                        myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_1"), node));
                    });
                    socket.On("SetStart",
                       (data) =>
                       {
                           var d = (JObject)data;
                           int machineId = Convert.ToInt32(d["MachineId"]);

                           myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_2"), machineId, d["Type"], d["UserId"]));
                           ky_machine currentMachine = null;
                           foreach (var machine in myTcpServer.machine)
                           {
                               if (machine.Value.kId == machineId)
                                   currentMachine = machine.Value;
                           }

                           if (currentMachine != null && (DateTime.Now - currentMachine.alive).TotalMinutes < 5)
                           {
                               myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_3"), currentMachine.alive.ToString("yyyy-MM-dd HH:mm:ss")));
                               businessControl bControl = new businessControl();
                               bControl.dateTime = DateTime.Now;
                               bControl.ip = idIp[machineId];
                               if (d["Type"] != null)
                                   bControl.business = (BussinessType)Enum.Parse(typeof(BussinessType), d["Type"].ToString(), true);
                               else
                                   bControl.business = BussinessType.HM;
                               if (d["UserId"] != null)
                                   bControl.userId = Convert.ToInt32(d["UserId"]);
                               else
                                   bControl.userId = 0;
                               if (bControl.business == BussinessType.KHDK)
                               {
                                   myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_4"), d["BundleCount"]));
                                   int bundleCount = 100;
                                   if (d["BundleCount"] != null)
                                       bundleCount = Convert.ToInt32(d["BundleCount"]);
                                   bControl.bundleCount = bundleCount;
                               }
                               myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_5"), bControl.business.ToString()));
                               myTcpServer.BusinessControl(TcpServer.MyBusinessStatus.Start, bControl);
                           }
                           else
                           {
                               if (currentMachine == null)
                                   myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_6"), machineId));
                               else
                                   myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_7"), currentMachine.alive.ToString("yyyy-MM-dd HH:mm:ss")));
                               var obj = new JObject();
                               obj["MachineId"] = machineId;
                               socket.Emit("NoMachine", obj);
                           }
                       });
                    socket.On("SetEnd",
                                  (data) =>
                                  {
                                      var d = (JObject)data;
                                      int machineId = Convert.ToInt32(d["MachineId"]);
                                      myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_8"), machineId));
                                      if (idIp.ContainsKey(machineId))
                                      {
                                          businessControl bControl = new businessControl();
                                          bControl.ip = idIp[machineId];
                                          if (d["Cancel"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog(clsMsg.getMsg("buss_9"));
                                              bControl.cancel = true;
                                          }
                                          else
                                              bControl.cancel = false;
                                          if (d["BussinessNumber"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_10"), d["BussinessNumber"]));
                                              bControl.bussinessNumber = d["BussinessNumber"].ToString();
                                          }
                                          else
                                              bControl.bussinessNumber = "";
                                          if (d["ATMId"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_11"), d["ATMId"].ToString()));
                                              bControl.atmId = d["ATMId"].ToString();
                                          }
                                          else
                                              bControl.atmId = "";
                                          if (d["CashBoxId"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_12"), d["CashBoxId"].ToString()));
                                              bControl.cashBoxId = d["CashBoxId"].ToString();
                                          }
                                          else
                                              bControl.cashBoxId = "";
                                          bool isClearCenter = false;
                                          if (d["ClearCenter"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_13"), d["ClearCenter"].ToString()));
                                              isClearCenter = Convert.ToBoolean(d["ClearCenter"]);
                                              if (isClearCenter)
                                                  bControl.isClearCenter = "T";
                                              else
                                                  bControl.isClearCenter = "F";
                                          }
                                          else
                                              bControl.isClearCenter = "";
                                          if (d["PackageNumber"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_14"), d["PackageNumber"].ToString()));
                                              bControl.packageNumber = d["PackageNumber"].ToString();
                                          }
                                          else
                                              bControl.packageNumber = "";
                                          if (d["BundleNumbers"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_15"),d["BundleNumbers"].ToString()));
                                              bControl.bundleNumbers = d["BundleNumbers"].ToString();
                                          }
                                          else
                                              bControl.bundleNumbers = "";
                                          myTcpServer.BusinessControl(TcpServer.MyBusinessStatus.End, bControl);
                                      }
                                  });
                    socket.On("NoMachine", (data) =>
                    {
                        myTcpServer.TCPEvent.OnBussninessLog(string.Format(clsMsg.getMsg("buss_16"), data));
                    });
                    socket.On("ExistNode", (data) =>
                    {
                        var d = (JObject)data;
                        string NodeId = "";
                        string IpAddress = "";
                        if (d["NodeId"] != null)
                        {
                            NodeId = d["NodeId"].ToString();
                        }
                        if (d["IpAddress"] != null)
                        {
                            IpAddress = d["IpAddress"].ToString();
                        }
                        var str = string.Format(clsMsg.getMsg("buss_17"), NodeId, IpAddress);
                        myTcpServer.TCPEvent.OnBussninessLog(str);
                        AutoClosingMessageBox.Show(str, clsMsg.getMsg("msg_Tip"), 5000);
                        SocketIoStop();
                        Thread.Sleep(60000);
                        SocketIoConnect();
                    });
                    socket.On("reconnecting", (nextRetry) =>
                    {
                        string str = string.Format(clsMsg.getMsg("buss_18"), nextRetry);
                        myTcpServer.TCPEvent.OnBussninessLog(str);
                        
                    });
                    socket.On("disconnect", (data) =>
                    {
                        string str = clsMsg.getMsg("buss_19");
                        myTcpServer.TCPEvent.OnBussninessLog(str);
                    });
                }
                else
                {
                    string str = clsMsg.getMsg("buss_20");
                    myTcpServer.TCPEvent.OnBussninessLog(str);
                    AutoClosingMessageBox.Show(str, clsMsg.getMsg("msg_Tip"), 5000);
                }

            }
            catch (Exception e)
            {
                MyLog.ConnectionException(clsMsg.getMsg("buss_21"), e);
            }
        }

        //关闭Socket.Io
        private void SocketIoStop()
        {
            if (socket != null)
            {
                socket.Disconnect();
                socket.Close();
            }
        }



        #endregion

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Exit)
                e.Cancel = false;
            else if (DialogResult.No == MessageBox.Show(clsMsg.getMsg("msg_10"), clsMsg.getMsg("msg_Tip"), MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                e.Cancel = true;
            if (!e.Cancel)
            {
                waitingForm.SetText(clsMsg.getMsg("wf_StopListen"));
                new Action(myTcpServer.Stop).BeginInvoke(new AsyncCallback(CloseLoading), null);
                waitingForm.ShowDialog();
                waitingForm.SetText(clsMsg.getMsg("wf_StopPush"));
                new Action(SocketIoStop).BeginInvoke(new AsyncCallback(CloseLoading), null);
                waitingForm.ShowDialog();
                waitingForm.SetText(clsMsg.getMsg("wf_SaveFile"));
                new Action(KyDataOperation.SaveSqlToFile).BeginInvoke(new AsyncCallback(CloseLoading), null);
                waitingForm.ShowDialog();
                System.Environment.Exit(0);

            }
        }

        /// <summary>
        /// 密码回车登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb_PassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (txb_PassWord.ContainsFocus)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btn_Login.PerformClick();
                }
            }
        }
        /// <summary>
        /// 重写，显示菜单
        /// </summary>
        /// <param name="e"></param>
        public override void OnShowMenu(MouseEventArgs e)
        {
            if (userId == 0)
                MenuItem_LogOut.Visible = false;
            else
                MenuItem_LogOut.Visible = true;
            contextMenuStrip_Main.Show(Cursor.Position);
        }


        #region 菜单功能
        /// <summary>
        /// 服务器设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_ServerSetting_Click(object sender, EventArgs e)
        {
            waitingForm.SetText(clsMsg.getMsg("wf_StopPush"));
            new Action(SocketIoStop).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
            //记录本地IP与端口号，当本地IP与端口号发生改变时，重启TcpServer端
            string localIp = KangYiCollection.Properties.Settings.Default.LocalIp;
            int port = KangYiCollection.Properties.Settings.Default.Port;

            //记录设备IP与端口号，当设备IP与端口号发生改变时，
            string deviceIp = KangYiCollection.Properties.Settings.Default.DeviceIp;
            int devicePort = KangYiCollection.Properties.Settings.Default.DeviceDbPort;

            //记录推送IP与端口号，当推送IP与端口号发生改变时，
            string pushIp = KangYiCollection.Properties.Settings.Default.PushIp;
            int pushPort = KangYiCollection.Properties.Settings.Default.PushPort;

            //记录绑定网点id
            string obindNodeId = JsonConvert.SerializeObject(bindNodeId);
            //显示设置界面
            BaseWinform.ServerSettings frm = new ServerSettings();
            frm.ShowDialog();

            //设置数据库连接字符串
            DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.ServerIp, KangYiCollection.Properties.Settings.Default.ServerDbPort, DbHelperMySQL.DataBaseServer.Sphinx);
            DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.DeviceIp, KangYiCollection.Properties.Settings.Default.DeviceDbPort, DbHelperMySQL.DataBaseServer.Device);
            DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.PictureIp, KangYiCollection.Properties.Settings.Default.PicturtDbPort, DbHelperMySQL.DataBaseServer.Image);

            //获取绑定的网点ID
            string nbindNodeId = JsonConvert.SerializeObject(frm.bindNodeId);
            //本地IP或者端口号、设备IP与端口号改过之后，要重新启动 TcpServer端
            if (localIp != KangYiCollection.Properties.Settings.Default.LocalIp || port != KangYiCollection.Properties.Settings.Default.Port
                || deviceIp != KangYiCollection.Properties.Settings.Default.DeviceIp || devicePort != KangYiCollection.Properties.Settings.Default.DeviceDbPort)
            {
                Application.DoEvents();
                if (myTcpServer.IsRunning)
                {
                    waitingForm.SetText(clsMsg.getMsg("wf_StopListen"));
                    new Action(myTcpServer.Stop).BeginInvoke(new AsyncCallback(CloseLoading), null);
                    waitingForm.ShowDialog();
                }
                waitingForm.SetText(clsMsg.getMsg("wf_StartListen"));
                new Action(StartTcpServer).BeginInvoke(new AsyncCallback(CloseLoading), null);
                waitingForm.ShowDialog();
            }

            //当绑定网点id发生改变时
            if ((frm.bindNodeId.Count != 0 && obindNodeId != nbindNodeId))
            {

                bindNodeId = frm.bindNodeId;

            }
            waitingForm.SetText(clsMsg.getMsg("wf_StartPush"));
            new Action(SocketIoConnect).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();
        }
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Log_Click(object sender, EventArgs e)
        {
            try
            {
                BaseWinform.LogForm logForm = new LogForm();

                myTcpServer.TCPEvent.LogEvent += new EventHandler<MyTCP.LogEventArgs>(logForm.myTcpServer_LogEvent);
                logForm.ShowDialog();
                myTcpServer.TCPEvent.LogEvent -= new EventHandler<MyTCP.LogEventArgs>(logForm.myTcpServer_LogEvent);
            }
            catch (Exception ex)
            {
                MyLog.UnHandleException(clsMsg.getMsg("msg_16"), ex);
                AutoClosingMessageBox.Show(MyLog.GetExceptionMsg(ex, clsMsg.getMsg("msg_16")), clsMsg.getMsg("msg_Tip"), 5000);
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_LogOut_Click(object sender, EventArgs e)
        {
            userId = 0;
            userNumber = "";
            this.tab_FileUpload.Parent = null;
            this.tab_DeviceControl.Parent = null;
            this.tab_UserLogin.Parent = materialTabControl1;
        }
        /// <summary>
        /// 功能设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_FunctionSetting_Click(object sender, EventArgs e)
        {
            FunctionSettings frm = new FunctionSettings();
            frm.ShowDialog();
        }

        #endregion


        /// <summary>
        /// CRH查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_CRHReview_Click(object sender, EventArgs e)
        {
            CRHReview CRHReview = new CRHReview();
            CRHReview.ShowDialog();
        }


        private void NodeManager_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                switchToTray(true);
        }
        private void switchToTray(bool bHide)
        {
            if (!bHide)
            {
                Show();
                WindowState = FormWindowState.Normal;
                BringToFront();
            }
            else
            {
                Hide();
                WindowState = FormWindowState.Minimized;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Minimized)
                    switchToTray(false);
                else
                    switchToTray(true);
            }
        }

        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show(clsMsg.getMsg("msg_10"), clsMsg.getMsg("msg_Tip"), MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                Exit = true;
                Close();
            }
        }

        private void lb_OpenMainDir_Click(object sender, EventArgs e)
        {
            string foldPath = Application.StartupPath;
            System.Diagnostics.Process.Start("explorer.exe", foldPath);
        }
        private int colorSchemeIndex = KangYiCollection.Properties.Settings.Default.Color;

        private void lab_Version_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 2) colorSchemeIndex = 0;
            KangYiCollection.Properties.Settings.Default.Color = colorSchemeIndex;
            KangYiCollection.Properties.Settings.Default.Save();
            ChangeColorTheme();
        }
        /// <summary>
        /// 背景主题
        /// </summary>
        private void ChangeColorTheme()
        {
            switch (colorSchemeIndex)
            {
                case 0://黑色
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;
                case 1://蓝色
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
                    break;
                case 2://绿色
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                    break;
            }
        }
        private object UpdateMachineObj = new object();
        /// <summary>
        /// 每10分钟更新一次 从数据库中查询一次机具信息，并更新到Server端中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_UpdateMachine_Tick(object sender)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Program.CurrentLanguage);
            if (Monitor.TryEnter(UpdateMachineObj, 500))
            {
                if (KangYiCollection.Properties.Settings.Default.DeviceIp != "" && KyDataOperation.TestConnectDevice())
                {
                    //获取绑定网点内的机器
                    int[] nodeIds = new int[bindNodeId.Count];
                    if (nodeIds.Length > 0)
                    {
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
                Monitor.Exit(UpdateMachineObj);
            }
        }
        private object ImportFSNObj = new object();
        /// <summary>
        /// 其他厂家FSN接入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_ImportFSN_Tick(object sender)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Program.CurrentLanguage);
            if (Monitor.TryEnter(ImportFSNObj, 500))
            {
                bool flag = KangYiCollection.Properties.Settings.Default.OtherFactoryAccess;
                if (flag)
                {
                    string importDir = KangYiCollection.Properties.Settings.Default.OtherFactoryAccessDir;
                    if (Directory.Exists(importDir))
                    {
                        FSNImport.FsnImport(importDir, KangYiCollection.Properties.Settings.Default.PictureIp, myTcpServer.TCPEvent);
                    }
                    else
                        myTcpServer.TCPEvent.OnFSNImportLog(string.Format(clsMsg.getMsg("log_1"), importDir));
                }
                Monitor.Exit(ImportFSNObj);
            }
        }
        private object ExportCRHObj = new object();
        /// <summary>
        /// 导出CRH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_ExportCRH_Tick(object sender)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Program.CurrentLanguage);
            if (Monitor.TryEnter(ExportCRHObj, 500))
            {
                string Time = DateTime.Now.ToString("HH:mm:ss");
                if (Time == KangYiCollection.Properties.Settings.Default.CRHStartTime)
                {
                    bool flag = KangYiCollection.Properties.Settings.Default.CRHExport;
                    if (flag)
                    {
                        string exportDir = KangYiCollection.Properties.Settings.Default.CRHDir;
                        if (Directory.Exists(exportDir))
                        {
                            DateTime startTime;
                            DateTime endTime;
                            if (KangYiCollection.Properties.Settings.Default.CRHYesterday)
                            {
                                startTime = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
                                endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                //默认导出当天
                                startTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                endTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                            }
                            CRHExport crhExport = new CRHExport(startTime, endTime, exportDir);
                        }
                        else
                            MyLog.CRHLog(string.Format(clsMsg.getMsg("log_1"), exportDir));
                    }
                    else
                        MyLog.CRHLog(clsMsg.getMsg("log_2"));
                }
                Monitor.Exit(ExportCRHObj);
            }
        }
        private object UploadSqlObj = new object();
        /// <summary>
        /// 上传Sql
        /// </summary>
        /// <param name="sender"></param>
        private void timer_UploadSql_Tick(object sender)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Program.CurrentLanguage);
            if (Monitor.TryEnter(UploadSqlObj, 500))
            {
                if (KyDataOperation.TestConnectServer())
                {
                    if (KyDataOperation.sqlQueue.Count < KyDataOperation.MaxSqlCount)
                    {
                        string sqlFile = KyDataOperation.SqlDir + "\\UnUpload.sql";
                        List<string> sqls = FileOperation.ReadLines(KyDataOperation.SqlDir, sqlFile, 100);
                        if (sqls.Count > 0)
                        {
                            foreach (var sql in sqls)
                            {
                                KyDataOperation.sqlQueue.Enqueue(sql);
                            }
                        }
                    }
                    List<string> strSqls = new List<string>();
                    for (int i = 0; i < 100; i++)
                    {
                        if (KyDataOperation.sqlQueue.Count > 0)
                        {
                            string sql = KyDataOperation.sqlQueue.Dequeue();
                            if (sql != null && sql != "")
                            {
                                strSqls.Add(sql);
                            }
                        }
                    }

                    if (strSqls.Count > 0)
                    {
                        if (!KyDataOperation.InsertWithSql(strSqls))
                        {
                            foreach (var sql in strSqls)
                            {
                                KyDataOperation.SqlEnqueue(sql);
                            }
                        }
                    }
                    if (KyDataOperation.sqlQueue.Count > 0)
                        MyLog.TestLog(string.Format(clsMsg.getMsg("log_3"), KyDataOperation.sqlQueue.Count));
                }
                else
                {
                    Thread.Sleep(60000);
                }
                Monitor.Exit(UploadSqlObj);
            }
        }
        private object UploadPicturesObj = new object();
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        private void timer_UploadPictures_Tick(object sender)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Program.CurrentLanguage);
            if (Monitor.TryEnter(UploadPicturesObj, 500))
            {
                if (KyDataOperation.TestConnectImage())
                {
                    if (MySetting.GetProgramValue("UploadPicture"))
                    {
                        if (KyDataOperation.pictureQueue.Count < KyDataOperation.MaxPictureFileCount)
                        {
                            if (Directory.Exists(KyDataOperation.PictureFileDir))
                            {
                                string[] pictureFiles = Directory.GetFiles(KyDataOperation.PictureFileDir).Except(KyDataOperation.pictureQueue).Take(KyDataOperation.MaxPictureFileCount).ToArray();
                                foreach (var pic in pictureFiles)
                                {
                                    if (!KyDataOperation.pictureQueue.Contains(pic))
                                        KyDataOperation.pictureQueue.Enqueue(pic);
                                }
                            }
                        }
                        List<ky_picture> pics = new List<ky_picture>();
                        string fileName = "";
                        if (KyDataOperation.pictureQueue.Count > 0)
                        {
                            fileName = KyDataOperation.pictureQueue.Dequeue();
                            if (fileName != null)
                            {
                                pics = ObjectToFile.DeSerializeObject<List<ky_picture>>(fileName);
                            }

                        }

                        if (KyDataOperation.pictureQueue.Count > 0)
                        {
                            myTcpServer.TCPEvent.OnCommandLog(new KyModel.TCPMessage()
                            {
                                MessageType = TCPMessageType.Common_Message,
                                Message =string.Format(clsMsg.getMsg("log_4"), KyDataOperation.pictureQueue.Count) 
                            });

                        }
                        if (pics != null && pics.Count > 0)
                        {
                            if (KyDataOperation.InsertPictures(pics))
                            {
                                if (File.Exists(fileName))
                                    File.Delete(fileName);
                            }
                            else
                            {
                                myTcpServer.TCPEvent.OnCommandLog(new KyModel.TCPMessage()
                                {
                                    MessageType = TCPMessageType.Common_Message,
                                    Message = string.Format(clsMsg.getMsg("log_5"), fileName) 
                                });

                                KyDataOperation.pictureQueue.Enqueue(fileName);
                            }
                        }
                    }
                    else
                        KyDataOperation.pictureQueue.Clear();
                }
                else
                {
                    Thread.Sleep(60000);
                }
                Monitor.Exit(UploadPicturesObj);
            }
        }



    }
}
