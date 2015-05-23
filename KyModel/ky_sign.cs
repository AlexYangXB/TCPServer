using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace KyModel
{
     public class ky_sign
    {
        public long id;
        public int date;//时间戳
        public string sign;//冠字号码
        public long batchId;//批次Id
        public int value;//面额
        public int version;//版本
        public int currency;//币种ID
        public int status;//钞票状态
        public string hjson;

    }
}
