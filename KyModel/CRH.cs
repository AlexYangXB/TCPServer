using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace KyModel
{
    public struct CRH
    {
        public byte[] Date;
        public byte[] BankCode;
        public List<CRHRecord> records;
        public CRH(ky_agent_batch batch)
        {
            BankCode = new byte[4];
            Date = new byte[2];
            records = null;
            byte[] dd = new byte[2] { 1, 3 };
            byte[] ddd = new byte[4] { 32, 32, 44, 22 };
            BankCode = ddd;
            Date = dd;
        }
 

    }
    public struct CRHRecord
    {
 
    }
}
