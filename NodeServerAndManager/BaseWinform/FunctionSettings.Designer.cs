namespace KangYiCollection.BaseWinform
{
    partial class FunctionSettings
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
            this.tab_OtherFactoryAccess = new System.Windows.Forms.TabPage();
            this.lab_FactoryDir = new System.Windows.Forms.Label();
            this.btn_FactoryDirLook = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txb_OtherFactoryAccessDir = new System.Windows.Forms.TextBox();
            this.chk_FactoryAccess = new MaterialSkin.Controls.MaterialCheckBox();
            this.tab_CRHExport = new System.Windows.Forms.TabPage();
            this.btn_CRHExport = new MaterialSkin.Controls.MaterialRaisedButton();
            this.gb_CRH1 = new System.Windows.Forms.GroupBox();
            this.dt_CRHStartTime = new System.Windows.Forms.DateTimePicker();
            this.chk_CRHExport = new MaterialSkin.Controls.MaterialCheckBox();
            this.gb_CRH3 = new System.Windows.Forms.GroupBox();
            this.lab_CRHDir = new System.Windows.Forms.Label();
            this.btn_CRHDir = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txb_CHRDir = new System.Windows.Forms.TextBox();
            this.gb_CRH2 = new System.Windows.Forms.GroupBox();
            this.rb_CRHYesterday = new MaterialSkin.Controls.MaterialRadioButton();
            this.rb_CRHToday = new MaterialSkin.Controls.MaterialRadioButton();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1.SuspendLayout();
            this.tab_OtherFactoryAccess.SuspendLayout();
            this.tab_CRHExport.SuspendLayout();
            this.gb_CRH1.SuspendLayout();
            this.gb_CRH3.SuspendLayout();
            this.gb_CRH2.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tab_OtherFactoryAccess);
            this.materialTabControl1.Controls.Add(this.tab_CRHExport);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 122);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(703, 356);
            this.materialTabControl1.TabIndex = 9;
            // 
            // tab_OtherFactoryAccess
            // 
            this.tab_OtherFactoryAccess.Controls.Add(this.lab_FactoryDir);
            this.tab_OtherFactoryAccess.Controls.Add(this.btn_FactoryDirLook);
            this.tab_OtherFactoryAccess.Controls.Add(this.txb_OtherFactoryAccessDir);
            this.tab_OtherFactoryAccess.Controls.Add(this.chk_FactoryAccess);
            this.tab_OtherFactoryAccess.Location = new System.Drawing.Point(4, 22);
            this.tab_OtherFactoryAccess.Name = "tab_OtherFactoryAccess";
            this.tab_OtherFactoryAccess.Size = new System.Drawing.Size(695, 330);
            this.tab_OtherFactoryAccess.TabIndex = 0;
            this.tab_OtherFactoryAccess.Text = "他厂接入";
            this.tab_OtherFactoryAccess.UseVisualStyleBackColor = true;
            // 
            // lab_FactoryDir
            // 
            this.lab_FactoryDir.AutoSize = true;
            this.lab_FactoryDir.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_FactoryDir.Location = new System.Drawing.Point(63, 76);
            this.lab_FactoryDir.Name = "lab_FactoryDir";
            this.lab_FactoryDir.Size = new System.Drawing.Size(63, 14);
            this.lab_FactoryDir.TabIndex = 11;
            this.lab_FactoryDir.Text = "文件夹：";
            // 
            // btn_FactoryDirLook
            // 
            this.btn_FactoryDirLook.Depth = 0;
            this.btn_FactoryDirLook.Location = new System.Drawing.Point(505, 65);
            this.btn_FactoryDirLook.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_FactoryDirLook.Name = "btn_FactoryDirLook";
            this.btn_FactoryDirLook.Primary = true;
            this.btn_FactoryDirLook.Size = new System.Drawing.Size(75, 38);
            this.btn_FactoryDirLook.TabIndex = 10;
            this.btn_FactoryDirLook.Text = "浏览";
            this.btn_FactoryDirLook.UseVisualStyleBackColor = true;
            this.btn_FactoryDirLook.Click += new System.EventHandler(this.btn_FactoryDirLook_Click);
            // 
            // txb_OtherFactoryAccessDir
            // 
            this.txb_OtherFactoryAccessDir.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_OtherFactoryAccessDir.Location = new System.Drawing.Point(132, 71);
            this.txb_OtherFactoryAccessDir.Name = "txb_OtherFactoryAccessDir";
            this.txb_OtherFactoryAccessDir.Size = new System.Drawing.Size(350, 26);
            this.txb_OtherFactoryAccessDir.TabIndex = 9;
            // 
            // chk_FactoryAccess
            // 
            this.chk_FactoryAccess.AutoSize = true;
            this.chk_FactoryAccess.Depth = 0;
            this.chk_FactoryAccess.Font = new System.Drawing.Font("Roboto", 10F);
            this.chk_FactoryAccess.Location = new System.Drawing.Point(45, 23);
            this.chk_FactoryAccess.Margin = new System.Windows.Forms.Padding(0);
            this.chk_FactoryAccess.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chk_FactoryAccess.MouseState = MaterialSkin.MouseState.HOVER;
            this.chk_FactoryAccess.Name = "chk_FactoryAccess";
            this.chk_FactoryAccess.Ripple = true;
            this.chk_FactoryAccess.Size = new System.Drawing.Size(267, 30);
            this.chk_FactoryAccess.TabIndex = 0;
            this.chk_FactoryAccess.Text = "将指定文件夹下的FSN文件导入系统";
            this.chk_FactoryAccess.UseVisualStyleBackColor = true;
            // 
            // tab_CRHExport
            // 
            this.tab_CRHExport.Controls.Add(this.btn_CRHExport);
            this.tab_CRHExport.Controls.Add(this.gb_CRH1);
            this.tab_CRHExport.Controls.Add(this.gb_CRH3);
            this.tab_CRHExport.Controls.Add(this.gb_CRH2);
            this.tab_CRHExport.Location = new System.Drawing.Point(4, 22);
            this.tab_CRHExport.Name = "tab_CRHExport";
            this.tab_CRHExport.Size = new System.Drawing.Size(695, 330);
            this.tab_CRHExport.TabIndex = 1;
            this.tab_CRHExport.Text = "CRH导出";
            this.tab_CRHExport.UseVisualStyleBackColor = true;
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
            // gb_CRH1
            // 
            this.gb_CRH1.Controls.Add(this.dt_CRHStartTime);
            this.gb_CRH1.Controls.Add(this.chk_CRHExport);
            this.gb_CRH1.Location = new System.Drawing.Point(62, 31);
            this.gb_CRH1.Name = "gb_CRH1";
            this.gb_CRH1.Size = new System.Drawing.Size(207, 125);
            this.gb_CRH1.TabIndex = 5;
            this.gb_CRH1.TabStop = false;
            this.gb_CRH1.Text = "功能设置";
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
            // gb_CRH3
            // 
            this.gb_CRH3.Controls.Add(this.lab_CRHDir);
            this.gb_CRH3.Controls.Add(this.btn_CRHDir);
            this.gb_CRH3.Controls.Add(this.txb_CHRDir);
            this.gb_CRH3.Location = new System.Drawing.Point(62, 175);
            this.gb_CRH3.Name = "gb_CRH3";
            this.gb_CRH3.Size = new System.Drawing.Size(559, 95);
            this.gb_CRH3.TabIndex = 4;
            this.gb_CRH3.TabStop = false;
            this.gb_CRH3.Text = "自动导出路径设置";
            // 
            // lab_CRHDir
            // 
            this.lab_CRHDir.AutoSize = true;
            this.lab_CRHDir.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_CRHDir.Location = new System.Drawing.Point(22, 44);
            this.lab_CRHDir.Name = "lab_CRHDir";
            this.lab_CRHDir.Size = new System.Drawing.Size(63, 14);
            this.lab_CRHDir.TabIndex = 15;
            this.lab_CRHDir.Text = "文件夹：";
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
            // txb_CHRDir
            // 
            this.txb_CHRDir.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_CHRDir.Location = new System.Drawing.Point(93, 39);
            this.txb_CHRDir.Name = "txb_CHRDir";
            this.txb_CHRDir.Size = new System.Drawing.Size(324, 26);
            this.txb_CHRDir.TabIndex = 12;
            // 
            // gb_CRH2
            // 
            this.gb_CRH2.Controls.Add(this.rb_CRHYesterday);
            this.gb_CRH2.Controls.Add(this.rb_CRHToday);
            this.gb_CRH2.Location = new System.Drawing.Point(291, 37);
            this.gb_CRH2.Name = "gb_CRH2";
            this.gb_CRH2.Size = new System.Drawing.Size(317, 119);
            this.gb_CRH2.TabIndex = 3;
            this.gb_CRH2.TabStop = false;
            this.gb_CRH2.Text = "数据时间范围设置";
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
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 55);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(850, 44);
            this.materialTabSelector1.TabIndex = 10;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // FunctionSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 482);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FunctionSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SystemSettings_FormClosing);
            this.Load += new System.EventHandler(this.SystemSettings_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.tab_OtherFactoryAccess.ResumeLayout(false);
            this.tab_OtherFactoryAccess.PerformLayout();
            this.tab_CRHExport.ResumeLayout(false);
            this.gb_CRH1.ResumeLayout(false);
            this.gb_CRH1.PerformLayout();
            this.gb_CRH3.ResumeLayout(false);
            this.gb_CRH3.PerformLayout();
            this.gb_CRH2.ResumeLayout(false);
            this.gb_CRH2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.TabPage tab_OtherFactoryAccess;
        private MaterialSkin.Controls.MaterialCheckBox chk_FactoryAccess;
        private MaterialSkin.Controls.MaterialRaisedButton btn_FactoryDirLook;
        private System.Windows.Forms.TextBox txb_OtherFactoryAccessDir;
        private System.Windows.Forms.TabPage tab_CRHExport;
        private MaterialSkin.Controls.MaterialRadioButton rb_CRHToday;
        private MaterialSkin.Controls.MaterialRadioButton rb_CRHYesterday;
        private MaterialSkin.Controls.MaterialCheckBox chk_CRHExport;
        private System.Windows.Forms.GroupBox gb_CRH2;
        private System.Windows.Forms.GroupBox gb_CRH3;
        private MaterialSkin.Controls.MaterialRaisedButton btn_CRHDir;
        private System.Windows.Forms.TextBox txb_CHRDir;
        private System.Windows.Forms.GroupBox gb_CRH1;
        private MaterialSkin.Controls.MaterialRaisedButton btn_CRHExport;
        private System.Windows.Forms.DateTimePicker dt_CRHStartTime;
        private System.Windows.Forms.Label lab_FactoryDir;
        private System.Windows.Forms.Label lab_CRHDir;

    }
}