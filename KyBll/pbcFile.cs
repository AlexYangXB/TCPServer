using System;
using System.Collections.Generic;
using System.Text;
using KyBll.Base;
namespace KyBll
{
    public class pbcFile
    {

        public bool UnZipGzh(string gzhZip, string targetDirectory)
        {
            try
            {
                Base.ZipClass zipClass = new ZipClass();
                zipClass.UNZipFile(gzhZip, targetDirectory);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
