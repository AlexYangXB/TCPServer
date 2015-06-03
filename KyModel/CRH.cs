using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

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
            if (batch.Machine != null)
            {
                MachineType = Convert.ToByte(batch.MachineType);
                MachineModel = GetStringByte(batch.Machine.kMachineModel, 8);
                MachineNumber = GetStringByte(batch.Machine.kMachineNumber, 10);
            }
            else
            {
                //没有找到机具的情况
                MachineType = Convert.ToByte(3);
                MachineModel = GetStringByte("HT9000A", 8);
                MachineNumber = GetStringByte("123456", 10);
            }
            BussinessNumber = GetStringByte(batch.BussinessNumber, 50);
            Reverse = new byte[10];
            records = null;
        }
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
 
    }
   
}
