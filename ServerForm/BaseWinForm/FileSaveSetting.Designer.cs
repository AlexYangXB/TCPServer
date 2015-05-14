namespace ServerForm.BaseWinForm
{
    partial class FileSaveSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lab_FtpTest = new System.Windows.Forms.LinkLabel();
            this.txb_PassWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_User = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txb_Dir = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txb_FTP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_Scan = new System.Windows.Forms.Button();
            this.txb_LocalSave = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radbtn_Ftp = new System.Windows.Forms.RadioButton();
            this.radbtn_Local = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btn_Cancel);
            this.groupBox1.Controls.Add(this.btn_OK);
            this.groupBox1.Controls.Add(this.lab_FtpTest);
            this.groupBox1.Controls.Add(this.txb_PassWord);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txb_User);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txb_Dir);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txb_FTP);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btn_Scan);
            this.groupBox1.Controls.Add(this.txb_LocalSave);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radbtn_Ftp);
            this.groupBox1.Controls.Add(this.radbtn_Local);
            this.groupBox1.Location = new System.Drawing.Point(13, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 234);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(268, 199);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 45;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(187, 199);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 44;
            this.btn_OK.Text = "保存";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // lab_FtpTest
            // 
            this.lab_FtpTest.AutoSize = true;
            this.lab_FtpTest.Location = new System.Drawing.Point(445, 151);
            this.lab_FtpTest.Name = "lab_FtpTest";
            this.lab_FtpTest.Size = new System.Drawing.Size(53, 12);
            this.lab_FtpTest.TabIndex = 43;
            this.lab_FtpTest.TabStop = true;
            this.lab_FtpTest.Text = "测试连接";
            this.lab_FtpTest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lab_FtpTest_LinkClicked);
            // 
            // txb_PassWord
            // 
            this.txb_PassWord.Location = new System.Drawing.Point(294, 161);
            this.txb_PassWord.Name = "txb_PassWord";
            this.txb_PassWord.Size = new System.Drawing.Size(135, 21);
            this.txb_PassWord.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "密码";
            // 
            // txb_User
            // 
            this.txb_User.Location = new System.Drawing.Point(98, 160);
            this.txb_User.Name = "txb_User";
            this.txb_User.Size = new System.Drawing.Size(135, 21);
            this.txb_User.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "用户名";
            // 
            // txb_Dir
            // 
            this.txb_Dir.Location = new System.Drawing.Point(294, 133);
            this.txb_Dir.Name = "txb_Dir";
            this.txb_Dir.Size = new System.Drawing.Size(135, 21);
            this.txb_Dir.TabIndex = 38;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(250, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "文件夹";
            // 
            // txb_FTP
            // 
            this.txb_FTP.Location = new System.Drawing.Point(98, 132);
            this.txb_FTP.Name = "txb_FTP";
            this.txb_FTP.Size = new System.Drawing.Size(135, 21);
            this.txb_FTP.TabIndex = 36;
            this.txb_FTP.Tag = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "FTP站点IP";
            // 
            // btn_Scan
            // 
            this.btn_Scan.Location = new System.Drawing.Point(447, 60);
            this.btn_Scan.Name = "btn_Scan";
            this.btn_Scan.Size = new System.Drawing.Size(75, 23);
            this.btn_Scan.TabIndex = 4;
            this.btn_Scan.Text = "浏览";
            this.btn_Scan.UseVisualStyleBackColor = true;
            this.btn_Scan.Click += new System.EventHandler(this.btn_Scan_Click);
            // 
            // txb_LocalSave
            // 
            this.txb_LocalSave.Location = new System.Drawing.Point(125, 61);
            this.txb_LocalSave.Name = "txb_LocalSave";
            this.txb_LocalSave.Size = new System.Drawing.Size(304, 21);
            this.txb_LocalSave.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "本地保存路径";
            // 
            // radbtn_Ftp
            // 
            this.radbtn_Ftp.AutoSize = true;
            this.radbtn_Ftp.Location = new System.Drawing.Point(10, 106);
            this.radbtn_Ftp.Name = "radbtn_Ftp";
            this.radbtn_Ftp.Size = new System.Drawing.Size(41, 16);
            this.radbtn_Ftp.TabIndex = 1;
            this.radbtn_Ftp.TabStop = true;
            this.radbtn_Ftp.Text = "FTP";
            this.radbtn_Ftp.UseVisualStyleBackColor = true;
            this.radbtn_Ftp.CheckedChanged += new System.EventHandler(this.radbtn_Ftp_CheckedChanged);
            // 
            // radbtn_Local
            // 
            this.radbtn_Local.AutoSize = true;
            this.radbtn_Local.Location = new System.Drawing.Point(10, 31);
            this.radbtn_Local.Name = "radbtn_Local";
            this.radbtn_Local.Size = new System.Drawing.Size(71, 16);
            this.radbtn_Local.TabIndex = 0;
            this.radbtn_Local.TabStop = true;
            this.radbtn_Local.Text = "本地保存";
            this.radbtn_Local.UseVisualStyleBackColor = true;
            this.radbtn_Local.CheckedChanged += new System.EventHandler(this.radbtn_Local_CheckedChanged);
            // 
            // FileSaveSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(556, 247);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileSaveSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件保存设置";
            this.Load += new System.EventHandler(this.FileSaveSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.LinkLabel lab_FtpTest;
        private System.Windows.Forms.TextBox txb_PassWord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_User;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_Dir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txb_FTP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_Scan;
        private System.Windows.Forms.TextBox txb_LocalSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radbtn_Ftp;
        private System.Windows.Forms.RadioButton radbtn_Local;
    }
}