using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using KyData;
using KyData.DataBase;
using KyData.DbTable;
using KyModel;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
namespace Utility
{

    public class SaveDataToDB
    {

        /// <summary>
        /// 保存FSN文件到数据库中
        /// </summary>
        /// <param name="fsnName"></param>
        /// <param name="pictureServerId"></param>
        /// <param name="machine"></param>
        /// <param name="isBusinessControl"></param>
        /// <param name="machineNumber"></param>
        /// <param name="machineModel"> </param>
        public static void SaveFsn(string fsnName, ky_machine machine)
        {
            long batchId = KyDataLayer2.GuidToLongID();
            bool result = false;
            ky_batch batch = GenerateBatchFromFsn(batchId, fsnName, machine, out result);
            //保存批次
            KyDataOperation.InsertSignBatch(batch);
        }


        /// <summary>
        /// 手动上传FSN文件到数据库
        /// </summary>
        /// <param name="fsnName"></param>
        /// <param name="pictureServerId"></param>
        /// <param name="machine"></param>
        /// <param name="machineId2"></param>
        /// <returns></returns>
        public static bool UploadFsn(string fileName, ky_machine machine)
        {
            bool result = false;
            long batchId = KyDataLayer2.GuidToLongID();
            ky_batch batch = GenerateBatchFromFsn(batchId, fileName, machine, out result);
            //保存批次
            if (result)
                result = KyDataOperation.InsertSignBatch(batch);
            //更新冠字号码文件上传表ky_import_file
            //KyDataOperation.
            if (result)
            {
                fileName = Path.GetFileName(fileName);
                result = KyDataOperation.InsertImportFile(Convert.ToInt64(batchId), fileName, DateTime.Now, machine.business, machine.kNodeId);
            }
            return result;
        }

        /// <summary>
        /// 转换Gzh到ky_gzh_package
        /// </summary>
        /// <param name="gzh"></param>
        /// <returns></returns>
        public static ky_gzh_package Gzh(KYDataLayer1.Gzh gzh)
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
            gzhLayer2.kNodeId = KyDataOperation.GetNodeId(gzh.nodeNumber);
            return gzhLayer2;
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
        /// 上传FSN文件，并返回批次号
        /// </summary>
        /// <param name="file"></param>
        /// <param name="pictureServerId"></param>
        /// <param name="gzhLayer2"> </param>
        /// <param name="machineData"> </param>
        /// <returns></returns>
        public static long UploadFsn(string file, ky_gzh_package gzh_package, ky_machine fsnMachine)
        {
            //获取FSN文件内的机具编号
            string machineModel = "";
            string[] str = KyData.KyDataLayer2.GetMachineNumberFromFSN(file, out machineModel).Split("/".ToCharArray());
            string machineMac = "";
            string factory = "";
            bool result=false;
            if (str.Length == 3)
            {
                factory = str[1];
                machineMac = str[2];
            }
            //获取厂家ID
            int factoryId = KyDataOperation.GetFactoryId(factory);
            if (factoryId == 0)//厂家编号不存在
            {
                result = KyDataOperation.InsertFactory("", factory);
                if (result)
                {
                    factoryId = KyDataOperation.GetFactoryId(factory);
                }
            }

            int machineId = 0;
            int machineId2 = 0;
            machineId = Utility.KyDataOperation.GetMachineId(machineMac);
            if (machineId == 0)//未在机具列表中找到该机具编号
            {
                //获取数据库内的上传文件的机具列表
                machineId2 = Utility.KyDataOperation.GetMachineIdFromImportMachine(machineMac);
                if (machineId2 == 0)//未在上传文件的机具列表中找到该机具编号
                {
                    int id = Utility.KyDataOperation.InsertMachineToImportMachine(machineMac, gzh_package.kNodeId, factoryId);
                    if (id > 0)
                        machineId2 = id;
                }
            }

            fsnMachine.kMachineNumber = machineMac;
            fsnMachine.kNodeId = gzh_package.kNodeId;          
            fsnMachine.kFactoryId = factoryId;                 
            fsnMachine.business = "KHDK";                      
            fsnMachine.kId = machineId;
            fsnMachine.importMachineId = machineId2;


            result = false;

            long batchId = KyDataLayer2.GuidToLongID();
            ky_batch batch = GenerateBatchFromFsn(batchId, file, fsnMachine, out result);
            //保存批次
            if (result)
            {
                result = KyDataOperation.InsertSignBatch(batch);
                string fileName = Path.GetFileName(file);
                result = KyDataOperation.InsertImportFile(Convert.ToInt64(batchId), fileName, DateTime.Now, fsnMachine.business, fsnMachine.kNodeId);
                return batchId;
            }
            else
                return 0;
        }

        /// <summary>
        /// 保存GZH文件到数据库中,目标文件夹内包含一包的钞票信息（即一个GZH文件和多个FSN文件）
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
                gzh_package = Gzh(KyDataLayer2.ReadGzh(Gzhfiles[0]));
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
                            ky_machine machine = new ky_machine()
                                                      {
                                                          userId = userId,
                                                          startBusinessCtl = false,
                                                          imgServerId = pictureServerId
                                                      };
                            //②保存FSN文件，获得BatchId
                            long batchId = UploadFsn(FsnFiles[i], gzh_package, machine);
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
        /// 保存GZH文件到数据库中,目标文件夹内包含一包的钞票信息（即一个GZH文件和多个FSN文件）
        /// </summary>
        /// <param name="gzhDirectory">gzh文件夹（包含一个GZH文件和多个FSN文件）</param>
        /// <param name="machine"></param>
        /// <returns></returns>
        public static bool SaveKHDK(string gzhDirectory,ky_machine machine)
        {
            bool result = false;
            int startIndex = 1;
            DateTime fileTime = DateTime.Now;
            //获取后缀名为FSN、fsn的文件
            string[] FsnFiles = Directory.GetFiles(gzhDirectory, "*.FSN");
            ky_gzh_package gzh_package = new ky_gzh_package();

            //赋值
            gzh_package.kDate = fileTime;
            gzh_package.kBranchId = KyDataOperation.GetBranchIdWithNodeId(machine.kNodeId);
            gzh_package.kNodeId = machine.kNodeId;
            gzh_package.kType = 3;
            gzh_package.kFSNNumber = FsnFiles.Length;
            gzh_package.kCashCenter = machine.isClearCenter;
            gzh_package.kVersion = "1";
            gzh_package.kPackageNumber = machine.packageNumber;
            gzh_package.kCurrency = "CNY";

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
                gzh_package.kUserId = machine.userId;
                int packageId = KyDataOperation.InsertGzhPackage(gzh_package);

                for (int i = 0; i < FsnFiles.Length; i++)
                {
                    string bundleNumber = string.Format("{0}{1,4}", fileTime.ToString("yyMMdd"), startIndex + i).Replace(" ", "0");
                    ky_machine fsnMachine = new ky_machine()
                    {
                        userId = machine.userId,
                        startBusinessCtl = false,
                        imgServerId=machine.imgServerId
                    };
                    //②保存FSN文件，获得BatchId
                    long batchId = UploadFsn(FsnFiles[i], gzh_package, fsnMachine);
                    //③保存GzhBundle，获得BundleId
                    int bundleId = SaveGzhBundle(bundleNumber, batchId, "");
                    //④保存Package_Bundle信息
                    result = KyDataOperation.SavePackageBundle(bundleId, packageId);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据FSN文件生成批次信息
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="fileName"></param>
        /// <param name="machine"></param>
        /// <param name="imgServerId"></param>
        /// <param name="importMachineId"></param>
        /// <returns></returns>
        public static ky_batch GenerateBatchFromFsn(long batchId, string fileName, ky_machine machine, out bool result)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            long fileLenght = fileInfo.Length;
            int signCount = Convert.ToInt32((fileLenght - 32) / 1644);
            ky_batch batch = new ky_batch();
            result = false;
            if (signCount > 0 && KyDataLayer2.IsCorrectFileFormat(fileName, ".FSN"))
            {
                int pages = (signCount + 999) / 1000;
                int totalPage, cc;
                //批次号

                int date = 0;
                int totalCount = 0, totalValue = 0;
                for (int i = 0; i < pages; i++)
                {
                    List<KYDataLayer1.SignTypeL2> signs = new List<KYDataLayer1.SignTypeL2>();
                    signs = KyDataLayer2.ReadFromFSNInPage(fileName, i + 1, 1000, out totalPage, out cc);
                    result = KyDataOperation.InsertSign(batchId, i * 1000, signs);
                    if (i == 0)
                    {
                        date = DateTimeAndTimeStamp.ConvertDateTimeInt(signs[0].Date);
                    }
                    totalCount += signs.Count;
                    foreach (var v in signs)
                    {
                        totalValue += Convert.ToInt32(v.Value);
                    }
                }
                batch = new ky_batch()
                {
                    id = batchId,
                    ktype = machine.business,
                    kdate = date,
                    knode = machine.kNodeId,
                    kfactory = machine.kFactoryId,
                    kimgserver = machine.imgServerId,
                    ktotalnumber = totalCount,
                    ktotalvalue = totalValue,
                    kuser = machine.userId,
                };
                if (machine.business == null || machine.business == "")
                {
                    batch.ktype = "HM";
                    if (machine.business == "")
                    {
                        batch.kuser = 0;
                    }
                }

                //batch.kmachine = new long[] { 1, 2 };
                //batch.kmachine[0] = machine.kId;
                JObject jo = new JObject();
                if (machine.startBusinessCtl)
                {
                    if ((batch.ktype == "ATMP" ||batch.ktype == "ATMQ"|| batch.ktype == "CAQK" || batch.ktype == "CACK"))
                        jo["katm"] = machine.atmId;
                    if (batch.ktype == "QK" || batch.ktype == "CK")
                        jo["kbussinessNumber"] = machine.bussinessNumber;
                    if(batch.ktype=="ATMP")
                        jo["kcashbox"] = machine.cashBoxId;
                }
                if (machine.kId == 0)
                {
                    jo["kmachine"] = machine.importMachineId;
                }
                batch.hjson = JsonConvert.SerializeObject(jo);
            }
            return batch;
        }
    }
}
