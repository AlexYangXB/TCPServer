using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
namespace KyModel
{
    public class KYDataLayer1
    {
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct SignType
        {
            public string Date;             //操作时间
            public string Sign;             //冠字号码
            public UInt32 Version;          //版别
            public string Currency;         //币种
            public UInt32 Value;            //币值
            public int Slope;               //倾斜率
            public UInt32 True;             //真假残旧
            public string MachineType;      //机具类型
            public string MachineMac;       //机具编号
            public string UserNum;          //柜员编号
            public string NodeCode;         //网点编号
            public string BranchCode;       //分行编号
            public string ErrorCode;        //错误码
            public string Reserve;          //保留字、批次
            public string ImageType;        //冠字号码图片格式
            public string BusinessType;     //业务类型
            public string MachineModel;     //机型
            public byte[] imageData;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct DataHead
        {
            public string fileVer;          //文件版本
            public string HaveImg;          //是否包含图片
            public int Count;               //记录数
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct FSNHead_L1
        {
            public int[] headStart;        //20,10,7,26,0,1,46/45,S,N,o     
            public UInt32 Count;               //记录数
            public int[] headEnd;          //0,1,2,3
        }
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct FSNHead_L2
        {
            //public string headStart;        //20,10,7,26,0,1
            public bool HaveImg;            //是否包含图片
            //public string headString;       //S,N,o
            public UInt32 Count;               //记录数
            //public string headEnd;          //0,1,2,3
            public bool IsFsnFile;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class SignTypeL2
        {
            public DateTime Date;             //操作时间
            public string Sign;             //冠字号码
            public UInt32 Version;          //版别
            public UInt32 Currency;         //币种
            public UInt32 Value;               //币值
            public int Slope;               //倾斜率
            public UInt32 True;             //真假残旧
            public string MachineType;      //机具类型
            public string MachineMac;       //机具编号
            public string UserNum;          //柜员编号
            public string NodeCode;         //网点编号
            public string BranchCode;       //分行编号
            public string ErrorCode;        //错误码
            public string Reserve;          //保留字、批次
            public string ImageType;        //冠字号码图片格式
            public string BusinessType;     //业务类型
            public string Face;             //面向
            public string MachineModel;     //机型  2015.05.07
            //public ushort Height;              //图片高
            //public ushort Width;               //图片宽
            public byte[] imageData;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class Amount
        {
            public int BundleNumber = 0;
            public int TotalValue=0;        //总金额
            public int TotalCnt=0;          //总张数
            public int TotalErr=0;          //疑币张数
            public int TotalTrue=0;         //真币张数
            public int TotalWorn=0;         //残币张数
            public int TotalOld=0;          //旧币张数
            public string FirstSign = "";   //首张冠字号码
            public string LastSign = "";    //末张冠字号码
        }

       

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class GzhLayer2
        {
            public DateTime Date;               //记录日期
            public int BranchId;                //报送银行Id
            public int NodeId;                  //生成网点Id
            public int Business;             //业务类型
            public int FileCnt;                 //FSN文件个数
            public string IsCashCenter;         //是否为清分中心 是：T  否：F
            public string FileVersion;          //文件版本
            public string PackageNumber;        //包号
            public string Currency;             //币种
            public string Reserved;             //预留字段
            public string FileName;             //GZH文件名
            public int UserId;
            public int TotalValue;
            public int TotalNumber;
        }


        [DllImport("kernel32", EntryPoint = "CopyMemory")]
        public static extern void CopyMemory(byte[] destination, IntPtr source, int length);
        //币种
        public static Dictionary<string, UInt32> Money = new Dictionary<string, UInt32>()
                                                             {
                                                                {"CNY", 1},    //人民币
                                                                {"USD", 2},    //美金
                                                                {"EUR", 3},    //欧元
                                                                {"JPY", 4},    //日元
                                                                {"HKD", 5},    //港币
                                                                {"GBP", 6},    //英镑
                                                                {"AUD", 7},    //澳元
                                                                {"CAD", 8},    //加币
                                                                {"SGD", 9},    //新加坡币
                                                                {"KRW", 10},    //韩元
                                                                {"TWD", 11},   //台币
                                                                {"NZD", 12},   //新西兰元（纽元）
                                                                {"MOP", 13},   //澳门元
                                                                {"PHP", 14},   //菲律宾比索
                                                                {"MYR", 15},   //马来西亚林吉特
                                                                {"THB", 16},   //泰铢
                                                                {"SEK", 17},   //瑞典克朗
                                                                {"DKK", 18},   //丹麦克朗
                                                                {"NOK", 19},   //挪威克朗
                                                                {"CHF", 20},   //瑞士法郎
                                                                {"ZAR", 21},   //南非兰特
                                                                {"VND", 22},   //越南盾
                                                                {"BUK", 23},   //缅甸元
                                                                {"RUB", 24},   //俄罗斯卢布
                                                                {"UKR", 25},   //乌克兰
                                                                {"IDR", 26},   //印尼盾
                                                             };
        //业务类型
        public static Dictionary<string,int> BusinessType=new Dictionary<string, int>()
                                                              {
                                                                  {"HM",1},    //号码记录，无业务是应为此类型
                                                                  {"FK",2},    //付款
                                                                  {"SK",3},    //存款
                                                                  {"QK",4},    //柜台取款
                                                                  {"CK",5},    //柜台存款
                                                                  {"ATMP",6},  //ATM配钞
                                                                  {"ATMQ",7},  //ATM清钞
                                                                  {"CAQK",8},  //ATM或CRS设备取款
                                                                  {"CACK",9},  //CRS设备存款
                                                              };

       

        

        public static SignTypeL2 unPack_SignType_L2(SignType signL1)
        {
            SignTypeL2 SignL2 = new SignTypeL2()
            {
                Sign = signL1.Sign,
                
                Value = signL1.Value,
                Version = signL1.Version,
                MachineMac = signL1.MachineMac,
                imageData = signL1.imageData,
                Slope = 0,
                //UserNum = signL1.UserNum.Replace('\0', ' ').Replace(" ", ""),
                //Reserve = signL1.Reserve.Replace('\0',' ').Replace(" ",""),
                //NodeCode = signL1.NodeCode.Replace('\0', ' ').Replace(" ", ""),
                //BranchCode = signL1.BranchCode.Replace('\0', ' ').Replace(" ", ""),
                ErrorCode = signL1.ErrorCode,
                ImageType = signL1.ImageType,
                BusinessType = signL1.BusinessType,
                MachineModel = signL1.MachineModel
            };

            try
            {
                try
                {
                    SignL2.Date = DateTime.ParseExact(signL1.Date, "yyyyMMddHHmmss", null);
                }
                catch (Exception)
                {

                }
                if (signL1.UserNum != null)
                    SignL2.UserNum = signL1.UserNum.Replace('\0', ' ').Replace(" ", "");
                if (signL1.Reserve != null)
                    SignL2.Reserve = signL1.Reserve.Replace('\0', ' ').Replace(" ", "");
                if (signL1.NodeCode != null)
                    SignL2.NodeCode = signL1.NodeCode.Replace('\0', ' ').Replace(" ", "");
                if (signL1.BranchCode != null)
                    SignL2.BranchCode = signL1.BranchCode.Replace('\0', ' ').Replace(" ", "");
                if (signL1.Slope != 0)
                    SignL2.Slope = signL1.Slope;
                SignL2.Version = signL1.Version;
                //if (SignL2.Version == "9999")
                //{
                //    SignL2.Version = "不考虑年份";
                //}
                //真假残旧标志
                SignL2.True = signL1.True+1;

                //币种
                //SetMoney();
                if (Money.ContainsKey(signL1.Currency))
                    SignL2.Currency = Money[signL1.Currency];
                //if (signL1.True == "0")
                //{
                //    SignL2.True = "真币";
                //}
                //else if (signL1.True == "1")
                //{
                //    SignL2.True = "疑币";
                //}
                //else if (signL1.True == "2")
                //{
                //    SignL2.True = "残旧";
                //}
                //else if (signL1.True == "3")
                //{
                //    SignL2.True = "假币";
                //}
                //else
                //{
                //    SignL2.True = signL1.True;
                //}
                ////
                //if (signL1.Value == 0)
                //{
                //    SignL2.True = "疑币";
                //}
                //机具类型
                if (signL1.MachineType == "0")
                {
                    SignL2.MachineType = "点钞机";
                }
                else if (signL1.MachineType == "1")
                {
                    SignL2.MachineType = "清分机";
                }
                else if (signL1.MachineType == "2")
                {
                    SignL2.MachineType = "ATM";
                }

            }
            catch (Exception)
            {
                //throw;
            }
            return SignL2;
        }

     
        
      

        


        /// <summary>    
        /// CopyMemoryEx    
        /// </summary>    
        /// <param name="dest">DataGraphPackage</param>    
        /// <param name="source">源数据缓存</param>
        /// <param name="sourceStart"> </param>
        /// <param name="length"> </param>
        /// <returns></returns>    
        public unsafe static void CopyMemoryEx(byte[] dest, byte[] source, int sourceStart, int length)
        {
            fixed (byte* sr = &source[sourceStart])
            {
                IntPtr sp = (IntPtr)sr;
                CopyMemory(dest, sp, length);
            }
        }

        public static FSNHead_L1 UnPack_FSNHead_L1(byte[] bBuffer)
        {
            FSNHead_L1 FsnHead_L1 = new FSNHead_L1();
            FsnHead_L1.headStart = new int[10];
            FsnHead_L1.headEnd = new int[4];
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    FsnHead_L1.headStart[i] = BitConverter.ToUInt16(bBuffer, i * 2);
                }
                FsnHead_L1.Count = BitConverter.ToUInt32(bBuffer, 20);
                for (int i = 0; i < 4; i++)
                {
                    FsnHead_L1.headEnd[i] += BitConverter.ToUInt16(bBuffer, 24 + i * 2);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return FsnHead_L1;
        }

        public static FSNHead_L2 UnPack_FSNHead_L2(FSNHead_L1 fsnHeadL1)
        {
            FSNHead_L2 fsnHeadL2 = new FSNHead_L2();
            try
            {
                fsnHeadL2.Count = fsnHeadL1.Count;
                string headString = "", headEnd = "";
                for (int i = 0; i < fsnHeadL1.headStart.Length; i++)
                {
                    headString += fsnHeadL1.headStart[i].ToString();
                }
                for (int i = 0; i < fsnHeadL1.headEnd.Length; i++)
                {
                    headEnd += fsnHeadL1.headEnd[i].ToString();
                }
                if (headEnd == "0123")
                {
                    if (headString == "201072601468378111")
                    {
                        fsnHeadL2.IsFsnFile = true;
                        fsnHeadL2.HaveImg = true;
                    }
                    else if (headString == "201072601458378111")
                    {
                        fsnHeadL2.IsFsnFile = true;
                        fsnHeadL2.HaveImg = false;
                    }
                    else
                    {
                        fsnHeadL2.IsFsnFile = false;
                        fsnHeadL2.HaveImg = true;
                    }
                }
                else
                {
                    fsnHeadL2.IsFsnFile = false;
                    fsnHeadL2.HaveImg = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fsnHeadL2;
        }

        //解析FSN、KY0文件的
        public static SignType UnPack_FSNData(byte[] bBuffer, string imageType)
        {
            SignType sign = new SignType();
            try
            {
                UInt32 year, month, day, hour, min, sec;
                UInt16 Date = BitConverter.ToUInt16(bBuffer, 0);
                UInt16 Time = BitConverter.ToUInt16(bBuffer, 2);
                day = (uint)Date & 0x1F;
                month = (uint)((Date >> 5) & 0x0F);
                year = (uint)((Date >> 9) + 1980);
                sec = (uint)(Time & 0x1F) << 1;
                min = (uint)(Time >> 5) & 0x3F;
                hour = (uint)(Time >> 11);
                sign.Date = year.ToString("D4") + month.ToString("D2") + day.ToString("D2") + hour.ToString("D2") +
                            min.ToString("D2") + sec.ToString("D2");
                sign.True = BitConverter.ToUInt16(bBuffer, 4);
                //if (sign.True > 4 && sign.True != 9999)
                //    sign.True = 9999;
                //if (BitConverter.ToUInt16(bBuffer, 4).ToString() == "0")
                //{
                //    sign.True = "疑币";
                //}
                //else if (BitConverter.ToUInt16(bBuffer, 4).ToString() == "1")
                //{
                //    sign.True = "真币";
                //}
                //else if (BitConverter.ToUInt16(bBuffer, 4).ToString() == "2")
                //{
                //    sign.True = "残币";
                //}
                //else if (BitConverter.ToUInt16(bBuffer, 4).ToString() == "3")
                //{
                //    sign.True = "旧币";
                //}
                //else
                //{
                //    sign.True = BitConverter.ToUInt16(bBuffer, 4).ToString();
                //}
                for (int i = 0; i < 3; i++)
                {
                    sign.ErrorCode += BitConverter.ToUInt16(bBuffer, 6 + (i * 2)).ToString() + "-";
                }
                sign.ErrorCode = sign.ErrorCode.Substring(0, sign.ErrorCode.Length - 1);
                sign.Currency = Encoding.ASCII.GetString(bBuffer, 12, 8).Replace('\0', ' ').Replace(" ", "");
                if (BitConverter.ToUInt16(bBuffer, 20) == 0)
                {
                    sign.Version = 1990;
                }
                else if (BitConverter.ToUInt16(bBuffer, 20) == 1)
                {
                    sign.Version = 1999;
                }
                else if (BitConverter.ToUInt16(bBuffer, 20) == 2)
                {
                    sign.Version = 2005;
                }
                else
                {
                    sign.Version = 9999;
                }
                sign.Value = BitConverter.ToUInt16(bBuffer, 22);
                int SignNum = BitConverter.ToUInt16(bBuffer, 24);
                sign.Sign = Encoding.ASCII.GetString(bBuffer, 26, 24).Replace('\0', ' ').Replace(" ", "");
                //2015.5.22 将所有数字字母之外的字符替换为'_'
                var regex = new Regex(@"([^a-zA-z0-9\s])");
                sign.Sign = regex.Replace(sign.Sign, "_");
                //sign.MachineMac = Encoding.ASCII.GetString(bBuffer, 50, 48).Replace('\0', ' ').Replace(" ", "");
                byte[] machineMacByte = new byte[48];
                byte[] mac = new byte[24];
                byte[] businessByte = new byte[24];
                Array.Copy(bBuffer, 50, machineMacByte, 0, 48);
                for (int i = 0; i < 24; i++)
                {
                    mac[i] = machineMacByte[i * 2];
                    businessByte[i] = machineMacByte[i * 2 + 1];
                }
                sign.MachineMac = Encoding.ASCII.GetString(mac, 0, 24);
                string strHead = Encoding.ASCII.GetString(businessByte, 0, 3);
                if (strHead == "ABC")
                {
                    int businessType = businessByte[3];
                    switch (businessType)
                    {
                        case 0:
                            sign.BusinessType = "";
                            break;
                        case 1:
                            sign.BusinessType = "柜台取款";
                            break;
                        case 2:
                            sign.BusinessType = "柜台存款";
                            break;
                        case 3:
                            sign.BusinessType = "ATM配钞";
                            break;
                    }
                    sign.BranchCode = Encoding.ASCII.GetString(businessByte, 4, 6).Replace('\0', ' ').Replace(" ", "");
                    sign.NodeCode = Encoding.ASCII.GetString(businessByte, 10, 6).Replace('\0', ' ').Replace(" ", "");
                }
                sign.Reserve = BitConverter.ToUInt16(bBuffer, 98).ToString();
                UInt16 res = BitConverter.ToUInt16(bBuffer, 98);
                //2015.05.07
                int machineSN0Len = 0, machineLen = 0;
                machineSN0Len = res & 0X000F;
                machineLen = (res >> 4) & 0X000F;
                if(machineSN0Len>4&&machineLen>0)
                {
                    string machineMM = "";
                    string[] strMachine = sign.MachineMac.Split("/".ToCharArray());
                    if (strMachine.Length == 3)
                        machineMM = strMachine[2];
                    sign.MachineModel = machineMM.Substring(0, machineLen);
                }
                sign.Slope = BitConverter.ToInt16(bBuffer, 106);
                sign.ImageType = imageType;
                if (imageType == "" || imageType == "BMP")
                {
                    sign.ImageType = "";
                    sign.imageData = new byte[1536];
                    CopyMemoryEx(sign.imageData, bBuffer, bBuffer.Length - 1536, 1536);
                }
                else if (imageType == "JPG")
                {
                    int ImageLen = BitConverter.ToInt32(bBuffer, 108);
                    sign.imageData = new byte[ImageLen];
                    if (ImageLen > 2044)
                    {
                        for (int k = 2044; k < ImageLen; k++)
                        {
                            sign.imageData[k] = 255;
                        }
                        ImageLen = 2044;
                    }
                    CopyMemoryEx(sign.imageData, bBuffer, 112, ImageLen);
                    sign.ImageType = "JPG";
                }
                else if (imageType == "16BMP")
                {
                    sign.imageData = new byte[384];
                    CopyMemoryEx(sign.imageData, bBuffer, bBuffer.Length - 384, 384);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sign;
        }
    }
}
