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

    }
    public enum TCPMessageType
    {
        NewConnection=0,
        NoMachineIp=1,
        /// <summary>
        /// 传输数据请求命令
        /// </summary>
        NET_SIMPLE=2,
        /// <summary>
        /// 纸币信息发送命令
        /// </summary>
        NET_UP=3,
        /// <summary>
        /// 传输数据结束命令
        /// </summary>
        NET_CLOSE=4,
        /// <summary>
        /// 时间同步命令
        /// </summary>
        NET_TIME=5
    }
}
