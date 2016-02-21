using CollectApp.common;
using CollectApp.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CollectApp.services
{
    class S_SingleThreadConfig
    {

        //采集的总共条数
        public static int Total = 0;

        //当前采集的下标
        public static int CurrentIndex = 0;

        /// <summary>
        /// 采集的表格
        /// </summary>
        public static DataTable CollectDt;

        //导出的文件夹路径
        public static string floderPath = "";

        public static int Start = 0;

        public static int End = 0;
        

        /// <summary>
        /// 采集并导出的方法[根据类别采集]
        /// </summary>
        public static void Collect2Export()
        {
            CurrentIndex = Start;
            var baseUrl = "http://www.e-cantonfair.com/china-supplier/";
            //采集数据
            DataTable dt = Core.GetEmptyDataTable();
            for (int i = Start; i <= End; i++)
            {
                var url = baseUrl + String.Format("contact-us-{0}.html", i);
                Data data = Core.GetDataByUrl(url);
                Core.Add2Dt(dt, data);
                CurrentIndex = i;
            }

            //添加到dataSet中
            string fileName = String.Format("{0}-{1}采集结果", Start, End);
            dt.TableName = fileName;

            CollectDt = dt;

            Hashtable ht = GetHashtable();

            string flag = new ExcelHelper(String.Format("{0}/{1}.xls", floderPath, fileName), "").DatatableToExcel(dt,ht);

            //Log.Info(log);

            //记录导出失败的日志
            if (flag != "")
            {
                Log.Write(flag);
            }
        }

        /// <summary>
        /// 获取Excel的表头
        /// </summary>
        /// <returns></returns>
        private static Hashtable GetHashtable()
        {
            Hashtable ht = new Hashtable();

            ht.Add("CompanyName", "公司名");
            ht.Add("Phase", "期数");
            ht.Add("Contacts", "联系人");
            ht.Add("Post", "职务");
            ht.Add("Telephone", "电话");
            ht.Add("MobilePhone", "移动电话");
            ht.Add("Fax", "传真");
            ht.Add("Address", "地址");
            ht.Add("Country_Region", "国家");
            ht.Add("Province", "省份");
            ht.Add("City", "城市");
            ht.Add("Facebook", "Facebook");
            ht.Add("URL", "URL");

            return ht;
        }
    }
}

