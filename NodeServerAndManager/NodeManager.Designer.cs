namespace KangYiCollection
{
    partial class NodeManager
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeManager));
            this.timer_UpdateMachine = new System.Windows.Forms.Timer(this.components);
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txb_Message = new System.Windows.Forms.TextBox();
            this.rad_GZH = new System.Windows.Forms.RadioButton();
            this.rad_FSN = new System.Windows.Forms.RadioButton();
            this.txb_FilePath = new System.Windows.Forms.TextBox();
            this.btn_Scan = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmb_BusinessType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmb_CashBox2 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmb_Factory = new System.Windows.Forms.ComboBox();
            this.cmb_ATM2 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmb_Node = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv_machine = new System.Windows.Forms.DataGridView();
            this.kId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kIpAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lb_OpenMainDir = new System.Windows.Forms.Label();
            this.btn_Login = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lab_Version = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_PassWord = new System.Windows.Forms.TextBox();
            this.txb_User = new System.Windows.Forms.TextBox();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.contextMenuStrip_Main = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.MenuItem_ServerSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_FunctionSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_CRHReview = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Log = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_LogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.timer_ImportFSN = new System.Windows.Forms.Timer(this.components);
            this.timer_ExportCRH = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon_Tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_Tray = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.MenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_machine)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.contextMenuStrip_Main.SuspendLayout();
            this.contextMenuStrip_Tray.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_UpdateMachine
            // 
            this.timer_UpdateMachine.Interval = 600000;
            this.timer_UpdateMachine.Tick += new System.EventHandler(this.timer_UpdateMachine_Tick);
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Controls.Add(this.tabPage3);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 79);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(653, 469);
            this.materialTabControl1.TabIndex = 8;
            this.materialTabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_Ok);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.txb_Message);
            this.tabPage1.Controls.Add(this.rad_GZH);
            this.tabPage1.Controls.Add(this.rad_FSN);
            this.tabPage1.Controls.Add(this.txb_FilePath);
            this.tabPage1.Controls.Add(this.btn_Scan);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(645, 443);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "文件上传";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(298, 402);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 34;
            this.btn_Ok.Text = "确定";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(144, 254);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 33;
            this.label13.Text = "上传提示:";
            // 
            // txb_Message
            // 
            this.txb_Message.Location = new System.Drawing.Point(146, 272);
            this.txb_Message.Multiline = true;
            this.txb_Message.Name = "txb_Message";
            this.txb_Message.Size = new System.Drawing.Size(385, 120);
            this.txb_Message.TabIndex = 32;
            // 
            // rad_GZH
            // 
            this.rad_GZH.AutoSize = true;
            this.rad_GZH.Location = new System.Drawing.Point(84, 63);
            this.rad_GZH.Name = "rad_GZH";
            this.rad_GZH.Size = new System.Drawing.Size(65, 16);
            this.rad_GZH.TabIndex = 31;
            this.rad_GZH.Text = "GZH文件";
            this.rad_GZH.UseVisualStyleBackColor = true;
            this.rad_GZH.CheckedChanged += new System.EventHandler(this.rad_GZH_CheckedChanged);
            // 
            // rad_FSN
            // 
            this.rad_FSN.AutoSize = true;
            this.rad_FSN.Checked = true;
            this.rad_FSN.Location = new System.Drawing.Point(84, 40);
            this.rad_FSN.Name = "rad_FSN";
            this.rad_FSN.Size = new System.Drawing.Size(65, 16);
            this.rad_FSN.TabIndex = 30;
            this.rad_FSN.TabStop = true;
            this.rad_FSN.Text = "FSN文件";
            this.rad_FSN.UseVisualStyleBackColor = true;
            this.rad_FSN.CheckedChanged += new System.EventHandler(this.rad_FSN_CheckedChanged);
            // 
            // txb_FilePath
            // 
            this.txb_FilePath.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_FilePath.Location = new System.Drawing.Point(176, 40);
            this.txb_FilePath.Name = "txb_FilePath";
            this.txb_FilePath.Size = new System.Drawing.Size(299, 23);
            this.txb_FilePath.TabIndex = 28;
            // 
            // btn_Scan
            // 
            this.btn_Scan.Location = new System.Drawing.Point(481, 40);
            this.btn_Scan.Name = "btn_Scan";
            this.btn_Scan.Size = new System.Drawing.Size(75, 23);
            this.btn_Scan.TabIndex = 29;
            this.btn_Scan.Text = "选择文件";
            this.btn_Scan.UseVisualStyleBackColor = true;
            this.btn_Scan.Click += new System.EventHandler(this.btn_Scan_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.cmb_BusinessType);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmb_CashBox2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cmb_Factory);
            this.groupBox1.Controls.Add(this.cmb_ATM2);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cmb_Node);
            this.groupBox1.Location = new System.Drawing.Point(127, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(407, 154);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(16, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 14);
            this.label12.TabIndex = 12;
            this.label12.Text = "交易类型";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(16, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 14);
            this.label15.TabIndex = 22;
            this.label15.Text = "所属厂家";
            // 
            // cmb_BusinessType
            // 
            this.cmb_BusinessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_BusinessType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_BusinessType.FormattingEnabled = true;
            this.cmb_BusinessType.Items.AddRange(new object[] {
            "号码记录",
            "柜面取款",
            "柜面存款",
            "ATM配钞",
            "ATM清钞"});
            this.cmb_BusinessType.Location = new System.Drawing.Point(85, 68);
            this.cmb_BusinessType.Name = "cmb_BusinessType";
            this.cmb_BusinessType.Size = new System.Drawing.Size(314, 22);
            this.cmb_BusinessType.TabIndex = 9;
            this.cmb_BusinessType.SelectedIndexChanged += new System.EventHandler(this.cmb_BusinessType_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(16, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 13;
            this.label11.Text = " ATM编号";
            // 
            // cmb_CashBox2
            // 
            this.cmb_CashBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CashBox2.Enabled = false;
            this.cmb_CashBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_CashBox2.FormattingEnabled = true;
            this.cmb_CashBox2.Items.AddRange(new object[] {
            "号码记录",
            "付款",
            "收款",
            "柜面取款",
            "柜面存款",
            "ATM配钞",
            "ATM清钞"});
            this.cmb_CashBox2.Location = new System.Drawing.Point(86, 125);
            this.cmb_CashBox2.Name = "cmb_CashBox2";
            this.cmb_CashBox2.Size = new System.Drawing.Size(314, 22);
            this.cmb_CashBox2.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(9, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 14);
            this.label10.TabIndex = 14;
            this.label10.Text = " 钞箱编号";
            // 
            // cmb_Factory
            // 
            this.cmb_Factory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Factory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Factory.FormattingEnabled = true;
            this.cmb_Factory.Location = new System.Drawing.Point(85, 13);
            this.cmb_Factory.Name = "cmb_Factory";
            this.cmb_Factory.Size = new System.Drawing.Size(314, 22);
            this.cmb_Factory.TabIndex = 22;
            // 
            // cmb_ATM2
            // 
            this.cmb_ATM2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ATM2.Enabled = false;
            this.cmb_ATM2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_ATM2.FormattingEnabled = true;
            this.cmb_ATM2.Items.AddRange(new object[] {
            "号码记录",
            "付款",
            "收款",
            "柜面取款",
            "柜面存款",
            "ATM配钞",
            "ATM清钞"});
            this.cmb_ATM2.Location = new System.Drawing.Point(85, 96);
            this.cmb_ATM2.Name = "cmb_ATM2";
            this.cmb_ATM2.Size = new System.Drawing.Size(314, 22);
            this.cmb_ATM2.TabIndex = 18;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(16, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 14);
            this.label14.TabIndex = 19;
            this.label14.Text = "所属网点";
            // 
            // cmb_Node
            // 
            this.cmb_Node.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Node.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Node.FormattingEnabled = true;
            this.cmb_Node.Location = new System.Drawing.Point(85, 40);
            this.cmb_Node.Name = "cmb_Node";
            this.cmb_Node.Size = new System.Drawing.Size(314, 22);
            this.cmb_Node.TabIndex = 20;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_machine);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(645, 443);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设备监控";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv_machine
            // 
            this.dgv_machine.AllowUserToAddRows = false;
            this.dgv_machine.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_machine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_machine.ColumnHeadersHeight = 24;
            this.dgv_machine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_machine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kId,
            this.kIpAddress,
            this.kUpdateTime,
            this.kStatus});
            this.dgv_machine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_machine.Location = new System.Drawing.Point(3, 3);
            this.dgv_machine.Name = "dgv_machine";
            this.dgv_machine.ReadOnly = true;
            this.dgv_machine.RowHeadersVisible = false;
            this.dgv_machine.RowTemplate.Height = 23;
            this.dgv_machine.Size = new System.Drawing.Size(639, 437);
            this.dgv_machine.TabIndex = 1;
            // 
            // kId
            // 
            this.kId.DataPropertyName = "kId";
            this.kId.HeaderText = "kId";
            this.kId.Name = "kId";
            this.kId.ReadOnly = true;
            this.kId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kId.Visible = false;
            this.kId.Width = 60;
            // 
            // kIpAddress
            // 
            this.kIpAddress.DataPropertyName = "kIpAddress";
            this.kIpAddress.FillWeight = 200F;
            this.kIpAddress.HeaderText = "设备IP";
            this.kIpAddress.MinimumWidth = 160;
            this.kIpAddress.Name = "kIpAddress";
            this.kIpAddress.ReadOnly = true;
            this.kIpAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kIpAddress.Width = 200;
            // 
            // kUpdateTime
            // 
            this.kUpdateTime.DataPropertyName = "kUpdateTime";
            this.kUpdateTime.FillWeight = 200F;
            this.kUpdateTime.HeaderText = "最近上传时间";
            this.kUpdateTime.MinimumWidth = 150;
            this.kUpdateTime.Name = "kUpdateTime";
            this.kUpdateTime.ReadOnly = true;
            this.kUpdateTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kUpdateTime.Width = 200;
            // 
            // kStatus
            // 
            this.kStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kStatus.DataPropertyName = "kStatus";
            this.kStatus.HeaderText = "状态";
            this.kStatus.Name = "kStatus";
            this.kStatus.ReadOnly = true;
            this.kStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lb_OpenMainDir);
            this.tabPage3.Controls.Add(this.btn_Login);
            this.tabPage3.Controls.Add(this.lab_Version);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.txb_PassWord);
            this.tabPage3.Controls.Add(this.txb_User);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(645, 443);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "用户登录";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lb_OpenMainDir
            // 
            this.lb_OpenMainDir.AutoSize = true;
            this.lb_OpenMainDir.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_OpenMainDir.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lb_OpenMainDir.Location = new System.Drawing.Point(527, 397);
            this.lb_OpenMainDir.Name = "lb_OpenMainDir";
            this.lb_OpenMainDir.Size = new System.Drawing.Size(77, 14);
            this.lb_OpenMainDir.TabIndex = 17;
            this.lb_OpenMainDir.Text = "打开主目录";
            this.lb_OpenMainDir.Click += new System.EventHandler(this.lb_OpenMainDir_Click);
            // 
            // btn_Login
            // 
            this.btn_Login.Depth = 0;
            this.btn_Login.Location = new System.Drawing.Point(277, 255);
            this.btn_Login.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Primary = true;
            this.btn_Login.Size = new System.Drawing.Size(115, 39);
            this.btn_Login.TabIndex = 16;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // lab_Version
            // 
            this.lab_Version.AutoSize = true;
            this.lab_Version.Location = new System.Drawing.Point(19, 411);
            this.lab_Version.Name = "lab_Version";
            this.lab_Version.Size = new System.Drawing.Size(101, 12);
            this.lab_Version.TabIndex = 13;
            this.lab_Version.Text = "软件版本:1.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(216, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(216, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "用户";
            // 
            // txb_PassWord
            // 
            this.txb_PassWord.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_PassWord.Location = new System.Drawing.Point(262, 206);
            this.txb_PassWord.Name = "txb_PassWord";
            this.txb_PassWord.PasswordChar = '*';
            this.txb_PassWord.Size = new System.Drawing.Size(157, 26);
            this.txb_PassWord.TabIndex = 9;
            this.txb_PassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txb_PassWord_KeyDown);
            // 
            // txb_User
            // 
            this.txb_User.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_User.Location = new System.Drawing.Point(262, 144);
            this.txb_User.Name = "txb_User";
            this.txb_User.Size = new System.Drawing.Size(157, 26);
            this.txb_User.TabIndex = 8;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BackColor = System.Drawing.Color.White;
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.materialTabSelector1.ForeColor = System.Drawing.SystemColors.Control;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 28);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(680, 45);
            this.materialTabSelector1.TabIndex = 1;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // contextMenuStrip_Main
            // 
            this.contextMenuStrip_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.contextMenuStrip_Main.Depth = 0;
            this.contextMenuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_ServerSetting,
            this.MenuItem_FunctionSetting,
            this.MenuItem_CRHReview,
            this.MenuItem_Log,
            this.MenuItem_LogOut});
            this.contextMenuStrip_Main.MouseState = MaterialSkin.MouseState.HOVER;
            this.contextMenuStrip_Main.Name = "materialContextMenuStrip1";
            this.contextMenuStrip_Main.Size = new System.Drawing.Size(137, 114);
            // 
            // MenuItem_ServerSetting
            // 
            this.MenuItem_ServerSetting.Name = "MenuItem_ServerSetting";
            this.MenuItem_ServerSetting.Size = new System.Drawing.Size(136, 22);
            this.MenuItem_ServerSetting.Text = "服务器设置";
            this.MenuItem_ServerSetting.Click += new System.EventHandler(this.MenuItem_ServerSetting_Click);
            // 
            // MenuItem_FunctionSetting
            // 
            this.MenuItem_FunctionSetting.Name = "MenuItem_FunctionSetting";
            this.MenuItem_FunctionSetting.Size = new System.Drawing.Size(136, 22);
            this.MenuItem_FunctionSetting.Text = "功能设置";
            this.MenuItem_FunctionSetting.Click += new System.EventHandler(this.MenuItem_FunctionSetting_Click);
            // 
            // MenuItem_CRHReview
            // 
            this.MenuItem_CRHReview.Name = "MenuItem_CRHReview";
            this.MenuItem_CRHReview.Size = new System.Drawing.Size(136, 22);
            this.MenuItem_CRHReview.Text = "CRH查看";
            this.MenuItem_CRHReview.Click += new System.EventHandler(this.MenuItem_CRHReview_Click);
            // 
            // MenuItem_Log
            // 
            this.MenuItem_Log.Name = "MenuItem_Log";
            this.MenuItem_Log.Size = new System.Drawing.Size(136, 22);
            this.MenuItem_Log.Text = "日志";
            this.MenuItem_Log.Click += new System.EventHandler(this.MenuItem_Log_Click);
            // 
            // MenuItem_LogOut
            // 
            this.MenuItem_LogOut.Name = "MenuItem_LogOut";
            this.MenuItem_LogOut.Size = new System.Drawing.Size(136, 22);
            this.MenuItem_LogOut.Text = "注销";
            this.MenuItem_LogOut.Click += new System.EventHandler(this.MenuItem_LogOut_Click);
            // 
            // timer_ImportFSN
            // 
            this.timer_ImportFSN.Enabled = true;
            this.timer_ImportFSN.Interval = 10000;
            // 
            // timer_ExportCRH
            // 
            this.timer_ExportCRH.Enabled = true;
            this.timer_ExportCRH.Interval = 3000;
            this.timer_ExportCRH.Tick += new System.EventHandler(this.timer_ExportCRH_Tick);
            // 
            // notifyIcon_Tray
            // 
            this.notifyIcon_Tray.ContextMenuStrip = this.contextMenuStrip_Tray;
            this.notifyIcon_Tray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_Tray.Icon")));
            this.notifyIcon_Tray.Text = "康艺网点管理端";
            this.notifyIcon_Tray.Visible = true;
            this.notifyIcon_Tray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip_Tray
            // 
            this.contextMenuStrip_Tray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.contextMenuStrip_Tray.Depth = 0;
            this.contextMenuStrip_Tray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Exit});
            this.contextMenuStrip_Tray.MouseState = MaterialSkin.MouseState.HOVER;
            this.contextMenuStrip_Tray.Name = "contextMenuStrip_Tray";
            this.contextMenuStrip_Tray.Size = new System.Drawing.Size(101, 26);
            // 
            // MenuItem_Exit
            // 
            this.MenuItem_Exit.Name = "MenuItem_Exit";
            this.MenuItem_Exit.Size = new System.Drawing.Size(100, 22);
            this.MenuItem_Exit.Text = "退出";
            this.MenuItem_Exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
            // 
            // NodeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 549);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.materialTabSelector1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NodeManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.NodeManager_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NodeManager_FormClosing);
            this.Load += new System.EventHandler(this.NodeManager_Load);
            this.Resize += new System.EventHandler(this.NodeManager_Resize);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_machine)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.contextMenuStrip_Main.ResumeLayout(false);
            this.contextMenuStrip_Tray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_UpdateMachine;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txb_Message;
        private System.Windows.Forms.RadioButton rad_GZH;
        private System.Windows.Forms.RadioButton rad_FSN;
        private System.Windows.Forms.TextBox txb_FilePath;
        private System.Windows.Forms.Button btn_Scan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmb_BusinessType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_CashBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_Factory;
        private System.Windows.Forms.ComboBox cmb_ATM2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmb_Node;
        private System.Windows.Forms.DataGridView dgv_machine;
        private System.Windows.Forms.DataGridViewTextBoxColumn kId;
        private System.Windows.Forms.DataGridViewTextBoxColumn kIpAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn kUpdateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn kStatus;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lab_Version;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_PassWord;
        private System.Windows.Forms.TextBox txb_User;
        private MaterialSkin.Controls.MaterialRaisedButton btn_Login;
        private MaterialSkin.Controls.MaterialContextMenuStrip contextMenuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_ServerSetting;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Log;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_LogOut;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_FunctionSetting;
        private System.Windows.Forms.Timer timer_ImportFSN;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_CRHReview;
        private System.Windows.Forms.Timer timer_ExportCRH;
        private System.Windows.Forms.NotifyIcon notifyIcon_Tray;
        private MaterialSkin.Controls.MaterialContextMenuStrip contextMenuStrip_Tray;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Exit;
        private System.Windows.Forms.Label lb_OpenMainDir;
    }
}

