using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Utility;

namespace ServerForm.BaseWinForm
{
    public partial class FileSaveSetting : Form
    {
        public FileSaveSetting()
        {
            InitializeComponent();
        }

        private void FileSaveSetting_Load(object sender, EventArgs e)
        {
            //是否本地保存
            if(Properties.Settings.Default.IsLocalSave)
            {
                radbtn_Local.Checked = true;
                txb_LocalSave.Enabled = true;
                btn_Scan.Enabled = true;
                txb_FTP.Enabled = false;
                txb_Dir.Enabled = false;
                txb_User.Enabled = false;
                txb_PassWord.Enabled = false;
                lab_FtpTest.Enabled = false;
            }
            else//FTP
            {
                radbtn_Ftp.Checked = true;
                txb_LocalSave.Enabled = false;
                btn_Scan.Enabled = false;
                txb_FTP.Enabled = true;
                txb_Dir.Enabled = true;
                txb_User.Enabled = true;
                txb_PassWord.Enabled = true;
                lab_FtpTest.Enabled = true;
            }
            txb_LocalSave.Text = Properties.Settings.Default.LocalSavePath;
            txb_FTP.Text = Properties.Settings.Default.FtpIp;
            txb_Dir.Text = Properties.Settings.Default.FtpPath;
            txb_User.Text = Properties.Settings.Default.FtpUser;
            txb_PassWord.Text = Properties.Settings.Default.FtpPassWord;
        }

        //浏览本地路径
        private void btn_Scan_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (DialogResult.OK == fbd.ShowDialog())
            {
                txb_LocalSave.Text = fbd.SelectedPath;
            }
        }
        //测试FTP连接
        private void lab_FtpTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (FtpConnectTest())
            {
                MessageBox.Show("可以连接到FTP站点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("无法连接到FTP站点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //本地连接 radioButton
        private void radbtn_Local_CheckedChanged(object sender, EventArgs e)
        {
            if(radbtn_Local.Checked)
            {
                txb_LocalSave.Enabled = true;
                btn_Scan.Enabled = true;
            }
            else
            {
                txb_LocalSave.Enabled = false;
                btn_Scan.Enabled = false;
            }
        }
        //FTP radioButton
        private void radbtn_Ftp_CheckedChanged(object sender, EventArgs e)
        {
            if(radbtn_Ftp.Checked)
            {
                txb_FTP.Enabled = true;
                txb_Dir.Enabled = true;
                txb_User.Enabled = true;
                txb_PassWord.Enabled = true;
                lab_FtpTest.Enabled = true;
            }
            else
            {
                txb_FTP.Enabled = false;
                txb_Dir.Enabled = false;
                txb_User.Enabled = false;
                txb_PassWord.Enabled = false;
                lab_FtpTest.Enabled=false;
            }
        }
        //确定
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if(radbtn_Local.Checked)
            {
                string floder = txb_LocalSave.Text.Trim();
                //本地保存路径为空
                if(floder=="")
                {
                    MessageBox.Show("请选择本地保存路径！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //本地保存路径不存在在
                if(!Directory.Exists(floder))
                {
                    string message = string.Format("'{0}'路径不存在，是否创建？", floder);
                    if(DialogResult.Yes==MessageBox.Show(message,"提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information))
                    {
                        Directory.CreateDirectory(floder);
                    }
                    else
                    {
                        return;
                    }
                }
                Properties.Settings.Default.IsLocalSave = true;
                Properties.Settings.Default.LocalSavePath = txb_LocalSave.Text.Trim();
                Properties.Settings.Default.Save();
            }
            else if(radbtn_Ftp.Checked)
            {
                if(txb_FTP.Text=="")
                {
                    MessageBox.Show("请设置FTP站点IP！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if(!FtpConnectTest())
                {
                    MessageBox.Show("无法连接到FTP站点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Properties.Settings.Default.IsLocalSave = false;
                Properties.Settings.Default.FtpIp = txb_FTP.Text.Trim();
                Properties.Settings.Default.FtpPath = txb_Dir.Text.Trim();
                Properties.Settings.Default.FtpUser = txb_User.Text.Trim();
                Properties.Settings.Default.FtpPassWord = txb_PassWord.Text.Trim();
                Properties.Settings.Default.Save();
            }
            this.Close();
        }
        //取消
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        //FTP连接测试
        private bool FtpConnectTest()
        {
            
            string ip = "", dir = "", user = "", passWord = "";
            int port = 21;
            string[] ipAndPort = txb_FTP.Text.Trim().Split(":".ToCharArray());
            ip = ipAndPort[0];
            port = 21;
            if (ipAndPort.Length == 2)
                port = int.Parse(ipAndPort[1]);
            dir = txb_Dir.Text.Trim();
            user = txb_User.Text.Trim();
            passWord = txb_PassWord.Text.Trim();

            if (Utility.FtpOperation.FtpConnect(ip, port, dir, user, passWord))
            {
                //MessageBox.Show("可以连接到FTP站点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                return false;
                //MessageBox.Show("无法连接到FTP站点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
