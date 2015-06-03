using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KyModel.Models;
using KyModel;
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
            int count=KyDataOperation.GetBatchCount(start,end);
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
 
            }
            return CRHs;
        }
        public static ky_agent_batch GenerateBatchFromDB(ky_agent_batch batch)
        {
            //默认第一台机具
            int machineId =Convert.ToInt32(batch.kmachine.Split(',')[0]);
            int nodeId = batch.knode;
            batch.machine = KyDataOperation.GetMachineByMachineId(machineId);
            batch.node = KyDataOperation.GetNodeByNodeId(nodeId);
            batch.branch = KyDataOperation.GetBranchWithNodeId(nodeId);

            return batch;
        }
 
    }
}
