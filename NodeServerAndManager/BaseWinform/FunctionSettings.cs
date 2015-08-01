using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialSkin;
using System.IO;
using KyBll;
using KyModel;
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
        }

        private void SystemSettings_Load(object sender, EventArgs e)
        {
            //其他厂家接入
            chk_FactoryAccess.Checked = KangYiCollection.Properties.Settings.Default.OtherFactoryAccess;
            txb_OtherFactoryAccessDir.Text = KangYiCollection.Properties.Settings.Default.OtherFactoryAccessDir;
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
                if (Directory.Exists(txb_OtherFactoryAccessDir.Text))
                {
                    KangYiCollection.Properties.Settings.Default.OtherFactoryAccessDir = txb_OtherFactoryAccessDir.Text;
                }
                else
                {
                    MessageBox.Show("其他厂家接入文件夹路径不存在！");
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
                    MessageBox.Show("CRH导出路径不存在！");
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

        private void btn_FactoryDirLook_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                txb_OtherFactoryAccessDir.Text = foldPath;
            }
        }

        private void btn_CRHDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                txb_CHRDir.Text = foldPath;
            }
        }

        private void btn_CRHExport_Click(object sender, EventArgs e)
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
                waitingForm.SetText("正在导出CRH...");
                new Action(ExportToCRH).BeginInvoke(new AsyncCallback(CloseLoading), null);
                waitingForm.ShowDialog();
                if (OnTopMessageBox.Show("导出成功！，是否打开导出目录？", "确认") == DialogResult.OK)
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
