using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace KyModel
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class GZH
    {

        public DateTime date;                 //记录日期
        public string branchNumber;        //报送银行编号
        public string nodeNumber;           //生成网点编号
        public string business;             //业务类型
        public int fileCnt;                 //FSN文件个数
        public string isCashCenter;         //是否为清分中心 是：T  否：F
        public string fileVersion;          //文件版本
        public string packageNumber;        //包号
        public string currency;             //币种
        public string reserved;             //预留字段
        public string fileName;             //GZH文件名
    }
}
