namespace NodeServerAndManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeManager));
            this.btn_InsertData = new System.Windows.Forms.Button();
            this.btn_MachineMonitoring = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lab_ServerSettings = new System.Windows.Forms.Label();
            this.lab_Version = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_PassWord = new System.Windows.Forms.TextBox();
            this.txb_User = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_Logout = new System.Windows.Forms.Button();
            this.btn_BusinessControl = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmb_BusinessType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmb_CashBox2 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmb_Factory = new System.Windows.Forms.ComboBox();
            this.cmb_ATM2 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmb_Node = new System.Windows.Forms.ComboBox();
            this.rad_GZH = new System.Windows.Forms.RadioButton();
            this.rad_FSN = new System.Windows.Forms.RadioButton();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txb_Message = new System.Windows.Forms.TextBox();
            this.btn_Scan = new System.Windows.Forms.Button();
            this.txb_FilePath = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgv_machine = new System.Windows.Forms.DataGridView();
            this.kId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kIpAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer_UpdateMachine = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_Minimize = new System.Windows.Forms.Button();
            this.lab_Tital = new System.Windows.Forms.Label();
            this.lab_LogDetail = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_machine)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_InsertData
            // 
            this.btn_InsertData.BackgroundImage = global::NodeServerAndManager.Properties.Resources.冠字号码文件上传;
            this.btn_InsertData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_InsertData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_InsertData.Location = new System.Drawing.Point(133, 99);
            this.btn_InsertData.Name = "btn_InsertData";
            this.btn_InsertData.Size = new System.Drawing.Size(100, 100);
            this.btn_InsertData.TabIndex = 1;
            this.btn_InsertData.UseVisualStyleBackColor = true;
            this.btn_InsertData.Click += new System.EventHandler(this.btn_InsertData_Click);
            // 
            // btn_MachineMonitoring
            // 
            this.btn_MachineMonitoring.BackgroundImage = global::NodeServerAndManager.Properties.Resources.设备监控;
            this.btn_MachineMonitoring.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_MachineMonitoring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MachineMonitoring.Location = new System.Drawing.Point(292, 99);
            this.btn_MachineMonitoring.Name = "btn_MachineMonitoring";
            this.btn_MachineMonitoring.Size = new System.Drawing.Size(100, 100);
            this.btn_MachineMonitoring.TabIndex = 2;
            this.btn_MachineMonitoring.UseVisualStyleBackColor = true;
            this.btn_MachineMonitoring.Click += new System.EventHandler(this.btn_MachineMonitoring_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(581, 385);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lab_LogDetail);
            this.tabPage1.Controls.Add(this.lab_ServerSettings);
            this.tabPage1.Controls.Add(this.lab_Version);
            this.tabPage1.Controls.Add(this.btn_Login);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txb_PassWord);
            this.tabPage1.Controls.Add(this.txb_User);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(573, 359);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "登录界面";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lab_ServerSettings
            // 
            this.lab_ServerSettings.AutoSize = true;
            this.lab_ServerSettings.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ServerSettings.ForeColor = System.Drawing.Color.Blue;
            this.lab_ServerSettings.Location = new System.Drawing.Point(473, 322);
            this.lab_ServerSettings.Name = "lab_ServerSettings";
            this.lab_ServerSettings.Size = new System.Drawing.Size(77, 14);
            this.lab_ServerSettings.TabIndex = 6;
            this.lab_ServerSettings.Text = "服务器设置";
            this.lab_ServerSettings.Click += new System.EventHandler(this.lab_ServerSettings_Click);
            // 
            // lab_Version
            // 
            this.lab_Version.AutoSize = true;
            this.lab_Version.Location = new System.Drawing.Point(16, 324);
            this.lab_Version.Name = "lab_Version";
            this.lab_Version.Size = new System.Drawing.Size(101, 12);
            this.lab_Version.TabIndex = 5;
            this.lab_Version.Text = "软件版本:1.0.0.0";
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(248, 213);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(174, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "密码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(174, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "用户";
            // 
            // txb_PassWord
            // 
            this.txb_PassWord.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_PassWord.Location = new System.Drawing.Point(220, 154);
            this.txb_PassWord.Name = "txb_PassWord";
            this.txb_PassWord.PasswordChar = '*';
            this.txb_PassWord.Size = new System.Drawing.Size(157, 26);
            this.txb_PassWord.TabIndex = 1;
            // 
            // txb_User
            // 
            this.txb_User.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_User.Location = new System.Drawing.Point(220, 92);
            this.txb_User.Name = "txb_User";
            this.txb_User.Size = new System.Drawing.Size(157, 26);
            this.txb_User.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_Logout);
            this.tabPage2.Controls.Add(this.btn_MachineMonitoring);
            this.tabPage2.Controls.Add(this.btn_BusinessControl);
            this.tabPage2.Controls.Add(this.btn_InsertData);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(573, 359);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "主界面";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_Logout
            // 
            this.btn_Logout.Location = new System.Drawing.Point(463, 301);
            this.btn_Logout.Name = "btn_Logout";
            this.btn_Logout.Size = new System.Drawing.Size(66, 23);
            this.btn_Logout.TabIndex = 3;
            this.btn_Logout.Text = "注销";
            this.btn_Logout.UseVisualStyleBackColor = true;
            this.btn_Logout.Click += new System.EventHandler(this.btn_Logout_Click);
            // 
            // btn_BusinessControl
            // 
            this.btn_BusinessControl.BackgroundImage = global::NodeServerAndManager.Properties.Resources.交易控制;
            this.btn_BusinessControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_BusinessControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BusinessControl.Location = new System.Drawing.Point(64, 224);
            this.btn_BusinessControl.Name = "btn_BusinessControl";
            this.btn_BusinessControl.Size = new System.Drawing.Size(100, 100);
            this.btn_BusinessControl.TabIndex = 0;
            this.btn_BusinessControl.UseVisualStyleBackColor = true;
            this.btn_BusinessControl.Visible = false;
            this.btn_BusinessControl.Click += new System.EventHandler(this.btn_BusinessControl_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.rad_GZH);
            this.tabPage4.Controls.Add(this.rad_FSN);
            this.tabPage4.Controls.Add(this.btn_Ok);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.txb_Message);
            this.tabPage4.Controls.Add(this.btn_Scan);
            this.tabPage4.Controls.Add(this.txb_FilePath);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(573, 359);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "冠字号码上传";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cmb_BusinessType);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmb_CashBox2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cmb_Factory);
            this.groupBox1.Controls.Add(this.cmb_ATM2);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cmb_Node);
            this.groupBox1.Location = new System.Drawing.Point(82, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(407, 154);
            this.groupBox1.TabIndex = 26;
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
            "ATM清钞",
            "跨行调款"});
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
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(16, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 14);
            this.label15.TabIndex = 21;
            this.label15.Text = "所属厂家";
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
            this.cmb_Node.SelectedIndexChanged += new System.EventHandler(this.cmb_Node_SelectedIndexChanged);
            // 
            // rad_GZH
            // 
            this.rad_GZH.AutoSize = true;
            this.rad_GZH.Location = new System.Drawing.Point(9, 31);
            this.rad_GZH.Name = "rad_GZH";
            this.rad_GZH.Size = new System.Drawing.Size(65, 16);
            this.rad_GZH.TabIndex = 25;
            this.rad_GZH.Text = "GZH文件";
            this.rad_GZH.UseVisualStyleBackColor = true;
            this.rad_GZH.CheckedChanged += new System.EventHandler(this.rad_GZH_CheckedChanged);
            // 
            // rad_FSN
            // 
            this.rad_FSN.AutoSize = true;
            this.rad_FSN.Checked = true;
            this.rad_FSN.Location = new System.Drawing.Point(9, 8);
            this.rad_FSN.Name = "rad_FSN";
            this.rad_FSN.Size = new System.Drawing.Size(65, 16);
            this.rad_FSN.TabIndex = 24;
            this.rad_FSN.TabStop = true;
            this.rad_FSN.Text = "FSN文件";
            this.rad_FSN.UseVisualStyleBackColor = true;
            this.rad_FSN.CheckedChanged += new System.EventHandler(this.rad_FSN_CheckedChanged);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(249, 327);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 17;
            this.btn_Ok.Text = "确定";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(95, 179);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 16;
            this.label13.Text = "上传提示:";
            // 
            // txb_Message
            // 
            this.txb_Message.Location = new System.Drawing.Point(97, 197);
            this.txb_Message.Multiline = true;
            this.txb_Message.Name = "txb_Message";
            this.txb_Message.Size = new System.Drawing.Size(385, 120);
            this.txb_Message.TabIndex = 15;
            // 
            // btn_Scan
            // 
            this.btn_Scan.Location = new System.Drawing.Point(406, 8);
            this.btn_Scan.Name = "btn_Scan";
            this.btn_Scan.Size = new System.Drawing.Size(75, 23);
            this.btn_Scan.TabIndex = 1;
            this.btn_Scan.Text = "选择文件";
            this.btn_Scan.UseVisualStyleBackColor = true;
            this.btn_Scan.Click += new System.EventHandler(this.btn_Scan_Click);
            // 
            // txb_FilePath
            // 
            this.txb_FilePath.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_FilePath.Location = new System.Drawing.Point(101, 8);
            this.txb_FilePath.Name = "txb_FilePath";
            this.txb_FilePath.Size = new System.Drawing.Size(299, 23);
            this.txb_FilePath.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgv_machine);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(573, 359);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "设备监控";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgv_machine
            // 
            this.dgv_machine.AllowUserToAddRows = false;
            this.dgv_machine.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_machine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_machine.ColumnHeadersHeight = 24;
            this.dgv_machine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_machine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kId,
            this.kIpAddress,
            this.kUpdateTime,
            this.kStatus});
            this.dgv_machine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_machine.Location = new System.Drawing.Point(0, 0);
            this.dgv_machine.Name = "dgv_machine";
            this.dgv_machine.ReadOnly = true;
            this.dgv_machine.RowHeadersVisible = false;
            this.dgv_machine.RowTemplate.Height = 23;
            this.dgv_machine.Size = new System.Drawing.Size(573, 359);
            this.dgv_machine.TabIndex = 0;
            // 
            // kId
            // 
            this.kId.DataPropertyName = "kId";
            this.kId.HeaderText = "kId";
            this.kId.Name = "kId";
            this.kId.ReadOnly = true;
            this.kId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kId.Visible = false;
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
            // timer_UpdateMachine
            // 
            this.timer_UpdateMachine.Interval = 600000;
            this.timer_UpdateMachine.Tick += new System.EventHandler(this.timer_UpdateMachine_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightCyan;
            this.panel2.BackgroundImage = global::NodeServerAndManager.Properties.Resources.标题栏1;
            this.panel2.Controls.Add(this.btn_Exit);
            this.panel2.Controls.Add(this.btn_Minimize);
            this.panel2.Controls.Add(this.lab_Tital);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(580, 46);
            this.panel2.TabIndex = 4;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.Transparent;
            this.btn_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Exit.Location = new System.Drawing.Point(530, 5);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(35, 35);
            this.btn_Exit.TabIndex = 2;
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Minimize
            // 
            this.btn_Minimize.BackColor = System.Drawing.Color.Transparent;
            this.btn_Minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Minimize.Location = new System.Drawing.Point(493, 5);
            this.btn_Minimize.Name = "btn_Minimize";
            this.btn_Minimize.Size = new System.Drawing.Size(35, 35);
            this.btn_Minimize.TabIndex = 1;
            this.btn_Minimize.UseVisualStyleBackColor = false;
            this.btn_Minimize.Click += new System.EventHandler(this.btn_Minimize_Click);
            // 
            // lab_Tital
            // 
            this.lab_Tital.AutoSize = true;
            this.lab_Tital.BackColor = System.Drawing.Color.Transparent;
            this.lab_Tital.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Tital.Location = new System.Drawing.Point(256, 13);
            this.lab_Tital.Name = "lab_Tital";
            this.lab_Tital.Size = new System.Drawing.Size(49, 20);
            this.lab_Tital.TabIndex = 0;
            this.lab_Tital.Text = "登录";
            // 
            // lab_LogDetail
            // 
            this.lab_LogDetail.AutoSize = true;
            this.lab_LogDetail.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_LogDetail.ForeColor = System.Drawing.Color.Blue;
            this.lab_LogDetail.Location = new System.Drawing.Point(498, 288);
            this.lab_LogDetail.Name = "lab_LogDetail";
            this.lab_LogDetail.Size = new System.Drawing.Size(35, 14);
            this.lab_LogDetail.TabIndex = 7;
            this.lab_LogDetail.Text = "日志";
            this.lab_LogDetail.Click += new System.EventHandler(this.lab_LogDetail_Click);
            // 
            // NodeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 410);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网点服务器";
            this.Activated += new System.EventHandler(this.NodeManager_Activated);
            this.Load += new System.EventHandler(this.NodeManager_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_machine)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_BusinessControl;
        private System.Windows.Forms.Button btn_InsertData;
        private System.Windows.Forms.Button btn_MachineMonitoring;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lab_ServerSettings;
        private System.Windows.Forms.Label lab_Version;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_PassWord;
        private System.Windows.Forms.TextBox txb_User;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgv_machine;
        private System.Windows.Forms.DataGridViewTextBoxColumn kId;
        private System.Windows.Forms.DataGridViewTextBoxColumn kIpAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn kUpdateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn kStatus;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txb_Message;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmb_BusinessType;
        private System.Windows.Forms.Button btn_Scan;
        private System.Windows.Forms.TextBox txb_FilePath;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lab_Tital;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_Minimize;
        private System.Windows.Forms.Button btn_Logout;
        private System.Windows.Forms.ComboBox cmb_ATM2;
        private System.Windows.Forms.ComboBox cmb_Node;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmb_Factory;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmb_CashBox2;
        private System.Windows.Forms.Timer timer_UpdateMachine;
        private System.Windows.Forms.RadioButton rad_GZH;
        private System.Windows.Forms.RadioButton rad_FSN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lab_LogDetail;
    }
}

