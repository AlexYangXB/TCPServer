using System;
using ServiceStack.DataAnnotations;
namespace KyModel
{
    //ky_machine
    public class ky_machine
    {

        public int kId { get; set; }
        public int kMachineType { get; set; }
        public string kMachineNumber { get; set; }
        public string kMachineModel { get; set; }
        public string kIpAddress { get; set; }
        public int kStatus { get; set; }
        public int kNodeId { get; set; }
        public int kFactoryId { get; set; }
        public DateTime kUpdateTime { get; set; }


        //新添加 用于交易控制
        /// <summary>
        /// 开始交易控制
        /// </summary>
        [Ignore]
        public bool startBusinessCtl { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [Ignore]
        public string fileName { get; set; }
        /// <summary>
        /// 交易控制开始时间，主要用于区分交易控制之前下的钞
        /// </summary>
        [Ignore]
        public DateTime dateTime { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        [Ignore]
        public string business { get; set; }
        /// <summary>
        /// 每捆钞票张数
        /// </summary>
        [Ignore]
        public int bundleCount { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Ignore]
        public int userId { get; set; }
        /// <summary>
        /// 业务流水号
        /// </summary>
        [Ignore]
        public string bussinessNumber { get; set; }
        /// <summary>
        /// ATM编号
        /// </summary>
        [Ignore]
        public int atmId { get; set; }
        /// <summary>
        /// 钞箱编号
        /// </summary>
        [Ignore]
        public int cashBoxId { get; set; }
        /// <summary>
        /// 是否清分中心
        /// </summary>
        [Ignore]
        public string isClearCenter { get; set; }
        /// <summary>
        /// 包号
        /// </summary>
        [Ignore]
        public string packageNumber { get; set; }

    }
}