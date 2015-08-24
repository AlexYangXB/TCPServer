namespace KangYiCollection.BaseWinform
{
    partial class ServerSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerSettings));
            this.lab_ServerIp = new System.Windows.Forms.Label();
            this.lab_PictureIp = new System.Windows.Forms.Label();
            this.btn_Confirm = new System.Windows.Forms.Button();
            this.lab_DeviceIp = new System.Windows.Forms.Label();
            this.lab_ServerTest = new System.Windows.Forms.Label();
            this.lab_DeviceTest = new System.Windows.Forms.Label();
            this.lab_PictureTest = new System.Windows.Forms.Label();
            this.cmb_imageServer = new System.Windows.Forms.ComboBox();
            this.lab_ServerPort = new System.Windows.Forms.Label();
            this.txb_SphinxPort = new System.Windows.Forms.TextBox();
            this.txb_DevicePort = new System.Windows.Forms.TextBox();
            this.lab_DevicePort = new System.Windows.Forms.Label();
            this.txb_ImagePort = new System.Windows.Forms.TextBox();
            this.lab_PicturePort = new System.Windows.Forms.Label();
            this.gb_BaseSetting = new System.Windows.Forms.GroupBox();
            this.lab_LocalPort = new System.Windows.Forms.Label();
            this.txb_LocalPort = new System.Windows.Forms.TextBox();
            this.lab_LocalIp = new System.Windows.Forms.Label();
            this.ipControl_Local = new KangYiCollection.Control.IpControl();
            this.gb_DataSetting = new System.Windows.Forms.GroupBox();
            this.lab_PushPort = new System.Windows.Forms.Label();
            this.ipControl_Push = new KangYiCollection.Control.IpControl();
            this.txb_PushPort = new System.Windows.Forms.TextBox();
            this.lab_PushIp = new System.Windows.Forms.Label();
            this.lab_PushTest = new System.Windows.Forms.Label();
            this.ipControl_Server = new KangYiCollection.Control.IpControl();
            this.ipControl_Device = new KangYiCollection.Control.IpControl();
            this.gb_BindNode = new System.Windows.Forms.GroupBox();
            this.txb_BindNode = new System.Windows.Forms.TextBox();
            this.lab_BindNode = new System.Windows.Forms.Label();
            this.chkList_Node = new System.Windows.Forms.CheckedListBox();
            this.gb_BaseSetting.SuspendLayout();
            this.gb_DataSetting.SuspendLayout();
            this.gb_BindNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // lab_ServerIp
            // 
            this.lab_ServerIp.AutoSize = true;
            this.lab_ServerIp.Location = new System.Drawing.Point(10, 28);
            this.lab_ServerIp.Name = "lab_ServerIp";
            this.lab_ServerIp.Size = new System.Drawing.Size(77, 12);
            this.lab_ServerIp.TabIndex = 1;
            this.lab_ServerIp.Text = "数据服务器IP";
            // 
            // lab_PictureIp
            // 
            this.lab_PictureIp.AutoSize = true;
            this.lab_PictureIp.Location = new System.Drawing.Point(10, 142);
            this.lab_PictureIp.Name = "lab_PictureIp";
            this.lab_PictureIp.Size = new System.Drawing.Size(77, 12);
            this.lab_PictureIp.TabIndex = 5;
            this.lab_PictureIp.Text = "图像数据库IP";
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.Location = new System.Drawing.Point(203, 476);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_Confirm.TabIndex = 6;
            this.btn_Confirm.Text = "确定";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            this.btn_Confirm.Click += new System.EventHandler(this.btn_Confirm_Click);
            // 
            // lab_DeviceIp
            // 
            this.lab_DeviceIp.AutoSize = true;
            this.lab_DeviceIp.Location = new System.Drawing.Point(10, 66);
            this.lab_DeviceIp.Name = "lab_DeviceIp";
            this.lab_DeviceIp.Size = new System.Drawing.Size(77, 12);
            this.lab_DeviceIp.TabIndex = 3;
            this.lab_DeviceIp.Text = "设备服务器IP";
            // 
            // lab_ServerTest
            // 
            this.lab_ServerTest.AutoSize = true;
            this.lab_ServerTest.ForeColor = System.Drawing.Color.Blue;
            this.lab_ServerTest.Location = new System.Drawing.Point(466, 30);
            this.lab_ServerTest.Name = "lab_ServerTest";
            this.lab_ServerTest.Size = new System.Drawing.Size(29, 12);
            this.lab_ServerTest.TabIndex = 7;
            this.lab_ServerTest.Text = "测试";
            this.lab_ServerTest.Click += new System.EventHandler(this.lab_ServerTest_Click);
            // 
            // lab_DeviceTest
            // 
            this.lab_DeviceTest.AutoSize = true;
            this.lab_DeviceTest.ForeColor = System.Drawing.Color.Blue;
            this.lab_DeviceTest.Location = new System.Drawing.Point(465, 66);
            this.lab_DeviceTest.Name = "lab_DeviceTest";
            this.lab_DeviceTest.Size = new System.Drawing.Size(29, 12);
            this.lab_DeviceTest.TabIndex = 8;
            this.lab_DeviceTest.Text = "测试";
            this.lab_DeviceTest.Click += new System.EventHandler(this.lab_DeviceTest_Click);
            // 
            // lab_PictureTest
            // 
            this.lab_PictureTest.AutoSize = true;
            this.lab_PictureTest.ForeColor = System.Drawing.Color.Blue;
            this.lab_PictureTest.Location = new System.Drawing.Point(466, 142);
            this.lab_PictureTest.Name = "lab_PictureTest";
            this.lab_PictureTest.Size = new System.Drawing.Size(29, 12);
            this.lab_PictureTest.TabIndex = 9;
            this.lab_PictureTest.Text = "测试";
            this.lab_PictureTest.Click += new System.EventHandler(this.lab_PictureTest_Click);
            // 
            // cmb_imageServer
            // 
            this.cmb_imageServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_imageServer.FormattingEnabled = true;
            this.cmb_imageServer.Location = new System.Drawing.Point(102, 137);
            this.cmb_imageServer.Name = "cmb_imageServer";
            this.cmb_imageServer.Size = new System.Drawing.Size(227, 20);
            this.cmb_imageServer.TabIndex = 6;
            this.cmb_imageServer.SelectedIndexChanged += new System.EventHandler(this.cmb_imageServer_SelectedIndexChanged);
            // 
            // lab_ServerPort
            // 
            this.lab_ServerPort.AutoSize = true;
            this.lab_ServerPort.Location = new System.Drawing.Point(343, 31);
            this.lab_ServerPort.Name = "lab_ServerPort";
            this.lab_ServerPort.Size = new System.Drawing.Size(29, 12);
            this.lab_ServerPort.TabIndex = 11;
            this.lab_ServerPort.Text = "端口";
            // 
            // txb_SphinxPort
            // 
            this.txb_SphinxPort.Location = new System.Drawing.Point(388, 25);
            this.txb_SphinxPort.Name = "txb_SphinxPort";
            this.txb_SphinxPort.Size = new System.Drawing.Size(72, 21);
            this.txb_SphinxPort.TabIndex = 1;
            this.txb_SphinxPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_Port_KeyPress);
            // 
            // txb_DevicePort
            // 
            this.txb_DevicePort.Location = new System.Drawing.Point(388, 61);
            this.txb_DevicePort.Name = "txb_DevicePort";
            this.txb_DevicePort.Size = new System.Drawing.Size(72, 21);
            this.txb_DevicePort.TabIndex = 3;
            this.txb_DevicePort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_Port_KeyPress);
            // 
            // lab_DevicePort
            // 
            this.lab_DevicePort.AutoSize = true;
            this.lab_DevicePort.Location = new System.Drawing.Point(343, 67);
            this.lab_DevicePort.Name = "lab_DevicePort";
            this.lab_DevicePort.Size = new System.Drawing.Size(29, 12);
            this.lab_DevicePort.TabIndex = 13;
            this.lab_DevicePort.Text = "端口";
            // 
            // txb_ImagePort
            // 
            this.txb_ImagePort.Location = new System.Drawing.Point(388, 137);
            this.txb_ImagePort.Name = "txb_ImagePort";
            this.txb_ImagePort.Size = new System.Drawing.Size(72, 21);
            this.txb_ImagePort.TabIndex = 7;
            this.txb_ImagePort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_Port_KeyPress);
            // 
            // lab_PicturePort
            // 
            this.lab_PicturePort.AutoSize = true;
            this.lab_PicturePort.Location = new System.Drawing.Point(343, 143);
            this.lab_PicturePort.Name = "lab_PicturePort";
            this.lab_PicturePort.Size = new System.Drawing.Size(29, 12);
            this.lab_PicturePort.TabIndex = 15;
            this.lab_PicturePort.Text = "端口";
            // 
            // gb_BaseSetting
            // 
            this.gb_BaseSetting.Controls.Add(this.lab_LocalPort);
            this.gb_BaseSetting.Controls.Add(this.txb_LocalPort);
            this.gb_BaseSetting.Controls.Add(this.lab_LocalIp);
            this.gb_BaseSetting.Controls.Add(this.ipControl_Local);
            this.gb_BaseSetting.Location = new System.Drawing.Point(12, 76);
            this.gb_BaseSetting.Name = "gb_BaseSetting";
            this.gb_BaseSetting.Size = new System.Drawing.Size(526, 58);
            this.gb_BaseSetting.TabIndex = 17;
            this.gb_BaseSetting.TabStop = false;
            this.gb_BaseSetting.Text = "基本设置";
            // 
            // lab_LocalPort
            // 
            this.lab_LocalPort.AutoSize = true;
            this.lab_LocalPort.Location = new System.Drawing.Point(340, 28);
            this.lab_LocalPort.Name = "lab_LocalPort";
            this.lab_LocalPort.Size = new System.Drawing.Size(29, 12);
            this.lab_LocalPort.TabIndex = 6;
            this.lab_LocalPort.Text = "端口";
            // 
            // txb_LocalPort
            // 
            this.txb_LocalPort.Location = new System.Drawing.Point(389, 22);
            this.txb_LocalPort.Name = "txb_LocalPort";
            this.txb_LocalPort.Size = new System.Drawing.Size(72, 21);
            this.txb_LocalPort.TabIndex = 7;
            // 
            // lab_LocalIp
            // 
            this.lab_LocalIp.AutoSize = true;
            this.lab_LocalIp.Location = new System.Drawing.Point(41, 28);
            this.lab_LocalIp.Name = "lab_LocalIp";
            this.lab_LocalIp.Size = new System.Drawing.Size(41, 12);
            this.lab_LocalIp.TabIndex = 5;
            this.lab_LocalIp.Text = "本机IP";
            // 
            // ipControl_Local
            // 
            this.ipControl_Local.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipControl_Local.Location = new System.Drawing.Point(91, 14);
            this.ipControl_Local.Name = "ipControl_Local";
            this.ipControl_Local.Size = new System.Drawing.Size(248, 39);
            this.ipControl_Local.TabIndex = 4;
            this.ipControl_Local.Value = ((System.Net.IPAddress)(resources.GetObject("ipControl_Local.Value")));
            // 
            // gb_DataSetting
            // 
            this.gb_DataSetting.Controls.Add(this.lab_PushPort);
            this.gb_DataSetting.Controls.Add(this.ipControl_Push);
            this.gb_DataSetting.Controls.Add(this.txb_PushPort);
            this.gb_DataSetting.Controls.Add(this.lab_PushIp);
            this.gb_DataSetting.Controls.Add(this.lab_PushTest);
            this.gb_DataSetting.Controls.Add(this.lab_PicturePort);
            this.gb_DataSetting.Controls.Add(this.lab_DevicePort);
            this.gb_DataSetting.Controls.Add(this.lab_ServerPort);
            this.gb_DataSetting.Controls.Add(this.lab_ServerIp);
            this.gb_DataSetting.Controls.Add(this.ipControl_Server);
            this.gb_DataSetting.Controls.Add(this.txb_ImagePort);
            this.gb_DataSetting.Controls.Add(this.lab_PictureIp);
            this.gb_DataSetting.Controls.Add(this.ipControl_Device);
            this.gb_DataSetting.Controls.Add(this.txb_DevicePort);
            this.gb_DataSetting.Controls.Add(this.lab_DeviceIp);
            this.gb_DataSetting.Controls.Add(this.lab_ServerTest);
            this.gb_DataSetting.Controls.Add(this.txb_SphinxPort);
            this.gb_DataSetting.Controls.Add(this.lab_DeviceTest);
            this.gb_DataSetting.Controls.Add(this.lab_PictureTest);
            this.gb_DataSetting.Controls.Add(this.cmb_imageServer);
            this.gb_DataSetting.Location = new System.Drawing.Point(12, 151);
            this.gb_DataSetting.Name = "gb_DataSetting";
            this.gb_DataSetting.Size = new System.Drawing.Size(526, 175);
            this.gb_DataSetting.TabIndex = 18;
            this.gb_DataSetting.TabStop = false;
            this.gb_DataSetting.Text = "数据库服务器设置";
            // 
            // lab_PushPort
            // 
            this.lab_PushPort.AutoSize = true;
            this.lab_PushPort.Location = new System.Drawing.Point(343, 104);
            this.lab_PushPort.Name = "lab_PushPort";
            this.lab_PushPort.Size = new System.Drawing.Size(29, 12);
            this.lab_PushPort.TabIndex = 20;
            this.lab_PushPort.Text = "端口";
            // 
            // ipControl_Push
            // 
            this.ipControl_Push.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipControl_Push.Location = new System.Drawing.Point(92, 88);
            this.ipControl_Push.Name = "ipControl_Push";
            this.ipControl_Push.Size = new System.Drawing.Size(248, 39);
            this.ipControl_Push.TabIndex = 4;
            this.ipControl_Push.Value = ((System.Net.IPAddress)(resources.GetObject("ipControl_Push.Value")));
            // 
            // txb_PushPort
            // 
            this.txb_PushPort.Location = new System.Drawing.Point(389, 98);
            this.txb_PushPort.Name = "txb_PushPort";
            this.txb_PushPort.Size = new System.Drawing.Size(72, 21);
            this.txb_PushPort.TabIndex = 4;
            this.txb_PushPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_Port_KeyPress);
            // 
            // lab_PushIp
            // 
            this.lab_PushIp.AutoSize = true;
            this.lab_PushIp.Location = new System.Drawing.Point(10, 103);
            this.lab_PushIp.Name = "lab_PushIp";
            this.lab_PushIp.Size = new System.Drawing.Size(77, 12);
            this.lab_PushIp.TabIndex = 18;
            this.lab_PushIp.Text = "推送服务器IP";
            // 
            // lab_PushTest
            // 
            this.lab_PushTest.AutoSize = true;
            this.lab_PushTest.ForeColor = System.Drawing.Color.Blue;
            this.lab_PushTest.Location = new System.Drawing.Point(466, 103);
            this.lab_PushTest.Name = "lab_PushTest";
            this.lab_PushTest.Size = new System.Drawing.Size(29, 12);
            this.lab_PushTest.TabIndex = 19;
            this.lab_PushTest.Text = "测试";
            this.lab_PushTest.Click += new System.EventHandler(this.lab_PushTest_Click);
            // 
            // ipControl_Server
            // 
            this.ipControl_Server.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipControl_Server.Location = new System.Drawing.Point(92, 15);
            this.ipControl_Server.Name = "ipControl_Server";
            this.ipControl_Server.Size = new System.Drawing.Size(248, 39);
            this.ipControl_Server.TabIndex = 0;
            this.ipControl_Server.Value = ((System.Net.IPAddress)(resources.GetObject("ipControl_Server.Value")));
            this.ipControl_Server.Leave += new System.EventHandler(this.ipControl_Server_Leave);
            // 
            // ipControl_Device
            // 
            this.ipControl_Device.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipControl_Device.Location = new System.Drawing.Point(92, 51);
            this.ipControl_Device.Name = "ipControl_Device";
            this.ipControl_Device.Size = new System.Drawing.Size(248, 39);
            this.ipControl_Device.TabIndex = 2;
            this.ipControl_Device.Value = ((System.Net.IPAddress)(resources.GetObject("ipControl_Device.Value")));
            // 
            // gb_BindNode
            // 
            this.gb_BindNode.Controls.Add(this.txb_BindNode);
            this.gb_BindNode.Controls.Add(this.lab_BindNode);
            this.gb_BindNode.Controls.Add(this.chkList_Node);
            this.gb_BindNode.Location = new System.Drawing.Point(18, 332);
            this.gb_BindNode.Name = "gb_BindNode";
            this.gb_BindNode.Size = new System.Drawing.Size(520, 138);
            this.gb_BindNode.TabIndex = 19;
            this.gb_BindNode.TabStop = false;
            this.gb_BindNode.Text = "绑定网点";
            // 
            // txb_BindNode
            // 
            this.txb_BindNode.Enabled = false;
            this.txb_BindNode.Location = new System.Drawing.Point(94, 16);
            this.txb_BindNode.Name = "txb_BindNode";
            this.txb_BindNode.Size = new System.Drawing.Size(336, 21);
            this.txb_BindNode.TabIndex = 0;
            // 
            // lab_BindNode
            // 
            this.lab_BindNode.AutoSize = true;
            this.lab_BindNode.Location = new System.Drawing.Point(16, 21);
            this.lab_BindNode.Name = "lab_BindNode";
            this.lab_BindNode.Size = new System.Drawing.Size(53, 12);
            this.lab_BindNode.TabIndex = 1;
            this.lab_BindNode.Text = "绑定网点";
            // 
            // chkList_Node
            // 
            this.chkList_Node.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chkList_Node.CheckOnClick = true;
            this.chkList_Node.FormattingEnabled = true;
            this.chkList_Node.HorizontalScrollbar = true;
            this.chkList_Node.Location = new System.Drawing.Point(94, 48);
            this.chkList_Node.Margin = new System.Windows.Forms.Padding(0);
            this.chkList_Node.Name = "chkList_Node";
            this.chkList_Node.Size = new System.Drawing.Size(288, 66);
            this.chkList_Node.TabIndex = 1;
            this.chkList_Node.SelectedIndexChanged += new System.EventHandler(this.chkList_Node_SelectedIndexChanged);
            // 
            // ServerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(550, 510);
            this.Controls.Add(this.gb_BindNode);
            this.Controls.Add(this.gb_DataSetting);
            this.Controls.Add(this.gb_BaseSetting);
            this.Controls.Add(this.btn_Confirm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerSettings";
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "服务器设置";
            this.Load += new System.EventHandler(this.ServerSettings_Load);
            this.gb_BaseSetting.ResumeLayout(false);
            this.gb_BaseSetting.PerformLayout();
            this.gb_DataSetting.ResumeLayout(false);
            this.gb_DataSetting.PerformLayout();
            this.gb_BindNode.ResumeLayout(false);
            this.gb_BindNode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Control.IpControl ipControl_Server;
        private System.Windows.Forms.Label lab_ServerIp;
        private System.Windows.Forms.Label lab_PictureIp;
        private System.Windows.Forms.Button btn_Confirm;
        private System.Windows.Forms.Label lab_DeviceIp;
        private Control.IpControl ipControl_Device;
        private System.Windows.Forms.Label lab_ServerTest;
        private System.Windows.Forms.Label lab_DeviceTest;
        private System.Windows.Forms.Label lab_PictureTest;
        private System.Windows.Forms.ComboBox cmb_imageServer;
        private System.Windows.Forms.Label lab_ServerPort;
        private System.Windows.Forms.TextBox txb_SphinxPort;
        private System.Windows.Forms.TextBox txb_DevicePort;
        private System.Windows.Forms.Label lab_DevicePort;
        private System.Windows.Forms.TextBox txb_ImagePort;
        private System.Windows.Forms.Label lab_PicturePort;
        private System.Windows.Forms.GroupBox gb_BaseSetting;
        private System.Windows.Forms.GroupBox gb_DataSetting;
        private System.Windows.Forms.TextBox txb_LocalPort;
        private System.Windows.Forms.Label lab_LocalPort;
        private System.Windows.Forms.Label lab_LocalIp;
        private Control.IpControl ipControl_Local;
        private System.Windows.Forms.GroupBox gb_BindNode;
        private System.Windows.Forms.Label lab_BindNode;
        private System.Windows.Forms.TextBox txb_BindNode;
        private System.Windows.Forms.Label lab_PushPort;
        private Control.IpControl ipControl_Push;
        private System.Windows.Forms.TextBox txb_PushPort;
        private System.Windows.Forms.Label lab_PushIp;
        private System.Windows.Forms.Label lab_PushTest;
        private System.Windows.Forms.CheckedListBox chkList_Node;
    }
}