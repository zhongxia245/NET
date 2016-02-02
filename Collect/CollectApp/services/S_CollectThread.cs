using CollectApp.common;
using CollectApp.config;
using CollectApp.model;
using HFSoft.Component.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace CollectApp.services
{
    public class S_CollectThread
    {
        /// <summary>
        /// 多线程采集回来的结果
        /// </summary>
        public static DataSet DS = new DataSet();
        /// <summary>
        /// DS大小
        /// </summary>
        public static int DataTableCount = 0;

        /// <summary>
        /// 总条数
        /// </summary>
        public static int Total = 0;

        /// <summary>
        /// 已经完成的类别
        /// </summary>
        public static List<String> CompleteList = new List<string>();

        /// <summary>
        /// 保存的路径
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        /// 需要采集的类别
        /// </summary>
        public M_Category Category { get; set; }
        public S_CollectThread() { }

        /// <summary>
        /// 初始化线程
        /// </summary>
        /// <param name="threadPoolCount">线程池数量</param>
        public S_CollectThread(M_Category category,String folderPath)
        {
            this.Category = category;
            this.FolderPath = folderPath;
        }


        public void run()
        {
            //区分进出口
            string url_cn_base ;
            string url_en_base ;
            if (this.Category.Param.IndexOf("areano") == -1)
            {
                url_cn_base = Config.BaseURL_CN;
                url_en_base = Config.BaseURL_EN;
            }
            else {
                url_cn_base = Config.BaseURL_CN_Imp;
                url_en_base = Config.BaseURL_EN_Imp;
                this.Category.Title = "进口:" + this.Category.Title;
            }

            var url_cn = String.Format("{0}?{1}", url_cn_base, this.Category.Param);
            var url_en = String.Format("{0}?{1}", url_en_base, this.Category.Param);

            DataTable result_cn = Core.GetAllCompanyName_DataTable(url_cn,this.Category.TimePhase,this.Category.Title);
            DataTable result_en = Core.GetAllCompanyName_DataTable(url_en, this.Category.TimePhase, this.Category.Title);
            DataTable dt = S_Core.MergeTableColumn(result_cn, result_en);

            //添加到dataSet中
            dt.TableName = String.Format("第{0}期_{1}", this.Category.TimePhase, this.Category.Title);

            string flag = new ExcelHelper(String.Format("{0}/{1}.xls", FolderPath, String.Format("第{0}期_{1}", this.Category.TimePhase, this.Category.Title)), "").DatatableToExcel(dt);
            if (flag != "") {
                Log.Write(flag);
            }
            //保存到全局变量中
            S_CollectThread.CompleteList.Add(this.Category.Title);
            S_CollectThread.DS.Tables.Add(dt);
            S_CollectThread.Total += dt.Rows.Count;
        }

    }
}
