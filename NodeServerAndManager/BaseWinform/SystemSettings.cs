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
            OtherFactoryAccessCheckBox.Checked = Properties.Settings.Default.OtherFactoryAccess;
            txb_OtherFactoryAccessDir.Text = Properties.Settings.Default.OtherFactoryAccessDir;
        }

        private void SystemSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            //其他厂家接入保存
            bool flag=true;
            if (OtherFactoryAccessCheckBox.Checked)
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
            if (flag)
            {
                Properties.Settings.Default.OtherFactoryAccess = OtherFactoryAccessCheckBox.Checked;
                Properties.Settings.Default.Save();
            }
            else
                e.Cancel = true;
        }

        private void OtherFactoryAccessButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath=dialog.SelectedPath;
                txb_OtherFactoryAccessDir.Text = foldPath;
            }
        }
    }
}
