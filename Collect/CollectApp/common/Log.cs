using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CollectApp.common
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Log
    {
        private static string LogPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private static string LogName = "log.txt";
        public static void Write(string log){
            DirectoryInfo d = Directory.CreateDirectory(LogPath);
            FileStream fs = new FileStream(LogPath + "/" + LogName, System.IO.FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            sw.WriteLine(String.Format("{0}：{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), log));
            sw.Close();
            fs.Close();  
        }

    }
}
