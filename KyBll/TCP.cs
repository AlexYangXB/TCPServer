using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using KyModel;
namespace KyBll
{
    public class TCP
    {
        public static string ByteToStringX2(byte[] bytes)
        {
            string all = "";
            string tmp1 = "";
            string tmp2 = "";
            int count = 0;
            foreach (byte b in bytes)
            {
                tmp1 += b.ToString("X2") + " ";
                var ascii = System.Text.Encoding.ASCII.GetString(new byte[] { b });
                //var regex = new Regex(@"([^a-zA-z0-9\s])");
                //ascii = regex.Replace(ascii, ".")+" ";
                if (b < 32 || b > 126)
                    ascii = "_";

                tmp2 += ascii + " ";
                count++;
                if (count % 20 == 0)
                {
                    tmp1 = tmp1.PadRight(60, ' ');
                    all += tmp1 + " : " + tmp2.Replace('\n', ' ').Replace('\f', ' ') + Environment.NewLine;
                    tmp1 = tmp2 = "";
                }

            }
            if (tmp1 != "" || tmp2 != "")
            {
                tmp1 = tmp1.PadRight(60, ' ');
                all += tmp1 + " : " + tmp2.Replace('\n', ' ').Replace('\f', ' ');
            }
            return all;
        }
        public static string TCPMessageFormat(TCPMessage TCPMessage)
        {
            string date = DateTime.Now.ToString("[ yyyy-MM-dd HH:mm:ss ] ");
            string message = date;
            string CommandFormat = "";
            if (TCPMessage.Command != null)
            {
                CommandFormat = " len " + TCPMessage.Command.Length + Environment.NewLine + TCP.ByteToStringX2(TCPMessage.Command);
            }
            if (TCPMessage.MessageType == TCPMessageType.NewConnection)
            {
                message += " new connection " + TCPMessage.IpAndPort;
            }
            if (TCPMessage.MessageType == TCPMessageType.NoMachineIp)
            {
                message += TCPMessage.IpAndPort + "Machine Ip not exists ";
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_SIMPLE)
            {
                message += TCPMessage.IpAndPort + ", Request upload Command   " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_UP)
            {
                message += TCPMessage.IpAndPort + ", Send Data Command  " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_CLOSE)
            {
                message += TCPMessage.IpAndPort + ", Close connection Command  " + CommandFormat;
            }
            if (TCPMessage.MessageType == TCPMessageType.NET_TIME)
            {
                message += TCPMessage.IpAndPort + ",Time Synchronization Command  " + CommandFormat;
            }
            message += Environment.NewLine;
            return message;
        }
    }
}
