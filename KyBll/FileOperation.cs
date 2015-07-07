using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace KyBll
{
    public class FileOperation
    {
        /// <summary>
        /// 读取文件前n行，并将前n行删除
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="fileName"></param>
        /// <param name="lineCount"></param>
        /// <returns></returns>
        public static List<string> ReadLines(string dir,string fileName, int lineCount)
        {
            List<string> topLines = new List<string>();
            int index = 0;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            string tmpFile = dir + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".tmp";
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string str = sr.ReadLine();
                    while (str != null)
                    {
                        index++;
                        if (str != "")
                        {
                            if (index < lineCount)
                            {
                                topLines.Add(str);
                            }
                            else
                            {
                                MyLog.ShareWrite(str, tmpFile);
                            }
                        }
                        str = sr.ReadLine();
                    }
                }
            }
            if(File.Exists(fileName))
                File.Delete(fileName);
            if(File.Exists(tmpFile))
            {
                File.Move(tmpFile,fileName);
            }
            return topLines;
        }
    }
}
