using System;
using System.IO;
using System.Windows.Forms;
using KyModel;
using MaterialSkin;
using KyBll;
namespace KangYiCollection.BaseWinform
{
    public partial class CRHReview : MaterialSkin.Controls.MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public CRHReview()
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            InitializeComponent();
            this.Text = clsMsg.getMsg("MenuItem_CRHReview");
            foreach (var item in this.Controls)
            {
                if (item is Label)
                {
                    Label lab = (Label)item;
                    lab.Text = clsMsg.getMsg(lab.Name);
                }
                if (item is Button)
                {
                    Button btn = (Button)item;
                    btn.Text = clsMsg.getMsg(btn.Name);
                }
            }
            int index = 0;
            foreach (ListViewItem item in this.liv_CRHCommon.Items)
            {
                item.SubItems[0].Text = clsMsg.getMsg("CRHCommon_"+index);
                index++;
            }
            foreach (ColumnHeader ch in liv_CRHRecord.Columns)
            {
                ch.Text = clsMsg.getMsg(ch.Tag.ToString());
            }
            this.key.Text = clsMsg.getMsg(this.key.Name);
            this.value.Text = clsMsg.getMsg(this.value.Name);

        }

        private void btn_CRHOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (Directory.Exists(KangYiCollection.Properties.Settings.Default.CRHDir))
                of.InitialDirectory = KangYiCollection.Properties.Settings.Default.CRHDir;
            of.Filter = "CRH文件|*.CRH";
            if (of.ShowDialog() == DialogResult.OK)
            {
                string fileName = of.FileName;
                FileInfo fi = new FileInfo(fileName);
                long length = fi.Length;
                byte[] bBuffer = new byte[length];
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    fs.Read(bBuffer, 0, (int)length);
                }
                this.liv_CRHCommon.BeginUpdate();
                this.liv_CRHRecord.BeginUpdate();
                CRHDisplay crh = new CRHDisplay(bBuffer);
                this.liv_CRHCommon.Items[0].SubItems[1].Text = crh.Date;
                this.liv_CRHCommon.Items[1].SubItems[1].Text =crh.BankCode;
                this.liv_CRHCommon.Items[2].SubItems[1].Text =crh.NodeCode;
                this.liv_CRHCommon.Items[3].SubItems[1].Text =crh.BussinessType;
                this.liv_CRHCommon.Items[4].SubItems[1].Text =crh.RecordCount.ToString();
                this.liv_CRHCommon.Items[5].SubItems[1].Text =crh.ClearCenter;
                this.liv_CRHCommon.Items[6].SubItems[1].Text =crh.FileVersion;
                this.liv_CRHCommon.Items[7].SubItems[1].Text =crh.MachineType;
                this.liv_CRHCommon.Items[8].SubItems[1].Text =crh.MachineModel;
                this.liv_CRHCommon.Items[9].SubItems[1].Text =crh.MachineNumber;
                this.liv_CRHCommon.Items[10].SubItems[1].Text =crh.BussinessNumber;
                this.liv_CRHRecord.Items.Clear();
                int count = 1;
                foreach (var record in crh.records)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = ""+count++;
                    lvi.SubItems.Add(record.Time);
                    lvi.SubItems.Add(record.Sign);
                    lvi.SubItems.Add(record.SignVersion);
                    lvi.SubItems.Add(record.SignValue);
                    this.liv_CRHRecord.Items.Add(lvi);
                }
                this.liv_CRHCommon.EndUpdate();
                this.liv_CRHRecord.EndUpdate();
            }
           
            
        }
    }
}
