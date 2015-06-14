using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MyTcpServer;
using ServerForm.BaseWinForm;
using Utility;

namespace ServerForm
{
    public partial class CollectionServer : Form
    {
        private string fsnSavePath = Application.StartupPath + "\\FSN";
        private DataTable machine = new DataTable();//点钞机列表
        private MyTcpServer.MyTcpServerSimple myTcpServerSimple=new MyTcpServerSimple();
        private string FtpIp = "";
        private int FtpPort = 21;
        public CollectionServer()
        {
            InitializeComponent();
            if (!Directory.Exists(fsnSavePath))
                Directory.CreateDirectory(fsnSavePath);
        }

        //写文件
        private void WriteMachineList(string details, string mode, int index)
        {
            if (mode == "new")//增加新内容
            {
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\KySetting.ini", true, Encoding.Default))
                {
                    sw.Write(details);
                }
            }
            else if (mode == "delete")//删除内容
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\KySetting.ini", Encoding.Default))
                {
                    int i = 0;
                    while (sr.Peek() >= 0)
                    {
                        string strLine = sr.ReadLine();
                        if (i != index)
                        {
                            using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\KySetting.tmp", true, Encoding.Default))
                            {
                                sw.Write(strLine + Environment.NewLine);
                            }
                        }
                        i++;
                    }
                }
                if (File.Exists(Application.StartupPath + "\\KySetting.ini"))
                {
                    File.Delete(Application.StartupPath + "\\KySetting.ini");
                }
                if (File.Exists(Application.StartupPath + "\\KySetting.tmp"))
                    File.Move(Application.StartupPath + "\\KySetting.tmp", Application.StartupPath + "\\KySetting.ini");
            }
            else if (mode == "modify")//修改内容
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\KySetting.ini", Encoding.Default))
                {
                    int i = 0;
                    while (sr.Peek() >= 0)
                    {
                        string strLine = sr.ReadLine();
                        if (i != index)
                        {
                            using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\KySetting.tmp", true, Encoding.Default))
                            {
                                sw.Write(strLine + Environment.NewLine);
                            }
                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\KySetting.tmp", true, Encoding.Default))
                            {
                                sw.Write(details);
                            }
                        }
                        i++;
                    }
                }
                if (File.Exists(Application.StartupPath + "\\KySetting.ini"))
                {
                    File.Delete(Application.StartupPath + "\\KySetting.ini");
                }
                File.Move(Application.StartupPath + "\\KySetting.tmp", Application.StartupPath + "\\KySetting.ini");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            myTcpServerSimple.CmdEvent += new EventHandler<MyTcpServerSimple.CmdEventArgs>(myTcpServerSimple_CmdEvent);
            //当需要上传FTp时，设置FTP
            if(!ServerForm.Properties.Settings.Default.IsLocalSave)
            {
                string[] ipAndPort = ServerForm.Properties.Settings.Default.FtpIp.Split(":".ToCharArray());
                if(ipAndPort.Length==2)
                {
                    FtpIp = ipAndPort[0];
                    FtpPort = int.Parse(ipAndPort[1]);
                }
            }

            //设置机器列表
            machine.Columns.Add("kIndex", typeof(int)).SetOrdinal(0);
            machine.Columns.Add("kMachineType", typeof(string)).SetOrdinal(1);
            machine.Columns.Add("kIpAddress", typeof(string)).SetOrdinal(2);
            machine.Columns.Add("kTimeSync", typeof(string)).SetOrdinal(3);
            machine.Columns.Add("kLastTime", typeof(string)).SetOrdinal(4);
            dgv_Machine.DataSource = machine;
            cmb_MachineType.Text = string.Format("点钞机");

            //读取机器数据到列表中
            if (File.Exists(Application.StartupPath + "\\KySetting.ini"))
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\KySetting.ini", Encoding.Default))
                {
                    //int i = 0;
                    while (sr.Peek() >= 0)
                    {
                        string strLine = sr.ReadLine();
                        string[] strDetails = strLine.Split(',');
                        if (strDetails.Length == 3)
                        {
                            //写入列表中
                            DataRow dr = machine.NewRow();
                            dr["kIndex"] = machine.Rows.Count + 1;
                            dr["kMachineType"] = strDetails[0];
                            dr["kIpAddress"] = strDetails[1];
                            dr["kTimeSync"] = "";
                            dr["kLastTime"] = "";
                            machine.Rows.Add(dr);
                        }
                    }
                }
            }

            //设置Server端的IP地址列表，启动Server端采集数据
            List<string> ip=new List<string>();
            for (int i = 0; i < machine.Rows.Count; i++)
            {
                if(!ip.Contains(machine.Rows[i]["kIpAddress"].ToString()))
                {
                    ip.Add(machine.Rows[i]["kIpAddress"].ToString());
                }
            }
            myTcpServerSimple.IpList = ip;
            //本地保存，且本地保存路径不为空，且本地保存路径存在
            if (ServerForm.Properties.Settings.Default.IsLocalSave && ServerForm.Properties.Settings.Default.LocalSavePath != "" && Directory.Exists(ServerForm.Properties.Settings.Default.LocalSavePath))
                myTcpServerSimple.DataSaveFolder = ServerForm.Properties.Settings.Default.LocalSavePath;
            else
                myTcpServerSimple.DataSaveFolder = fsnSavePath;
            if (ServerForm.Properties.Settings.Default.LocalIp != "")
            {
                myTcpServerSimple.StartListenling(ServerForm.Properties.Settings.Default.LocalIp, ServerForm.Properties.Settings.Default.Port);
            }
        }

        void myTcpServerSimple_CmdEvent(object sender, MyTcpServerSimple.CmdEventArgs e)
        {
            for (int i = 0; i < machine.Rows.Count; i++)
            {
                if (e.IP == machine.Rows[i]["kIpAddress"].ToString())
                {
                    //
                    if (e.Cmd == 0x0010)//时间同步
                    {
                        machine.Rows[i]["kTimeSync"] = "已同步";
                    }
                    else if (e.Cmd == 0x00A1)//更新最后上传时间
                    {
                        machine.Rows[i]["kLastTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else if(e.Cmd > 0 && e.Cmd < 10)//上传文件
                    {
                        if(!ServerForm.Properties.Settings.Default.IsLocalSave)
                        {
                            string[] files = Directory.GetFiles(fsnSavePath);
                            FtpOperation.FilesUpload(FtpIp, FtpPort, ServerForm.Properties.Settings.Default.FtpPath,
                                                             ServerForm.Properties.Settings.Default.FtpUser,
                                                             ServerForm.Properties.Settings.Default.FtpPassWord, files);
                        }
                    }
                }
            }
        }

        //添加
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if(txb_IpAddress.Text.Trim()==""||cmb_MachineType.Text=="")
            {
                MessageBox.Show("请设置‘机具类型’和‘IP地址’","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            //判断是否有重复
            for (int i = 0; i < machine.Rows.Count; i++)
            {
                if (machine.Rows[i]["kIpAddress"].ToString() == txb_IpAddress.Text.Trim())
                {
                    string message = string.Format("列表中已经存在该IP地址:" + txb_IpAddress.Text.Trim() + "，请重新设置。");
                    MessageBox.Show(message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string machineType = cmb_MachineType.Text.Trim();
            string ip = txb_IpAddress.Text.Trim();
            string details=string.Format("{0},{1},",machineType,ip)+Environment.NewLine;
            WriteMachineList(details, "new", 1);
            DataRow dr = machine.NewRow();
            dr["kIndex"] = machine.Rows.Count + 1;
            dr["kMachineType"] = machineType;
            dr["kIpAddress"] = ip;
            dr["kTimeSync"] = "";
            dr["kLastTime"] = "";
            machine.Rows.Add(dr);

            txb_IpAddress.Text = "";
            //增加IP地址到监听端的IP地址列表中
            if(!myTcpServerSimple.IpList.Contains(ip))
            {
                myTcpServerSimple.IpList.Add(ip);
            }
        }

        //删除
        private void Delete_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("是否删除选定数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
            {
                if (dgv_Machine.SelectedRows.Count > 0)
                {
                    WriteMachineList("", "delete", dgv_Machine.SelectedRows[0].Index);
                    DataRow dr = machine.Rows[dgv_Machine.SelectedRows[0].Index];
                    machine.Rows.Remove(dr);
                }
            }
        }

        //修改
        private void Modification_Click(object sender, EventArgs e)
        {
            if (dgv_Machine.SelectedRows.Count > 0)
            {
                int selectedIndex = dgv_Machine.SelectedRows[0].Index;
                string machineType = dgv_Machine.SelectedRows[0].Cells["kMachineType"].Value.ToString();
                string ip = dgv_Machine.SelectedRows[0].Cells["kIpAddress"].Value.ToString();
                //string machineNum = dgv_Machine.SelectedRows[0].Cells["kMachineNumber"].Value.ToString();
                string[] ipList = new string[dgv_Machine.Rows.Count];
                for (int i = 0; i < dgv_Machine.Rows.Count; i++)
                {
                    ipList[i] = dgv_Machine.Rows[i].Cells["kIpAddress"].Value.ToString();
                }
                BaseWinForm.Modification f = new Modification(machineType, ip, ipList, selectedIndex);

                f.MessageSent += delegate(object caller, string MachineType, string IpAddress)
                {
                    machine.Rows[selectedIndex]["kMachineType"] = MachineType;
                    machine.Rows[selectedIndex]["kIpAddress"] = IpAddress;
                    string strLine = string.Format("{0},{1}", MachineType, IpAddress)+Environment.NewLine;
                    WriteMachineList(strLine, "modify", selectedIndex);
                    //更新监听端的IP地址列表
                    if(ip!=IpAddress)
                    {
                        myTcpServerSimple.IpList.Remove(ip);
                        if(!myTcpServerSimple.IpList.Contains((IpAddress)))
                        {
                            myTcpServerSimple.IpList.Add(IpAddress);
                        }
                    }
                };
                f.ShowDialog();
            }
        }

        private void txb_IpAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;  //不为数字和"."就移除
            }
        }

        private void 软件设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ip = ServerForm.Properties.Settings.Default.LocalIp;
            int port = ServerForm.Properties.Settings.Default.Port;
            BaseWinForm.Setting frm = new Setting();
            frm.ShowDialog();
            //IP地址或者端口号进行了修改，就要重启监听线程
            if (ip != ServerForm.Properties.Settings.Default.LocalIp || port != ServerForm.Properties.Settings.Default.Port)
            {
                //if(myTcpServerSimple.IsRunning)
                //    myTcpServerSimple.Stop();//会报错
                myTcpServerSimple.StartListenling(ServerForm.Properties.Settings.Default.LocalIp, ServerForm.Properties.Settings.Default.Port);
            }
        }

        private void 文件保存设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseWinForm.FileSaveSetting frm=new FileSaveSetting();
            frm.ShowDialog();
            //本地保存，且本地保存路径不为空，且本地保存路径存在
            if (ServerForm.Properties.Settings.Default.IsLocalSave && ServerForm.Properties.Settings.Default.LocalSavePath != "" && Directory.Exists(ServerForm.Properties.Settings.Default.LocalSavePath))
                myTcpServerSimple.DataSaveFolder = ServerForm.Properties.Settings.Default.LocalSavePath;
            else
                myTcpServerSimple.DataSaveFolder = fsnSavePath;

            //当需要上传FTp时，设置FTP
            if (!ServerForm.Properties.Settings.Default.IsLocalSave)
            {
                string[] ipAndPort = ServerForm.Properties.Settings.Default.FtpIp.Split(":".ToCharArray());
                if (ipAndPort.Length == 2)
                {
                    FtpIp = ipAndPort[0];
                    FtpPort = int.Parse(ipAndPort[1]);
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //限制窗口的最小长宽
            if (this.Width < 640)
                Width = 640;
            if (this.Height < 434)
                Height = 434;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DialogResult.No==MessageBox.Show("关闭软件后将无法接收数据，是否继续？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information))
            {
                e.Cancel = true;
            }
            //if(myTcpServerSimple.IsRunning)//如果监听线程正在运行，就要结束线程
            //    myTcpServerSimple.Stop();
        }
    }
}
