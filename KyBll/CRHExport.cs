using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KyModel.Models;
using KyModel;
using Newtonsoft.Json;
namespace KyBll
{
    public class CRHExport
    {
        /// <summary>
        /// 根据时间获取所有批次
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
                    Log.CRHLog(e, "批次转CRH异常");
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
        public static ky_agent_batch  BatchAnalyses(ky_agent_batch batch)
        {
            if (batch.hjson == null)
                batch.hjson = "";
            BatchHjson hjson = new BatchHjson();
            hjson = (BatchHjson)JsonConvert.DeserializeObject(batch.hjson, hjson.GetType());     
            if (hjson == null)
                hjson =new  BatchHjson();
            //默认第一台机具
            int machineId = Convert.ToInt32(batch.kmachine.Split(',')[0]);
            if (machineId != 0)
                batch.Machine = KyDataOperation.GetMachineByMachineId(machineId);
            else
            {
                ky_import_machine import_machine = KyDataOperation.GetmportMachineByImportMachineId(hjson.kmachine);
                ky_machine machine = null;
                if (import_machine != null)
                {
                    machine = new ky_machine
                    {
                        kMachineModel = import_machine.kMachineModel,
                        kFactoryId = import_machine.kFactoryId,
                        kNodeId = import_machine.kNodeId,
                        kMachineType = import_machine.kMachineType,
                        kMachineNumber = import_machine.kMachineNumber
                    };
                }
                batch.Machine = machine;
            }
            int nodeId = batch.knode;

            batch.Node = KyDataOperation.GetNodeByNodeId(nodeId);
            batch.Branch = KyDataOperation.GetBranchWithNodeId(nodeId);
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
            if (batch.Machine != null)
            {
                var MachineType = batch.Machine.kMachineType;
                switch (MachineType)
                {
                    //1 清分机具  =>1 大型清分机 2 中型清分机 3 小型清分机
                    //3 点钞机 => 4 一口半点钞机 5 A类点钞机 
                    //2 存取款一体机 =>6 存取款一体机 
                    //4 取款机 => 7 ATM取款机  
                    //5 兑换机具 => 9 兑换机具 
                    //0 未定义 =>10 其他机具 8 循环柜员机
                    case 1:
                    case 2:
                    case 3: batch.MachineType = 1; break;
                    case 4: 
                    case 5: batch.MachineType = 3; break;
                    case 6: batch.MachineType = 2; break;
                    case 7: batch.MachineType = 4; break;
                    case 9: batch.MachineType = 5; break;
                    default: batch.MachineType = 0; break;
                }
            }
            return batch;
        }

    }
}
