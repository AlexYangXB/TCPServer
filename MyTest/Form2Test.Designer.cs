namespace MyTest
{
    partial class Form2Test
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txb_Folder = new System.Windows.Forms.TextBox();
            this.txb_Des = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Scan = new System.Windows.Forms.Button();
            this.cmb_Source = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件夹";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "原始文件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(301, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "目标文件";
            // 
            // txb_Folder
            // 
            this.txb_Folder.Location = new System.Drawing.Point(97, 50);
            this.txb_Folder.Name = "txb_Folder";
            this.txb_Folder.Size = new System.Drawing.Size(362, 21);
            this.txb_Folder.TabIndex = 3;
            // 
            // txb_Des
            // 
            this.txb_Des.Location = new System.Drawing.Point(360, 101);
            this.txb_Des.Name = "txb_Des";
            this.txb_Des.Size = new System.Drawing.Size(153, 21);
            this.txb_Des.TabIndex = 5;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(230, 175);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 6;
            this.btn_Start.Text = "开始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Scan
            // 
            this.btn_Scan.Location = new System.Drawing.Point(466, 47);
            this.btn_Scan.Name = "btn_Scan";
            this.btn_Scan.Size = new System.Drawing.Size(55, 23);
            this.btn_Scan.TabIndex = 7;
            this.btn_Scan.Text = "浏览";
            this.btn_Scan.UseVisualStyleBackColor = true;
            this.btn_Scan.Click += new System.EventHandler(this.btn_Scan_Click);
            // 
            // cmb_Source
            // 
            this.cmb_Source.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Source.FormattingEnabled = true;
            this.cmb_Source.Location = new System.Drawing.Point(100, 101);
            this.cmb_Source.Name = "cmb_Source";
            this.cmb_Source.Size = new System.Drawing.Size(195, 20);
            this.cmb_Source.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(123, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 340);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmb_Source);
            this.Controls.Add(this.btn_Scan);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.txb_Des);
            this.Controls.Add(this.txb_Folder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form2Test";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txb_Folder;
        private System.Windows.Forms.TextBox txb_Des;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Scan;
        private System.Windows.Forms.ComboBox cmb_Source;
        private System.Windows.Forms.Button button1;
    }
}