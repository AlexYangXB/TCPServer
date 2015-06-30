namespace MyTest
{
    partial class TCPClient
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
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.tf_FileCnt = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tf_MachineNumber = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.tf_Value = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.tf_PerFileCount = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.tf_ServerIp = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.tf_ServerPort = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.rb_Log = new System.Windows.Forms.RichTextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(392, 137);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(125, 19);
            this.materialLabel1.TabIndex = 2;
            this.materialLabel1.Text = "单次发送文件数:";
            // 
            // tf_FileCnt
            // 
            this.tf_FileCnt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tf_FileCnt.Depth = 0;
            this.tf_FileCnt.Hint = "";
            this.tf_FileCnt.Location = new System.Drawing.Point(534, 133);
            this.tf_FileCnt.MaxLength = 32767;
            this.tf_FileCnt.MouseState = MaterialSkin.MouseState.HOVER;
            this.tf_FileCnt.Name = "tf_FileCnt";
            this.tf_FileCnt.PasswordChar = '\0';
            this.tf_FileCnt.SelectedText = "";
            this.tf_FileCnt.SelectionLength = 0;
            this.tf_FileCnt.SelectionStart = 0;
            this.tf_FileCnt.Size = new System.Drawing.Size(132, 23);
            this.tf_FileCnt.TabIndex = 3;
            this.tf_FileCnt.TabStop = false;
            this.tf_FileCnt.Text = "20";
            this.tf_FileCnt.UseSystemPasswordChar = false;
            // 
            // tf_MachineNumber
            // 
            this.tf_MachineNumber.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tf_MachineNumber.Depth = 0;
            this.tf_MachineNumber.Hint = "";
            this.tf_MachineNumber.Location = new System.Drawing.Point(534, 170);
            this.tf_MachineNumber.MaxLength = 32767;
            this.tf_MachineNumber.MouseState = MaterialSkin.MouseState.HOVER;
            this.tf_MachineNumber.Name = "tf_MachineNumber";
            this.tf_MachineNumber.Padding = new System.Windows.Forms.Padding(5);
            this.tf_MachineNumber.PasswordChar = '\0';
            this.tf_MachineNumber.SelectedText = "";
            this.tf_MachineNumber.SelectionLength = 0;
            this.tf_MachineNumber.SelectionStart = 0;
            this.tf_MachineNumber.Size = new System.Drawing.Size(132, 23);
            this.tf_MachineNumber.TabIndex = 5;
            this.tf_MachineNumber.TabStop = false;
            this.tf_MachineNumber.Text = "9000A9L0001";
            this.tf_MachineNumber.UseSystemPasswordChar = false;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(408, 174);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(109, 19);
            this.materialLabel2.TabIndex = 4;
            this.materialLabel2.Text = "模拟机具编号:";
            // 
            // tf_Value
            // 
            this.tf_Value.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tf_Value.Depth = 0;
            this.tf_Value.Hint = "";
            this.tf_Value.Location = new System.Drawing.Point(534, 212);
            this.tf_Value.MaxLength = 32767;
            this.tf_Value.MouseState = MaterialSkin.MouseState.HOVER;
            this.tf_Value.Name = "tf_Value";
            this.tf_Value.Padding = new System.Windows.Forms.Padding(5);
            this.tf_Value.PasswordChar = '\0';
            this.tf_Value.SelectedText = "";
            this.tf_Value.SelectionLength = 0;
            this.tf_Value.SelectionStart = 0;
            this.tf_Value.Size = new System.Drawing.Size(132, 23);
            this.tf_Value.TabIndex = 7;
            this.tf_Value.TabStop = false;
            this.tf_Value.Text = "100";
            this.tf_Value.UseSystemPasswordChar = false;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(408, 216);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(109, 19);
            this.materialLabel3.TabIndex = 6;
            this.materialLabel3.Text = "模拟纸币面值:";
            // 
            // tf_PerFileCount
            // 
            this.tf_PerFileCount.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tf_PerFileCount.Depth = 0;
            this.tf_PerFileCount.Hint = "";
            this.tf_PerFileCount.Location = new System.Drawing.Point(534, 95);
            this.tf_PerFileCount.MaxLength = 32767;
            this.tf_PerFileCount.MouseState = MaterialSkin.MouseState.HOVER;
            this.tf_PerFileCount.Name = "tf_PerFileCount";
            this.tf_PerFileCount.Padding = new System.Windows.Forms.Padding(5);
            this.tf_PerFileCount.PasswordChar = '\0';
            this.tf_PerFileCount.SelectedText = "";
            this.tf_PerFileCount.SelectionLength = 0;
            this.tf_PerFileCount.SelectionStart = 0;
            this.tf_PerFileCount.Size = new System.Drawing.Size(132, 23);
            this.tf_PerFileCount.TabIndex = 11;
            this.tf_PerFileCount.TabStop = false;
            this.tf_PerFileCount.Text = "1000";
            this.tf_PerFileCount.UseSystemPasswordChar = false;
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(391, 99);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(125, 19);
            this.materialLabel5.TabIndex = 10;
            this.materialLabel5.Text = "单文件冠字号数:";
            // 
            // tf_ServerIp
            // 
            this.tf_ServerIp.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tf_ServerIp.Depth = 0;
            this.tf_ServerIp.Hint = "";
            this.tf_ServerIp.Location = new System.Drawing.Point(143, 99);
            this.tf_ServerIp.MaxLength = 32767;
            this.tf_ServerIp.MouseState = MaterialSkin.MouseState.HOVER;
            this.tf_ServerIp.Name = "tf_ServerIp";
            this.tf_ServerIp.Padding = new System.Windows.Forms.Padding(5);
            this.tf_ServerIp.PasswordChar = '\0';
            this.tf_ServerIp.SelectedText = "";
            this.tf_ServerIp.SelectionLength = 0;
            this.tf_ServerIp.SelectionStart = 0;
            this.tf_ServerIp.Size = new System.Drawing.Size(132, 23);
            this.tf_ServerIp.TabIndex = 13;
            this.tf_ServerIp.TabStop = false;
            this.tf_ServerIp.Text = "192.168.1.157";
            this.tf_ServerIp.UseSystemPasswordChar = false;
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.Location = new System.Drawing.Point(48, 103);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(78, 19);
            this.materialLabel6.TabIndex = 12;
            this.materialLabel6.Text = "服务器IP :";
            // 
            // tf_ServerPort
            // 
            this.tf_ServerPort.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tf_ServerPort.Depth = 0;
            this.tf_ServerPort.Hint = "";
            this.tf_ServerPort.Location = new System.Drawing.Point(143, 140);
            this.tf_ServerPort.MaxLength = 32767;
            this.tf_ServerPort.MouseState = MaterialSkin.MouseState.HOVER;
            this.tf_ServerPort.Name = "tf_ServerPort";
            this.tf_ServerPort.Padding = new System.Windows.Forms.Padding(5);
            this.tf_ServerPort.PasswordChar = '\0';
            this.tf_ServerPort.SelectedText = "";
            this.tf_ServerPort.SelectionLength = 0;
            this.tf_ServerPort.SelectionStart = 0;
            this.tf_ServerPort.Size = new System.Drawing.Size(132, 23);
            this.tf_ServerPort.TabIndex = 15;
            this.tf_ServerPort.TabStop = false;
            this.tf_ServerPort.Text = "1000";
            this.tf_ServerPort.UseSystemPasswordChar = false;
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel7.Location = new System.Drawing.Point(29, 144);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(97, 19);
            this.materialLabel7.TabIndex = 14;
            this.materialLabel7.Text = "服务器端口 :";
            // 
            // rb_Log
            // 
            this.rb_Log.Location = new System.Drawing.Point(24, 281);
            this.rb_Log.Name = "rb_Log";
            this.rb_Log.Size = new System.Drawing.Size(642, 216);
            this.rb_Log.TabIndex = 16;
            this.rb_Log.Text = "";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(133, 205);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(158, 45);
            this.btn_Start.TabIndex = 17;
            this.btn_Start.Text = "开始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // TCPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 521);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.rb_Log);
            this.Controls.Add(this.tf_ServerPort);
            this.Controls.Add(this.materialLabel7);
            this.Controls.Add(this.tf_ServerIp);
            this.Controls.Add(this.materialLabel6);
            this.Controls.Add(this.tf_PerFileCount);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.tf_Value);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.tf_MachineNumber);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.tf_FileCnt);
            this.Controls.Add(this.materialLabel1);
            this.Name = "TCPClient";
            this.Text = "模拟客户端";
            this.Load += new System.EventHandler(this.TCPClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialSingleLineTextField tf_FileCnt;
        private MaterialSkin.Controls.MaterialSingleLineTextField tf_MachineNumber;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialSingleLineTextField tf_Value;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialSingleLineTextField tf_PerFileCount;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialSingleLineTextField tf_ServerIp;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialSingleLineTextField tf_ServerPort;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private System.Windows.Forms.RichTextBox rb_Log;
        private System.Windows.Forms.Button btn_Start;

    }
}