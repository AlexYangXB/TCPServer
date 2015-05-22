using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Configuration;
namespace MyTest
{

    public partial class Form2Test : Form
    {
        public List<string> PicName=new List<string>();
        public Form2Test()
        {
            InitializeComponent();
            
        }

        private void btn_Scan_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (DialogResult.OK == fbd.ShowDialog())
            {
                string FloderPath = fbd.SelectedPath;
                string[] FSNfiles = Directory.GetFiles(FloderPath, "*.FSN", SearchOption.TopDirectoryOnly);
                txb_Folder.Text = FloderPath;
                for(int i=0;i<FSNfiles.Length;i++)
                {
                    string file = Path.GetFileName(FSNfiles[i]);
                    cmb_Source.Items.Add(file);
                }
            }
        }

        //写文件
        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                txb_Des.Text = "GZHM_" + DateTime.Now.ToString("yyyyMMdd") + ".FSN";

                string floder = txb_Folder.Text;
                string SoreceFile = cmb_Source.Text;
                string DesFile = txb_Des.Text.Trim();
                string Source = floder + "\\" + SoreceFile;
                string des = floder + "\\" + DesFile;
                using (FileStream fs = new FileStream(Source, FileMode.Open, FileAccess.Read))
                {
                    byte[] bBuf = new byte[1024];
                    using (FileStream fw = new FileStream(des, FileMode.Create, FileAccess.Write))
                    {
                        int cnt = 0;
                        do
                        {
                            cnt = fs.Read(bBuf, 0, bBuf.Length);
                            if (cnt > 0)
                                fw.Write(bBuf, 0, cnt);
                        } while (cnt > 0);
                    }
                }
                MessageBox.Show("成功");
            }
            catch
            {
                MessageBox.Show("失败");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
