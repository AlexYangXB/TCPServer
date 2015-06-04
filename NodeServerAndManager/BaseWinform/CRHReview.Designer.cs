namespace NodeServerAndManager.BaseWinform
{
    partial class CRHReview
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_CRHOpenFile = new MaterialSkin.Controls.MaterialRaisedButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NodeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BussinessType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecordCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClearCenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BussinessNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_CRHOpenFile
            // 
            this.btn_CRHOpenFile.Depth = 0;
            this.btn_CRHOpenFile.Location = new System.Drawing.Point(12, 93);
            this.btn_CRHOpenFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_CRHOpenFile.Name = "btn_CRHOpenFile";
            this.btn_CRHOpenFile.Primary = true;
            this.btn_CRHOpenFile.Size = new System.Drawing.Size(146, 42);
            this.btn_CRHOpenFile.TabIndex = 0;
            this.btn_CRHOpenFile.Text = "打开文件";
            this.btn_CRHOpenFile.UseVisualStyleBackColor = true;
            this.btn_CRHOpenFile.Click += new System.EventHandler(this.btn_CRHOpenFile_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Sign,
            this.SignVersion,
            this.SignValue});
            this.dataGridView1.Location = new System.Drawing.Point(368, 213);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(403, 257);
            this.dataGridView1.TabIndex = 1;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "记录时间";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Sign
            // 
            this.Sign.DataPropertyName = "Sign";
            this.Sign.HeaderText = "冠字号码";
            this.Sign.Name = "Sign";
            this.Sign.ReadOnly = true;
            // 
            // SignVersion
            // 
            this.SignVersion.DataPropertyName = "SignVersion";
            this.SignVersion.HeaderText = "版别";
            this.SignVersion.Name = "SignVersion";
            this.SignVersion.ReadOnly = true;
            // 
            // SignValue
            // 
            this.SignValue.DataPropertyName = "SignValue";
            this.SignValue.HeaderText = "币值";
            this.SignValue.Name = "SignValue";
            this.SignValue.ReadOnly = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.BankCode,
            this.NodeCode,
            this.BussinessType,
            this.RecordCount,
            this.ClearCenter,
            this.FileVersion,
            this.MachineType,
            this.MachineModel,
            this.MachineNumber,
            this.BussinessNumber});
            this.dataGridView2.Location = new System.Drawing.Point(12, 159);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1104, 48);
            this.dataGridView2.TabIndex = 2;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "记录日期";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // BankCode
            // 
            this.BankCode.DataPropertyName = "BankCode";
            this.BankCode.HeaderText = "报送银行编码";
            this.BankCode.Name = "BankCode";
            this.BankCode.ReadOnly = true;
            // 
            // NodeCode
            // 
            this.NodeCode.DataPropertyName = "NodeCode";
            this.NodeCode.HeaderText = "生成网点编码";
            this.NodeCode.Name = "NodeCode";
            this.NodeCode.ReadOnly = true;
            // 
            // BussinessType
            // 
            this.BussinessType.DataPropertyName = "BussinessType";
            this.BussinessType.HeaderText = "业务类型";
            this.BussinessType.Name = "BussinessType";
            this.BussinessType.ReadOnly = true;
            // 
            // RecordCount
            // 
            this.RecordCount.DataPropertyName = "RecordCount";
            this.RecordCount.HeaderText = "记录数";
            this.RecordCount.Name = "RecordCount";
            this.RecordCount.ReadOnly = true;
            // 
            // ClearCenter
            // 
            this.ClearCenter.DataPropertyName = "ClearCenter";
            this.ClearCenter.HeaderText = "现金清分中心";
            this.ClearCenter.Name = "ClearCenter";
            this.ClearCenter.ReadOnly = true;
            // 
            // FileVersion
            // 
            this.FileVersion.DataPropertyName = "FileVersion";
            this.FileVersion.HeaderText = "文件版本";
            this.FileVersion.Name = "FileVersion";
            this.FileVersion.ReadOnly = true;
            // 
            // MachineType
            // 
            this.MachineType.DataPropertyName = "MachineType";
            this.MachineType.HeaderText = "设备类别";
            this.MachineType.Name = "MachineType";
            this.MachineType.ReadOnly = true;
            // 
            // MachineModel
            // 
            this.MachineModel.DataPropertyName = "MachineModel";
            this.MachineModel.HeaderText = "机型";
            this.MachineModel.Name = "MachineModel";
            this.MachineModel.ReadOnly = true;
            // 
            // MachineNumber
            // 
            this.MachineNumber.DataPropertyName = "MachineNumber";
            this.MachineNumber.HeaderText = "设备编号";
            this.MachineNumber.Name = "MachineNumber";
            this.MachineNumber.ReadOnly = true;
            // 
            // BussinessNumber
            // 
            this.BussinessNumber.DataPropertyName = "BussinessNumber";
            this.BussinessNumber.HeaderText = "业务关联信息";
            this.BussinessNumber.Name = "BussinessNumber";
            this.BussinessNumber.ReadOnly = true;
            // 
            // CRHReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 482);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_CRHOpenFile);
            this.Name = "CRHReview";
            this.Text = "CRH查看";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btn_CRHOpenFile;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sign;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignValue;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NodeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BussinessType;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClearCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineType;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn BussinessNumber;
    }
}