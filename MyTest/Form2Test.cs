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

            //DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(Hashtable));
            //Hashtable hashtable = new Hashtable();   //实例化Hashtable对象
            //hashtable.Add("id", "600719");     //向Hashtable哈希表中添加元素
            //hashtable.Add("name", "denylau");
            //hashtable.Add("sex", "男");

            //JObject jo = new JObject();
            //jo["katm"] = 1;
            //string str = JsonConvert.SerializeObject(jo);
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    data.WriteObject(stream, hashtable);
            //    string szJson = Encoding.UTF8.GetString(stream.ToArray());
                
            //    MessageBox.Show(szJson);
            //}
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UInt16 Date;		  	//验钞启动日期
            UInt16 Time;		  	//验钞启动时间
            string BankCode = "1234567";
            string NodeCode = "1234567";
            byte BussinessType = 1;
            int TotalCount = 100;
            string ClearCenter = "T";
            byte Version = 1;
            byte MachineType = 3;
            string MachineModel = "HT9000A";
            string MachineNumber = "564003";
            string BussinessNumber = "123456";
            byte[] Reverse = new byte[10];
            string[] dTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Split(' ');
            //char[] arr=new char[2];
            //arr[0] = '-';
            //arr[1] = '/';
            string[] date = dTime[0].Split('-');
            string[] time = dTime[1].Split(':');
            Date = (UInt16)(((int.Parse(date[0]) - 1980) << 9) + (int.Parse(date[1]) << 5) + int.Parse(date[2]));
            Time = (UInt16)(((int.Parse(time[0]) << 11) + (int.Parse(time[1]) << 5) + (int.Parse(time[0]) >> 1)));
            BankCode = GetLastPart(BankCode, 6);
            NodeCode = GetLastPart(NodeCode, 6);
            MachineModel = GetLastPart(MachineModel, 8);
            MachineNumber = GetLastPart(MachineNumber, 10);
            if (BussinessType == 0 || BussinessType == 3)
            {
                BussinessNumber = "0";
            }
            BussinessNumber = GetLastPart(BussinessNumber, 50);
            byte[] BankCodeByte=Encoding.ASCII.GetBytes(BankCode);
            byte[] NodeCodeByte = Encoding.ASCII.GetBytes(NodeCode);
            byte[] ClearCenterByte = Encoding.ASCII.GetBytes(ClearCenter);
            byte[] MachineModelByte = Encoding.ASCII.GetBytes(MachineModel);
            byte[] MachineNumberByte = Encoding.ASCII.GetBytes(MachineNumber);
            byte[] BussinessNumberByte = Encoding.ASCII.GetBytes(BussinessNumber);
            string fileName = Environment.CurrentDirectory + "/" + "123.CRH";
            byte[] TotalCountByte = BitConverter.GetBytes(TotalCount);


            string SignNumber = "H123456789";
            SignNumber = GetLastPart(SignNumber, 12);
            byte[] SignNumberByte = Encoding.ASCII.GetBytes(SignNumber);
            byte SignVersion = 2;
            byte SignValue = 100;
            byte[] SplitByte = Encoding.ASCII.GetBytes(",");
            byte[] LineByte = Encoding.ASCII.GetBytes(Environment.NewLine);
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(BitConverter.GetBytes(Date), 0, sizeof(ushort));
                fs.Write(BankCodeByte,0,BankCodeByte.Length);
                fs.Write(NodeCodeByte, 0, NodeCodeByte.Length);
                fs.WriteByte(BussinessType);
                fs.Write(TotalCountByte, 0, TotalCountByte.Length);
                fs.Write(ClearCenterByte, 0, ClearCenterByte.Length);
                fs.WriteByte(Version);
                fs.WriteByte(MachineType);
                fs.Write(MachineModelByte, 0, MachineModelByte.Length);
                fs.Write(MachineNumberByte, 0, MachineNumberByte.Length);
                fs.Write(BussinessNumberByte, 0, BussinessNumberByte.Length);
                fs.Write(Reverse, 0, Reverse.Length);

                for (var i = 0; i < 100; i++)
                {
                    fs.Write(BitConverter.GetBytes(Time), 0, sizeof(ushort));
                    fs.Write(SplitByte, 0, 1);
                    fs.Write(SignNumberByte, 0, SignNumberByte.Length);
                    fs.Write(SplitByte, 0, 1);
                    fs.WriteByte(SignVersion);
                    fs.Write(SplitByte, 0, 1);
                    fs.WriteByte(SignValue);
                    fs.Write(LineByte, 0, LineByte.Length);
                }
            }
        }
        public string GetLastPart(string str,int length)
        {
            if (str.Length > length)
                str = str.Substring(str.Length - length, length);
            else
                str = string.Format("{0," + length + "}", str).Replace(" ", "_");
            return str;
        }
    }
}
