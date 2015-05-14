﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using KyData.DataBase;

namespace KyData
{
    public class KyDataLayer2
    {
        //各种格式文件的数据大小
        private const int DatSize = 1632, FsnSize = 1644, Ky0Size = 2156;
        //各种格式文件的文件头大小
        private const int DatHead = 12, FsnHead = 32, Ky0Head = 32;

        /// <summary>
        /// 判断是否为正确的文件格式
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExtensions"></param>
        /// <returns></returns>
        public static bool IsCorrectFileFormat(string fileName, string fileExtensions)
        {
            bool result = false;
            if (fileExtensions == ".FSN" || fileExtensions == ".KY0")
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] buf = new byte[32];
                    fs.Read(buf, 0, buf.Length);
                    KYDataLayer1.FSNHead_L2 dataHead = KYDataLayer1.UnPack_FSNHead_L2(KYDataLayer1.UnPack_FSNHead_L1(buf));
                    if (dataHead.IsFsnFile)
                        result = true;
                }
            }
            else if (fileExtensions == ".DAT")
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    byte[] buf = new byte[12];
                    fs.Read(buf, 0, buf.Length);
                    KYDataLayer1.DataHead dataHead = new KYDataLayer1.DataHead();
                    try
                    {
                        Encoding coding = Encoding.ASCII;
                        dataHead.fileVer = coding.GetString(buf, 0, 3);
                        dataHead.HaveImg = coding.GetString(buf, 3, 1);
                        dataHead.Count = int.Parse(coding.GetString(buf, 4, 8));
                        if (dataHead.fileVer == "1.0" && dataHead.Count > 0)
                        {
                            result = true;
                        }
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }


        #region  解析文件
        /// <summary>
        /// 解析DAT文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<KYDataLayer1.SignTypeL2> ReadFromDAT(string filePath)
        {
            List<KYDataLayer1.SignTypeL2> signList = new List<KYDataLayer1.SignTypeL2>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //解析头文件
                byte[] buf = new byte[DatHead];
                fs.Read(buf, 0, buf.Length);
                KYDataLayer1.DataHead dataHead = KYDataLayer1.unPack_DataHead_L1(buf);

                //解析数据
                byte[] bBuf = new byte[DatSize];
                for (int i = 0; i < dataHead.Count; i++)
                {
                    try
                    {
                        if (fs.Read(bBuf, 0, bBuf.Length) > 0)
                        {
                            //KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.unPack_SignType_L1(bBuf));
                            KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.unPack_SignType_L1(bBuf));
                            signList.Add(sign);
                        }
                        else
                        {
                            break;
                        }

                    }
                    catch (Exception)
                    {
                        //string strErr = e.ToString();
                        //throw;
                    }

                }

            }
            return signList;
        }

        /// <summary>
        /// 分页解析DAT文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="page"> </param>
        /// <param name="numPerPage"> </param>
        /// <param name="totalPage"> </param>
        /// <param name="count"> </param>
        /// <returns></returns>
        public static List<KYDataLayer1.SignTypeL2> ReadFromDATInPage(string filePath, int page, int numPerPage, out int totalPage, out int count)
        {
            //数据记录数
            FileInfo fileInfo = new FileInfo(filePath);
            count = Convert.ToInt32((fileInfo.Length - DatHead) / DatSize);
            totalPage = Convert.ToInt32((count + (numPerPage - 1)) / numPerPage);

            List<KYDataLayer1.SignTypeL2> signList = new List<KYDataLayer1.SignTypeL2>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //解析头文件
                byte[] bufhead = new byte[DatHead];
                fs.Read(bufhead, 0, bufhead.Length);
                KYDataLayer1.DataHead dataHead = KYDataLayer1.unPack_DataHead_L1(bufhead);

                //解析数据
                byte[] bBuf = new byte[DatSize];
                long offset = (page - 1) * numPerPage * DatSize;
                fs.Seek(offset, SeekOrigin.Current);
                for (long i = (page - 1) * numPerPage; i < dataHead.Count && i < page * numPerPage; i++)
                {
                    try
                    {
                        if (fs.Read(bBuf, 0, bBuf.Length) > 0)
                        {
                            KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.unPack_SignType_L1(bBuf));
                            signList.Add(sign);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

            }
            return signList;
        }

        /// <summary>
        /// 解析标准FSN文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<KYDataLayer1.SignTypeL2> ReadFromFSN(string filePath)
        {
            List<KYDataLayer1.SignTypeL2> signList = new List<KYDataLayer1.SignTypeL2>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //解析头文件
                byte[] buf = new byte[FsnHead];
                fs.Read(buf, 0, buf.Length);
                KYDataLayer1.FSNHead_L2 dataHead = KYDataLayer1.UnPack_FSNHead_L2(KYDataLayer1.UnPack_FSNHead_L1(buf));

                //解析数据
                byte[] bBuf = new byte[FsnSize];
                if (dataHead.IsFsnFile)
                {
                    if (!dataHead.HaveImg)
                    {
                        bBuf = new byte[100];
                    }

                    for (int i = 0; i < dataHead.Count; i++)
                    {
                        if (fs.Read(bBuf, 0, bBuf.Length) > 0)
                        {
                            KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.UnPack_FSNData(bBuf, ""));
                            signList.Add(sign);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    signList = null;
                }
            }
            return signList;
        }
        /// <summary>
        /// 解析KY0文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<KYDataLayer1.SignTypeL2> ReadFromKY0(string filePath)
        {
            List<KYDataLayer1.SignTypeL2> signList = new List<KYDataLayer1.SignTypeL2>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //解析头文件
                byte[] buf = new byte[Ky0Head];
                fs.Read(buf, 0, buf.Length);
                KYDataLayer1.FSNHead_L2 dataHead = KYDataLayer1.UnPack_FSNHead_L2(KYDataLayer1.UnPack_FSNHead_L1(buf));

                //解析数据
                byte[] bBuf = new byte[Ky0Size];
                if (dataHead.IsFsnFile)
                {
                    if (!dataHead.HaveImg)
                    {
                        bBuf = new byte[100];
                    }

                    for (int i = 0; i < dataHead.Count; i++)
                    {
                        if (fs.Read(bBuf, 0, bBuf.Length) > 0)
                        {
                            try
                            {
                                KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.UnPack_FSNData(bBuf, "JPG"));
                                signList.Add(sign);
                            }
                            catch (Exception)
                            {
                            }

                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    signList = null;
                }
            }
            return signList;
        }

        /// <summary>
        /// 分页解析标准FSN文件
        /// </summary>
        /// <param name="filePath"> 文件路径 </param>
        /// <param name="page"> 页码 从1开始 </param>
        /// <param name="numPerPage"> 每页显示的记录数据 </param>
        /// <param name="totalPage"> 总页数 </param>
        /// <param name="count"> 总记录数 </param>
        /// <returns></returns>
        public static List<KYDataLayer1.SignTypeL2> ReadFromFSNInPage(string filePath, int page, int numPerPage, out int totalPage, out int count)
        {
            //数据记录数
            FileInfo fileInfo = new FileInfo(filePath);
            count = Convert.ToInt32((fileInfo.Length - FsnHead) / FsnSize);
            totalPage =Convert.ToInt32((count + (numPerPage - 1)) / numPerPage);

            List<KYDataLayer1.SignTypeL2> signList = new List<KYDataLayer1.SignTypeL2>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //解析头文件
                byte[] buf = new byte[FsnHead];
                fs.Read(buf, 0, buf.Length);
                KYDataLayer1.FSNHead_L2 dataHead = KYDataLayer1.UnPack_FSNHead_L2(KYDataLayer1.UnPack_FSNHead_L1(buf));

                //解析数据
                byte[] bBuf = new byte[FsnSize];
                if (dataHead.IsFsnFile)
                {
                    if (!dataHead.HaveImg)
                    {
                        bBuf = new byte[100];
                    }
                    long offset = (page - 1) * numPerPage * FsnSize;
                    fs.Seek(offset, SeekOrigin.Current);
                    for (long i = (page - 1) * numPerPage; i < dataHead.Count && i < page * numPerPage; i++)
                    {
                        if (fs.Read(bBuf, 0, bBuf.Length) > 0)
                        {
                            KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.UnPack_FSNData(bBuf, ""));
                            signList.Add(sign);
                        }
                    }
                }
                else
                {
                    signList = null;
                    totalPage = 0;
                    count = 0;
                }
            }
            return signList;
        }

        /// <summary>
        /// 分页解析KY0文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<KYDataLayer1.SignTypeL2> ReadFromKY0InPage(string filePath, int page, int numPerPage, out int totalPage, out int count)
        {
            //数据记录数
            FileInfo fileInfo = new FileInfo(filePath);
            count = Convert.ToInt32((fileInfo.Length - Ky0Head) / Ky0Size);
            totalPage = Convert.ToInt32((count + (numPerPage - 1)) / numPerPage);


            List<KYDataLayer1.SignTypeL2> signList = new List<KYDataLayer1.SignTypeL2>();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //解析文件头
                byte[] buf = new byte[Ky0Head];
                fs.Read(buf, 0, buf.Length);
                KYDataLayer1.FSNHead_L2 dataHead = KYDataLayer1.UnPack_FSNHead_L2(KYDataLayer1.UnPack_FSNHead_L1(buf));

                //解析数据
                byte[] bBuf = new byte[Ky0Size];
                if (dataHead.IsFsnFile)
                {
                    if (!dataHead.HaveImg)
                    {
                        bBuf = new byte[100];
                    }

                    //count = Convert.ToInt32(dataHead.Count);
                    long offset = (page - 1) * numPerPage * Ky0Size;
                    fs.Seek(offset, SeekOrigin.Current);
                    for (long i = (page - 1) * numPerPage; i < dataHead.Count && i < page * numPerPage; i++)
                    {
                        if (fs.Read(bBuf, 0, bBuf.Length) > 0)
                        {
                            KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.UnPack_FSNData(bBuf, "JPG"));
                            signList.Add(sign);
                        }
                    }
                }
                else
                {
                    signList = null;
                    totalPage = 0;
                    count = 0;
                }
            }
            return signList;
        }

        /// <summary>
        /// 从FSN文件中获取统计数据
        /// </summary>
        /// <param name="filePath">FSN文件路径</param>
        /// <param name="amount">KYDataLayer1.Amount 结构的统计数据</param>
        public static void GetTotalValueFromFSN(string filePath,out DataBase.KYDataLayer1.Amount amount)
        {
            //List<KYDataLayer1.SignTypeL2> signList = new List<KYDataLayer1.SignTypeL2>();
            amount=new KYDataLayer1.Amount();
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //解析头文件
                byte[] buf = new byte[FsnHead];
                fs.Read(buf, 0, buf.Length);
                KYDataLayer1.FSNHead_L2 dataHead = KYDataLayer1.UnPack_FSNHead_L2(KYDataLayer1.UnPack_FSNHead_L1(buf));

                //解析数据
                byte[] bBuf = new byte[FsnSize];
                if (dataHead.IsFsnFile)
                {
                    if (!dataHead.HaveImg)
                    {
                        bBuf = new byte[100];
                    }

                    for (int i = 0; i < dataHead.Count; i++)
                    {
                        if (fs.Read(bBuf, 0, bBuf.Length) > 0)
                        {
                            KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.UnPack_FSNData(bBuf, ""));
                            if (i == 0)
                                amount.FirstSign = sign.Sign;
                            if (i == dataHead.Count - 1)
                                amount.LastSign = sign.Sign;
                            amount.TotalValue += Convert.ToInt32(sign.Value);
                            amount.TotalCnt++;
                            switch (sign.True)
                            {
                                case 1:
                                    amount.TotalErr++;
                                    break;
                                case 2:
                                    amount.TotalTrue++;
                                    break;
                                case 3:
                                    amount.TotalWorn++;
                                    break;
                                case 4:
                                    amount.TotalOld++;
                                    break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 从FSN文件中获取机具编号
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="machineModel"> </param>
        /// <returns>机具编号</returns>
        public static string GetMachineNumberFromFSN(string filePath,out string machineModel)
        {
            machineModel = "";
            string machineNumber = "";
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //解析头文件
                byte[] buf = new byte[FsnHead];
                fs.Read(buf, 0, buf.Length);
                KYDataLayer1.FSNHead_L2 dataHead = KYDataLayer1.UnPack_FSNHead_L2(KYDataLayer1.UnPack_FSNHead_L1(buf));

                //解析数据
                byte[] bBuf = new byte[FsnSize];
                if (dataHead.IsFsnFile)
                {
                    if (!dataHead.HaveImg)
                    {
                        bBuf = new byte[100];
                    }

                    for (int i = 0; i < dataHead.Count; i++)
                    {
                        if (fs.Read(bBuf, 0, bBuf.Length) > 0)
                        {
                            KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.UnPack_FSNData(bBuf, ""));
                            machineNumber = sign.MachineMac;
                            machineModel = sign.MachineModel;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return machineNumber;
        }

        //获取文件中的第一个点钞时间
        public static DateTime GetDateTime(byte[] bFsn)
        {
            byte[] buf = new byte[1644];
            Array.Copy(bFsn, 32, buf, 0, 32);
            KYDataLayer1.SignTypeL2 sign = KYDataLayer1.unPack_SignType_L2(KYDataLayer1.UnPack_FSNData(buf, ""));
            return sign.Date;
        }

        /// <summary>
        /// 读取GZH文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static KYDataLayer1.Gzh ReadGzh(string file)
        {
            try
            {
                KYDataLayer1.Gzh gzh = new KYDataLayer1.Gzh();
                gzh.fileName = Path.GetFileName(file);

                using (StreamReader sr = new StreamReader(file, Encoding.ASCII))
                {
                    string strLine = sr.ReadLine();
                    string[] str = strLine.Split(":".ToCharArray());
                    if (str.Length == 10)
                    {
                        
                        try
                        {
                            gzh.date = DateTime.ParseExact(str[0], "yyyyMMddHHmmss", null);
                        }
                        catch (Exception)
                        {

                        }
                        gzh.branchNumber = str[1];
                        gzh.nodeNumber = str[2];
                        gzh.business = str[3];
                        gzh.fileCnt = int.Parse(str[4]);
                        gzh.isCashCenter = str[5];
                        gzh.fileVersion = str[6];
                        gzh.packageNumber = str[7];
                        gzh.currency = str[8];
                        gzh.reserved = str[9];
                    }
                    else
                    {
                        return null;
                    }
                }
                return gzh;
            }
            catch (Exception)
            {
                return null;
            }
        }


        

        #endregion

        /// <summary>
        /// 根据文件的最大记录数据把文件分割成多个文件
        /// </summary>
        /// <param name="fsnFile">fsn文件名 格式为：yyyyMddHHmmss_9K0001_Q.FSN(日期时间_机具编号_业务类型.FSN)</param>
        /// <param name="desFolder"> </param>
        /// <param name="fileMaxRecord">文件最大记录数</param>
        /// <param name="fsnFiles">分割后的文件名数组</param>
        public static void DecompositionFile(string fsnFile,string desFolder , int fileMaxRecord)
        {
            FileInfo fileInfo = new FileInfo(fsnFile);
            long Cnt = (fileInfo.Length - 32) / 1644;
            int fileCnt = Convert.ToInt32((Cnt + (fileMaxRecord - 1)) / fileMaxRecord);
            string[] fsnFiles = new string[fileCnt];
            for (int i = 0; i < fileCnt; i++)
            {
                fsnFiles[i] = string.Format("{0}\\{1}.FSN", desFolder, i);
                using (FileStream fs = new FileStream(fsnFile, FileMode.Open, FileAccess.Read))
                {
                    //头文件
                    byte[] buf = new byte[32];
                    fs.Read(buf, 0, buf.Length);
                    //内容
                    byte[] bufData = new byte[1644];
                    int offset = i * fileMaxRecord * 1644;
                    fs.Seek(offset, SeekOrigin.Current);
                    using (FileStream fsw = new FileStream(fsnFiles[i], FileMode.Create, FileAccess.Write))
                    {
                        fsw.Write(buf, 0, buf.Length);
                        for (int k = 0; k < fileMaxRecord && k < Cnt - (i * fileMaxRecord); k++)
                        {
                            fs.Read(bufData, 0, bufData.Length);
                            fsw.Write(bufData, 0, bufData.Length);
                        }
                    }
                    using (FileStream fsw = new FileStream(fsnFiles[i], FileMode.Open, FileAccess.Write))
                    {
                        fileInfo = new FileInfo(fsnFiles[i]);
                        long Cnt1 = (fileInfo.Length - 32) / 1644;
                        byte[] tmp = BitConverter.GetBytes((UInt32)Cnt1);
                        fsw.Seek(20, SeekOrigin.Begin);
                        fsw.Write(tmp, 0, tmp.Length);
                        fsw.Close();
                    }
                }
            }
            File.Delete(fsnFile);
        }
       


        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {

            byte[] buffer = Guid.NewGuid().ToByteArray();

            return BitConverter.ToInt64(buffer, 0);

        }
    }
}
