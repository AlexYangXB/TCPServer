using System;
using System.IO;
using System.Windows.Forms;
using KyBase;
using KyBll;
using MaterialSkin;
namespace KangYiCollection.BaseWinform
{
    public partial class FunctionSettings : MaterialSkin.Controls.MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
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
        //手动导出CRH
        private DateTime startTime;
        private DateTime endTime;
        private string foldPath;
        public FunctionSettings()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            foreach (TabPage tabPage in materialTabControl1.TabPages)
            {
                tabPage.Text = clsMsg.getMsg(tabPage.Name);
                foreach (var item in tabPage.Controls)
                {
                    if (item is Label)
                    {
                        Label lab = (Label)item;
                        if (lab.Name.Contains("Directory"))
                            lab.Text = clsMsg.getMsg("lab_Directory");
                        else
                            lab.Text = clsMsg.getMsg(lab.Name);
                    }
                    if (item is Button)
                    {
                        Button btn = (Button)item;
                        if (btn.Name.Contains("Look"))
                            btn.Text = clsMsg.getMsg("btn_Look");
                        else
                            btn.Text = clsMsg.getMsg(btn.Name);
                    }
                    if (item is MaterialSkin.Controls.MaterialCheckBox)
                    {
                        MaterialSkin.Controls.MaterialCheckBox mchk = (MaterialSkin.Controls.MaterialCheckBox)item;
                        mchk.Text = clsMsg.getMsg(mchk.Name);
                    }
                    if (item is GroupBox)
                    {
                        GroupBox gb = (GroupBox)item;
                        gb.Text = clsMsg.getMsg(gb.Name);
                        foreach (var subitem in gb.Controls)
                        {
                            if (subitem is Label)
                            {
                                Label lab = (Label)subitem;
                                if (lab.Name.Contains("Directory"))
                                    lab.Text = clsMsg.getMsg("lab_Directory");
                                else
                                    lab.Text = clsMsg.getMsg(lab.Name);
                            }
                            if (subitem is Button)
                            {
                                Button btn = (Button)subitem;
                                if (btn.Name.Contains("Look"))
                                    btn.Text = clsMsg.getMsg("btn_Look");
                                else
                                    btn.Text = clsMsg.getMsg(btn.Name);
                            }
                            if (subitem is MaterialSkin.Controls.MaterialCheckBox)
                            {
                                MaterialSkin.Controls.MaterialCheckBox mchk = (MaterialSkin.Controls.MaterialCheckBox)subitem;
                                mchk.Text = clsMsg.getMsg(mchk.Name);
                            }
                            if (subitem is MaterialSkin.Controls.MaterialRadioButton)
                            {
                                MaterialSkin.Controls.MaterialRadioButton mrad = (MaterialSkin.Controls.MaterialRadioButton)subitem;
                                mrad.Text = clsMsg.getMsg(mrad.Name);
                            }
                        }
                    }
                }
            }
            this.Text = clsMsg.getMsg("MenuItem_FunctionSetting");
        }

        private void SystemSettings_Load(object sender, EventArgs e)
        {
            //其他厂家接入
            chk_FactoryAccess.Checked = KangYiCollection.Properties.Settings.Default.OtherFactoryAccess;
            txb_FactoryDir.Text = KangYiCollection.Properties.Settings.Default.OtherFactoryAccessDir;
            //CRH导出
            chk_CRHExport.Checked = KangYiCollection.Properties.Settings.Default.CRHExport;
            dt_CRHStartTime.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + KangYiCollection.Properties.Settings.Default.CRHStartTime);
            rb_CRHYesterday.Checked = KangYiCollection.Properties.Settings.Default.CRHYesterday;
            rb_CRHToday.Checked = KangYiCollection.Properties.Settings.Default.CRHToday;
            txb_CHRDir.Text = KangYiCollection.Properties.Settings.Default.CRHDir;
        }

        private void SystemSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool flag = true;
            //其他厂家接入保存
            if (chk_FactoryAccess.Checked)
            {
                if (Directory.Exists(txb_FactoryDir.Text))
                {
                    KangYiCollection.Properties.Settings.Default.OtherFactoryAccessDir = txb_FactoryDir.Text;
                }
                else
                {
                    MessageBox.Show(clsMsg.getMsg("msg_17"));
                    flag = false;
                }
            }
            //CRH导出
            if (chk_CRHExport.Checked)
            {
                if (Directory.Exists(txb_CHRDir.Text))
                {
                    KangYiCollection.Properties.Settings.Default.CRHDir = txb_CHRDir.Text;
                    KangYiCollection.Properties.Settings.Default.CRHExport = chk_CRHExport.Checked;
                }
                else
                {
                    MessageBox.Show(clsMsg.getMsg("msg_18"));
                    flag = false;
                }
            }
            if (flag)
            {
                //其他厂家接入保存
                KangYiCollection.Properties.Settings.Default.OtherFactoryAccess = chk_FactoryAccess.Checked;
                //CRH导出
                KangYiCollection.Properties.Settings.Default.CRHStartTime = dt_CRHStartTime.Value.ToString("HH:mm:ss");
                KangYiCollection.Properties.Settings.Default.CRHToday = rb_CRHToday.Checked;
                KangYiCollection.Properties.Settings.Default.CRHYesterday = rb_CRHYesterday.Checked;
                KangYiCollection.Properties.Settings.Default.Save();
            }
            else
                e.Cancel = true;

        }

        private void btn_FactoryLook_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                txb_FactoryDir.Text = foldPath;
            }
        }

        private void btn_CRHLook_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                txb_CHRDir.Text = foldPath;
            }
        }

        private void btn_CRHManual_Click(object sender, EventArgs e)
        {
            if (rb_CRHYesterday.Checked)
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
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (Directory.Exists(KangYiCollection.Properties.Settings.Default.CRHDir))
                dialog.SelectedPath = KangYiCollection.Properties.Settings.Default.CRHDir;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foldPath = dialog.SelectedPath;
                Application.DoEvents();
                waitingForm.SetText(clsMsg.getMsg("wf_CRHExport"));
                new Action(ExportToCRH).BeginInvoke(new AsyncCallback(CloseLoading), null);
                waitingForm.ShowDialog();
                if (OnTopMessageBox.Show(clsMsg.getMsg("msg_19"), clsMsg.getMsg("msg_Tip")) == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start("explorer.exe", foldPath);
                }
            }

        }
        private void ExportToCRH()
        {
            CRHExport crhExport = new CRHExport(startTime, endTime, foldPath);
        }
    }
}
