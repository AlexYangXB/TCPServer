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
            this.lab_Setting = new System.Windows.Forms.Label();
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txb_BusinessSerial = new System.Windows.Forms.TextBox();
            this.cmb_CashBox = new System.Windows.Forms.ComboBox();
            this.btn_End = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_Business = new System.Windows.Forms.ComboBox();
            this.cmb_MachineIp = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lab_CounterfeitMoney = new System.Windows.Forms.Label();
            this.lab_RealNote = new System.Windows.Forms.Label();
            this.lab_TotalAmount = new System.Windows.Forms.Label();
            this.cmb_ATM = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rad_GZH = new System.Windows.Forms.RadioButton();
            this.rad_FSN = new System.Windows.Forms.RadioButton();
            this.cmb_CashBox2 = new System.Windows.Forms.ComboBox();
            this.cmb_Factory = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmb_Node = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmb_ATM2 = new System.Windows.Forms.ComboBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txb_Message = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmb_BusinessType = new System.Windows.Forms.ComboBox();
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
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage4.SuspendLayout();
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
            this.btn_InsertData.Location = new System.Drawing.Point(228, 99);
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
            this.btn_MachineMonitoring.Location = new System.Drawing.Point(387, 99);
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
            this.tabControl1.Controls.Add(this.tabPage3);
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
            this.tabPage1.Controls.Add(this.lab_Setting);
            this.tabPage1.Controls.Add(this.lab_ServerSettings);
            this.tabPage1.Controls.Add(this.lab_Version);
            this.tabPage1.Controls.Add(this.btn_Login);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txb_PassWord);
            this.tabPage1.Controls.Add(this.txb_User);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(573, 360);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "登录界面";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lab_Setting
            // 
            this.lab_Setting.AutoSize = true;
            this.lab_Setting.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Setting.ForeColor = System.Drawing.Color.Blue;
            this.lab_Setting.Location = new System.Drawing.Point(487, 296);
            this.lab_Setting.Name = "lab_Setting";
            this.lab_Setting.Size = new System.Drawing.Size(63, 14);
            this.lab_Setting.TabIndex = 8;
            this.lab_Setting.Text = "基本设置";
            this.lab_Setting.Click += new System.EventHandler(this.lab_Setting_Click);
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
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(573, 360);
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
            this.btn_BusinessControl.Location = new System.Drawing.Point(69, 99);
            this.btn_BusinessControl.Name = "btn_BusinessControl";
            this.btn_BusinessControl.Size = new System.Drawing.Size(100, 100);
            this.btn_BusinessControl.TabIndex = 0;
            this.btn_BusinessControl.UseVisualStyleBackColor = true;
            this.btn_BusinessControl.Click += new System.EventHandler(this.btn_BusinessControl_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txb_BusinessSerial);
            this.tabPage3.Controls.Add(this.cmb_CashBox);
            this.tabPage3.Controls.Add(this.btn_End);
            this.tabPage3.Controls.Add(this.btn_Cancel);
            this.tabPage3.Controls.Add(this.btn_Start);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.cmb_Business);
            this.tabPage3.Controls.Add(this.cmb_MachineIp);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Controls.Add(this.cmb_ATM);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(573, 360);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "交易控制";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txb_BusinessSerial
            // 
            this.txb_BusinessSerial.Location = new System.Drawing.Point(185, 161);
            this.txb_BusinessSerial.Name = "txb_BusinessSerial";
            this.txb_BusinessSerial.Size = new System.Drawing.Size(196, 21);
            this.txb_BusinessSerial.TabIndex = 14;
            this.txb_BusinessSerial.Visible = false;
            this.txb_BusinessSerial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_BusinessSerial_KeyPress);
            // 
            // cmb_CashBox
            // 
            this.cmb_CashBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CashBox.Enabled = false;
            this.cmb_CashBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_CashBox.FormattingEnabled = true;
            this.cmb_CashBox.Location = new System.Drawing.Point(185, 189);
            this.cmb_CashBox.Name = "cmb_CashBox";
            this.cmb_CashBox.Size = new System.Drawing.Size(196, 22);
            this.cmb_CashBox.TabIndex = 13;
            // 
            // btn_End
            // 
            this.btn_End.Enabled = false;
            this.btn_End.Location = new System.Drawing.Point(248, 263);
            this.btn_End.Name = "btn_End";
            this.btn_End.Size = new System.Drawing.Size(75, 23);
            this.btn_End.TabIndex = 11;
            this.btn_End.Text = "结束";
            this.btn_End.UseVisualStyleBackColor = true;
            this.btn_End.Click += new System.EventHandler(this.btn_End_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Enabled = false;
            this.btn_Cancel.Location = new System.Drawing.Point(392, 263);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(104, 263);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 9;
            this.btn_Start.Text = "开始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(107, 192);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 8;
            this.label9.Text = " 钞箱编号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(94, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "    ATM编号";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(114, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "交易类型";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(128, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "设备IP";
            // 
            // cmb_Business
            // 
            this.cmb_Business.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Business.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Business.FormattingEnabled = true;
            this.cmb_Business.Items.AddRange(new object[] {
            "付款",
            "收款",
            "柜面取款",
            "柜面存款",
            "ATM配钞",
            "ATM清钞"});
            this.cmb_Business.Location = new System.Drawing.Point(185, 132);
            this.cmb_Business.Name = "cmb_Business";
            this.cmb_Business.Size = new System.Drawing.Size(196, 22);
            this.cmb_Business.TabIndex = 2;
            this.cmb_Business.SelectedIndexChanged += new System.EventHandler(this.cmb_Business_SelectedIndexChanged);
            // 
            // cmb_MachineIp
            // 
            this.cmb_MachineIp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MachineIp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_MachineIp.FormattingEnabled = true;
            this.cmb_MachineIp.Location = new System.Drawing.Point(185, 104);
            this.cmb_MachineIp.Name = "cmb_MachineIp";
            this.cmb_MachineIp.Size = new System.Drawing.Size(196, 22);
            this.cmb_MachineIp.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lab_CounterfeitMoney);
            this.panel1.Controls.Add(this.lab_RealNote);
            this.panel1.Controls.Add(this.lab_TotalAmount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 56);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(389, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "疑币张数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(202, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "真币张数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(29, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "总金额:";
            // 
            // lab_CounterfeitMoney
            // 
            this.lab_CounterfeitMoney.AutoSize = true;
            this.lab_CounterfeitMoney.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_CounterfeitMoney.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lab_CounterfeitMoney.Location = new System.Drawing.Point(465, 21);
            this.lab_CounterfeitMoney.Name = "lab_CounterfeitMoney";
            this.lab_CounterfeitMoney.Size = new System.Drawing.Size(14, 14);
            this.lab_CounterfeitMoney.TabIndex = 2;
            this.lab_CounterfeitMoney.Text = "0";
            // 
            // lab_RealNote
            // 
            this.lab_RealNote.AutoSize = true;
            this.lab_RealNote.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_RealNote.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lab_RealNote.Location = new System.Drawing.Point(278, 21);
            this.lab_RealNote.Name = "lab_RealNote";
            this.lab_RealNote.Size = new System.Drawing.Size(14, 14);
            this.lab_RealNote.TabIndex = 1;
            this.lab_RealNote.Text = "0";
            // 
            // lab_TotalAmount
            // 
            this.lab_TotalAmount.AutoSize = true;
            this.lab_TotalAmount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_TotalAmount.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lab_TotalAmount.Location = new System.Drawing.Point(91, 21);
            this.lab_TotalAmount.Name = "lab_TotalAmount";
            this.lab_TotalAmount.Size = new System.Drawing.Size(14, 14);
            this.lab_TotalAmount.TabIndex = 0;
            this.lab_TotalAmount.Text = "0";
            // 
            // cmb_ATM
            // 
            this.cmb_ATM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ATM.Enabled = false;
            this.cmb_ATM.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_ATM.FormattingEnabled = true;
            this.cmb_ATM.Location = new System.Drawing.Point(185, 160);
            this.cmb_ATM.Name = "cmb_ATM";
            this.cmb_ATM.Size = new System.Drawing.Size(196, 22);
            this.cmb_ATM.TabIndex = 12;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rad_GZH);
            this.tabPage4.Controls.Add(this.rad_FSN);
            this.tabPage4.Controls.Add(this.cmb_CashBox2);
            this.tabPage4.Controls.Add(this.cmb_Factory);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.cmb_Node);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.cmb_ATM2);
            this.tabPage4.Controls.Add(this.btn_Ok);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.txb_Message);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.cmb_BusinessType);
            this.tabPage4.Controls.Add(this.btn_Scan);
            this.tabPage4.Controls.Add(this.txb_FilePath);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(573, 360);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "冠字号码上传";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rad_GZH
            // 
            this.rad_GZH.AutoSize = true;
            this.rad_GZH.Location = new System.Drawing.Point(9, 31);
            this.rad_GZH.Name = "rad_GZH";
            this.rad_GZH.Size = new System.Drawing.Size(65, 16);
            this.rad_GZH.TabIndex = 25;
            this.rad_GZH.TabStop = true;
            this.rad_GZH.Text = "GZH文件";
            this.rad_GZH.UseVisualStyleBackColor = true;
            // 
            // rad_FSN
            // 
            this.rad_FSN.AutoSize = true;
            this.rad_FSN.Location = new System.Drawing.Point(9, 8);
            this.rad_FSN.Name = "rad_FSN";
            this.rad_FSN.Size = new System.Drawing.Size(65, 16);
            this.rad_FSN.TabIndex = 24;
            this.rad_FSN.TabStop = true;
            this.rad_FSN.Text = "FSN文件";
            this.rad_FSN.UseVisualStyleBackColor = true;
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
            this.cmb_CashBox2.Location = new System.Drawing.Point(168, 149);
            this.cmb_CashBox2.Name = "cmb_CashBox2";
            this.cmb_CashBox2.Size = new System.Drawing.Size(314, 22);
            this.cmb_CashBox2.TabIndex = 23;
            // 
            // cmb_Factory
            // 
            this.cmb_Factory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Factory.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Factory.FormattingEnabled = true;
            this.cmb_Factory.Location = new System.Drawing.Point(167, 37);
            this.cmb_Factory.Name = "cmb_Factory";
            this.cmb_Factory.Size = new System.Drawing.Size(314, 22);
            this.cmb_Factory.TabIndex = 22;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(98, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 14);
            this.label15.TabIndex = 21;
            this.label15.Text = "所属厂家";
            // 
            // cmb_Node
            // 
            this.cmb_Node.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Node.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Node.FormattingEnabled = true;
            this.cmb_Node.Location = new System.Drawing.Point(167, 64);
            this.cmb_Node.Name = "cmb_Node";
            this.cmb_Node.Size = new System.Drawing.Size(314, 22);
            this.cmb_Node.TabIndex = 20;
            this.cmb_Node.SelectedIndexChanged += new System.EventHandler(this.cmb_Node_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(98, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 14);
            this.label14.TabIndex = 19;
            this.label14.Text = "所属网点";
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
            this.cmb_ATM2.Location = new System.Drawing.Point(167, 120);
            this.cmb_ATM2.Name = "cmb_ATM2";
            this.cmb_ATM2.Size = new System.Drawing.Size(314, 22);
            this.cmb_ATM2.TabIndex = 18;
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(91, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 14);
            this.label10.TabIndex = 14;
            this.label10.Text = " 钞箱编号";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(98, 123);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 14);
            this.label11.TabIndex = 13;
            this.label11.Text = " ATM编号";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(98, 95);
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
            "付款",
            "收款",
            "柜面取款",
            "柜面存款",
            "ATM配钞",
            "ATM清钞"});
            this.cmb_BusinessType.Location = new System.Drawing.Point(167, 92);
            this.cmb_BusinessType.Name = "cmb_BusinessType";
            this.cmb_BusinessType.Size = new System.Drawing.Size(314, 22);
            this.cmb_BusinessType.TabIndex = 9;
            this.cmb_BusinessType.SelectedIndexChanged += new System.EventHandler(this.cmb_BusinessType_SelectedIndexChanged);
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
            this.tabPage5.Location = new System.Drawing.Point(4, 21);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(573, 360);
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
            this.dgv_machine.Size = new System.Drawing.Size(573, 360);
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
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btn_End;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_Business;
        private System.Windows.Forms.ComboBox cmb_MachineIp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lab_CounterfeitMoney;
        private System.Windows.Forms.Label lab_RealNote;
        private System.Windows.Forms.Label lab_TotalAmount;
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
        private System.Windows.Forms.ComboBox cmb_ATM;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lab_Tital;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_Minimize;
        private System.Windows.Forms.Button btn_Logout;
        private System.Windows.Forms.ComboBox cmb_ATM2;
        private System.Windows.Forms.Label lab_Setting;
        private System.Windows.Forms.ComboBox cmb_Node;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmb_Factory;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmb_CashBox2;
        private System.Windows.Forms.ComboBox cmb_CashBox;
        private System.Windows.Forms.Timer timer_UpdateMachine;
        private System.Windows.Forms.TextBox txb_BusinessSerial;
        private System.Windows.Forms.RadioButton rad_GZH;
        private System.Windows.Forms.RadioButton rad_FSN;
    }
}

