namespace ServerForm
{
    partial class CollectionServer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionServer));
            this.dgv_Machine = new System.Windows.Forms.DataGridView();
            this.kIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kMachineType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kIpAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kTimeSync = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kLastTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.Modification = new System.Windows.Forms.ToolStripMenuItem();
            this.txb_IpAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_MachineType = new System.Windows.Forms.ComboBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.软件设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件保存设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Machine)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_Machine
            // 
            this.dgv_Machine.AllowUserToAddRows = false;
            this.dgv_Machine.AllowUserToDeleteRows = false;
            this.dgv_Machine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Machine.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Machine.ColumnHeadersHeight = 24;
            this.dgv_Machine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Machine.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kIndex,
            this.kMachineType,
            this.kIpAddress,
            this.kTimeSync,
            this.kLastTime});
            this.dgv_Machine.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv_Machine.Location = new System.Drawing.Point(0, 24);
            this.dgv_Machine.Name = "dgv_Machine";
            this.dgv_Machine.ReadOnly = true;
            this.dgv_Machine.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_Machine.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_Machine.RowTemplate.Height = 23;
            this.dgv_Machine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Machine.Size = new System.Drawing.Size(632, 315);
            this.dgv_Machine.TabIndex = 2;
            // 
            // kIndex
            // 
            this.kIndex.DataPropertyName = "kIndex";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kIndex.DefaultCellStyle = dataGridViewCellStyle2;
            this.kIndex.HeaderText = "序号";
            this.kIndex.Name = "kIndex";
            this.kIndex.ReadOnly = true;
            this.kIndex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kIndex.Width = 50;
            // 
            // kMachineType
            // 
            this.kMachineType.DataPropertyName = "kMachineType";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kMachineType.DefaultCellStyle = dataGridViewCellStyle3;
            this.kMachineType.HeaderText = "机具类型";
            this.kMachineType.Name = "kMachineType";
            this.kMachineType.ReadOnly = true;
            this.kMachineType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // kIpAddress
            // 
            this.kIpAddress.DataPropertyName = "kIpAddress";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kIpAddress.DefaultCellStyle = dataGridViewCellStyle4;
            this.kIpAddress.HeaderText = "IP地址";
            this.kIpAddress.Name = "kIpAddress";
            this.kIpAddress.ReadOnly = true;
            this.kIpAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.kIpAddress.Width = 200;
            // 
            // kTimeSync
            // 
            this.kTimeSync.DataPropertyName = "kTimeSync";
            this.kTimeSync.HeaderText = "时间同步";
            this.kTimeSync.Name = "kTimeSync";
            this.kTimeSync.ReadOnly = true;
            this.kTimeSync.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // kLastTime
            // 
            this.kLastTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kLastTime.DataPropertyName = "kLastTime";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.kLastTime.DefaultCellStyle = dataGridViewCellStyle5;
            this.kLastTime.HeaderText = "最后上传时间";
            this.kLastTime.MinimumWidth = 120;
            this.kLastTime.Name = "kLastTime";
            this.kLastTime.ReadOnly = true;
            this.kLastTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Delete,
            this.Modification});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 48);
            // 
            // Delete
            // 
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(94, 22);
            this.Delete.Text = "删除";
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Modification
            // 
            this.Modification.Name = "Modification";
            this.Modification.Size = new System.Drawing.Size(94, 22);
            this.Modification.Text = "修改";
            this.Modification.Click += new System.EventHandler(this.Modification_Click);
            // 
            // txb_IpAddress
            // 
            this.txb_IpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txb_IpAddress.Location = new System.Drawing.Point(273, 356);
            this.txb_IpAddress.Name = "txb_IpAddress";
            this.txb_IpAddress.Size = new System.Drawing.Size(132, 21);
            this.txb_IpAddress.TabIndex = 11;
            this.txb_IpAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txb_IpAddress_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 359);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "IP地址";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 359);
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
            this.cmb_MachineType.Location = new System.Drawing.Point(62, 356);
            this.cmb_MachineType.Name = "cmb_MachineType";
            this.cmb_MachineType.Size = new System.Drawing.Size(121, 20);
            this.cmb_MachineType.TabIndex = 8;
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Add.Location = new System.Drawing.Point(420, 356);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 7;
            this.btn_Add.Text = "添加";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(632, 24);
            this.menu.TabIndex = 12;
            this.menu.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.软件设置ToolStripMenuItem,
            this.文件保存设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 软件设置ToolStripMenuItem
            // 
            this.软件设置ToolStripMenuItem.Name = "软件设置ToolStripMenuItem";
            this.软件设置ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.软件设置ToolStripMenuItem.Text = "软件设置";
            this.软件设置ToolStripMenuItem.Click += new System.EventHandler(this.软件设置ToolStripMenuItem_Click);
            // 
            // 文件保存设置ToolStripMenuItem
            // 
            this.文件保存设置ToolStripMenuItem.Name = "文件保存设置ToolStripMenuItem";
            this.文件保存设置ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.文件保存设置ToolStripMenuItem.Text = "文件保存设置";
            this.文件保存设置ToolStripMenuItem.Click += new System.EventHandler(this.文件保存设置ToolStripMenuItem_Click);
            // 
            // CollectionServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(632, 400);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txb_IpAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_MachineType);
            this.Controls.Add(this.dgv_Machine);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "CollectionServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "纸币数据采集端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Machine)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Machine;
        private System.Windows.Forms.DataGridViewTextBoxColumn kIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn kMachineType;
        private System.Windows.Forms.DataGridViewTextBoxColumn kIpAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn kTimeSync;
        private System.Windows.Forms.DataGridViewTextBoxColumn kLastTime;
        private System.Windows.Forms.TextBox txb_IpAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_MachineType;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 软件设置ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Delete;
        private System.Windows.Forms.ToolStripMenuItem Modification;
        private System.Windows.Forms.ToolStripMenuItem 文件保存设置ToolStripMenuItem;
    }
}

