using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using KyModel;
using KyModel.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace KyBll
{
    public class FSNImport
    {
        /// <summary>
        /// 其他厂商FSN接入
        /// </summary>
        /// <param name="importDir"></param>
        /// <param name="pictureIp"></param>
        public static void FsnImport(string importDir,string pictureIp,MyTCP TCPEvent)
        {
            TCPEvent.OnFSNImportLog("FSN导入正在运行");
            string[] allFiles = Directory.GetFiles(importDir, "*",SearchOption.AllDirectories);
            List<string> dataFiles=new List<string>();
            foreach (var file in allFiles)
            {
                if(!file.Contains("ErrFiles"))
                    dataFiles.Add(file);

                    
            }
            for (int i = 0; i < dataFiles.Count; i++)
            {
                try
                {
                    string fileName = dataFiles[i];
                    FileInfo fi = new FileInfo(fileName);
                    if (fi.Extension != ".FSN" && fi.Extension != ".fsn")
                    {
                        TCPEvent.OnFSNImportLog(fi.FullName+"扩展名不是FSN或者fsn，移入ErrFiles");
                        MoveErrFile(importDir, fi.FullName);
                        continue;
                    }
                    else
                    {
                        DateTime LastWriteTime = fi.LastWriteTime;
                        DateTime timeNow = DateTime.Now;
                        TimeSpan sp = timeNow - LastWriteTime;
                        if (sp.TotalMinutes < 3)
                        {
                            continue;
                        }
                    }
                    string fsnFileName = Path.GetFileNameWithoutExtension(fileName);
                    string[] info = fsnFileName.Split("_".ToCharArray());
                    if (info.Length < 9)
                    {
                        MoveErrFile(importDir, fi.FullName);
                        TCPEvent.OnFSNImportLog(fi.Name + "文件格式不正确！");
                        continue;
                    }
                    int verStart = info[0].IndexOf("13");
                    if (verStart == -1)
                    {
                        MoveErrFile(importDir, fi.FullName);
                        TCPEvent.OnFSNImportLog(fi.Name + "的文件版本不正确！");
                        continue;
                    }
                    string currency = "", factory = "",
                           time = "", node = "", machineType = "", machineModel = "", machineNumber = "", user = "",
                           bussinessType = "", bussinessNumber = "", atmNumber = "", cashboxNumber = "";
                    currency = info[0].Substring(0, verStart).Trim();
                    factory = info[1].Trim();
                    time = info[2].Trim();
                    node = info[3].Trim();
                    machineType = info[4].Trim();
                    machineModel = info[5].Trim();
                    machineNumber = info[6].Trim();
                    user = info[7].Trim();
                    bussinessType = info[8].Trim();
                    int factoryId = KyDataOperation.GetFactoryId(factory);
                    if (factoryId == 0)
                    {
                        MoveErrFile(importDir, fi.FullName);
                        TCPEvent.OnFSNImportLog(fi.Name + "中的厂家简称不存在！");
                        continue;
                    }
                    int nodeId = KyDataOperation.GetNodeIdByNodeNumber(node);
                    if (nodeId == 0)
                    {
                        MoveErrFile(importDir, fi.FullName);
                        TCPEvent.OnFSNImportLog(fi.Name + "中的网点编号不存在！");
                        continue;
                    }
                    int userId = KyDataOperation.GetUserId(user);
                    int machineId = 0;
                    int machineId2 = 0;
                    if (machineModel == "0")
                    {
                        machineModel = "";
                    }
                    else
                    {
                        machineNumber = machineModel + machineNumber;
                    }
                    machineId = KyDataOperation.GetMachineIdByMachineNumber(machineNumber);
                    if (machineId == 0)//未在机具列表中找到该机具编号
                    {
                        //获取数据库内的上传文件的机具列表
                        machineId2 = KyDataOperation.GetMachineIdFromImportMachine(machineNumber);
                        if (machineId2 == 0)//未在上传文件的机具列表中找到该机具编号
                        {
                            MachineType m = 0;
                            try
                            {
                                m = (MachineType)Enum.Parse(typeof(MachineType), machineType, true);
                            }
                            catch (Exception ex)
                            {
                                MoveErrFile(importDir, fi.FullName);
                                TCPEvent.OnFSNImportLog(fi.Name + "的设备类别不存在！",ex);
                                continue;

                            }
                            ky_import_machine imort_machine = new ky_import_machine
                            {
                                kMachineModel = machineModel,
                                kMachineNumber = machineNumber,
                                kMachineType = (int)m,
                                kFactoryId = factoryId,
                                kNodeId = nodeId
                            };
                            int id = KyDataOperation.InsertMachineToImportMachine(imort_machine);
                            if (id > 0)
                                machineId2 = id;
                        }
                    }
                    BussinessType bussiness = BussinessType.HM;
                    bussiness = (BussinessType)Enum.Parse(typeof(BussinessType), bussinessType, true);
                    int atmId = 0, cashBoxId = 0;
                    if (bussiness == BussinessType.ATMP || bussiness == BussinessType.ATMQ)
                    {
                        if (info.Length == 11)
                        {
                            atmNumber = info[9].Trim();
                            cashboxNumber = info[10].Trim();
                            if (atmNumber != "0")
                            {
                                atmId = KyDataOperation.GetATMId(atmNumber, nodeId);
                                if (atmId == 0)
                                {
                                    ky_atm atm = new ky_atm
                                    {
                                        kATMNumber = atmNumber,
                                        kNodeId = nodeId,
                                        kStatus = 1
                                    };
                                    atmId = KyDataOperation.InsertATM(atm);
                                }
                            }
                            if (cashboxNumber != "0")
                            {
                                cashBoxId = KyDataOperation.GetCashBoxId(cashboxNumber, nodeId);
                                if (cashBoxId == 0)
                                {
                                    ky_cashbox cashbox = new ky_cashbox
                                    {
                                        kCashBoxNumber = cashboxNumber,
                                        kNodeId = nodeId
                                    };
                                    cashBoxId = KyDataOperation.InsertCashBox(cashbox);
                                }
                            }
                        }
                    }
                    if (bussiness == BussinessType.CACK || bussiness == BussinessType.CAQK
                    || bussiness == BussinessType.CK || bussiness == BussinessType.QK)
                    {
                        if (info.Length == 10)
                            bussinessNumber = info[9];
                    }
                    ky_machine machineTmp = new ky_machine();
                    machineTmp.kMachineNumber = machineNumber;
                    machineTmp.kNodeId = nodeId;
                    machineTmp.kFactoryId = factoryId;
                    machineTmp.business = bussiness;
                    machineTmp.kId = machineId;
                    machineTmp.atmId = atmId;
                    machineTmp.cashBoxId = cashBoxId;
                    machineTmp.userId = userId;
                    machineTmp.bussinessNumber = bussinessNumber;
                    machineTmp.imgServerId = KyDataOperation.GetPictureServerId(pictureIp);
                    machineTmp.importMachineId = machineId2;
                    long batchId = UploadFsn(fi.FullName, machineTmp);
                    if (batchId > 0)
                    {
                        File.Delete(fi.FullName);
                        TCPEvent.OnFSNImportLog(fi.Name + "导入成功！");
                    }
                    else
                    {
                        MoveErrFile(importDir, fi.FullName);
                        TCPEvent.OnFSNImportLog(fi.Name + "导入失败！");
                    }
                }
                catch (Exception ex)
                {
                    TCPEvent.OnFSNImportLog("FSN文件导入异常", ex);
                    continue;
                }
            }
            
        }
        /// <summary>
        /// 将异常文件移入ErrFiles目录
        /// </summary>
        /// <param name="importDir"></param>
        /// <param name="sourceFile"></param>
        public static void MoveErrFile(string importDir, string sourceFile)
        {
            try
            {
                string errDir = importDir + "/ErrFiles";
                if (!Directory.Exists(errDir))
                    Directory.CreateDirectory(errDir);
                string destFile = errDir + "/" + Path.GetFileName(sourceFile);
                if (!File.Exists(destFile))
                    File.Move(sourceFile, destFile);
                else
                {
                    string[] errFiles=Directory.GetFiles(errDir);
                    int jj = 0;
                    string fileNameWithoutEx = Path.GetFileNameWithoutExtension(sourceFile);
                    string fileEx = Path.GetExtension(sourceFile);
                    string file = "";
                    do
                    {
                        file = string.Format("{0}\\{1}({2}){3}", errDir, fileNameWithoutEx, jj, fileEx);
                        jj++;
                    } while (File.Exists(file));
                    File.Move(sourceFile, file);
                }
                    
            }
            catch (Exception ex)
            {
                throw ex ;
            }
        }
        /// <summary>
        /// 保存FSN文件到数据库中 并返回批次id
        /// </summary>
        /// <param name="fsnName"></param>
        /// <param name="pictureServerId"></param>
        /// <param name="machine"></param>
        /// <param name="isBusinessControl"></param>
        /// <param name="machineNumber"></param>
        /// <param name="machineModel"> </param>
        public static long SaveFsn(string fsnName, ky_machine machine)
        {
            long batchId = KyDataLayer2.GuidToLongID();
            List<KYDataLayer1.SignTypeL2> signs = new List<KYDataLayer1.SignTypeL2>();
            ky_batch batch = GenerateBatchFromFsn(batchId, fsnName, machine, out signs);
            bool result=KyDataOperation.InsertSignBatch(batch);
            if (result)
            {
                for (int i = 0; i < signs.Count; )
                {
                    List<KYDataLayer1.SignTypeL2> splitSigns = signs.Skip(i).Take(10000).ToList();
                    result = KyDataOperation.InsertSign(batchId, i, splitSigns);
                    i += 10000;
                }
            }
            //保存批次
            if (result)
                return batchId;
            else
                return 0;
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
        public static ky_batch GenerateBatchFromFsn(long batchId, string fileName, ky_machine machine,out List<KYDataLayer1.SignTypeL2> allSigns)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            long fileLenght = fileInfo.Length;
            int signCount = Convert.ToInt32((fileLenght - 32) / 1644);
            ky_batch batch = new ky_batch();
            allSigns = new List<KYDataLayer1.SignTypeL2>();
            if (signCount > 0 && KyDataLayer2.IsCorrectFileFormat(fileName, ".FSN"))
            {
                int pages = (signCount + 999) / 1000;
                int totalPage, cc;
                int date = 0;
                int totalCount = 0, totalValue = 0;
                for (int i = 0; i < pages; i++)
                {
                    List<KYDataLayer1.SignTypeL2> signs = new List<KYDataLayer1.SignTypeL2>();
                    signs = KyDataLayer2.ReadFromFSNInPage(fileName, i + 1, 1000, out totalPage, out cc);
                    allSigns.AddRange(signs);
                    //result = KyDataOperation.InsertSign(batchId, i * 1000, signs);
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
                    ktype = machine.business.ToString(),
                    kdate = date,
                    knode = machine.kNodeId,
                    kfactory = machine.kFactoryId,
                    kimgserver = machine.imgServerId,
                    ktotalnumber = totalCount,
                    ktotalvalue = totalValue,
                    kuser = machine.userId,
                };
                if (machine.business == BussinessType.HM)
                {
                    batch.ktype = BussinessType.HM.ToString();
                    batch.kuser = 0;
                }

                batch.kmachine = machine.kId.ToString();
                JObject jo = new JObject();
                if (batch.ktype != BussinessType.HM.ToString())
                {
                    if ((batch.ktype == BussinessType.ATMP.ToString() || batch.ktype == BussinessType.ATMQ.ToString()))
                    {
                        jo["katm"] = machine.atmId;
                        jo["kcashbox"] = machine.cashBoxId;
                    }
                    if (batch.ktype == BussinessType.QK.ToString() || batch.ktype == BussinessType.CK.ToString()
                        || batch.ktype == BussinessType.CACK.ToString() || batch.ktype == BussinessType.CAQK.ToString())
                        jo["kbussinessnumber"] = machine.bussinessNumber;

                }
                if (machine.kId == 0)
                {
                    jo["kmachine"] = machine.importMachineId;
                }
                batch.hjson = JsonConvert.SerializeObject(jo);
            }
            return batch;
        }

        /// <summary>
        /// 手动上传FSN文件到数据库
        /// </summary>
        /// <param name="fsnName"></param>
        /// <param name="pictureServerId"></param>
        /// <param name="machine"></param>
        /// <param name="machineId2"></param>
        /// <returns></returns>
        public static long UploadFsn(string fileName, ky_machine machine)
        {
            bool result = false;
            long batchId = KyDataLayer2.GuidToLongID();
            List<KYDataLayer1.SignTypeL2> signs = new List<KYDataLayer1.SignTypeL2>();
            ky_batch batch = GenerateBatchFromFsn(batchId, fileName, machine, out signs);
            //保存批次
            result = KyDataOperation.InsertSignBatch(batch);
            if (result)
            {
                for(int i=0;i<signs.Count;)
                {
                    List<KYDataLayer1.SignTypeL2> splitSigns = signs.Skip(i).Take(10000).ToList();
                    result = KyDataOperation.InsertSign(batchId, i, splitSigns);
                    i += 10000;
                }
            }
            //更新冠字号码文件上传表ky_import_file
            //KyDataOperation.
            if (result)
            {
                fileName = Path.GetFileName(fileName);
                result = KyDataOperation.InsertImportFile(Convert.ToInt64(batchId), fileName, DateTime.Now, machine.business.ToString(), machine.kNodeId);
                Log.ImportLog(fileName + "的冠字号码时间是" + DateTimeAndTimeStamp.GetTime(batch.kdate.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
                batchId = 0;
            return batchId;
        }
    }
}
