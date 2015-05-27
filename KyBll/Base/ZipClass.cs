using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace KyBll.Base
{
    public class ZipClass
    {
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="fileToZip">待解压的文件名</param>
        /// <param name="zipedFile">解压后的文件夹名</param>
        /// <returns>是否成功</returns>
        public bool UNZipFile(string fileToZip, string zipedFile)
        {
            try
            {
                FastZip fastZip = new FastZip();
                fastZip.ExtractZip(fileToZip, zipedFile, "");
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 递归压缩文件夹方法
        /// </summary>
        /// <param name="folderToZip"></param>
        /// <param name="s"></param>
        /// <param name="ParentFolderName"></param>
        private bool ZipFileDictory(string folderToZip, ZipOutputStream s, string ParentFolderName)
        {
            bool res = true;
            string[] folders, filenames;
            ZipEntry entry = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();
            try
            {
                //////创建当前文件夹
                // 20121221
                //entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(folderToZip) + "/"));  //加上 “/” 才会当成是文件夹创建
                //s.PutNextEntry(entry);
                //s.Flush();
                //先压缩文件，再递归压缩文件夹 
                filenames = Directory.GetFiles(folderToZip);
                foreach (string file in filenames)
                {
                    //打开压缩文件
                    fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    //20121221
                    //entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(folderToZip) + "/" + Path.GetFileName(file)));
                    entry = new ZipEntry(Path.Combine(ParentFolderName, "" + Path.GetFileName(file)));
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                if (entry != null)
                    entry = null;
                GC.Collect();
                GC.Collect(1);
            }
            folders = Directory.GetDirectories(folderToZip);
            foreach (string folder in folders)
            {
                if (!ZipFileDictory(folder, s, Path.Combine(ParentFolderName, Path.GetFileName(folderToZip))))
                    return false;
            }
            return res;
        }




    }
}
