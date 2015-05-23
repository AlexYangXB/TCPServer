using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace KyModel
{
    public class ky_batch
    {
        /// <summary>
        /// 批次ID
        /// </summary>
        public long id{get;set;}
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
    }
}
