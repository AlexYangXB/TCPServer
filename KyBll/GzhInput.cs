using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using KySDK;

namespace KyBll
{
    public class GzhInput
    {
        /// <summary>
        /// GZH压缩包解压并保存到数据库
        /// </summary>
        /// <param name="gzhZip">GZH压缩包（ZIP类型）</param>
        /// <param name="targetDirectory">解压到目标文件夹</param>
        /// <param name="pictureServerId"> </param>
        /// <param name="userId"> </param>
        /// <returns></returns>
        public bool UploadGzhFile(string gzhZip,string targetDirectory,int pictureServerId,int userId)
        {
            KySDK.pbcFile pbc=new pbcFile();
            bool success = pbc.UnZipGzh(gzhZip, targetDirectory);
            if(success)
            {
                //string[] ZipFile = Directory.GetFiles(targetDirectory, "*.ZIP");
                string[] zipFile = Directory.GetFiles(targetDirectory, "*.ZIP");
                if (zipFile.Length > 0)//说明还有压缩文件，gzhZip为二次压缩文件
                {
                    for (int i = 0; i < zipFile.Length; i++)
                    {
                        success = pbc.UnZipGzh(zipFile[i], targetDirectory + "\\tmp");
                        if (success)
                        {
                            bool result = Utility.SaveDataToDB.UploadGzhPackage(targetDirectory + "\\tmp",
                                                                                pictureServerId, userId);
                            //删除文件
                            if (Directory.Exists(targetDirectory + "\\tmp"))
                            {
                                string[] files = Directory.GetFiles(targetDirectory + "\\tmp");
                                foreach (var file in files)
                                {
                                    File.Delete(file);
                                }
                            }
                            if (!result)
                                return false;
                        }
                    }
                    return true;
                }
                else//说明没有压缩文件了，gzhZip为一次压缩文件
                {
                    return Utility.SaveDataToDB.UploadGzhPackage(targetDirectory, pictureServerId, userId);
                }
            }
            else
            {
                return false;
            }
            
        }
    }
}
