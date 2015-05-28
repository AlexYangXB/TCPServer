using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
                var regex = new Regex(@"([^a-zA-z0-9\s])");
                ascii = regex.Replace(ascii, ".")+" ";
                tmp2 += ascii;
                count++;
                if (count % 16 == 0)
                {
                    all += tmp1 + " : " + tmp2.Replace('\n',' ') + Environment.NewLine;
                    tmp1 = tmp2 = "";
                }

            }
            if (tmp1 != "" || tmp2 != "")
                all += tmp1 + " : " + tmp2.Replace('\n', ' ');
            return all;
        }
    }
}
