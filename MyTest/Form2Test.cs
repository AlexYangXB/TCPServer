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
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
            //string inputReplaced = null;
            //var regex = new Regex(@"([^a-zA-z0-9\s])");

            //inputReplaced = regex.Replace("1234的?ADFCVD.,.2!!@#$", "_");
            //MessageBox.Show(inputReplaced);

            //string str=String.Format("{0}\"katm\":[{1}],\"kcashbox\":[{2}]{3}", "{", 1,2, "}");
            //MessageBox.Show(str);

            DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(Hashtable));
            KyModel.ky_user user = new KyModel.ky_user { kUserName="123",updated_at=DateTime.Now};
            Hashtable hashtable = new Hashtable();   //实例化Hashtable对象
            hashtable.Add("id", "600719");     //向Hashtable哈希表中添加元素
            hashtable.Add("name", "denylau");
            hashtable.Add("sex", "男");

            JObject jo = new JObject();
            jo["katm"] = 1;
            string str = JsonConvert.SerializeObject(jo);
            using (MemoryStream stream = new MemoryStream())
            {
                data.WriteObject(stream, hashtable);
                string szJson = Encoding.UTF8.GetString(stream.ToArray());
                
                MessageBox.Show(szJson);
            }
            
        }
    }
}
