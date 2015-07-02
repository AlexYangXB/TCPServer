using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace KyBll
{
    public class MySetting
    {
       public static string iniFile = Application.StartupPath + "/config.ini";
       public static bool GetProgramValue(string key)
       {
           IniFile g = new IniFile(iniFile);
           int value = g.ReadInt("PROGRAM", key, 1);
           if (value == 1)
               return true;
           else
               return false;
       }
    }
}
