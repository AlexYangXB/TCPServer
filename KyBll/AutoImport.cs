using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using KyModel.Models;
using KyModel;
namespace KyBll
{
    public class AutoImport
    {
        public static void FsnImport(string importDir,string pictureIp)
        {
            string[] dataFiles = Directory.GetFiles(importDir);
            for (int i = 0; i < dataFiles.Length; i++)
            {
                string fileName=dataFiles[i];
                FileInfo fi = new FileInfo(fileName);
                if (fi.Extension != ".FSN")
                {
                    MoveErrFile(importDir, fi.Name);
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
                if(info.Length<9)
                {
                    MoveErrFile(importDir, fi.Name);
                    Log.ImportLog(null, fi.Name + "文件格式不正确！");
                    continue;
                }
                int verStart=info[0].IndexOf("13");
                if (verStart == -1)
                {
                    MoveErrFile(importDir, fi.Name);
                    Log.ImportLog(null, fi.Name + "的文件版本不正确！");
                    continue;
                }
                string currency = "",factory = "",
                       time = "",node = "",machineType = "",machineModel="",machineNumber = "",user = "",
                       bussinessType="",bussinessNumber="",atmNumber="",cashboxNumber="";
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
                    MoveErrFile(importDir, fi.Name);
                    Log.ImportLog(null, fi.Name + "中的厂家简称不存在！");
                    continue;
                }
                int nodeId = KyDataOperation.GetNodeId(node);
                if(nodeId==0)
                {
                    MoveErrFile(importDir, fi.Name);
                    Log.ImportLog(null, fi.Name + "中的网点编号不存在！");
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
                machineId = KyDataOperation.GetMachineId(machineNumber);
                if (machineId == 0)//未在机具列表中找到该机具编号
                {
                    //获取数据库内的上传文件的机具列表
                    machineId2 = KyDataOperation.GetMachineIdFromImportMachine(machineNumber);
                    if (machineId2 == 0)//未在上传文件的机具列表中找到该机具编号
                    {
                        MachineType m=0;
                        try
                        {
                            m = (MachineType)Enum.Parse(typeof(MachineType), machineType, true);
                        }
                        catch (Exception ex)
                        {
                            MoveErrFile(importDir, fi.Name);
                            Log.ImportLog(ex, fi.Name + "的设备类别不存在！");
                            continue;

                        }
                        ky_import_machine imort_machine = new ky_import_machine 
                        {
                            kMachineModel = machineModel,
                            kMachineNumber=machineNumber,
                            kMachineType=(int)m,
                            kFactoryId=factoryId,
                            kNodeId=nodeId
                        };
                        int id = KyDataOperation.InsertMachineToImportMachine(imort_machine);
                        if (id > 0)
                            machineId2 = id;
                    }
                }
                BussinessType bussiness=BussinessType.HM;
                bussiness = (BussinessType)Enum.Parse(typeof(BussinessType), bussinessType, true);
                int atmId = 0, cashBoxId = 0;
                if (bussiness == BussinessType.ATMP||bussiness==BussinessType.ATMQ)
                {
                    if (info.Length == 11)
                    {
                        atmNumber = info[9].Trim();
                        cashboxNumber = info[10].Trim();
                        if (atmNumber != "0")
                        {
                            atmId = KyDataOperation.GetATMId(atmNumber,nodeId);
                            if (atmId == 0)
                            {
                                ky_atm atm = new ky_atm { 
                                    kATMNumber=atmNumber,
                                    kNodeId=nodeId,
                                    kStatus=1
                                };
                                atmId=KyDataOperation.InsertATM(atm);
                            }
                        }
                        if (cashboxNumber != "0")
                        {
                            cashBoxId = KyDataOperation.GetCashBoxId(cashboxNumber,nodeId);
                            if (cashBoxId == 0)
                            {
                                ky_cashbox cashbox = new ky_cashbox {
                                    kCashBoxNumber=cashboxNumber,
                                    kNodeId=nodeId
                                };
                                cashBoxId=KyDataOperation.InsertCashBox(cashbox);
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
                bool result = SaveDataToDB.UploadFsn(fi.FullName, machineTmp);
                if (result)
                {
                   // File.Delete(fi.FullName);
                    Log.ImportLog(null, fi.Name + "导入成功！");
                }
                else
                {
                    MoveErrFile(importDir, fi.Name);
                    Log.ImportLog(null, fi.Name + "导入失败！");
                }
            }
        }
        public static void MoveErrFile(string importDir, string fileName)
        {
            try
            {
                string errDir = importDir + "/ErrFiles";
                if (!Directory.Exists(errDir))
                    Directory.CreateDirectory(errDir);
                string destFile = errDir + "/" + fileName;
                string sourceFile = importDir + "/" + fileName;
                if (!File.Exists(destFile))
                    File.Move(sourceFile, destFile);
                else
                    File.Delete(sourceFile);
            }
            catch (Exception ex)
            {
                Log.ImportLog(ex,"移动文件异常");
            }
        }
    }
}
