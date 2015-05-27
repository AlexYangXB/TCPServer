using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyBll
{
    public class TCP
    {
        public static string ByteToStringX2(byte[] bytes)
        {
           string temp = "";
           foreach (byte b in bytes)
                temp += b.ToString("X2");
            return temp;
        }
    }
}
