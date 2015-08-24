using System;
using System.Collections.Generic;
using System.Text;
using KyBase;
namespace KyModel
{
    public struct CRH
    {
        /// <summary>
        /// 记录日期  2字节
        /// </summary>
        public byte[] Date;
        /// <summary>
        /// 报送银行编码 6字节
        /// </summary>
        public byte[] BankCode;
        /// <summary>
        /// 生成网点编码 6字节
        /// </summary>
        public byte[] NodeCode;
        /// <summary>
        /// 业务类型 1字节
        /// </summary>
        public byte BussinessType;
        /// <summary>
        /// 记录数 4字节
        /// </summary>
        public byte[] RecordCount;
        /// <summary>
        /// 是否现金清分中心  1字节  默认为F
        /// </summary>
        public byte ClearCenter;
        /// <summary>
        /// 文件版本 1字节
        /// </summary>
        public byte FileVersion;
        /// <summary>
        /// 设备类别 1字节
        /// </summary>
        public byte MachineType;
        /// <summary>
        /// 机型 8字节
        /// </summary>
        public byte[] MachineModel;
        /// <summary>
        /// 设备编号 10字节
        /// </summary>
        public byte[] MachineNumber;
        /// <summary>
        /// 业务关联信息  50字节
        /// </summary>
        public byte[] BussinessNumber;
        /// <summary>
        /// 保留字 10字节
        /// </summary>
        public byte[] Reverse;
        public List<CRHRecord> records;
        public string fileName;
        public CRH(ky_agent_batch batch)
        {
            string[] dTime=batch.Date.ToString("yyyy-MM-dd HH:mm:ss").Split(' ');
            string[] date = dTime[0].Split('-');
            string[] time = dTime[1].Split(':');
            Date = BitConverter.GetBytes(((int.Parse(date[0]) - 1980) << 9) + (int.Parse(date[1]) << 5) + int.Parse(date[2]));
            BankCode = GetStringByte(batch.Branch.kBranchNumber, 6);
            NodeCode = GetStringByte(batch.Node.kNodeNumber, 6);
            BussinessType = Convert.ToByte(batch.BussinessType);
            RecordCount = BitConverter.GetBytes(batch.ktotalnumber);
            ClearCenter = Convert.ToByte('F');
            FileVersion = Convert.ToByte(1);
            MachineType = Convert.ToByte(batch.Machine.kMachineType);
            MachineModel = GetStringByte(batch.Machine.kMachineModel, 8);
            MachineNumber = GetStringByte(batch.Machine.kMachineNumber, 10);
            BussinessNumber = GetStringByte(batch.BussinessNumber, 50);
            Reverse = new byte[10];
            records = new List<CRHRecord>();
            if (batch.Signs != null)
            {
                byte[] StartTime=BitConverter.GetBytes(((int.Parse(time[0]) << 11) + (int.Parse(time[1]) << 5) + (int.Parse(time[0]) >> 1)));
                foreach (ky_agent_sign sign in batch.Signs)
                {
                    CRHRecord record = new CRHRecord
                    {
                        //机器开始点钞时间
                        Time = StartTime,
                        Sign = GetStringByte(sign.ksign, 12),
                        SignVersion = Convert.ToByte(sign.kversion),
                        SignValue = Convert.ToByte(sign.kvalue)
                    };
                    records.Add(record);
                }
            }
            fileName = Encoding.ASCII.GetString(BankCode).Replace("_", "") + "_" + Encoding.ASCII.GetString(MachineModel).Replace("_", "").Replace("/", "-").Replace("\\", "-") + "_"
                + Encoding.ASCII.GetString(MachineNumber).Replace("_", "").Replace("/", "-").Replace("\\", "-")+"_" + batch.Date.ToString("yyyyMMddHHmmss") + ".CRH";
        }
        
        /// <summary>
        /// 字符串转byte数组，不足左补_,大于长度截取末尾指定长字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetStringByte(string str,int length)
        {
            if (str.Length > length)
                str = str.Substring(str.Length - length, length);
            else
                str = string.Format("{0," + length + "}", str).Replace(" ", "_");
            return Encoding.ASCII.GetBytes(str);
        }
 

    }
    public struct CRHRecord
    {
        /// <summary>
        /// 记录时间  2字节
        /// </summary>
        public byte[] Time;
        /// <summary>
        /// 冠字号码  12字节
        /// </summary>
        public byte[] Sign;
        /// <summary>
        /// 版别 1字节
        /// </summary>
        public byte SignVersion;
        /// <summary>
        /// 币值  1字节
        /// </summary>
        public byte SignValue;
    }
    public class CRHDisplay
    {
        public string Date { get; set; }
        public string BankCode { get; set; }
        public string NodeCode { get; set; }
        public string BussinessType { get; set; }
        public int RecordCount { get; set; }
        public string ClearCenter { get; set; }
        public string FileVersion { get; set; }
        public string MachineType { get; set; }
        public string MachineModel { get; set; }
        public string MachineNumber { get; set; }
        public string BussinessNumber { get; set; }
        public List<CRHDisplayRecord> records { get; set; }
        public CRHDisplay(byte[] bBuffer)
        {
           int index = 0;
           UInt16 date = BitConverter.ToUInt16(bBuffer, index); index += 2;
           UInt32 year, month, day;
           day = (uint)date & 0x1F;
           month = (uint)((date >> 5) & 0x0F);
           year = (uint)((date >> 9) + 1980);
           Date = year.ToString("D4") + month.ToString("D2") + day.ToString("D2");
           BankCode = Encoding.ASCII.GetString(bBuffer, index, 6).Replace("_", ""); index += 6;
           NodeCode = Encoding.ASCII.GetString(bBuffer, index, 6).Replace("_", ""); index += 6;
           BussinessType = Convert.ToString(bBuffer[index]); index += 1;
           switch (BussinessType)
           {
               case "1": BussinessType = clsMsg.getMsg("CRH_1"); break;
               case "2": BussinessType = clsMsg.getMsg("CRH_2"); break;
               case "3": BussinessType = clsMsg.getMsg("CRH_3"); break;
               default: BussinessType = clsMsg.getMsg("CRH_4"); break;
           }
           RecordCount = BitConverter.ToInt32(bBuffer, index); index += 4;
           ClearCenter = Encoding.ASCII.GetString(bBuffer,index,1); index += 1;
           switch (ClearCenter)
           {
               case "T": ClearCenter = clsMsg.getMsg("CRH_5"); break;
               default: ClearCenter = clsMsg.getMsg("CRH_6"); break;
           }
           FileVersion = Convert.ToString(bBuffer[index]); index += 1;
           MachineType = Convert.ToString(bBuffer[index]); index += 1;
           switch (MachineType)
           {
               case "1": MachineType = clsMsg.getMsg("CRH_7"); break;
               case "2": MachineType = clsMsg.getMsg("CRH_8"); break;
               case "3": MachineType = clsMsg.getMsg("CRH_9"); break;
               case "4": MachineType = clsMsg.getMsg("CRH_10"); break;
               case "5": MachineType = clsMsg.getMsg("CRH_11"); break;
               default: MachineType = clsMsg.getMsg("CRH_4"); break;
           }
           MachineModel = Encoding.ASCII.GetString(bBuffer, index, 8).Replace("_", ""); index += 8;
           MachineNumber = Encoding.ASCII.GetString(bBuffer, index, 10).Replace("_", ""); index += 10;
           BussinessNumber = Encoding.ASCII.GetString(bBuffer, index, 50).Replace("_", ""); index += 50;
           index += 10;//保留字
           records = new List<CRHDisplayRecord>();
           for (var i = 0; i < RecordCount; i++)
           {
               UInt32  hour, min, sec;
               UInt16 time=BitConverter.ToUInt16(bBuffer, index); index += 3;
               sec = (uint)(time & 0x1F) << 1;
               min = (uint)(time >> 5) & 0x3F;
               hour = (uint)(time >> 11);
               string  RecordTime=hour.ToString("D2") + min.ToString("D2") + sec.ToString("D2");
               string RecordSign=Encoding.ASCII.GetString(bBuffer, index, 12);index += 13;
               string RecordVersion = Convert.ToString(bBuffer[index]); index += 2;
               switch (RecordVersion)
               {
                   case "0": RecordVersion = "1990"; break;
                   case "1": RecordVersion = "1999"; break;
                   case "2": RecordVersion = "2005"; break;
                   case "255": RecordVersion = clsMsg.getMsg("CRH_12"); break;
                   default: RecordVersion = clsMsg.getMsg("CRH_4"); break;
               }
               string RecordValue = Convert.ToString(bBuffer[index]); index += 3;
               switch (RecordValue)
               {
                   case "0": RecordValue = clsMsg.getMsg("CRH_13"); break;
                   case "1": RecordValue = "1"; break;
                   case "2": RecordValue = "5"; break;
                   case "3": RecordValue = "10"; break;
                   case "4": RecordValue = "20"; break;
                   case "5": RecordValue = "50"; break;
                   case "6": RecordValue = "100"; break;
                   default: RecordValue = clsMsg.getMsg("CRH_4"); break;
               }
               CRHDisplayRecord record = new CRHDisplayRecord
               {
                   //机器开始点钞时间
                   Time = hour.ToString("D2") +":"+ min.ToString("D2") +":"+ sec.ToString("D2"),
                   Sign = RecordSign,
                   SignVersion = RecordVersion,
                   SignValue = RecordValue
               };
               records.Add(record);
           }
        }
 
    }
    public class CRHDisplayRecord
    {
        public string Time { get; set; }
        public string Sign { get; set; }
        public string SignVersion { get; set; }
        public string SignValue { get; set; }
    }
   
}
