using System;
using System.Collections.Generic;
using System.Text;
using KySDK.Base;

namespace KySDK
{
    public class pbcFile
    {
        /// <summary>
        /// 导入GZH文件到数据库
        /// </summary>
        /// <param name="gzhZip">GZH压缩文件</param>
        /// <returns></returns>
        public bool ImportGzh(string gzhZip)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
