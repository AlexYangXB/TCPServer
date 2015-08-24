using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace KyBase
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
        private void zip(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            foreach (string file in filenames)
            {

                if (Directory.Exists(file))
                {
                    zip(file, s, staticFile);
                }

                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);

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
        }
        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="zipFileName"></param>
        public void ZipDirFile(string dirName, string zipFileName)
        {
            if (dirName[dirName.Length - 1] != Path.DirectorySeparatorChar)
                dirName += Path.DirectorySeparatorChar;
            ZipOutputStream s = new ZipOutputStream(File.Create(zipFileName));
            s.SetLevel(6); // 0 - store only to 9 - means best compression
            zip(dirName, s, dirName);
            s.Finish();
            s.Close();
        }




    }
}
