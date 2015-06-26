using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using KyBll;
namespace MyTest
{
    public class MakeFsn
    {
        public static byte[] ExportFSN(int Value, int Count, string MachineNumber)
        {
            try
            {
                UInt16[] HeadStart = new ushort[4] { 20, 10, 7, 26 };
                UInt16[] HeadString = new ushort[6] { 0, 1, 46, 'S', 'N', 'o' };
                UInt16[] HeadEnd = new ushort[4] { 0, 1, 2, 3 };
                byte[] fsn = new byte[32 + 1644 * Count];
                int index = 0;
                fsn=CopyArray(HeadStart, fsn, index); index += 8;
                fsn=CopyArray(HeadString, fsn, index); index += 12;
                Array.Copy(BitConverter.GetBytes(Count), 0, fsn, index, 4); index += 4;
                fsn=CopyArray(HeadEnd, fsn, index); index += 8;
                for (int i = 0; i < Count; i++)
                {
                    UInt16 Date;
                    UInt16 Time;
                    UInt16 tfFlag;
                    UInt16[] ErrorCode = new ushort[3];
                    UInt16[] MoneyFlag = new ushort[4];
                    UInt16 Ver;
                    UInt16 Valuta;
                    UInt16 CharNUM;
                    UInt16[] SNo = new ushort[12];
                    UInt16[] MachineSNo = new ushort[24];
                    UInt16 Reserve1 = 0;
                    UInt16 Num;
                    UInt16 Height = 32;
                    UInt16 Width = 32;
                    UInt16 Reserve2 = 0;
                    byte[] imageData = new byte[1536];

                    DateTime signTime = DateTime.Now;
                    string dateTime = signTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string[] dTime = dateTime.Split(' ');
                    string[] date = dTime[0].Split('-');
                    string[] time = dTime[1].Split(':');
                    Date = (UInt16)(((int.Parse(date[0]) - 1980) << 9) + (int.Parse(date[1]) << 5) + int.Parse(date[2]));
                    Time = (UInt16)(((int.Parse(time[0]) << 11) + (int.Parse(time[1]) << 5) + (int.Parse(time[0]) >> 1)));
                    tfFlag = 1;
                    byte[] money = Encoding.ASCII.GetBytes("CNY");
                    for (int j = 0; j < money.Length; j++)
                    {
                        MoneyFlag[j] = money[j];
                    }
                    Ver = 2;
                    Valuta = (ushort)Value;
                    CharNUM = 10;
                    Num = 10;
                    byte[] bSign = Encoding.ASCII.GetBytes(GenerateSign());
                    for (int j = 0; j < bSign.Length; j++)
                    {
                        SNo[j] = bSign[j];
                    }
                    byte[] bMachine = Encoding.ASCII.GetBytes("BOC15/GZKY/"+MachineNumber);
                    for (int j = 0; j < bMachine.Length; j++)
                    {
                        MachineSNo[j] = bMachine[j];
                    }

                    Array.Copy(BitConverter.GetBytes(Date), 0, fsn, index, 2); index += 2;
                    Array.Copy(BitConverter.GetBytes(Time), 0, fsn, index, 2); index += 2;
                    Array.Copy(BitConverter.GetBytes(tfFlag), 0, fsn, index, 2); index += 2;
                    fsn = CopyArray(ErrorCode, fsn, index); index += 6;
                    fsn = CopyArray(MoneyFlag, fsn, index); index += 8;
                    Array.Copy(BitConverter.GetBytes(Ver), 0, fsn, index, 2); index += 2;
                    Array.Copy(BitConverter.GetBytes(Valuta), 0, fsn, index, 2); index += 2;
                    Array.Copy(BitConverter.GetBytes(CharNUM), 0, fsn, index, 2); index += 2;
                    fsn = CopyArray(SNo, fsn, index); index += 24;
                    fsn = CopyArray(MachineSNo, fsn, index); index += 48;
                    Array.Copy(BitConverter.GetBytes(Reserve1), 0, fsn, index, 2); index += 2;
                    Array.Copy(BitConverter.GetBytes(Num), 0, fsn, index, 2); index += 2;
                    Array.Copy(BitConverter.GetBytes(Height), 0, fsn, index, 2); index += 2;
                    Array.Copy(BitConverter.GetBytes(Width), 0, fsn, index, 2); index += 2;
                    Array.Copy(BitConverter.GetBytes(Reserve2), 0, fsn, index, 2); index += 2;
                    Array.Copy(imageData, 0,fsn, index, imageData.Length); index += imageData.Length;
                }
                return fsn;
            }
            catch (Exception e)
            {
                Log.UnHandleException("写入FSN异常", e);
                return null;
            }
        }
        public static byte[] CopyArray(UInt16[] bytes,byte[] dest,int index)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes(bytes[i]), 0, dest, index + i * 2, 2);
            }
            return dest;
        }
        public static string GenerateSign()
        {
            Guid g=Guid.NewGuid();
            string sign=g.ToString().Replace("-", "").ToUpper().Substring(0, 10);
            return sign;
        }
       
    }

}
