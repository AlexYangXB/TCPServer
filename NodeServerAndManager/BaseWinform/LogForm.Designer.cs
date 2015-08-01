namespace KangYiCollection.BaseWinform
{
    partial class LogForm
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
            this.CommandLogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tab_Cmd = new System.Windows.Forms.TabPage();
            this.tab_Bussiness = new System.Windows.Forms.TabPage();
            this.BussinessLogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.tab_FsnImport = new System.Windows.Forms.TabPage();
            this.FSNImortLogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1.SuspendLayout();
            this.tab_Cmd.SuspendLayout();
            this.tab_Bussiness.SuspendLayout();
            this.tab_FsnImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // CommandLogRichTextBox
            // 
            this.CommandLogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommandLogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommandLogRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.CommandLogRichTextBox.Name = "CommandLogRichTextBox";
            this.CommandLogRichTextBox.Size = new System.Drawing.Size(684, 363);
            this.CommandLogRichTextBox.TabIndex = 0;
            this.CommandLogRichTextBox.Text = "";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tab_Cmd);
            this.materialTabControl1.Controls.Add(this.tab_Bussiness);
            this.materialTabControl1.Controls.Add(this.tab_FsnImport);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(3, 71);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(698, 395);
            this.materialTabControl1.TabIndex = 1;
            // 
            // tab_Cmd
            // 
            this.tab_Cmd.Controls.Add(this.CommandLogRichTextBox);
            this.tab_Cmd.Location = new System.Drawing.Point(4, 22);
            this.tab_Cmd.Name = "tab_Cmd";
            this.tab_Cmd.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Cmd.Size = new System.Drawing.Size(690, 369);
            this.tab_Cmd.TabIndex = 0;
            this.tab_Cmd.Text = "命令日志";
            this.tab_Cmd.UseVisualStyleBackColor = true;
            // 
            // tab_Bussiness
            // 
            this.tab_Bussiness.Controls.Add(this.BussinessLogRichTextBox);
            this.tab_Bussiness.Location = new System.Drawing.Point(4, 22);
            this.tab_Bussiness.Name = "tab_Bussiness";
            this.tab_Bussiness.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Bussiness.Size = new System.Drawing.Size(690, 369);
            this.tab_Bussiness.TabIndex = 1;
            this.tab_Bussiness.Text = "业务日志";
            this.tab_Bussiness.UseVisualStyleBackColor = true;
            // 
            // BussinessLogRichTextBox
            // 
            this.BussinessLogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BussinessLogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BussinessLogRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.BussinessLogRichTextBox.Name = "BussinessLogRichTextBox";
            this.BussinessLogRichTextBox.Size = new System.Drawing.Size(684, 363);
            this.BussinessLogRichTextBox.TabIndex = 1;
            this.BussinessLogRichTextBox.Text = "";
            // 
            // tab_FsnImport
            // 
            this.tab_FsnImport.Controls.Add(this.FSNImortLogRichTextBox);
            this.tab_FsnImport.Location = new System.Drawing.Point(4, 22);
            this.tab_FsnImport.Name = "tab_FsnImport";
            this.tab_FsnImport.Size = new System.Drawing.Size(690, 369);
            this.tab_FsnImport.TabIndex = 2;
            this.tab_FsnImport.Text = "FSN导入日志";
            this.tab_FsnImport.UseVisualStyleBackColor = true;
            // 
            // FSNImortLogRichTextBox
            // 
            this.FSNImortLogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FSNImortLogRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FSNImortLogRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.FSNImortLogRichTextBox.Name = "FSNImortLogRichTextBox";
            this.FSNImortLogRichTextBox.Size = new System.Drawing.Size(690, 369);
            this.FSNImortLogRichTextBox.TabIndex = 2;
            this.FSNImortLogRichTextBox.Text = "";
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(-20, 25);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(721, 45);
            this.materialTabSelector1.TabIndex = 2;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(701, 464);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "日志";
            this.materialTabControl1.ResumeLayout(false);
            this.tab_Cmd.ResumeLayout(false);
            this.tab_Bussiness.ResumeLayout(false);
            this.tab_FsnImport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox CommandLogRichTextBox;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tab_Cmd;
        private System.Windows.Forms.TabPage tab_Bussiness;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.RichTextBox BussinessLogRichTextBox;
        private System.Windows.Forms.TabPage tab_FsnImport;
        private System.Windows.Forms.RichTextBox FSNImortLogRichTextBox;
    }
}