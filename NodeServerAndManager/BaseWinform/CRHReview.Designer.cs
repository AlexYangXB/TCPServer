namespace KangYiCollection.BaseWinform
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "记录日期",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "报送银行编码",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "生成网点编码",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "业务类型",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "记录数",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "现金清分中心",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "文件版本",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "设备类别",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "机型",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "设备编号",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "业务关联信息",
            ""}, -1);
            this.btn_CRHOpenFile = new MaterialSkin.Controls.MaterialRaisedButton();
            this.liv_CRHCommon = new System.Windows.Forms.ListView();
            this.key = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.liv_CRHRecord = new System.Windows.Forms.ListView();
            this.CRHRecord_number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CRHRecord_recordtime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CRHRecord_recordsign = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CRHRecord_recordversion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CRHRecord_recordvalue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lab_CRHCommon = new MaterialSkin.Controls.MaterialLabel();
            this.lab_CRHRecord = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // btn_CRHOpenFile
            // 
            this.btn_CRHOpenFile.Depth = 0;
            this.btn_CRHOpenFile.Location = new System.Drawing.Point(47, 109);
            this.btn_CRHOpenFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_CRHOpenFile.Name = "btn_CRHOpenFile";
            this.btn_CRHOpenFile.Primary = true;
            this.btn_CRHOpenFile.Size = new System.Drawing.Size(146, 42);
            this.btn_CRHOpenFile.TabIndex = 0;
            this.btn_CRHOpenFile.Text = "打开文件";
            this.btn_CRHOpenFile.UseVisualStyleBackColor = true;
            this.btn_CRHOpenFile.Click += new System.EventHandler(this.btn_CRHOpenFile_Click);
            // 
            // liv_CRHCommon
            // 
            this.liv_CRHCommon.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.key,
            this.value});
            this.liv_CRHCommon.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11});
            this.liv_CRHCommon.Location = new System.Drawing.Point(258, 181);
            this.liv_CRHCommon.Name = "liv_CRHCommon";
            this.liv_CRHCommon.Size = new System.Drawing.Size(261, 285);
            this.liv_CRHCommon.SmallImageList = this.imageList1;
            this.liv_CRHCommon.TabIndex = 3;
            this.liv_CRHCommon.UseCompatibleStateImageBehavior = false;
            this.liv_CRHCommon.View = System.Windows.Forms.View.Details;
            // 
            // key
            // 
            this.key.Text = "字段";
            this.key.Width = 130;
            // 
            // value
            // 
            this.value.Text = "值";
            this.value.Width = 114;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(18, 20);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // liv_CRHRecord
            // 
            this.liv_CRHRecord.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CRHRecord_number,
            this.CRHRecord_recordtime,
            this.CRHRecord_recordsign,
            this.CRHRecord_recordversion,
            this.CRHRecord_recordvalue});
            this.liv_CRHRecord.Location = new System.Drawing.Point(565, 158);
            this.liv_CRHRecord.Name = "liv_CRHRecord";
            this.liv_CRHRecord.Size = new System.Drawing.Size(466, 308);
            this.liv_CRHRecord.TabIndex = 4;
            this.liv_CRHRecord.UseCompatibleStateImageBehavior = false;
            this.liv_CRHRecord.View = System.Windows.Forms.View.Details;
            // 
            // CRHRecord_number
            // 
            this.CRHRecord_number.Text = "序号";
            // 
            // CRHRecord_recordtime
            // 
            this.CRHRecord_recordtime.Text = "记录时间";
            this.CRHRecord_recordtime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CRHRecord_recordtime.Width = 119;
            // 
            // CRHRecord_recordsign
            // 
            this.CRHRecord_recordsign.Text = "冠字号码";
            this.CRHRecord_recordsign.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CRHRecord_recordsign.Width = 111;
            // 
            // CRHRecord_recordversion
            // 
            this.CRHRecord_recordversion.DisplayIndex = 4;
            this.CRHRecord_recordversion.Text = "版别";
            this.CRHRecord_recordversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CRHRecord_recordversion.Width = 84;
            // 
            // CRHRecord_recordvalue
            // 
            this.CRHRecord_recordvalue.DisplayIndex = 3;
            this.CRHRecord_recordvalue.Text = "币值";
            this.CRHRecord_recordvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CRHRecord_recordvalue.Width = 80;
            // 
            // lab_CRHCommon
            // 
            this.lab_CRHCommon.AutoSize = true;
            this.lab_CRHCommon.Depth = 0;
            this.lab_CRHCommon.Font = new System.Drawing.Font("Roboto", 11F);
            this.lab_CRHCommon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lab_CRHCommon.Location = new System.Drawing.Point(254, 154);
            this.lab_CRHCommon.MouseState = MaterialSkin.MouseState.HOVER;
            this.lab_CRHCommon.Name = "lab_CRHCommon";
            this.lab_CRHCommon.Size = new System.Drawing.Size(121, 19);
            this.lab_CRHCommon.TabIndex = 5;
            this.lab_CRHCommon.Text = "公共信息部分：";
            // 
            // lab_CRHRecord
            // 
            this.lab_CRHRecord.AutoSize = true;
            this.lab_CRHRecord.Depth = 0;
            this.lab_CRHRecord.Font = new System.Drawing.Font("Roboto", 11F);
            this.lab_CRHRecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lab_CRHRecord.Location = new System.Drawing.Point(561, 132);
            this.lab_CRHRecord.MouseState = MaterialSkin.MouseState.HOVER;
            this.lab_CRHRecord.Name = "lab_CRHRecord";
            this.lab_CRHRecord.Size = new System.Drawing.Size(153, 19);
            this.lab_CRHRecord.TabIndex = 6;
            this.lab_CRHRecord.Text = "冠字号码记录部分：";
            // 
            // CRHReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 482);
            this.Controls.Add(this.lab_CRHRecord);
            this.Controls.Add(this.lab_CRHCommon);
            this.Controls.Add(this.liv_CRHRecord);
            this.Controls.Add(this.liv_CRHCommon);
            this.Controls.Add(this.btn_CRHOpenFile);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CRHReview";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CRH查看";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btn_CRHOpenFile;
        private System.Windows.Forms.ListView liv_CRHCommon;
        private System.Windows.Forms.ColumnHeader key;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView liv_CRHRecord;
        private System.Windows.Forms.ColumnHeader CRHRecord_recordtime;
        private System.Windows.Forms.ColumnHeader CRHRecord_recordsign;
        private System.Windows.Forms.ColumnHeader CRHRecord_recordversion;
        private System.Windows.Forms.ColumnHeader CRHRecord_recordvalue;
        private MaterialSkin.Controls.MaterialLabel lab_CRHCommon;
        private MaterialSkin.Controls.MaterialLabel lab_CRHRecord;
        private System.Windows.Forms.ColumnHeader CRHRecord_number;
    }
}