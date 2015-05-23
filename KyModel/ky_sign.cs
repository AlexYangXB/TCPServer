using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace KyModel
{
    public class ky_sign
    {
        /// <summary>
        /// 冠字号ID
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public int date { get; set; }
        /// <summary>
        /// 冠字号码
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 批次Id
        /// </summary>
        public long batchId { get; set; }
        /// <summary>
        /// 面额
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// 币种ID
        /// </summary>
        public int currency { get; set; }
        /// <summary>
        /// 钞票状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// JSON字段，可扩展
        /// </summary>
        public string hjson { get; set; }

    }
}
