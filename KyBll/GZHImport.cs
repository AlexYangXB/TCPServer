using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using KyBll.Base;
using KyModel;
using KyModel.Models;
namespace KyBll
{
    public class GZHImport
    {
        /// <summary>
        /// 读取GZH文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static GZH ReadGZH(string file)
        {
            try
            {
                GZH gzh = new GZH();
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
        /// <summary>
        /// 保存GZH bundle信息并获取ID
        /// </summary>
        /// <param name="bundleNumber"></param>
        /// <param name="batchId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int SaveGzhBundle(string bundleNumber, long batchId, string fileName)
        {
            if (batchId == 0)
                return 0;
            bool result = KyDataOperation.InsertGzhBundle(bundleNumber, batchId, fileName);
            if (result)
            {
                return KyDataOperation.GetGzhBundleId(bundleNumber, batchId);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 保存用户上传的GZH文件
        /// </summary>
        /// <param name="gzhDirectory">gzh文件夹（包含一个GZH文件和多个FSN文件）</param>
        /// <param name="pictureServerId"> 图片服务器Id</param>
        /// <param name="userId">用户Id </param>
        /// <returns></returns>
        public static bool UploadGzhPackage(string gzhDirectory, int pictureServerId, int userId)
        {
            bool result = false;
            //获取后缀名为GZH、gzh的文件
            string[] Gzhfiles = Directory.GetFiles(gzhDirectory, "*.GZH");
            //获取后缀名为FSN、fsn的文件
            string[] FsnFiles = Directory.GetFiles(gzhDirectory, "*.FSN");
            ky_gzh_package gzh_package = new ky_gzh_package();

            if (Gzhfiles.Length == 1) //表示只有一个GZH文件
            {
                //读取GZH文件信息
                gzh_package = GZH2ky_gzh_pakage(GZHImport.ReadGZH(Gzhfiles[0]));
                if (gzh_package.kFSNNumber == FsnFiles.Length)//表示GZH文件中的记录数=实际的FSN文件的总数
                {
                    //①保存GzhPackage信息，并获得packageId
                    //获取总张数，总金额
                    int TotalValue = 0, TotalNumber = 0;
                    for (int i = 0; i < FsnFiles.Length; i++)
                    {
                        KYDataLayer1.Amount amount;
                        KyDataLayer2.GetTotalValueFromFSN(FsnFiles[i], out amount);
                        TotalValue += amount.TotalValue;
                        TotalNumber += amount.TotalCnt;
                    }
                    gzh_package.kTotalNumber = TotalNumber;
                    gzh_package.kTotalValue = TotalValue;
                    gzh_package.kUserId = userId;
                    int packageId = KyDataOperation.InsertGzhPackage(gzh_package);

                    for (int i = 0; i < FsnFiles.Length; i++)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(FsnFiles[i]);
                        string[] str = fileName.Split("_".ToCharArray());
                        if (str.Length == 2)
                        {
                            string bundleNumber = str[1];
                            ky_machine machine = FSNFormat.FindMachineByFsn(FsnFiles[i], gzh_package.kNodeId);
                            machine.userId = userId;
                            machine.imgServerId = pictureServerId;
                            machine.business = BussinessType.KHDK;
                            //②保存FSN文件，获得BatchId
                            long batchId = FSNImport.UploadFsn(FsnFiles[i], machine);
                            //③保存GzhBundle，获得BundleId
                            int bundleId = SaveGzhBundle(bundleNumber, batchId, fileName);
                            //④保存Package_Bundle信息
                            result = KyDataOperation.SavePackageBundle(bundleId, packageId);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 转换Gzh到ky_gzh_package
        /// </summary>
        /// <param name="gzh"></param>
        /// <returns></returns>
        public static ky_gzh_package GZH2ky_gzh_pakage(GZH gzh)
        {
            ky_gzh_package gzhLayer2 = new ky_gzh_package()
            {
                kDate = gzh.date,
                kType = int.Parse(gzh.business),
                kFSNNumber = gzh.fileCnt,
                kCashCenter = gzh.isCashCenter,
                kVersion = gzh.fileVersion,
                kPackageNumber = gzh.packageNumber,
                kCurrency = gzh.currency,
                kFileName = gzh.fileName,
            };
            gzhLayer2.kBranchId = KyDataOperation.GetBranchId(gzh.branchNumber);
            gzhLayer2.kNodeId = KyDataOperation.GetNodeIdByNodeNumber(gzh.nodeNumber);
            return gzhLayer2;
        }

        /// <summary>
        /// GZH压缩包解压并保存到数据库
        /// </summary>
        /// <param name="gzhZip">GZH压缩包（ZIP类型）</param>
        /// <param name="targetDirectory">解压到目标文件夹</param>
        /// <param name="pictureServerId"> </param>
        /// <param name="userId"> </param>
        /// <returns></returns>
        public static bool UploadGzhFile(string gzhZip, string targetDirectory, int pictureServerId, int userId)
        {
            bool success = UnZipGzh(gzhZip, targetDirectory);
            if (success)
            {
                //string[] ZipFile = Directory.GetFiles(targetDirectory, "*.ZIP");
                string[] zipFile = Directory.GetFiles(targetDirectory, "*.ZIP");
                if (zipFile.Length > 0)//说明还有压缩文件，gzhZip为二次压缩文件
                {
                    for (int i = 0; i < zipFile.Length; i++)
                    {
                        success = UnZipGzh(zipFile[i], targetDirectory + "\\tmp");
                        if (success)
                        {
                            bool result = GZHImport.UploadGzhPackage(targetDirectory + "\\tmp",
                                                                                pictureServerId, userId);
                            //删除文件
                            if (Directory.Exists(targetDirectory + "\\tmp"))
                            {
                                string[] files = Directory.GetFiles(targetDirectory + "\\tmp");
                                foreach (var file in files)
                                {
                                    File.Delete(file);
                                }
                            }
                            if (!result)
                                return false;
                        }
                    }
                    return true;
                }
                else//说明没有压缩文件了，gzhZip为一次压缩文件
                {
                    return GZHImport.UploadGzhPackage(targetDirectory, pictureServerId, userId);
                }
            }
            else
            {
                return false;
            }

        }
        public static bool UnZipGzh(string gzhZip, string targetDirectory)
        {
            try
            {
                Base.ZipClass zipClass = new ZipClass();
                zipClass.UNZipFile(gzhZip, targetDirectory);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 保存业务KHDK
        /// </summary>
        /// <param name="gzhDirectory"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        public static bool SaveKHDK(List<string> FsnFiles, ky_machine machine)
        {
            bool result = false;
            int startIndex = 1;
            DateTime fileTime = DateTime.Now;
            ky_gzh_package gzh_package = new ky_gzh_package();

            //赋值
            gzh_package.kDate = fileTime;
            gzh_package.kBranchId = KyDataOperation.GetBranchIdWithNodeId(machine.kNodeId);
            gzh_package.kNodeId = machine.kNodeId;
            gzh_package.kType = 3;
            gzh_package.kFSNNumber = FsnFiles.Count;
            gzh_package.kCashCenter = machine.isClearCenter;
            gzh_package.kVersion = "1";
            gzh_package.kPackageNumber = machine.packageNumber;
            gzh_package.kCurrency = "CNY";

            if (gzh_package.kFSNNumber == FsnFiles.Count)//表示GZH文件中的记录数=实际的FSN文件的总数
            {
                //①保存GzhPackage信息，并获得packageId
                //获取总张数，总金额
                int TotalValue = 0, TotalNumber = 0;
                for (int i = 0; i < FsnFiles.Count; i++)
                {
                    KYDataLayer1.Amount amount;
                    KyDataLayer2.GetTotalValueFromFSN(FsnFiles[i], out amount);
                    TotalValue += amount.TotalValue;
                    TotalNumber += amount.TotalCnt;
                }
                gzh_package.kTotalNumber = TotalNumber;
                gzh_package.kTotalValue = TotalValue;
                gzh_package.kUserId = machine.userId;
                gzh_package.kFileName = "";
                int packageId = KyDataOperation.InsertGzhPackage(gzh_package);

                for (int i = 0; i < FsnFiles.Count; i++)
                {
                    string bundleNumber = string.Format("{0}{1,4}", fileTime.ToString("yyMMdd"), startIndex + i).Replace(" ", "0");
                    //②保存FSN文件，获得BatchId
                    long batchId = FSNImport.SaveFsn(FsnFiles[i], machine);
                    if (batchId != 0)
                    {
                        //③保存GzhBundle，获得BundleId
                        int bundleId = SaveGzhBundle(bundleNumber, batchId, "");
                        //④保存Package_Bundle信息
                        result = KyDataOperation.SavePackageBundle(bundleId, packageId);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 与最后一个文件合并，若总数超过fileMaxRecord，则新建FSN文件
        /// </summary>
        /// <param name="fsnFile">fsn文件名 格式为：yyyyMddHHmmss_9K0001_Q.FSN(日期时间_机具编号_业务类型.FSN)</param>
        /// <param name="desFolder"> </param>
        /// <param name="fileMaxRecord">文件最大记录数</param>
        /// <param name="fsnFiles">分割后的文件名数组</param>
        public static List<string> MergeLastFile(byte[] fsnByte, string desFolder, int fileMaxRecord)
        {
            List<string> modifyFiles = new List<string>();
            long totalCount = (fsnByte.Length - 32) / 1644;
            long Left = totalCount;
            string[] fsnFiles = Directory.GetFiles(desFolder);
            string LastFile = string.Format("{0}\\{1}.FSN", desFolder, 0);
            int FileCount = fsnFiles.Length;
            if (fsnFiles.Length > 0)
                LastFile = string.Format("{0}\\{1}.FSN", desFolder, fsnFiles.Length - 1);
            else
            {
                using (FileStream fs = new FileStream(LastFile, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    fs.Write(fsnByte, 0, 32);
                    FileCount++;
                }
            }
            FileInfo fileInfo = new FileInfo(LastFile);
            long Cnt = (fileInfo.Length - 32) / 1644;
            long LastLestCnt = fileMaxRecord - Cnt;
            if (LastLestCnt > totalCount)
                LastLestCnt = totalCount;
            if (LastLestCnt > 0)
            {
                int count = Convert.ToInt32(LastLestCnt * 1644);
                using (FileStream fs = new FileStream(LastFile, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    fs.Write(fsnByte,32, count);
                }
                using (FileStream fs = new FileStream(LastFile, FileMode.Open, FileAccess.Write, FileShare.Read))
                {
                    byte[] tmp = BitConverter.GetBytes((Convert.ToInt32(LastLestCnt + Cnt)));
                    fs.Seek(20, SeekOrigin.Begin);
                    fs.Write(tmp, 0, tmp.Length);
                    fs.Close();
                }
                modifyFiles.Add(LastFile);
            }
            int offset = Convert.ToInt32(LastLestCnt * 1644 + 32);
            Left = totalCount - LastLestCnt;
            int fileCnt = Convert.ToInt32((totalCount - LastLestCnt + fileMaxRecord-1) / fileMaxRecord);
            if (fileCnt > 0)
            {
                string[] newFiles = new string[fileCnt];
                for (int i = 0; i < fileCnt; i++)
                {
                    LastFile = string.Format("{0}\\{1}.FSN", desFolder, i + FileCount);
                    int writeCount = fileMaxRecord;
                    if (Left < fileMaxRecord)
                        writeCount = Convert.ToInt32(Left);
                    using (FileStream fs = new FileStream(LastFile, FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        fs.Write(fsnByte, 0, 32);
                        fs.Write(fsnByte, offset, writeCount * 1644);
                    }
                    using (FileStream fs = new FileStream(LastFile, FileMode.Open, FileAccess.Write, FileShare.Read))
                    {
                        byte[] tmp = BitConverter.GetBytes((writeCount));
                        fs.Seek(20, SeekOrigin.Begin);
                        fs.Write(tmp, 0, tmp.Length);
                        fs.Close();
                    }
                    offset += writeCount * 1644;
                    Left -= writeCount;
                    modifyFiles.Add(LastFile);
                }
            }
            return modifyFiles;
        }
    }
}
