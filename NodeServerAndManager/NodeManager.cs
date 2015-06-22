using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KangYiCollection.BaseWinform;
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
                //SPHINX数据服务器连接
                result = KyDataOperation.TestConnectServer();
                if (!result)
                {
                    MessageBox.Show("无法连接‘数据服务器’，请查看‘服务器设置’是否正确？", "提示", MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                result = KyDataOperation.TestConnectDevice();
                //Device数据库连接
                if (!result)
                {
                    MessageBox.Show("无法连接‘设备服务器’，请查看‘服务器设置’是否正确？", "提示", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                //图像数据库
                result = KyDataOperation.TestConnectImage();
                if (!result)
                {
                    MessageBox.Show("无法连接‘图像数据库’，请查看‘服务器设置’是否正确？", "提示", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                result = KyDataOperation.TestConnectPush(KangYiCollection.Properties.Settings.Default.PushIp, KangYiCollection.Properties.Settings.Default.PushPort);
                if (!result)
                {
                    MessageBox.Show("无法连接‘推送服务器’，请查看‘服务器设置’是否正确？", "提示", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

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
                    //连接到soket.IO
                    SocketIoConnect();
                }
                catch (Exception e)
                {
                    Log.ConnectionException("启动连接服务器异常！",e);
                    return;
                }
            }
            else
                MessageBox.Show("服务器还未配置，请在菜单下选择‘服务器设置’进行配置！", "提示", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            return;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Login_Click(object sender, EventArgs e)
        {

            waitingForm.SetText("正在登录...");
            new Action(Login).BeginInvoke(new AsyncCallback(CloseLoading), null);
            Application.DoEvents();
            waitingForm.ShowDialog();
            if (userId != 0)
            {
                this.tabPage1.Parent = materialTabControl1;
                this.tabPage2.Parent = materialTabControl1;
                this.tabPage3.Parent = null;
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
                        MessageBox.Show("密码错误,请重新输入！", "提示", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        txb_PassWord.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("用户不存在！", "提示", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    txb_User.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法连接设备数据库！", "提示", MessageBoxButtons.OK,
                           MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Log.DataBaseException("登陆失败！",ex);
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
            //打开定时器
            timer_UpdateMachine.Start();
            //设置数据库连接字符串
            DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.ServerIp, KangYiCollection.Properties.Settings.Default.ServerDbPort, DbHelperMySQL.DataBaseServer.Sphinx);
            DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.DeviceIp, KangYiCollection.Properties.Settings.Default.DeviceDbPort, DbHelperMySQL.DataBaseServer.Device);
            DbHelperMySQL.SetConnectionString(KangYiCollection.Properties.Settings.Default.PictureIp, KangYiCollection.Properties.Settings.Default.PicturtDbPort, DbHelperMySQL.DataBaseServer.Image);
            //判断路径是否存在，不存在就建立
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //启动TCP服务、并连接到socket.IO后关闭等待窗体
            waitingForm.SetText("正在连接到服务器...");
            Application.DoEvents();
            new Action(StartTcpServer).BeginInvoke(new AsyncCallback(CloseLoading), null);
            waitingForm.ShowDialog();

            this.tabPage1.Parent = null;
            this.tabPage2.Parent = null;
            //其他厂家接入FSN
            this.timer_ImportFSN.Tick += new System.EventHandler(this.timer_ImportFSN_Tick);


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
                myTcpServer.TCPEvent.OnBussninessLog("发送机具id" + id + ",总金额" + e.Amount.TotalValue + ",可疑币张数" + e.Amount.TotalErr + ",真币张数"
                    + e.Amount.TotalTrue + ",第一张冠字号" + e.Amount.FirstSign + ",末张冠字号" + e.Amount.LastSign + "捆钞序号" + e.Amount.BundleNumber);
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
            bool flag = true;
            foreach (TabPage tab in materialTabControl1.TabPages)
            {
                if (tab.Text == "用户登录")
                    flag = false;

            }
            if (flag)
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
                        }
                        break;
                    case 1:
                        //设备监控
                        if (KyDataOperation.TestConnectDevice())
                        {
                            List<ky_machine> dtMachine = KyDataOperation.GetMachineStatus(bindNodeId);
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
            if ((BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.ATMP || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.ATMQ)
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
                if ((BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.ATMP || (BussinessType)cmb_BusinessType.SelectedIndex == BussinessType.ATMQ)
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
                    long batchId = FSNImport.UploadFsn(uploadFile, machine);
                    if (batchId > 0)
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
        }


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
                if (node != "")
                {
                    var options = CreateOptions();
                    var uri = CreateUri();
                    socket = IO.Socket(uri, options);
                    socket.On(Socket.EVENT_CONNECT, () =>
                    {
                        socket.Emit("SetNodeId", node);
                        myTcpServer.TCPEvent.OnBussninessLog("发送绑定网点id" + node);
                    });
                    socket.On("SetStart",
                       (data) =>
                       {
                           var d = (JObject)data;
                           int machineId = Convert.ToInt32(d["MachineId"]);
                           myTcpServer.TCPEvent.OnBussninessLog("收到开始命令,机具id是" + machineId + ",业务类型是" + d["Type"] + ",用户id是" + d["UserId"]);
                           ky_machine currentMachine = null;
                           foreach (var machine in myTcpServer.machine)
                           {
                               if (machine.Value.kId == machineId)
                                   currentMachine = machine.Value;
                           }

                           if (currentMachine!=null&&(DateTime.Now - currentMachine.alive).TotalMinutes < 5)
                           {
                               myTcpServer.TCPEvent.OnBussninessLog("机器上次连接时间" + currentMachine.alive.ToString("yyyy-MM-dd HH:mm:ss"));
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
                                   myTcpServer.TCPEvent.OnBussninessLog("一批张数是" + d["BundleCount"]);
                                   int bundleCount = 100;
                                   if (d["BundleCount"] != null)
                                       bundleCount = Convert.ToInt32(d["BundleCount"]);
                                   bControl.bundleCount = bundleCount;
                               }
                               myTcpServer.TCPEvent.OnBussninessLog("解析业务类型为" + bControl.business.ToString());
                               myTcpServer.BusinessControl(TcpServer.MyBusinessStatus.Start, bControl);
                           }
                           else
                           {
                               if (currentMachine == null)
                                   myTcpServer.TCPEvent.OnBussninessLog("机具id是" + machineId + "已失去连接。");
                               else
                                   myTcpServer.TCPEvent.OnBussninessLog("机器上次连接时间" + currentMachine.alive.ToString("yyyy-MM-dd HH:mm:ss") + "大于当前时间5分钟，视为未连接。");
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
                                      myTcpServer.TCPEvent.OnBussninessLog("收到结束命令,机具id是" + machineId);
                                      if (idIp.ContainsKey(machineId))
                                      {
                                          businessControl bControl = new businessControl();
                                          bControl.ip = idIp[machineId];
                                          if (d["Cancel"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog("收到撤销命令");
                                              bControl.cancel = true;
                                          }
                                          else
                                              bControl.cancel = false;
                                          if (d["BussinessNumber"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog("业务流水号是" + d["BussinessNumber"]);
                                              bControl.bussinessNumber = d["BussinessNumber"].ToString();
                                          }
                                          else
                                              bControl.bussinessNumber = "";
                                          if (d["ATMId"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog("ATM id是" + d["ATMId"].ToString());
                                              bControl.atmId = d["ATMId"].ToString();
                                          }
                                          else
                                              bControl.atmId = "";
                                          if (d["CashBoxId"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog("CashBox id是" + d["CashBoxId"].ToString());
                                              bControl.cashBoxId = d["CashBoxId"].ToString();
                                          }
                                          else
                                              bControl.cashBoxId = "";
                                          bool isClearCenter = false;
                                          if (d["ClearCenter"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog("是否为清分中心：" + d["ClearCenter"].ToString());
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
                                              myTcpServer.TCPEvent.OnBussninessLog("包号是" + d["PackageNumber"].ToString());
                                              bControl.packageNumber = d["PackageNumber"].ToString();
                                          }
                                          else
                                              bControl.packageNumber = "";
                                          if (d["BundleNumbers"] != null)
                                          {
                                              myTcpServer.TCPEvent.OnBussninessLog("捆钞序号是是" + d["BundleNumbers"].ToString());
                                              bControl.bundleNumbers = d["BundleNumbers"].ToString();
                                          }
                                          else
                                              bControl.bundleNumbers = "";
                                          myTcpServer.BusinessControl(TcpServer.MyBusinessStatus.End, bControl);
                                      }
                                  });
                    socket.On("NoMachine", (data) =>
                    {
                        myTcpServer.TCPEvent.OnBussninessLog("用户已关闭浏览器,用户选择的机具id是" + data);
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
                        var str = "您绑定的网点id" + NodeId + "已被IP地址" + IpAddress + "绑定了！";
                        myTcpServer.TCPEvent.OnBussninessLog(str);
                        MessageBox.Show(str,"提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        SocketIoStop();
                    });
                    socket.On("reconnecting", (nextRetry) =>
                    {
                        myTcpServer.TCPEvent.OnBussninessLog("尝试重连，等待" + nextRetry + "秒。");
                    });
                    socket.On("disconnect", (data) =>
                    {
                        myTcpServer.TCPEvent.OnBussninessLog("断开连接！");
                    });
                }
                else
                {
                    MessageBox.Show("该客户端未绑定网点，请先绑定网点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception e)
            {
                Log.ConnectionException("推送服务器异常",e);
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
            else if (DialogResult.No == MessageBox.Show("退出软件后将无法接收纸币数据，是否退出软件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                e.Cancel = true;
            if (!e.Cancel)
            {
                myTcpServer.Stop();
                SocketIoStop();
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
            SocketIoStop();
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
            string nbindNodeId=JsonConvert.SerializeObject(frm.bindNodeId);
            //本地IP或者端口号、设备IP与端口号改过之后，要重新启动 TcpServer端
            if (localIp != KangYiCollection.Properties.Settings.Default.LocalIp || port != KangYiCollection.Properties.Settings.Default.Port
                || deviceIp != KangYiCollection.Properties.Settings.Default.DeviceIp || devicePort != KangYiCollection.Properties.Settings.Default.DeviceDbPort)
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
            
            //当绑定网点id发生改变时
            if ((frm.bindNodeId.Count!=0&&obindNodeId != nbindNodeId))
            {
                
                bindNodeId = frm.bindNodeId;
                
            }
            SocketIoConnect();
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
                MessageBox.Show(ex.ToString());
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
            this.tabPage1.Parent = null;
            this.tabPage2.Parent = null;
            this.tabPage3.Parent = materialTabControl1;
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
        /// 其他厂家FSN接入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_ImportFSN_Tick(object sender, EventArgs e)
        {
            bool flag = KangYiCollection.Properties.Settings.Default.OtherFactoryAccess;
            if (flag)
            {
                string importDir = KangYiCollection.Properties.Settings.Default.OtherFactoryAccessDir;
                if (Directory.Exists(importDir))
                {
                    FSNImport.FsnImport(importDir, KangYiCollection.Properties.Settings.Default.PictureIp,myTcpServer.TCPEvent);
                }
                else
                    myTcpServer.TCPEvent.OnFSNImportLog(importDir + "路径不存在！");
            }
        }
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
        /// <summary>
        /// 导出CRH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_ExportCRH_Tick(object sender, EventArgs e)
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
                        myTcpServer.TCPEvent.OnFSNImportLog(exportDir + "路径不存在！");
                }
            }
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
            if (DialogResult.Yes == MessageBox.Show("退出软件后将无法接收纸币数据，是否退出软件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
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



    }
}
