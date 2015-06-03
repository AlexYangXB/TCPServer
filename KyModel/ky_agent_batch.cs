using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KyModel.Models;
namespace KyModel
{
    public class ky_agent_batch
    {
        /// <summary>
        /// 批次ID
        /// </summary>
        public Int64 id { get; set; }
        /// <summary>
        /// 业务类型，用英文表示1、号码记录(HM) 2、柜面取款(QK) 3、柜面存款(CK) 4、ATM配钞(ATMP) 5、ATM清钞(ATMQ)
        /// </summary>
        public string ktype { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public int kdate { get; set; }
        /// <summary>
        /// 网点ID
        /// </summary>
        public int knode { get; set; }
        /// <summary>
        /// 厂家ID
        /// </summary>
        public int kfactory { get; set; }
        /// <summary>
        /// 机具ID
        /// </summary>
        public string kmachine { get; set; }
        /// <summary>
        /// 总张数
        /// </summary>
        public int ktotalnumber { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public int ktotalvalue { get; set; }
        /// <summary>
        /// 柜员ID
        /// </summary>
        public int kuser { get; set; }
        /// <summary>
        /// 图片数据的IP
        /// </summary>
        public int kimgserver { get; set; }
        /// <summary>
        /// JSON字段，可扩展
        /// </summary>
        public string hjson { get; set; }

        public ky_node Node { get; set; }
        public ky_branch Branch { get; set; }
        public ky_machine Machine { get; set; }
        public string BussinessNumber { get; set; }
        public DateTime Date { get; set; }
        public int BussinessType { get; set; }
        public int MachineType { get; set; }
    }
    public class BatchHjson {
        
        public int kmachine { get; set; }
        public int katm { get; set; }
        public int kcashobx { get; set; }
        public string kbussinessnumber { get; set; }
        public BatchHjson()
        {
            kmachine = 0;
            katm = 0;
            kcashobx = 0;
            kbussinessnumber = "";
        }
    }
}
