namespace NodeServerAndManager.BaseWinform
{
    partial class SystemSettings
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
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_OtherFactoryAccess = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txb_OtherFactoryAccessDir = new System.Windows.Forms.TextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.chk_OtherFactoryAccess = new MaterialSkin.Controls.MaterialCheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_CRHExport = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dt_CRHStartTime = new System.Windows.Forms.DateTimePicker();
            this.chk_CRHExport = new MaterialSkin.Controls.MaterialCheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_CRHDir = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.txb_CHRDir = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_CRHYesterday = new MaterialSkin.Controls.MaterialRadioButton();
            this.rb_CRHToday = new MaterialSkin.Controls.MaterialRadioButton();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 122);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(703, 356);
            this.materialTabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_OtherFactoryAccess);
            this.tabPage1.Controls.Add(this.txb_OtherFactoryAccessDir);
            this.tabPage1.Controls.Add(this.materialLabel1);
            this.tabPage1.Controls.Add(this.chk_OtherFactoryAccess);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(695, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "他厂接入";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_OtherFactoryAccess
            // 
            this.btn_OtherFactoryAccess.Depth = 0;
            this.btn_OtherFactoryAccess.Location = new System.Drawing.Point(505, 65);
            this.btn_OtherFactoryAccess.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_OtherFactoryAccess.Name = "btn_OtherFactoryAccess";
            this.btn_OtherFactoryAccess.Primary = true;
            this.btn_OtherFactoryAccess.Size = new System.Drawing.Size(75, 38);
            this.btn_OtherFactoryAccess.TabIndex = 10;
            this.btn_OtherFactoryAccess.Text = "浏览";
            this.btn_OtherFactoryAccess.UseVisualStyleBackColor = true;
            this.btn_OtherFactoryAccess.Click += new System.EventHandler(this.btn_OtherFactoryAccess_Click);
            // 
            // txb_OtherFactoryAccessDir
            // 
            this.txb_OtherFactoryAccessDir.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_OtherFactoryAccessDir.Location = new System.Drawing.Point(132, 71);
            this.txb_OtherFactoryAccessDir.Name = "txb_OtherFactoryAccessDir";
            this.txb_OtherFactoryAccessDir.Size = new System.Drawing.Size(350, 26);
            this.txb_OtherFactoryAccessDir.TabIndex = 9;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(60, 78);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(57, 19);
            this.materialLabel1.TabIndex = 1;
            this.materialLabel1.Text = "文件夹";
            // 
            // chk_OtherFactoryAccess
            // 
            this.chk_OtherFactoryAccess.AutoSize = true;
            this.chk_OtherFactoryAccess.Depth = 0;
            this.chk_OtherFactoryAccess.Font = new System.Drawing.Font("Roboto", 10F);
            this.chk_OtherFactoryAccess.Location = new System.Drawing.Point(45, 23);
            this.chk_OtherFactoryAccess.Margin = new System.Windows.Forms.Padding(0);
            this.chk_OtherFactoryAccess.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chk_OtherFactoryAccess.MouseState = MaterialSkin.MouseState.HOVER;
            this.chk_OtherFactoryAccess.Name = "chk_OtherFactoryAccess";
            this.chk_OtherFactoryAccess.Ripple = true;
            this.chk_OtherFactoryAccess.Size = new System.Drawing.Size(267, 30);
            this.chk_OtherFactoryAccess.TabIndex = 0;
            this.chk_OtherFactoryAccess.Text = "将指定文件夹下的FSN文件导入系统";
            this.chk_OtherFactoryAccess.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_CRHExport);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(695, 330);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CRH导出";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_CRHExport
            // 
            this.btn_CRHExport.Depth = 0;
            this.btn_CRHExport.Location = new System.Drawing.Point(270, 276);
            this.btn_CRHExport.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_CRHExport.Name = "btn_CRHExport";
            this.btn_CRHExport.Primary = true;
            this.btn_CRHExport.Size = new System.Drawing.Size(131, 40);
            this.btn_CRHExport.TabIndex = 14;
            this.btn_CRHExport.Text = "手动导出";
            this.btn_CRHExport.UseVisualStyleBackColor = true;
            this.btn_CRHExport.Click += new System.EventHandler(this.btn_CRHExport_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dt_CRHStartTime);
            this.groupBox3.Controls.Add(this.chk_CRHExport);
            this.groupBox3.Location = new System.Drawing.Point(62, 31);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(207, 125);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "功能设置";
            // 
            // dt_CRHStartTime
            // 
            this.dt_CRHStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dt_CRHStartTime.Location = new System.Drawing.Point(50, 86);
            this.dt_CRHStartTime.Name = "dt_CRHStartTime";
            this.dt_CRHStartTime.ShowUpDown = true;
            this.dt_CRHStartTime.Size = new System.Drawing.Size(118, 21);
            this.dt_CRHStartTime.TabIndex = 3;
            // 
            // chk_CRHExport
            // 
            this.chk_CRHExport.AutoSize = true;
            this.chk_CRHExport.Depth = 0;
            this.chk_CRHExport.Font = new System.Drawing.Font("Roboto", 10F);
            this.chk_CRHExport.Location = new System.Drawing.Point(25, 43);
            this.chk_CRHExport.Margin = new System.Windows.Forms.Padding(0);
            this.chk_CRHExport.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chk_CRHExport.MouseState = MaterialSkin.MouseState.HOVER;
            this.chk_CRHExport.Name = "chk_CRHExport";
            this.chk_CRHExport.Ripple = true;
            this.chk_CRHExport.Size = new System.Drawing.Size(121, 30);
            this.chk_CRHExport.TabIndex = 2;
            this.chk_CRHExport.Text = "启用自动导出";
            this.chk_CRHExport.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_CRHDir);
            this.groupBox2.Controls.Add(this.materialLabel2);
            this.groupBox2.Controls.Add(this.txb_CHRDir);
            this.groupBox2.Location = new System.Drawing.Point(62, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(559, 95);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "自动导出路径设置";
            // 
            // btn_CRHDir
            // 
            this.btn_CRHDir.Depth = 0;
            this.btn_CRHDir.Location = new System.Drawing.Point(449, 29);
            this.btn_CRHDir.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_CRHDir.Name = "btn_CRHDir";
            this.btn_CRHDir.Primary = true;
            this.btn_CRHDir.Size = new System.Drawing.Size(75, 36);
            this.btn_CRHDir.TabIndex = 13;
            this.btn_CRHDir.Text = "浏览";
            this.btn_CRHDir.UseVisualStyleBackColor = true;
            this.btn_CRHDir.Click += new System.EventHandler(this.btn_CRHDir_Click);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(21, 46);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(57, 19);
            this.materialLabel2.TabIndex = 11;
            this.materialLabel2.Text = "文件夹";
            // 
            // txb_CHRDir
            // 
            this.txb_CHRDir.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_CHRDir.Location = new System.Drawing.Point(93, 39);
            this.txb_CHRDir.Name = "txb_CHRDir";
            this.txb_CHRDir.Size = new System.Drawing.Size(324, 26);
            this.txb_CHRDir.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_CRHYesterday);
            this.groupBox1.Controls.Add(this.rb_CRHToday);
            this.groupBox1.Location = new System.Drawing.Point(291, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 119);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据时间范围设置";
            // 
            // rb_CRHYesterday
            // 
            this.rb_CRHYesterday.AutoSize = true;
            this.rb_CRHYesterday.Depth = 0;
            this.rb_CRHYesterday.Font = new System.Drawing.Font("Roboto", 10F);
            this.rb_CRHYesterday.Location = new System.Drawing.Point(40, 45);
            this.rb_CRHYesterday.Margin = new System.Windows.Forms.Padding(0);
            this.rb_CRHYesterday.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rb_CRHYesterday.MouseState = MaterialSkin.MouseState.HOVER;
            this.rb_CRHYesterday.Name = "rb_CRHYesterday";
            this.rb_CRHYesterday.Ripple = true;
            this.rb_CRHYesterday.Size = new System.Drawing.Size(59, 30);
            this.rb_CRHYesterday.TabIndex = 0;
            this.rb_CRHYesterday.TabStop = true;
            this.rb_CRHYesterday.Text = "昨天";
            this.rb_CRHYesterday.UseVisualStyleBackColor = true;
            // 
            // rb_CRHToday
            // 
            this.rb_CRHToday.AutoSize = true;
            this.rb_CRHToday.Depth = 0;
            this.rb_CRHToday.Font = new System.Drawing.Font("Roboto", 10F);
            this.rb_CRHToday.Location = new System.Drawing.Point(129, 45);
            this.rb_CRHToday.Margin = new System.Windows.Forms.Padding(0);
            this.rb_CRHToday.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rb_CRHToday.MouseState = MaterialSkin.MouseState.HOVER;
            this.rb_CRHToday.Name = "rb_CRHToday";
            this.rb_CRHToday.Ripple = true;
            this.rb_CRHToday.Size = new System.Drawing.Size(59, 30);
            this.rb_CRHToday.TabIndex = 1;
            this.rb_CRHToday.TabStop = true;
            this.rb_CRHToday.Text = "今天";
            this.rb_CRHToday.UseVisualStyleBackColor = true;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BackColor = System.Drawing.Color.White;
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.materialTabSelector1.ForeColor = System.Drawing.SystemColors.Control;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 62);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(850, 41);
            this.materialTabSelector1.TabIndex = 10;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // SystemSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 482);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SystemSettings_FormClosing);
            this.Load += new System.EventHandler(this.SystemSettings_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.TabPage tabPage1;
        private MaterialSkin.Controls.MaterialCheckBox chk_OtherFactoryAccess;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialRaisedButton btn_OtherFactoryAccess;
        private System.Windows.Forms.TextBox txb_OtherFactoryAccessDir;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialRadioButton rb_CRHToday;
        private MaterialSkin.Controls.MaterialRadioButton rb_CRHYesterday;
        private MaterialSkin.Controls.MaterialCheckBox chk_CRHExport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MaterialSkin.Controls.MaterialRaisedButton btn_CRHDir;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.TextBox txb_CHRDir;
        private System.Windows.Forms.GroupBox groupBox3;
        private MaterialSkin.Controls.MaterialRaisedButton btn_CRHExport;
        private System.Windows.Forms.DateTimePicker dt_CRHStartTime;

    }
}