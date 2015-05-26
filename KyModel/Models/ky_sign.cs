using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace KyModel.Models
{
    public class ky_sign
    {
        /// <summary>
        /// 冠字号ID
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 批内序号
        /// </summary>
        public int knumber { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public int kdate { get; set; }
        /// <summary>
        /// 冠字号码
        /// </summary>
        public string ksign { get; set; }
        /// <summary>
        /// 批次Id
        /// </summary>
        public long kbatchid { get; set; }
        /// <summary>
        /// 面额
        /// </summary>
        public uint kvalue { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public uint kversion { get; set; }
        /// <summary>
        /// 币种ID
        /// </summary>
        public uint kcurrency { get; set; }
        /// <summary>
        /// 钞票状态
        /// </summary>
        public uint kstatus { get; set; }
        /// <summary>
        /// JSON字段，可扩展
        /// </summary>
        public string hjson { get; set; }

    }
}
