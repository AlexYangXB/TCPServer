namespace NodeServerAndManager.BaseWinform
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
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.Location = new System.Drawing.Point(1, 63);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.Size = new System.Drawing.Size(698, 401);
            this.LogRichTextBox.TabIndex = 0;
            this.LogRichTextBox.Text = "";
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(701, 464);
            this.Controls.Add(this.LogRichTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "日志";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogRichTextBox;
    }
}