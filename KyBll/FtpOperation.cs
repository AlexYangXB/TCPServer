using System;
using System.Collections.Generic;
using System.IO;
using KyBase;

namespace Utility
{
    public class FtpOperation
    {
        //Ftp站点连接测试
        public static bool FtpConnect(string ip,int port,string dir,string user,string passWord)
        {
            bool result = false;
            FtpClient ftpClient=new FtpClient();
            try
            {
                if(user=="")
                    user = "anonymous";
                ftpClient.RemoteHost = ip;
                ftpClient.RemotePort = port;
                ftpClient.RemotePath = dir;
                ftpClient.RemoteUser = user;
                ftpClient.RemotePass = passWord;
                ftpClient.Connect();
                if(ftpClient.Connected)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        //上传单个文件
        public static bool FileUpload(string ip,int port,string dir,string user,string passWord,string file)
        {
            bool result = false;
            FtpClient ftpClient = new FtpClient();
            string fileShortName = Path.GetFileName(file);
            try
            {
                if (user == "")
                    user = "anonymous";
                ftpClient.RemoteHost = ip;
                ftpClient.RemotePort = port;
                ftpClient.RemotePath = dir;
                ftpClient.RemoteUser = user;
                ftpClient.RemotePass = passWord;
                ftpClient.Connect();
                if (ftpClient.Connected)
                {
                    //获取FTP上现有的文件名
                    string[] ftpExistFiles = ftpClient.GetFilesList("");
                    List<string> ftpFiles=new List<string>(ftpExistFiles);
                    //判断FTP上是否已有该文件名，有就更改文件名
                    if (ftpFiles.Contains(fileShortName))
                    {
                        int j = 0;
                        string tmpFileName;
                        do
                        {
                            j++;
                            tmpFileName = string.Format("{0}({1}){2}", Path.GetFileNameWithoutExtension(fileShortName), j, Path.GetExtension(fileShortName));
                        } while (ftpFiles.Contains(tmpFileName));
                        fileShortName = tmpFileName;
                    }
                    ftpClient.Upload(file,fileShortName+".tmp");
                    File.Delete(file);//上传成功后删除文件
                    ftpClient.Rename(fileShortName + ".tmp", fileShortName);//上传成功后修改文件名
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        //上传多个文件
        public static int FilesUpload(string ip,int port,string dir,string user,string passWord,string[] files)
        {
            int count = 0;
            for (int i = 0; i < files.Length; i++)
            {
                if (FileUpload(ip, port, dir, user, passWord, files[i]))
                    count++;
            }
            return count;
        }
    }
}
