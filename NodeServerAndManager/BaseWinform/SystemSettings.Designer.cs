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
            this.OtherFactoryAccessButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txb_OtherFactoryAccessDir = new System.Windows.Forms.TextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.OtherFactoryAccessCheckBox = new MaterialSkin.Controls.MaterialCheckBox();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 122);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(684, 356);
            this.materialTabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.OtherFactoryAccessButton);
            this.tabPage1.Controls.Add(this.txb_OtherFactoryAccessDir);
            this.tabPage1.Controls.Add(this.materialLabel1);
            this.tabPage1.Controls.Add(this.OtherFactoryAccessCheckBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(676, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "其他厂家接入";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // OtherFactoryAccessButton
            // 
            this.OtherFactoryAccessButton.Depth = 0;
            this.OtherFactoryAccessButton.Location = new System.Drawing.Point(505, 65);
            this.OtherFactoryAccessButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.OtherFactoryAccessButton.Name = "OtherFactoryAccessButton";
            this.OtherFactoryAccessButton.Primary = true;
            this.OtherFactoryAccessButton.Size = new System.Drawing.Size(75, 38);
            this.OtherFactoryAccessButton.TabIndex = 10;
            this.OtherFactoryAccessButton.Text = "浏览";
            this.OtherFactoryAccessButton.UseVisualStyleBackColor = true;
            this.OtherFactoryAccessButton.Click += new System.EventHandler(this.OtherFactoryAccessButton_Click);
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
            // OtherFactoryAccessCheckBox
            // 
            this.OtherFactoryAccessCheckBox.AutoSize = true;
            this.OtherFactoryAccessCheckBox.Depth = 0;
            this.OtherFactoryAccessCheckBox.Font = new System.Drawing.Font("Roboto", 10F);
            this.OtherFactoryAccessCheckBox.Location = new System.Drawing.Point(45, 23);
            this.OtherFactoryAccessCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.OtherFactoryAccessCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.OtherFactoryAccessCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.OtherFactoryAccessCheckBox.Name = "OtherFactoryAccessCheckBox";
            this.OtherFactoryAccessCheckBox.Ripple = true;
            this.OtherFactoryAccessCheckBox.Size = new System.Drawing.Size(267, 30);
            this.OtherFactoryAccessCheckBox.TabIndex = 0;
            this.OtherFactoryAccessCheckBox.Text = "将指定文件夹下的FSN文件导入系统";
            this.OtherFactoryAccessCheckBox.UseVisualStyleBackColor = true;
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
            this.materialTabSelector1.Size = new System.Drawing.Size(684, 41);
            this.materialTabSelector1.TabIndex = 10;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // SystemSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 482);
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
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.TabPage tabPage1;
        private MaterialSkin.Controls.MaterialCheckBox OtherFactoryAccessCheckBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialRaisedButton OtherFactoryAccessButton;
        private System.Windows.Forms.TextBox txb_OtherFactoryAccessDir;

    }
}