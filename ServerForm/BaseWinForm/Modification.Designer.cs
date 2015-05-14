namespace ServerForm.BaseWinForm
{
    partial class Modification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Modification));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_MachineType = new System.Windows.Forms.ComboBox();
            this.btn_Modified = new System.Windows.Forms.Button();
            this.ipControl_IP = new ServerForm.Control.IpControl();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "IP地址";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "机具类型";
            // 
            // cmb_MachineType
            // 
            this.cmb_MachineType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmb_MachineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MachineType.FormattingEnabled = true;
            this.cmb_MachineType.Items.AddRange(new object[] {
            "点钞机",
            "清分机",
            "ATM"});
            this.cmb_MachineType.Location = new System.Drawing.Point(74, 29);
            this.cmb_MachineType.Name = "cmb_MachineType";
            this.cmb_MachineType.Size = new System.Drawing.Size(121, 20);
            this.cmb_MachineType.TabIndex = 8;
            // 
            // btn_Modified
            // 
            this.btn_Modified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Modified.Location = new System.Drawing.Point(213, 79);
            this.btn_Modified.Name = "btn_Modified";
            this.btn_Modified.Size = new System.Drawing.Size(75, 23);
            this.btn_Modified.TabIndex = 7;
            this.btn_Modified.Text = "修改";
            this.btn_Modified.UseVisualStyleBackColor = true;
            this.btn_Modified.Click += new System.EventHandler(this.btn_Modified_Click);
            // 
            // ipControl_IP
            // 
            this.ipControl_IP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipControl_IP.Location = new System.Drawing.Point(254, 18);
            this.ipControl_IP.Name = "ipControl_IP";
            this.ipControl_IP.Size = new System.Drawing.Size(248, 39);
            this.ipControl_IP.TabIndex = 12;
            this.ipControl_IP.Value = ((System.Net.IPAddress)(resources.GetObject("ipControl_IP.Value")));
            // 
            // Modification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(501, 116);
            this.Controls.Add(this.ipControl_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_MachineType);
            this.Controls.Add(this.btn_Modified);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Modification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改";
            this.Load += new System.EventHandler(this.Modification_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_MachineType;
        private System.Windows.Forms.Button btn_Modified;
        private Control.IpControl ipControl_IP;
    }
}