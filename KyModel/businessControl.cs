using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyModel
{
    public class businessControl
    {
        public string ip;
        public int userId;              //用户ID
        public BussinessType business;         //交易类型
        public string atmId;            //ATM编号
        public string cashBoxId;        //钞箱编号
        public string bussinessNumber;  //业务流水号
        public DateTime dateTime;       //交易控制开始时间
        //2015.05.05
        public int bundleCount;         //每捆钞票张数
        public string isClearCenter;      //是否清分中心
        public string packageNumber;    //包号
        public bool cancel;            //撤销
        public string bundleNumbers;//捆钞序号
        
    }
}
