using CollectApp.common;
using CollectApp.config;
using CollectApp.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CollectApp.services
{
    class S_SingleThreadConfig
    {
        /// <summary>
        /// 多线程采集回来的结果
        /// </summary>
        public static DataSet DS = new DataSet();
        /// <summary>
        /// 所有类别的个数
        /// </summary>
        public static int DataTableCount = 0;

        //采集的总共条数
        public static int SingleTotal = 0;

        //网页上显示每个类别累加的总条数
        public static int SingleTotal_site = 0;

        //采集的类别集合
        public static List<M_Category> list;

        //采集回条数与网页显示的总条数不一致信息
        public static StringBuilder Sb_ErrorCategory = new StringBuilder();

        //导出的文件夹路径
        public static string floderPath = "";

        /// <summary>
        /// 重置属性的值
        /// </summary>
        public static void ReSet() {
            DataTableCount = 0;
            SingleTotal = 0;
            DS = new DataSet();
            list = null;
            Sb_ErrorCategory = new StringBuilder();
            floderPath = "";
        }

        /// <summary>
        /// 采集并导出的方法[根据类别采集]
        /// </summary>
        public static void Collect2Export()
        {
            foreach (M_Category item in list)
            {
                string url_cn_base;
                string url_en_base;
                //判断是进口或者出口
                if (item.Param.IndexOf("areano") == -1)
                {
                    url_cn_base = Config.BaseURL_CN;
                    url_en_base = Config.BaseURL_EN;
                }
                else
                {
                    url_cn_base = Config.BaseURL_CN_Imp;
                    url_en_base = Config.BaseURL_EN_Imp;
                    item.Title = "进口:" + item.Title;
                }
                //拼接URL路径
                var url_cn = String.Format("{0}?{1}", url_cn_base, item.Param);
                var url_en = String.Format("{0}?{1}", url_en_base, item.Param);


                DataTable result_cn = Core.GetAllCompanyName_DataTable(url_cn, item.TimePhase, item.Title);
                DataTable result_en = Core.GetAllCompanyName_DataTable(url_en, item.TimePhase, item.Title);
                DataTable dt = S_Core.MergeTableColumn(result_cn, result_en);


                //添加到dataSet中
                dt.TableName = String.Format("第{0}期_{1}", item.TimePhase, item.Title);

                string fileName = String.Format("第{0}期_{1}", item.TimePhase, item.Title);
                fileName = fileName.Trim().Replace("进口:", "进口_").Replace(" ", "").Replace("&nbsp;", "").Replace("、", "");

                string flag = new ExcelHelper(String.Format("{0}/{1}.xls", floderPath, fileName), "").DatatableToExcel(dt);

                //获取当前类别数据的总条数
                int categoryDataTotal_cn = Core.GetCategoryDataCount(url_cn, XPath.CATEGORYDATATOTAL);
                int categoryDataTotal_en = Core.GetCategoryDataCount(url_en, XPath.CATEGORYDATATOTAL);

                //记录当前类别的条数
                string log = String.Format("{0}==>cn：{1}，en：{2}，collect:{3}==>{4}", dt.TableName, categoryDataTotal_cn, categoryDataTotal_en, dt.Rows.Count, categoryDataTotal_cn == dt.Rows.Count);
                Log.Info(log);
                if (categoryDataTotal_cn != dt.Rows.Count)
                {
                    Sb_ErrorCategory = Sb_ErrorCategory.AppendLine(log);
                }

                SingleTotal_site += categoryDataTotal_cn;
                SingleTotal += dt.Rows.Count;
                DS.Tables.Add(dt);

                //记录导出失败的日志
                if (flag != "")
                {
                    Log.Write(flag);
                }
            }
        }
    }
}
