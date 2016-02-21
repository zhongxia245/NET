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
        //错误日志
        private static string LogName = "error.txt";
        //采集日志，记录采集的条数
        private static string ProgressLog = "collect.txt";
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Write(string msg)
        {
            DirectoryInfo d = Directory.CreateDirectory(LogPath);
            FileStream fs = new FileStream(LogPath + "/" + LogName, System.IO.FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            sw.WriteLine(String.Format("{0}：{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), msg));
            sw.Close();
            fs.Close();  
        }

        /// <summary>
        /// 采集日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            DirectoryInfo d = Directory.CreateDirectory(LogPath);
            FileStream fs = new FileStream(LogPath + "/" + ProgressLog, System.IO.FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            sw.WriteLine(String.Format("{0}：{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), msg));
            sw.Close();
            fs.Close();
        }

    }
}
