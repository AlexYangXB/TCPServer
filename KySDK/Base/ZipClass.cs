using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace KySDK.Base
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
        //public bool FastToZip(string DtrOrFileToZip,string zipName)
        //{
        //    FastZip fastZip = new FastZip();
        //    fastZip.CreateZip();
        //}

        #region ZipFileDictory
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
        #endregion


        #region ZipFileDictory

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="folderToZip">待压缩的文件夹，全路径格式</param>
        /// <param name="zipedFile">压缩后的文件名，全路径格式</param>
        /// <param name="password"> </param>
        /// <returns></returns>
        private bool ZipFileDictory(string folderToZip, string zipedFile, string password)
        {
            if (!Directory.Exists(folderToZip))
                return false;
            ZipOutputStream s = new ZipOutputStream(File.Create(zipedFile));
            s.SetLevel(6);
            if (!string.IsNullOrEmpty(password.Trim()))
                s.Password = password.Trim();
            bool res = ZipFileDictory(folderToZip, s, "");
            s.Finish();
            s.Close();
            return res;
        }

        #endregion

        #region ZipFile

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileToZip">要进行压缩的文件名</param>
        /// <param name="zipedFile">压缩后生成的压缩文件名</param>
        /// <param name="password"> </param>
        /// <returns></returns>
        private bool ZipFile(string fileToZip, string zipedFile, string password)
        {
            //如果文件没有找到，则报错
            if (!File.Exists(fileToZip))
                throw new FileNotFoundException("指定要压缩的文件: " + fileToZip + " 不存在!");
            //FileStream fs = null;
            FileStream ZipFile = null;
            ZipOutputStream ZipStream = null;
            ZipEntry ZipEntry = null;
            bool res = true;
            try
            {
                ZipFile = File.OpenRead(fileToZip);
                byte[] buffer = new byte[ZipFile.Length];
                ZipFile.Read(buffer, 0, buffer.Length);
                ZipFile.Close();
                ZipFile = File.Create(zipedFile);
                ZipStream = new ZipOutputStream(ZipFile);
                if (!string.IsNullOrEmpty(password.Trim()))
                    ZipStream.Password = password.Trim();
                ZipEntry = new ZipEntry(Path.GetFileName(fileToZip));
                ZipStream.PutNextEntry(ZipEntry);
                ZipStream.SetLevel(6);
                ZipStream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (ZipEntry != null)
                {
                    ZipEntry = null;
                }
                if (ZipStream != null)
                {
                    ZipStream.Finish();
                    ZipStream.Close();
                }
                if (ZipFile != null)
                {
                    ZipFile.Close();
                    ZipFile = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

            return res;
        }

        #endregion

        #region Zip

        /// <summary>
        /// 压缩文件 和 文件夹
        /// </summary>
        /// <param name="fileToZip">待压缩的文件或文件夹，全路径格式</param>
        /// <param name="zipedFile">压缩后生成的压缩文件名，全路径格式</param>
        /// <param name="password">压缩密码</param>
        /// <returns></returns>
        public bool Zip(string fileToZip, string zipedFile, string password)
        {
            if (Directory.Exists(fileToZip))
            {
                return ZipFileDictory(fileToZip, zipedFile, password);
            }
            if (File.Exists(fileToZip))
            {
                return ZipFile(fileToZip, zipedFile, password);
            }
            return false;
        }

        /// <summary>
        /// 压缩文件 和 文件夹
        /// </summary>
        /// <param name="fileToZip">待压缩的文件或文件夹，全路径格式</param>
        /// <param name="zipedFile">压缩后生成的压缩文件名，全路径格式</param>
        /// <returns></returns>
        //public bool Zip(string fileToZip, string zipedFile)
        //{
        //    return Zip(fileToZip, zipedFile, "");
        //}

        #endregion

        #region 2013-9-3重写压缩
        //压缩文件夹是能够可以争取压缩， 压缩文件时如果压缩文件与压缩后的文件在同一目录下就会出错
        /// <summary>
        /// 把一个文件或文件夹内的文件压缩
        /// </summary>
        /// <param name="fileToZip">待压缩的文件、或文件夹</param>
        /// <param name="zipedFile">压缩后的文件名</param>
        /// <returns>是否成功</returns>
        public bool Zip(string fileToZip, string zipedFile)
        {
            ArrayList arrFiles = new ArrayList();
            arrFiles.Add(zipedFile);
            if (Directory.Exists(fileToZip))//fileToZip为文件夹
            {
                string[] filenames = Directory.GetFiles(fileToZip);
                arrFiles.AddRange(filenames);
                return Create(arrFiles);
            }
            if (File.Exists(fileToZip))//fileToZip为文件
            {
                arrFiles.Add(fileToZip);
                return Create(arrFiles);
            }
            return false;
        }

        /// <summary>
        /// The currently active <see cref="ZipFile"/>.
        /// </summary>
        /// <remarks>Used for callbacks/delegates</remarks>
        ZipFile activeZipFile_;

        /// <summary>
        /// Create archives based on specifications passed and internal state
        /// </summary>		
        public bool Create(ArrayList fileSpecs)
        {
            try
            {
                string zipFileName = fileSpecs[0] as string;
                if (Path.GetExtension(zipFileName).Length == 0)
                {
                    zipFileName = Path.ChangeExtension(zipFileName, ".zip");
                }
                fileSpecs.RemoveAt(0);

                try
                {
                    using (ZipFile zf = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(zipFileName))
                    {
                        //zf.Password = password_;
                        //zf.UseZip64 = useZip64_;

                        zf.BeginUpdate();
                        activeZipFile_ = zf;
                        foreach (string spec in fileSpecs)
                        {
                            // This can fail with wildcards in spec...
                            string path = Path.GetDirectoryName(Path.GetFullPath(spec));
                            string fileSpec = Path.GetFileName(spec);

                            zf.NameTransform = new ZipNameTransform(path);

                            FileSystemScanner scanner = new FileSystemScanner(WildcardToRegex(fileSpec));
                            scanner.ProcessFile = new ProcessFileHandler(ProcessFile);
                            //scanner.ProcessDirectory = new ProcessDirectoryHandler(ProcessDirectory);
                            scanner.Scan(path, true);
                        }

                        zf.CommitUpdate();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Problem creating archive - '{0}'", ex.Message);
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Convert a wildcard expression to a regular expression
        /// </summary>
        /// <param name="wildcard">The wildcard expression to convert.</param>
        /// <returns>A regular expression representing the converted wildcard expression.</returns>
        private static string WildcardToRegex(string wildcard)
        {
            int dotPos = wildcard.IndexOf('.');
            bool dotted = (dotPos >= 0) && (dotPos < wildcard.Length - 1);
            string converted = wildcard.Replace(".", @"\.");
            converted = converted.Replace("?", ".");
            converted = converted.Replace("*", ".*");
            converted = converted.Replace("(", @"\(");
            converted = converted.Replace(")", @"\)");
            if (dotted)
            {
                converted += "$";
            }

            return converted;
        }

        /// <summary>
        /// Callback for adding a new file.
        /// </summary>
        /// <param name="sender">The scanner calling this delegate.</param>
        /// <param name="args">The event arguments.</param>
        private void ProcessFile(object sender, ScanEventArgs args)
        {
            activeZipFile_.Add(args.Name);
        }

        /// <summary>
        /// Callback for adding a new directory.
        /// </summary>
        /// <param name="sender">The scanner calling this delegate.</param>
        /// <param name="args">The event arguments.</param>
        /// <remarks>Directories are only added if they are empty and
        /// the user has specified that empty directories are to be added.</remarks>
        private void ProcessDirectory(object sender, DirectoryEventArgs args)
        {
            activeZipFile_.AddDirectory(args.Name);
        }
        #endregion
    }
}
