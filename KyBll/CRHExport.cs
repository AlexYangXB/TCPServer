using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KyModel.Models;
using KyModel;
using Newtonsoft.Json;
using System.IO;
using KyBll.Base;
namespace KyBll
{
    public class CRHExport
    {
        /// <summary>
        /// 导出CRH
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public CRHExport(DateTime startTime, DateTime endTime, string path)
        {
            Log.CRHLog("导出CRH开始。" );
            List<ky_agent_batch> batches = GetBatchesByTime(startTime, endTime);
            Log.CRHLog( "总共"+batches.Count+"个批次需要导出CRH文件。");
            if (batches.Count > 0)
            {
                List<CRH> crhs = BatchesToCRH(batches);
                if (crhs.Count > 0)
                {
                    string tmpPath = path + "/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
                    Log.CRHLog("建立临时目录"+tmpPath);
                    string BankCode =System.Text.Encoding.ASCII.GetString(crhs[0].BankCode).Replace("_","");
                    if (!Directory.Exists(tmpPath))
                        Directory.CreateDirectory(tmpPath);
                    foreach (CRH crh in crhs)
                    {
                        string crhFileName = tmpPath + crh.fileName;
                        Log.CRHLog( "建立新CRH文件" + crhFileName);
                        using (FileStream fs = new FileStream(crhFileName, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(crh.Date, 0, sizeof(ushort));
                            fs.Write(crh.BankCode, 0, crh.BankCode.Length);
                            fs.Write(crh.NodeCode, 0, crh.NodeCode.Length);
                            fs.WriteByte(crh.BussinessType);
                            fs.Write(crh.RecordCount, 0, crh.RecordCount.Length);
                            fs.WriteByte(crh.ClearCenter);
                            fs.WriteByte(crh.FileVersion);
                            fs.WriteByte(crh.MachineType);
                            fs.Write(crh.MachineModel, 0, crh.MachineModel.Length);
                            fs.Write(crh.MachineNumber, 0, crh.MachineNumber.Length);
                            fs.Write(crh.BussinessNumber, 0, crh.BussinessNumber.Length);
                            fs.Write(crh.Reverse, 0, crh.Reverse.Length);
                            byte[] SplitByte = Encoding.ASCII.GetBytes(",");
                            byte[] LineByte = Encoding.ASCII.GetBytes(Environment.NewLine);
                            foreach (CRHRecord sign in crh.records)
                            {
                                fs.Write(sign.Time, 0, sizeof(ushort));
                                fs.Write(SplitByte, 0, 1);
                                fs.Write(sign.Sign, 0, sign.Sign.Length);
                                fs.Write(SplitByte, 0, 1);
                                fs.WriteByte(sign.SignVersion);
                                fs.Write(SplitByte, 0, 1);
                                fs.WriteByte(sign.SignValue);
                                fs.Write(LineByte, 0, LineByte.Length);
                            }
                        }
                    }
                    ZipClass Zc = new ZipClass();
                    string zipFileName =path+"/"+  BankCode + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
                    Log.CRHLog( "生成CRH压缩包" + zipFileName);
                    Log.CRHLog("银行编码是" + BankCode);
                    Zc.ZipDirFile(tmpPath, zipFileName);
                    Directory.Delete(tmpPath, true);
                }
                else
                {
                    Log.CRHLog( "未能找到批次信息！");
                }
            }
            else
                Log.CRHLog("未能找到批次信息！");
            
        }
        /// <summary>
        /// 根据时间获取所有批次与冠字号码信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<ky_agent_batch> GetBatchesByTime(DateTime startTime, DateTime endTime)
        {
            //获取批次总数
            int start = DateTimeAndTimeStamp.ConvertDateTimeInt(startTime);
            int end = DateTimeAndTimeStamp.ConvertDateTimeInt(endTime);
            int count = KyDataOperation.GetBatchCount(start, end);
            List<ky_agent_batch> batches = KyDataOperation.GetBatches(start, end, count);
            int offset = 0;
            foreach (ky_agent_batch batch in batches)
            {
                long batchid = batch.id;
                List<ky_agent_sign> signs = KyDataOperation.GetSignByBatchId(batchid);
                if (signs != null)
                {
                    signs=signs.Distinct(new AgentSignComparer()).ToList();
                    batches[offset].Signs = signs;
                    batches[offset].ktotalnumber = signs.Count;
                }
                offset++;
                
            }
            return batches;
        }
        /// <summary>
        /// 批次转CRH
        /// </summary>
        /// <param name="batches"></param>
        /// <returns></returns>
        public static List<CRH> BatchesToCRH(List<ky_agent_batch> batches)
        {
            List<CRH> CRHs = new List<CRH>();
            foreach (ky_agent_batch batch in batches)
            {
                try
                {
                    ky_agent_batch tbatch = BatchAnalyses(batch);
                    CRH cr = new CRH(tbatch);
                    CRHs.Add(cr);
                }
                catch (Exception e)
                {
                    Log.CRHLog("批次id为" + batch.id + "网点id为"+batch.knode+"机具id为"+batch.kmachine+"转CRH异常",e );
                    continue;
                }
            }
            return CRHs;
        }
        /// <summary>
        /// 解析批次信息
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        public static ky_agent_batch BatchAnalyses(ky_agent_batch batch)
        {
            if (batch.hjson == null)
                batch.hjson = "";
            BatchHjson hjson = new BatchHjson();
            hjson = (BatchHjson)JsonConvert.DeserializeObject(batch.hjson, hjson.GetType());
            if (hjson == null)
                hjson = new BatchHjson();
            //默认第一台机具
            int machineId = Convert.ToInt32(batch.kmachine.Split(',')[0]);
            if (machineId != 0)
            {
                batch.Machine = KyDataOperation.GetMachineByMachineId(machineId);
                if(batch.Machine==null)
                    throw new Exception("未能找到id为" + machineId + "的机具！");
            }
            else
            {
                if (hjson.kmachine == 0)
                    throw new Exception("未能找到id为" + hjson.kmachine + "的非协议机具！");
                ky_import_machine import_machine = KyDataOperation.GetmportMachineByImportMachineId(hjson.kmachine);
                ky_machine machine = new ky_machine
                {
                    kMachineModel = import_machine.kMachineModel,
                    kFactoryId = import_machine.kFactoryId,
                    kNodeId = import_machine.kNodeId,
                    kMachineType = import_machine.kMachineType,
                    kMachineNumber = import_machine.kMachineNumber
                };
                batch.Machine = machine;
            }
            int nodeId = batch.knode;

            batch.Node = KyDataOperation.GetNodeByNodeId(nodeId);
            if(batch.Node==null)
                throw new Exception("未能找到id为" + nodeId + "的网点！");
            batch.Branch = KyDataOperation.GetBranchWithNodeId(nodeId);
            if (batch.Node == null)
                throw new Exception("未能找到id为" + nodeId + "的网点从属的银行！");
            batch.BussinessNumber = hjson.kbussinessnumber;
            batch.Date = DateTimeAndTimeStamp.GetTime(batch.kdate.ToString());
            switch (batch.ktype)
            {
                case "CK":
                case "CACK": batch.BussinessType = 1; break;
                case "QK":
                case "CAQK": batch.BussinessType = 2; break;
                case "KHDK":
                case "ATMP":
                case "ATMQ": batch.BussinessType = 3; batch.BussinessNumber = "0"; break;
                default: batch.BussinessType = 0; batch.BussinessNumber = "0"; break;

            }
            batch.Machine=FSNFormat.ConvertToFsnMachine(batch.Machine);
            if (batch.Signs != null)
            {
                for (int i = 0; i < batch.Signs.Count; i++)
                {
                    switch (batch.Signs[i].kversion)
                    {
                        case 1990: batch.Signs[i].kversion = 0; break;
                        case 1999: batch.Signs[i].kversion = 1; break;
                        case 2005: batch.Signs[i].kversion = 2; break;
                        default: batch.Signs[i].kversion = 255; break;
                    }
                    switch (batch.Signs[i].kvalue)
                    {
                        case 1: batch.Signs[i].kvalue = 1; break;
                        case 5: batch.Signs[i].kvalue = 2; break;
                        case 10: batch.Signs[i].kvalue = 3; break;
                        case 20: batch.Signs[i].kvalue = 4; break;
                        case 50: batch.Signs[i].kvalue = 5; break;
                        case 100: batch.Signs[i].kvalue = 6; break;
                        default: batch.Signs[i].kvalue = 0; break;
                    }
                }
            }
            return batch;
        }
    }
    /// <summary>
    /// 去除重复的冠字号码
    /// </summary>
    public class AgentSignComparer : IEqualityComparer<ky_agent_sign>
    {
        public bool Equals(ky_agent_sign x, ky_agent_sign y)
        {
            if (x.ksign == y.ksign)
                return true;
            else
                return false;
        }

        public int GetHashCode(ky_agent_sign obj)
        {
            return 0;
        }
    }  
}
