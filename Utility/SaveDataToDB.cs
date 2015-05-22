using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using KyData;
using KyData.DataBase;
using KyData.DbTable;
using KyModel;
namespace Utility
{
    
    public class SaveDataToDB
    {
        public static string CharTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 保存FSN文件到数据库中
        /// </summary>
        /// <param name="fsnName"></param>
        /// <param name="pictureServerId"></param>
        /// <param name="machine"></param>
        /// <param name="isBusinessControl"></param>
        /// <param name="machineNumber"></param>
        /// <param name="machineModel"> </param>
        public static void SaveFsn(string fsnName,int pictureServerId,ky_machine machine,bool isBusinessControl)
        {
            try
            {
                //machineNumber = "";
                //machineModel = "";
                FileInfo fileInfo = new FileInfo(fsnName);
                long fileLenght = fileInfo.Length;
                int signCount =Convert.ToInt32((fileLenght - 32) / 1644);
                if (signCount > 0 && KyDataLayer2.IsCorrectFileFormat(fsnName, ".FSN"))
                {
                    int pages = (signCount + 999) / 1000;
                    int totalPage, cc;
                    //批次号
                    Int64 batchId = KyDataLayer2.GuidToLongID();
                    int date = 0;
                    int totalCount = 0, totalValue = 0;
                    for (int i = 0; i < pages; i++)
                    {
                        List<KYDataLayer1.SignTypeL2> signs = new List<KYDataLayer1.SignTypeL2>();
                        signs = KyDataLayer2.ReadFromFSNInPage(fsnName, i + 1, 1000, out totalPage, out cc);
                        //替换不合法字符为‘#’
                        for (int jj = 0; jj < signs.Count; jj++)
                        {
                            string str = "";
                            string strSign = signs[jj].Sign.Trim();
                            for (int kk = 0; kk < strSign.Length; kk++)
                            {
                                if (CharTable.IndexOf(strSign[kk]) == -1)
                                {
                                    str += "_";
                                }
                                else
                                {
                                    str += strSign[kk];
                                }
                            }
                            signs[jj].Sign = str;
                        }
                        bool result = KyDataOperation.InsertSign(batchId, i * 1000, signs);
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
                    ky_batch_sphinx batch = new ky_batch_sphinx()
                    {
                        id = batchId,
                        type = machine.business,
                        date = date,
                        node = (uint)machine.kNodeId,
                        factory = (uint)machine.kFactoryId,
                        imgipaddress = (uint)pictureServerId,
                        totalnumber = (uint)totalCount,
                        totalvalue = (uint)totalValue,
                        recorduser = (uint)machine.userId,
                    };
                    if (machine.business == null || machine.business == "" || !isBusinessControl)
                    {
                        batch.type = "HM";
                        batch.recorduser = 0;
                    }
                    batch.machine = new uint[1];
                    batch.machine[0] = (uint)machine.kId;
                    string hjson = "";
                    if (machine.bussinessNumber != "")
                    {
                        hjson = string.Format("\"kbussinessNumber\":{0}", machine.bussinessNumber);
                    }
                    if ((batch.type == "ATMP" || batch.type == "ATMC" || batch.type == "CAQK" || batch.type == "CACK"))
                    {
                        if (hjson == "")
                            batch.hjson = string.Format("{0}\"katm\":{1},\"kcashbox\":{2}{3}", "{", machine.atmId,
                                                        machine.cashBoxId, "}");
                        else
                            batch.hjson = string.Format("{0}{1},\"katm\":{2},\"kcashbox\":{3}{4}", "{", hjson, machine.atmId,
                                                        machine.cashBoxId, "}");
                    }
                    else
                    {
                        if (hjson == "")
                            batch.hjson = "";
                        else
                            batch.hjson = string.Format("{0}{1}{2}", "{", hjson, "}");
                    }
                    //保存批次
                    KyDataOperation.InsertSignBatch(batch);
                    ////机器最后上传时间和机具编号
                    //if (machine.machineNumber != machineNumber && machineNumber != "" || machine.machineModel != machineModel && machineModel!="")
                    //    KyDataOperation.UpdateMachine(machine.ipAddress, machineNumber,machineModel);
                    //else
                    //    KyDataOperation.UpdateMachine(machine.ipAddress, "","");

                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        
        /// <summary>
        /// 手动上传FSN文件到数据库
        /// </summary>
        /// <param name="fsnName"></param>
        /// <param name="pictureServerId"></param>
        /// <param name="machine"></param>
        /// <param name="machineId2"></param>
        /// <returns></returns>
        public static bool UploadFsn(string fsnName, int pictureServerId, machineData machine,int machineId2)
        {
            bool result = false;

            //machineNumner = "";
            FileInfo fileInfo = new FileInfo(fsnName);
            long fileLenght = fileInfo.Length;
            int signCount = Convert.ToInt32((fileLenght - 32) / 1644);
            if (signCount > 0 && KyDataLayer2.IsCorrectFileFormat(fsnName, ".FSN"))
            {
                int pages = (signCount + 999) / 1000;
                int totalPage, cc;
                //批次号
                Int64 batchId = KyDataLayer2.GuidToLongID();
                int date = 0;
                int totalCount = 0, totalValue = 0;
                for (int i = 0; i < pages; i++)
                {
                    List<KYDataLayer1.SignTypeL2> signs = new List<KYDataLayer1.SignTypeL2>();
                    signs = KyDataLayer2.ReadFromFSNInPage(fsnName, i + 1, 1000, out totalPage, out cc);
                    //替换不合法字符为‘#’
                    for (int jj = 0; jj < signs.Count; jj++)
                    {
                        string str = "";
                        string strSign = signs[jj].Sign.Trim();
                        for (int kk = 0; kk < strSign.Length; kk++)
                        {
                            if (CharTable.IndexOf(strSign[kk]) == -1)
                            {
                                str += "_";
                            }
                            else
                            {
                                str += strSign[kk];
                            }
                        }
                        signs[jj].Sign = str;
                    }
                    result = KyDataOperation.InsertSign(batchId,i*1000, signs);
                    if (i == 0)
                    {
                        date = DateTimeAndTimeStamp.ConvertDateTimeInt(signs[0].Date);
                        //machineNumner = signs[0].MachineMac;
                    }
                    totalCount += signs.Count;
                    foreach (var v in signs)
                    {
                        totalValue += Convert.ToInt32(v.Value);
                    }
                }
                ky_batch_sphinx batch = new ky_batch_sphinx()
                {
                    id = batchId,
                    type = machine.business,
                    date = date,
                    node = (uint)machine.nodeId,
                    factory = (uint)machine.factoryId,
                    imgipaddress = (uint)pictureServerId,
                    totalnumber = (uint)totalCount,
                    totalvalue = (uint)totalValue,
                    recorduser = (uint)machine.userId,
                };
                if (machine.business == null || machine.business == "")
                {
                    batch.type = "HM";
                }
                batch.machine = new uint[1];
                batch.machine[0] = (uint)machine.id;
                if (machine.id != 0)
                {
                    if (machine.startBusinessCtl && (batch.type == "ATM" || batch.type == "CAQK" || batch.type == "CACK"))
                    {
                        batch.hjson = String.Format("{0}\"katm\":[{1}],\"kcashbox\":[{2}]{3}", "{", machine.atmId,
                                                    machine.cashBoxId, "}");
                    }
                }
                else
                {
                    if (machine.startBusinessCtl && (batch.type == "ATM" || batch.type == "CAQK" || batch.type == "CACK"))
                    {
                        batch.hjson = String.Format("{0}\"katm\":{1},\"kcashbox\":{2},\"kmachine\":{3}{4}", "{", machine.atmId,
                                                    machine.cashBoxId,machineId2, "}");
                    }
                    else
                    {
                        batch.hjson = String.Format("{0}\"kmachine\":{1}{2}", "{", machineId2, "}");
                    }
                }

                //保存批次
                if (result)
                    result=KyDataOperation.InsertSignBatch(batch);
                //更新冠字号码文件上传表ky_import_file
                //KyDataOperation.
                if(result)
                {
                    string fileName = Path.GetFileName(fsnName);
                    result = KyDataOperation.InsertImportFile(Convert.ToInt64(batchId), fileName, DateTime.Now, machine.business, machine.nodeId);
                }
            }
            return result;
        }

        /// <summary>
        /// 保存FSN文件，返回BatchId（批次号）
        /// </summary>
        /// <param name="fsnName"></param>
        /// <param name="pictureServerId"></param>
        /// <param name="machine"></param>
        /// <param name="machineId2"></param>
        /// <returns></returns>
        public static long UploadFsnBatchId(string fsnName, int pictureServerId, ky_machine machine, int machineId2)
        {
            bool result = false;
            Int64 returnId = 0; 

            //machineNumner = "";
            FileInfo fileInfo = new FileInfo(fsnName);
            long fileLenght = fileInfo.Length;
            int signCount = Convert.ToInt32((fileLenght - 32) / 1644);
            if (signCount > 0 && KyDataLayer2.IsCorrectFileFormat(fsnName, ".FSN"))
            {
                int pages = (signCount + 999) / 1000;
                int totalPage, cc;
                //批次号
                Int64 batchId = KyDataLayer2.GuidToLongID();
                returnId = batchId;
                int date = 0;
                int totalCount = 0, totalValue = 0;
                for (int i = 0; i < pages; i++)
                {
                    List<KYDataLayer1.SignTypeL2> signs = new List<KYDataLayer1.SignTypeL2>();
                    signs = KyDataLayer2.ReadFromFSNInPage(fsnName, i + 1, 1000, out totalPage, out cc);
                    //替换不合法字符为‘#’
                    for (int jj = 0; jj < signs.Count; jj++)
                    {
                        string str = "";
                        string strSign = signs[jj].Sign.Trim();
                        for (int kk = 0; kk < strSign.Length; kk++)
                        {
                            if (CharTable.IndexOf(strSign[kk]) == -1)
                            {
                                str += "_";
                            }
                            else
                            {
                                str += strSign[kk];
                            }
                        }
                        signs[jj].Sign = str;
                    }
                    result = KyDataOperation.InsertSign(batchId, i * 1000, signs);
                    if (i == 0)
                    {
                        date = DateTimeAndTimeStamp.ConvertDateTimeInt(signs[0].Date);
                        //machineNumner = signs[0].MachineMac;
                    }
                    totalCount += signs.Count;
                    foreach (var v in signs)
                    {
                        totalValue += Convert.ToInt32(v.Value);
                    }
                }
                ky_batch_sphinx batch = new ky_batch_sphinx()
                {
                    id = batchId,
                    type = machine.business,
                    date = date,
                    node = (uint)machine.kNodeId,
                    factory = (uint)machine.kFactoryId,
                    imgipaddress = (uint)pictureServerId,
                    totalnumber = (uint)totalCount,
                    totalvalue = (uint)totalValue,
                    recorduser = (uint)machine.userId,
                };
                if (machine.business == null || machine.business == "")
                {
                    batch.type = "HM";
                }
                batch.machine = new uint[1];
                batch.machine[0] = (uint)machine.kId;
                if (machine.kId != 0)
                {
                    if (machine.startBusinessCtl && (batch.type == "ATM" || batch.type == "CAQK" || batch.type == "CACK"))
                    {
                        batch.hjson = String.Format("{0}\"katm\":[{1}],\"kcashbox\":[{2}]{3}", "{", machine.atmId,
                                                    machine.cashBoxId, "}");
                    }
                    else
                    {
                        batch.hjson = "";
                    }
                }
                else
                {
                    if (machine.startBusinessCtl && (batch.type == "ATM" || batch.type == "CAQK" || batch.type == "CACK"))
                    {
                        batch.hjson = String.Format("{0}\"katm\":{1},\"kcashbox\":{2},\"kmachine\":{3}{4}", "{", machine.atmId,
                                                    machine.cashBoxId, machineId2, "}");
                    }
                    else
                    {
                        batch.hjson = String.Format("{0}\"kmachine\":{1}{2}", "{", machineId2, "}");
                    }
                }

                //保存批次
                if (result)
                    result = KyDataOperation.InsertSignBatch(batch);
                //更新冠字号码文件上传表ky_import_file
                //KyDataOperation.
                if (result)
                {
                    string fileName = Path.GetFileName(fsnName);
                    result = KyDataOperation.InsertImportFile(Convert.ToInt64(batchId), fileName, DateTime.Now, machine.business, machine.kNodeId);
                }
            }
            if (result)
                return returnId;
            else
            {
                return 0;
            }
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
        public static int SaveGzhBundle(string bundleNumber,long batchId,string fileName)
        {
            bool result = KyDataOperation.InsertGzhBundle(bundleNumber, batchId, fileName);
            if(result)
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
        public static long UploadFsn(string file,int pictureServerId,ky_gzh_package gzhLayer2,ky_machine machineData)
        {
            //获取FSN文件内的机具编号
            string machineModel = "";
            string[] str = KyData.KyDataLayer2.GetMachineNumberFromFSN(file,out machineModel).Split("/".ToCharArray());
            string machineMac = "";
            string factory = "";
            if(str.Length==3)
            {
                factory = str[1];
                machineMac = str[2];
            }
            //获取厂家ID
            int factoryId = KyDataOperation.GetFactoryId(factory);
            if(factoryId==0)//厂家编号不存在
            {
                bool result = KyDataOperation.InsertFactory("", factory);
                if(result)
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
                    int id = Utility.KyDataOperation.InsertMachineToImportMachine(machineMac, gzhLayer2.kNodeId, factoryId);
                    if (id>0)
                        machineId2 = id;
                }
            }

            machineData.kMachineNumber = machineMac;                 //机具编号
            machineData.kNodeId = gzhLayer2.kNodeId;                  //网点Id
            machineData.kFactoryId = factoryId;                      //厂家Id
            machineData.business = "KHDK";                          //业务类型
            machineData.kId = machineId;                             //机具Id
            return UploadFsnBatchId(file, pictureServerId, machineData, machineId2);
        }

        /// <summary>
        /// 保存GZH文件到数据库中,目标文件夹内包含一包的钞票信息（即一个GZH文件和多个FSN文件）
        /// </summary>
        /// <param name="gzhDirectory">gzh文件夹（包含一个GZH文件和多个FSN文件）</param>
        /// <param name="pictureServerId"> 图片服务器Id</param>
        /// <param name="userId">用户Id </param>
        /// <returns></returns>
        public static bool UploadGzhPackage(string gzhDirectory,int pictureServerId,int userId)
        {
            bool result = false;
            //获取后缀名为GZH、gzh的文件
            string[] Gzhfiles = Directory.GetFiles(gzhDirectory, "*.GZH");
            //获取后缀名为FSN、fsn的文件
            string[] FsnFiles = Directory.GetFiles(gzhDirectory, "*.FSN");
            ky_gzh_package gzhLayer2 = new ky_gzh_package();
            
            if(Gzhfiles.Length==1) //表示只有一个GZH文件
            {
                //读取GZH文件信息
                gzhLayer2 = Gzh(KyDataLayer2.ReadGzh(Gzhfiles[0]));
                if (gzhLayer2.kFSNNumber == FsnFiles.Length)//表示GZH文件中的记录数=实际的FSN文件的总数
                {
                    //①保存GzhPackage信息，并获得packageId
                    //获取总张数，总金额
                    int TotalValue = 0, TotalNumber = 0;
                    for(int i=0;i<FsnFiles.Length;i++)
                    {
                        KYDataLayer1.Amount amount;
                        KyDataLayer2.GetTotalValueFromFSN(FsnFiles[i], out amount);
                        TotalValue += amount.TotalValue;
                        TotalNumber += amount.TotalCnt;
                    }
                    gzhLayer2.kTotalNumber = TotalNumber;
                    gzhLayer2.kTotalValue = TotalValue;
                    gzhLayer2.kUserId = userId;
                    int packageId = KyDataOperation.InsertGzhPackage(gzhLayer2);

                    for(int i=0;i<FsnFiles.Length;i++)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(FsnFiles[i]);
                        string[] str = fileName.Split("_".ToCharArray());
                        if(str.Length==2)
                        {
                            string bundleNumber = str[1];
                            ky_machine machine = new ky_machine()
                                                      {
                                                          userId = userId,
                                                          startBusinessCtl=false,
                                                      };
                            //②保存FSN文件，获得BatchId
                            long batchId = UploadFsn(FsnFiles[i], pictureServerId, gzhLayer2, machine);
                            //③保存GzhBundle，获得BundleId
                            int bundleId = SaveGzhBundle(bundleNumber, batchId, fileName);
                            //④保存Package_Bundle信息
                            result= KyDataOperation.SavePackageBundle(bundleId, packageId);
                        }
                    }
                }
            }
            return result;
        }



        ///// <summary>
        ///// 保存GZH文件到数据库中,目标文件夹内包含一包的钞票信息（即一个GZH文件和多个FSN文件）
        ///// </summary>
        ///// <param name="gzhDirectory">gzh文件夹（包含一个GZH文件和多个FSN文件）</param>
        ///// <param name="pictureServerId"> 图片服务器Id</param>
        ///// <param name="userId">用户Id </param>
        ///// <returns></returns>
        ////--------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gzhDirectory"></param>
        /// <param name="isClearCenter"></param>
        /// <param name="packageNumber"></param>
        /// <param name="pictureServerId"></param>
        /// <param name="userId"></param>
        /// <param name="nodeId"></param>
        /// <param name="startIndex"></param>
        /// <param name="fileTime"></param>
        /// <returns></returns>
        public static bool SaveKHDK(string gzhDirectory,string isClearCenter,string packageNumber, int pictureServerId, int userId,int nodeId,int startIndex,DateTime fileTime)
        {
            bool result = false;
            //获取后缀名为FSN、fsn的文件
            string[] FsnFiles = Directory.GetFiles(gzhDirectory, "*.FSN");
            ky_gzh_package gzhLayer2 = new ky_gzh_package();
            
            //赋值
            gzhLayer2.kDate = fileTime;
            gzhLayer2.kBranchId = KyDataOperation.GetBranchIdWithNodeId(nodeId);
            gzhLayer2.kNodeId = nodeId;
            gzhLayer2.kType = 3;
            gzhLayer2.kFSNNumber = FsnFiles.Length;
            gzhLayer2.kCashCenter = isClearCenter;
            gzhLayer2.kVersion = "1";
            gzhLayer2.kPackageNumber = packageNumber;
            gzhLayer2.kCurrency = "CNY";

            if (gzhLayer2.kFSNNumber == FsnFiles.Length)//表示GZH文件中的记录数=实际的FSN文件的总数
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
                gzhLayer2.kTotalNumber = TotalNumber;
                gzhLayer2.kTotalValue = TotalValue;
                gzhLayer2.kUserId = userId;
                int packageId = KyDataOperation.InsertGzhPackage(gzhLayer2);

                for (int i = 0; i < FsnFiles.Length; i++)
                {
                    string bundleNumber = string.Format("{0}{1,4}", fileTime.ToString("yyMMdd"), startIndex + i).Replace(" ","0");
                    ky_machine machine = new ky_machine()
                    {
                        userId = userId,
                        startBusinessCtl = false,
                    };
                    //②保存FSN文件，获得BatchId
                    long batchId = UploadFsn(FsnFiles[i], pictureServerId, gzhLayer2, machine);
                    //③保存GzhBundle，获得BundleId
                    int bundleId = SaveGzhBundle(bundleNumber, batchId, "");
                    //④保存Package_Bundle信息
                    result = KyDataOperation.SavePackageBundle(bundleId, packageId);
                }
            }
            return result;
        }
    }
}
