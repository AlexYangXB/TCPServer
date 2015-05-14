using System;
using System.Collections.Generic;
using System.Text;

namespace KyData.DbTable
{
        //#批次表
        //index ky_batch
        //{       
        //        type                    = rt
        //        dict                    = keywords
        //        docinfo                 = extern
        //        path                    = C:/sphinx/data/batch/
        //        rt_mem_limit            = 128M
        //        #业务类型  HM(号码记录)，FK（付款），SK(收款)，QK（柜台取款），CK（柜台存款），ATM（ARM配钞），CAQK（ATM或CRS取款），CACK（CRS存款）
        //        rt_field                = ktype
        //        #记录时间
        //        rt_attr_string         = ktype
        //        rt_attr_timestamp       = kdate
        //        rt_attr_uint            = knode
        //        rt_attr_uint            = kfactory
        //        #可以填入多个机具编号，用小括号"()"括起来中间用逗号","分隔.  例如(1,2,3,4)或(1)
        //        rt_attr_multi           = kmachine
        //        rt_attr_uint            = ktotalnumber
        //        rt_attr_uint            = ktotalvalue
        //        rt_attr_uint            = krecorduser
        //        rt_attr_string          = kimgipaddress
        //        #json数据 包含counteruser、countercode 可以自己定义
        //        #格式 '{\"atm\":[1,2,3,4],\"cashbox\":[1,2,3,4]}'
        //        rt_attr_json            = hjson
        //}
    public class ky_batch_sphinx
    {
        public Int64 id;//批次ID
        public string type;//业务类型，用英文表示1、号码记录(signrecord) 2、柜面取款(counterdraw) 3、柜面存款(counterdeposit) 4、ATM配钞(atmmatch) 5、ATM清钞(atmclear)
        public int date;//时间戳
        public UInt32 node;//网点ID
        public UInt32 factory;//厂家ID
        public UInt32[] machine;//机具ID
        public UInt32 totalnumber;//总张数
        public UInt32 totalvalue;//总金额
        public UInt32 recorduser;//柜员ID
        public UInt32 imgipaddress;//图片数据的IP
        public string hjson;
    }
        //#冠字号码表
        //index ky_sign
        //{       
        //        type                       = rt
        //        dict                       = keywords
        //        min_infix_len              = 1
        //        docinfo                    = extern
        //        path                       = C:/sphinx/data/sign/
        //        rt_mem_limit               = 1024M
        //        rt_field                   = sign
        //        rt_attr_timestamp          = date
        //        rt_attr_string             = sign
        //        rt_attr_uint               = batchid
        //        rt_attr_uint               = value
        //        rt_attr_uint               = version
        //        rt_attr_uint               = currency
        //        rt_attr_uint               = status
        //        rt_attr_json            = hjson
        //}

    public class ky_sign_sphinx
    {
        public Int64 id;
        public int date;//时间戳
        public string sign;//冠字号码
        public Int64 batchId;//批次Id
        public UInt32 value;//面额
        public UInt32 version;//版本
        public UInt32 currency;//币种ID
        public UInt32 status;//钞票状态
        public string hjson;
    }

    public class machineData
    {
        public int id;
        public string machineType;      //机具类型
        public string machineModel;      //机型  2015.05.07
        public string machineNumber;    //机具编号
        public string ipAddress;        //IP地址
        public string stause;           //状态
        public int nodeId;              //所属网点ID
        public int factoryId;           //所属厂家ID
        //用于交易控制
        public bool startBusinessCtl;   //开始交易控制
        public string fileName;         //文件名
        public string business;         //交易类型
        public string atmId;            //ATM编号
        public string cashBoxId;        //钞箱编号
        public int userId;              //用户ID
        public string bussinessNumber;  //业务流水号
        public DateTime dateTime;       //交易控制开始时间，主要用于区分交易控制之前下的钞
        //2015.05.05
        public int bundleCount;         //每捆钞票张数
        public string isClearCenter;      //是否清分中心
        public string packageNumber;    //包号
    }

    public class businessControl
    {
        public string ip;
        public int userId;              //用户ID
        public string business;         //交易类型
        public string atmId;            //ATM编号
        public string cashBoxId;        //钞箱编号
        public string bussinessNumber;  //业务流水号
        public DateTime dateTime;       //交易控制开始时间
        //2015.05.05
        public int bundleCount;         //每捆钞票张数
        public string isClearCenter;      //是否清分中心
        public string packageNumber;    //包号
    }
}
