using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace KyModel
{
    public class TCPMessage
    {
        public string IpAndPort { get; set; }
        public byte[] Command { get; set; }
        public TCPMessageType MessageType { get; set; }
        public string Message { get; set; }
        
    }
    public enum TCPMessageType
    {
        /// <summary>
        /// 新连接
        /// </summary>
        NewConnection=0,
        /// <summary>
        /// 没有当前的机具IP
        /// </summary>
        NoMachineIp=1,
        
        /// <summary>
        /// 传输数据请求命令  54字节
        /// </summary>
        NET_SIMPLE=2,
        /// <summary>
        /// 纸币信息发送命令  86+1644*N字节
        /// </summary>
        NET_UP=3,
        /// <summary>
        /// 传输数据结束命令 44字节
        /// </summary>
        NET_CLOSE=4,
        /// <summary>
        /// 时间同步命令 42字节
        /// </summary>
        NET_TIME=5,
        /// <summary>
        /// 心跳命令  44字节
        /// </summary>
        NET_CONTINUE=6,
        /// <summary>
        /// 未知命令
        /// </summary>
        UnknownCommand=7,
        /// <summary>
        /// 存在相同的连接
        /// </summary>
        ExistConnection=8,
        /// <summary>
        /// 文件太大，超过5M
        /// </summary>
        FILE_TOO_BIG=9,
        /// <summary>
        /// 达到最大连接数 默认30
        /// </summary>
        Reach_Max_Connection=10,
        /// <summary>
        /// 达到一次连接文件数最大值 默认不超过30
        /// </summary>
        Reach_Max_File=11,
        /// <summary>
        /// 上一次心跳时间超过当前5分钟
        /// </summary>
        Out_Of_Date=12,
        /// <summary>
        /// 异常
        /// </summary>
        Exception=13,
        /// <summary>
        /// 关闭线程
        /// </summary>
        Thread_Close=14,
        /// <summary>
        /// 普通消息
        /// </summary>
        Common_Message=15

        
    }
    
}
