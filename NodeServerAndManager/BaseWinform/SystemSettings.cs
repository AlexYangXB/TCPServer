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
namespace NodeServerAndManager.BaseWinform
{
    public partial class SystemSettings : MaterialSkin.Controls.MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public SystemSettings()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
        }

        private void SystemSettings_Load(object sender, EventArgs e)
        {
            //其他厂家接入
            chk_OtherFactoryAccess.Checked = Properties.Settings.Default.OtherFactoryAccess;
            txb_OtherFactoryAccessDir.Text = Properties.Settings.Default.OtherFactoryAccessDir;
            //CRH导出
            chk_CRHExport.Checked = Properties.Settings.Default.CRHExport;
            dt_CRHStartTime.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + Properties.Settings.Default.CRHStartTime);
            rb_CRHYesterday.Checked = Properties.Settings.Default.CRHYesterday;
            rb_CRHToday.Checked = Properties.Settings.Default.CRHToday;
            txb_CHRDir.Text = Properties.Settings.Default.CRHDir;
        }

        private void SystemSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool flag = true;
            //其他厂家接入保存
            if (chk_OtherFactoryAccess.Checked)
            {
                if (Directory.Exists(txb_OtherFactoryAccessDir.Text))
                {
                    Properties.Settings.Default.OtherFactoryAccessDir = txb_OtherFactoryAccessDir.Text;
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
                    Properties.Settings.Default.CRHDir = txb_CHRDir.Text;
                    Properties.Settings.Default.CRHExport = chk_CRHExport.Checked;
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
                Properties.Settings.Default.OtherFactoryAccess = chk_OtherFactoryAccess.Checked;
                //CRH导出
                Properties.Settings.Default.CRHStartTime = dt_CRHStartTime.Value.ToString("HH:mm:ss");
                Properties.Settings.Default.CRHToday = rb_CRHToday.Checked;
                Properties.Settings.Default.CRHYesterday = rb_CRHYesterday.Checked;
                Properties.Settings.Default.Save();
            }
            else
                e.Cancel = true;
            
        }

        private void btn_OtherFactoryAccess_Click(object sender, EventArgs e)
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
            DateTime startTime = Convert.ToDateTime(DateTime.Now.AddDays(-10));
            DateTime endTime = Convert.ToDateTime(DateTime.Now.AddDays(1));
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                CRHExport crhExport = new CRHExport(startTime, endTime, foldPath);
            }
           
        }
    }
}
