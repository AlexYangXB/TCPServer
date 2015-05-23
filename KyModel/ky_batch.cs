using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyModel
{
    public class ky_batch
    {
        public long id;//批次ID
        public string ktype;//业务类型，用英文表示1、号码记录(HM) 2、柜面取款(QK) 3、柜面存款(CK) 4、ATM配钞(ATMP) 5、ATM清钞(ATMQ)
        public int kdate;//时间戳
        public int knode;//网点ID
        public int kfactory;//厂家ID
        public int[] kmachine;//机具ID
        public int ktotalnumber;//总张数
        public int ktotalvalue;//总金额
        public int krecorduser;//柜员ID
        public int kimgipaddress;//图片数据的IP
        public string hjson;
    }
}
