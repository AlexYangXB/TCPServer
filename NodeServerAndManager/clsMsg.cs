using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Globalization;

namespace KangYiCollection
{
   public class clsMsg
    {
        public static string getMsg(string MsgId)
        {
            //ResourceManager rm = new ResourceManager("KangYiCollection.Resource", Assembly.GetExecutingAssembly());
            ResourceManager rm = Resource_zh_CN.ResourceManager;
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            if(ci.Name=="zh-TW")
                rm = Resource_zh_TW.ResourceManager;
            return rm.GetString(MsgId);
        }

        
    }
}
