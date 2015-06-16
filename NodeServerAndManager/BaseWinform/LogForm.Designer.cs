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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BussinessLogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.FSNImortLogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CommandLogRichTextBox
            // 
            this.CommandLogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommandLogRichTextBox.Location = new System.Drawing.Point(13, 3);
            this.CommandLogRichTextBox.Name = "CommandLogRichTextBox";
            this.CommandLogRichTextBox.Size = new System.Drawing.Size(681, 363);
            this.CommandLogRichTextBox.TabIndex = 0;
            this.CommandLogRichTextBox.Text = "";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Controls.Add(this.tabPage3);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(3, 71);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(698, 395);
            this.materialTabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.CommandLogRichTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(690, 369);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "命令日志";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.BussinessLogRichTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(690, 369);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "业务日志";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // BussinessLogRichTextBox
            // 
            this.BussinessLogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BussinessLogRichTextBox.Location = new System.Drawing.Point(5, 3);
            this.BussinessLogRichTextBox.Name = "BussinessLogRichTextBox";
            this.BussinessLogRichTextBox.Size = new System.Drawing.Size(681, 363);
            this.BussinessLogRichTextBox.TabIndex = 1;
            this.BussinessLogRichTextBox.Text = "";
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.FSNImortLogRichTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(690, 369);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "FSN导入日志";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // FSNImortLogRichTextBox
            // 
            this.FSNImortLogRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FSNImortLogRichTextBox.Location = new System.Drawing.Point(5, 3);
            this.FSNImortLogRichTextBox.Name = "FSNImortLogRichTextBox";
            this.FSNImortLogRichTextBox.Size = new System.Drawing.Size(681, 363);
            this.FSNImortLogRichTextBox.TabIndex = 2;
            this.FSNImortLogRichTextBox.Text = "";
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
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox CommandLogRichTextBox;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private System.Windows.Forms.RichTextBox BussinessLogRichTextBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox FSNImortLogRichTextBox;
    }
}