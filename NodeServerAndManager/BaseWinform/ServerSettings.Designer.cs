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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Confirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lab_ServerTest = new System.Windows.Forms.Label();
            this.lab_DeviceTest = new System.Windows.Forms.Label();
            this.lab_PictureTest = new System.Windows.Forms.Label();
            this.cmb_imageServer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txb_SphinxPort = new System.Windows.Forms.TextBox();
            this.txb_DevicePort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txb_ImagePort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txb_LocalPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txb_PushPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lab_PushTest = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txb_BindNode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkList_Node = new System.Windows.Forms.CheckedListBox();
            this.ipControl_Push = new KangYiCollection.Control.IpControl();
            this.ipControl_Server = new KangYiCollection.Control.IpControl();
            this.ipControl_Device = new KangYiCollection.Control.IpControl();
            this.ipControl_Local = new KangYiCollection.Control.IpControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据服务器IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "图像数据库IP";
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.Location = new System.Drawing.Point(203, 516);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(75, 25);
            this.btn_Confirm.TabIndex = 6;
            this.btn_Confirm.Text = "确定";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            this.btn_Confirm.Click += new System.EventHandler(this.btn_Confirm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "设备服务器IP";
            // 
            // lab_ServerTest
            // 
            this.lab_ServerTest.AutoSize = true;
            this.lab_ServerTest.ForeColor = System.Drawing.Color.Blue;
            this.lab_ServerTest.Location = new System.Drawing.Point(442, 30);
            this.lab_ServerTest.Name = "lab_ServerTest";
            this.lab_ServerTest.Size = new System.Drawing.Size(31, 13);
            this.lab_ServerTest.TabIndex = 7;
            this.lab_ServerTest.Text = "测试";
            this.lab_ServerTest.Click += new System.EventHandler(this.lab_ServerTest_Click);
            // 
            // lab_DeviceTest
            // 
            this.lab_DeviceTest.AutoSize = true;
            this.lab_DeviceTest.ForeColor = System.Drawing.Color.Blue;
            this.lab_DeviceTest.Location = new System.Drawing.Point(442, 72);
            this.lab_DeviceTest.Name = "lab_DeviceTest";
            this.lab_DeviceTest.Size = new System.Drawing.Size(31, 13);
            this.lab_DeviceTest.TabIndex = 8;
            this.lab_DeviceTest.Text = "测试";
            this.lab_DeviceTest.Click += new System.EventHandler(this.lab_DeviceTest_Click);
            // 
            // lab_PictureTest
            // 
            this.lab_PictureTest.AutoSize = true;
            this.lab_PictureTest.ForeColor = System.Drawing.Color.Blue;
            this.lab_PictureTest.Location = new System.Drawing.Point(442, 154);
            this.lab_PictureTest.Name = "lab_PictureTest";
            this.lab_PictureTest.Size = new System.Drawing.Size(31, 13);
            this.lab_PictureTest.TabIndex = 9;
            this.lab_PictureTest.Text = "测试";
            this.lab_PictureTest.Click += new System.EventHandler(this.lab_PictureTest_Click);
            // 
            // cmb_imageServer
            // 
            this.cmb_imageServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_imageServer.FormattingEnabled = true;
            this.cmb_imageServer.Location = new System.Drawing.Point(94, 148);
            this.cmb_imageServer.Name = "cmb_imageServer";
            this.cmb_imageServer.Size = new System.Drawing.Size(227, 21);
            this.cmb_imageServer.TabIndex = 6;
            this.cmb_imageServer.SelectedIndexChanged += new System.EventHandler(this.cmb_imageServer_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(327, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "端口";
            // 
            // txb_SphinxPort
            // 
            this.txb_SphinxPort.Location = new System.Drawing.Point(358, 29);
            this.txb_SphinxPort.Name = "txb_SphinxPort";
            this.txb_SphinxPort.Size = new System.Drawing.Size(72, 20);
            this.txb_SphinxPort.TabIndex = 1;
            this.txb_SphinxPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_Port_KeyPress);
            // 
            // txb_DevicePort
            // 
            this.txb_DevicePort.Location = new System.Drawing.Point(358, 68);
            this.txb_DevicePort.Name = "txb_DevicePort";
            this.txb_DevicePort.Size = new System.Drawing.Size(72, 20);
            this.txb_DevicePort.TabIndex = 3;
            this.txb_DevicePort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_Port_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(327, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "端口";
            // 
            // txb_ImagePort
            // 
            this.txb_ImagePort.Enabled = false;
            this.txb_ImagePort.Location = new System.Drawing.Point(358, 151);
            this.txb_ImagePort.Name = "txb_ImagePort";
            this.txb_ImagePort.Size = new System.Drawing.Size(72, 20);
            this.txb_ImagePort.TabIndex = 7;
            this.txb_ImagePort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_Port_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(327, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "端口";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txb_LocalPort);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.ipControl_Local);
            this.groupBox1.Location = new System.Drawing.Point(12, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(476, 63);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本设置";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "端口";
            // 
            // txb_LocalPort
            // 
            this.txb_LocalPort.Location = new System.Drawing.Point(358, 23);
            this.txb_LocalPort.Name = "txb_LocalPort";
            this.txb_LocalPort.Size = new System.Drawing.Size(72, 20);
            this.txb_LocalPort.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "本机IP";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.ipControl_Push);
            this.groupBox2.Controls.Add(this.txb_PushPort);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lab_PushTest);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ipControl_Server);
            this.groupBox2.Controls.Add(this.txb_ImagePort);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ipControl_Device);
            this.groupBox2.Controls.Add(this.txb_DevicePort);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lab_ServerTest);
            this.groupBox2.Controls.Add(this.txb_SphinxPort);
            this.groupBox2.Controls.Add(this.lab_DeviceTest);
            this.groupBox2.Controls.Add(this.lab_PictureTest);
            this.groupBox2.Controls.Add(this.cmb_imageServer);
            this.groupBox2.Location = new System.Drawing.Point(12, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(476, 190);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据库服务器设置";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(328, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "端口";
            // 
            // txb_PushPort
            // 
            this.txb_PushPort.Location = new System.Drawing.Point(359, 108);
            this.txb_PushPort.Name = "txb_PushPort";
            this.txb_PushPort.Size = new System.Drawing.Size(72, 20);
            this.txb_PushPort.TabIndex = 4;
            this.txb_PushPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_Port_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 112);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "推送服务器IP";
            // 
            // lab_PushTest
            // 
            this.lab_PushTest.AutoSize = true;
            this.lab_PushTest.ForeColor = System.Drawing.Color.Blue;
            this.lab_PushTest.Location = new System.Drawing.Point(443, 112);
            this.lab_PushTest.Name = "lab_PushTest";
            this.lab_PushTest.Size = new System.Drawing.Size(31, 13);
            this.lab_PushTest.TabIndex = 19;
            this.lab_PushTest.Text = "测试";
            this.lab_PushTest.Click += new System.EventHandler(this.lab_PushTest_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txb_BindNode);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.chkList_Node);
            this.groupBox3.Location = new System.Drawing.Point(18, 360);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(476, 150);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "绑定网点";
            // 
            // txb_BindNode
            // 
            this.txb_BindNode.Enabled = false;
            this.txb_BindNode.Location = new System.Drawing.Point(94, 17);
            this.txb_BindNode.Name = "txb_BindNode";
            this.txb_BindNode.Size = new System.Drawing.Size(336, 20);
            this.txb_BindNode.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "绑定网点";
            // 
            // chkList_Node
            // 
            this.chkList_Node.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chkList_Node.CheckOnClick = true;
            this.chkList_Node.FormattingEnabled = true;
            this.chkList_Node.HorizontalScrollbar = true;
            this.chkList_Node.Location = new System.Drawing.Point(94, 52);
            this.chkList_Node.Margin = new System.Windows.Forms.Padding(0);
            this.chkList_Node.Name = "chkList_Node";
            this.chkList_Node.Size = new System.Drawing.Size(288, 77);
            this.chkList_Node.TabIndex = 1;
            this.chkList_Node.SelectedIndexChanged += new System.EventHandler(this.chkList_Node_SelectedIndexChanged);
            // 
            // ipControl_Push
            // 
            this.ipControl_Push.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipControl_Push.Location = new System.Drawing.Point(84, 95);
            this.ipControl_Push.Name = "ipControl_Push";
            this.ipControl_Push.Size = new System.Drawing.Size(248, 42);
            this.ipControl_Push.TabIndex = 4;
            this.ipControl_Push.Value = ((System.Net.IPAddress)(resources.GetObject("ipControl_Push.Value")));
            // 
            // ipControl_Server
            // 
            this.ipControl_Server.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipControl_Server.Location = new System.Drawing.Point(83, 16);
            this.ipControl_Server.Name = "ipControl_Server";
            this.ipControl_Server.Size = new System.Drawing.Size(248, 42);
            this.ipControl_Server.TabIndex = 0;
            this.ipControl_Server.Value = ((System.Net.IPAddress)(resources.GetObject("ipControl_Server.Value")));
            this.ipControl_Server.Leave += new System.EventHandler(this.ipControl_Server_Leave);
            // 
            // ipControl_Device
            // 
            this.ipControl_Device.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipControl_Device.Location = new System.Drawing.Point(83, 55);
            this.ipControl_Device.Name = "ipControl_Device";
            this.ipControl_Device.Size = new System.Drawing.Size(248, 42);
            this.ipControl_Device.TabIndex = 2;
            this.ipControl_Device.Value = ((System.Net.IPAddress)(resources.GetObject("ipControl_Device.Value")));
            // 
            // ipControl_Local
            // 
            this.ipControl_Local.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipControl_Local.Location = new System.Drawing.Point(83, 15);
            this.ipControl_Local.Name = "ipControl_Local";
            this.ipControl_Local.Size = new System.Drawing.Size(248, 42);
            this.ipControl_Local.TabIndex = 4;
            this.ipControl_Local.Value = ((System.Net.IPAddress)(resources.GetObject("ipControl_Local.Value")));
            // 
            // ServerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(513, 553);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Confirm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "服务器设置";
            this.Load += new System.EventHandler(this.ServerSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Control.IpControl ipControl_Server;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Confirm;
        private System.Windows.Forms.Label label3;
        private Control.IpControl ipControl_Device;
        private System.Windows.Forms.Label lab_ServerTest;
        private System.Windows.Forms.Label lab_DeviceTest;
        private System.Windows.Forms.Label lab_PictureTest;
        private System.Windows.Forms.ComboBox cmb_imageServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_SphinxPort;
        private System.Windows.Forms.TextBox txb_DevicePort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txb_ImagePort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txb_LocalPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Control.IpControl ipControl_Local;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txb_BindNode;
        private System.Windows.Forms.Label label10;
        private Control.IpControl ipControl_Push;
        private System.Windows.Forms.TextBox txb_PushPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lab_PushTest;
        private System.Windows.Forms.CheckedListBox chkList_Node;
    }
}