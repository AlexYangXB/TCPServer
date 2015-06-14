using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SqlFu;
namespace KyModel.Models
{
    public partial class ky_machine
    {
        public int kId { get; set; }
        public int kMachineType { get; set; }
        public string kMachineNumber { get; set; }
        public string kMachineModel { get; set; }
        public string kIpAddress { get; set; }
        public int kStatus { get; set; }
        public int kNodeId { get; set; }
        public int kFactoryId { get; set; }
        public Nullable<System.DateTime> kUpdateTime { get; set; }
        //新添加 用于交易控制
        /// <summary>
        /// 开始交易控制
        /// </summary>
        [QueryOnly]
        public bool startBusinessCtl { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [QueryOnly]
        public string fileName { get; set; }
        /// <summary>
        /// 交易控制开始时间，主要用于区分交易控制之前下的钞
        /// </summary>
        [QueryOnly]
        public DateTime dateTime { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        [QueryOnly]
        public BussinessType business { get; set; }
        /// <summary>
        /// 每捆钞票张数
        /// </summary>
        [QueryOnly]
        public int bundleCount { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [QueryOnly]
        public int userId { get; set; }
        /// <summary>
        /// 图像数据库ID
        /// </summary>
        [QueryOnly]
        public int imgServerId { get; set; }
        /// <summary>
        /// 导入文件机具ID
        /// </summary>
        [QueryOnly]
        public int importMachineId { get; set; }
        /// <summary>
        /// 业务流水号
        /// </summary>
        [QueryOnly]
        public string bussinessNumber { get; set; }
        /// <summary>
        /// ATM编号
        /// </summary>
        [QueryOnly]
        public int atmId { get; set; }
        /// <summary>
        /// 钞箱编号
        /// </summary>
        [QueryOnly]
        public int cashBoxId { get; set; }
        /// <summary>
        /// 是否清分中心
        /// </summary>
        [QueryOnly]
        public string isClearCenter { get; set; }
        /// <summary>
        /// 包号
        /// </summary>
        [QueryOnly]
        public string packageNumber { get; set; }
        /// <summary>
        /// 心跳时间
        /// </summary>
        [QueryOnly]
        public DateTime alive { get; set; }
        /// <summary>
        /// 业务临时目录
        /// </summary>
        [QueryOnly]
        public string tmpPath { get; set; }
    }
}
