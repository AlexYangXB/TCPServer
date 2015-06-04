using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using KyModel;
namespace NodeServerAndManager.BaseWinform
{
    public partial class CRHReview : MaterialSkin.Controls.MaterialForm
    {
        public CRHReview()
        {
            InitializeComponent();
        }

        private void btn_CRHOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "CRH文件|*.CRH";
            if (of.ShowDialog() == DialogResult.OK)
            {
                string fileName=of.FileName;
                FileInfo fi = new FileInfo(fileName);
                 long length = fi.Length;
                byte[] bBuffer=new byte[length];
                using(FileStream fs=new FileStream(fileName,FileMode.Open))
                {
                    fs.Read(bBuffer, 0, (int)length);
                }
                CRHDisplay crh = new CRHDisplay(bBuffer);

            }
        }
    }
}
