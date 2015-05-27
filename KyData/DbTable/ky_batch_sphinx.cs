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

   

    
}
