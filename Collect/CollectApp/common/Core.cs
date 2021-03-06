﻿using CollectApp.config;
using CollectApp.services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace CollectApp.common
{
    /// <summary>
    /// 爬虫的核心代码
    /// 1. 如何抓取出公司名称
    /// 2. 如何抓取出当前类别总页数
    /// 3. 如何根据url 和 解析规则 抓取出数据
    /// </summary>
    public class Core
    {

        #region 2. 爬虫，业务逻辑接口

        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetAll(string url)
        {
            Hashtable ht = new Hashtable();
            ht.Add(1, GetCategory(url, XPath.ONE));
            ht.Add(2, GetCategory(url, XPath.TWO));
            ht.Add(3, GetCategory(url, XPath.THREE));
            return ht;
        }
        

        /// <summary>
        /// 获取类别
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetCategory(string url, string xpath)
        {
            return GetHashtableByUrl(url, xpath);
        }

        /// <summary>
        /// 抓取指定站点公司的名称
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetAllCompanyName(string url)
        {
            //获取文档
            HtmlAgilityPack.HtmlDocument doc = GetHTML(url);

            //获取当前类别总页数
            int totalPage = GetTotalPage(doc, XPath.TOTALPAGE);

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= totalPage; i++)
            {
                string url_page = url + "&page=" + i;
                sb.Append(GetContentByUrl(url_page, XPath.COMPANYNAME));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 抓取指定站点公司的名称[DataTable]
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="timeStage">第几期</param>
        /// <param name="category">类别</param>
        /// <returns></returns>
        public static DataTable GetAllCompanyName_DataTable(string url,int timeStage,string category)
        {

            DataSet ds = new DataSet();
            DataTable dt;

            //获取文档
            HtmlAgilityPack.HtmlDocument doc = GetHTML(url);

            

            //获取当前类别总页数
            int totalPage = GetTotalPage(doc, XPath.TOTALPAGE);
            Console.WriteLine(String.Format("totalPage:{0}", totalPage));

            for (int i = 1; i <= totalPage; i++)
            {
                string url_page = url + "&page=" + i;
                Console.WriteLine(String.Format("url_page:{0}", url_page));
                // Thread.Sleep(500);

                while (true) {
                    DataTable subDt = GetDataTableByUrl(url_page, XPath.COMPANYNAME, timeStage, category);
                    //如果不是最后一页，或者采集不满10条，则表示采集不完整,重新采集
                    if (i != totalPage && subDt.Rows.Count != Config.PageSize)
                    {
                        Thread.Sleep(500);
                        Log.Info(String.Format("采集第{0}期，类别：{1}，条数不满10条:{2},重新采集中...", timeStage, category, subDt.Rows.Count));
                    }
                    else {
                        ds.Tables.Add(subDt);
                        break;
                    }
                }
            }

            //合并所有表格
            dt = GetAllDataTable(ds);

            return dt;
        }


        /// <summary>
        /// 获取当前类别公司的总页数
        /// </summary>
        /// <param name="doc">当前页面文档</param>
        /// <param name="xPath">解析规则</param>
        /// <returns></returns>
        public static int GetTotalPage(HtmlAgilityPack.HtmlDocument doc, string xPath)
        {
            string str_totalPage = GetContentByDoc(doc, xPath);
            str_totalPage = str_totalPage.Replace("\r\n", "");
            str_totalPage = str_totalPage.Replace("\t", "");

            string[] arr = str_totalPage.Split(new char[] { '/' });
            int totalPage = 1;
            if (arr.Length == 2)
            {
                totalPage = Int16.Parse(arr[1]);
            }
            return totalPage;
        }

        #endregion

        #region 1. 爬虫的核心底层接口

        #region 1.1 返回字符串的数据


        /// <summary>
        /// 根据需要采集的地址，和解析规则，采集回相对应的数据
        /// </summary>
        /// <param name="url">需要采集的地址</param>
        /// <param name="xPath">解析规则[HtmlAgilityPack类库的解析规则]</param>
        /// <returns></returns>
        public static string GetContentByUrl(string url, string xPath)
        {
            HtmlAgilityPack.HtmlDocument doc = GetHTML(url);
            //获取所有公司的名称
            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(xPath);

            StringBuilder sb = new StringBuilder();
            int count = 0;
            if (collection != null)
            {
                foreach (HtmlAgilityPack.HtmlNode item in collection)
                {
                    count++;
                    sb.Append(string.Format("{0}:{1}:{2}\r\n", count, item.InnerText, item.Attributes["href"].Value));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 根据HTML文档，和解析规则，解析出需要的数据
        /// </summary>
        /// <param name="doc">html文档【HtmlAgilityPack.HtmlDocument对象】</param>
        /// <param name="xPath">解析规则[HtmlAgilityPack类库的解析规则]</param>
        /// <returns></returns>
        public static string GetContentByDoc(HtmlAgilityPack.HtmlDocument doc, string xPath)
        {
            //获取所有公司的名称
            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(xPath);

            StringBuilder sb = new StringBuilder();
            int count = 0;
            if (collection != null)
            {
                foreach (HtmlAgilityPack.HtmlNode item in collection)
                {
                    count++;
                    sb.Append(string.Format("{0}:{1}\r\n", count, item.InnerText));
                }
            }
            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// 根据需要采集的地址，和解析规则，采集回相对应的数据[列表数据 DataTable]
        /// </summary>
        /// <param name="url">需要采集的地址</param>
        /// <param name="xPath">解析规则[HtmlAgilityPack类库的解析规则]</param>
        /// <param name="timeStage">第几期</param>
        /// <param name="category">类别</param>
        /// <returns></returns>
        public static DataTable GetDataTableByUrl(string url, string xPath,int timeStage=1,string category="")
        {
            DataTable dt = GetEmptyDataTable();

            HtmlAgilityPack.HtmlDocument doc = GetHTML(url);
            //获取所有公司的名称
            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(xPath);

            StringBuilder sb = new StringBuilder();
            if (collection != null)
            {
                string preId = "";
               
                foreach (HtmlAgilityPack.HtmlNode item in collection)
                {
                    DataRow dr = dt.NewRow();
                    //获取企业ID
                    string href = item.Attributes["href"] != null ? item.Attributes["href"].Value : "";
                    string[] strs = href.Split(new char[] { '=' });
                    string id = strs.Length == 2 ? strs[1] : "";
                    preId = id;

                    Console.WriteLine(String.Format("id:{0} || name:{1}",id,item.InnerText));
                    if (id != "")
                    {
                        dr["type"] = String.Format("第{0}期",timeStage);
                        dr["category"] = category;
                        dr["id"] = id;
                        dr["cn"] = item.InnerText;
                        dr["en"] = item.InnerText;
                        dt.Rows.Add(dr);
                    }
                    else {
                        dt.Rows[dt.Rows.Count - 1]["lianying_cn"] = item.InnerText.Replace("联营：", "").Replace("Pooling Enterprise:","").Replace("(", "").Replace(")", "");
                        dt.Rows[dt.Rows.Count - 1]["lianying_en"] = item.InnerText.Replace("联营：", "").Replace("Pooling Enterprise:", "").Replace("(", "").Replace(")", "");
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取该类别的数据总条数
        /// </summary>
        /// <param name="doc">HTML文档</param>
        /// <param name="xPath">解析规则</param>
        /// <returns></returns>
        public static int GetCategoryDataCount(string url, string xPath)
        {
            HtmlAgilityPack.HtmlDocument doc = GetHTML(url);
            int total = 0;
            //获取所有公司的名称
            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(xPath);
            if (collection != null) {
                string strtotal =  collection[0].InnerText;
                total = GetNumberInt(strtotal);
            }
            return total;
        }

      /// <summary>
        /// 获取字符串中的数字 
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
        public static int GetNumberInt(string str)
        {
            int result = 0;
            if (str != null && str != string.Empty)
            {
                str = str.Replace("\r\n", "").Replace(" ", "");
                str = Regex.Replace(str, @"[^\d]*", "");
                try
                {
                    result = int.Parse(str);
                }
                catch (Exception)
                {
                }
            }
            return result;
        } 


        /// <summary>
        /// 根据需要采集的地址，和解析规则，采集回相对应的数据[列表数据 DataTable]
        /// </summary>
        /// <param name="url">需要采集的地址</param>
        /// <param name="xPath">解析规则[HtmlAgilityPack类库的解析规则]</param>
        /// <returns></returns>
        public static Hashtable GetHashtableByUrl(string url, string xPath)
        {
            Hashtable ht = new Hashtable();
            HtmlAgilityPack.HtmlDocument doc = GetHTML(url);
            //获取所有公司的名称
            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(xPath);

            StringBuilder sb = new StringBuilder();
            if (collection != null)
            {
                foreach (HtmlAgilityPack.HtmlNode item in collection)
                {
                    //获取企业ID
                    string href = item.Attributes["href"] != null ? item.Attributes["href"].Value : "";
                    string[] strs = href.Split(new char[] { '?' });
                    string categryId = strs.Length == 2 ? strs[1] : "";
                    ht.Add(categryId, item.InnerText);
                }
            }
            return ht;
        }


        /// <summary>
        /// 根据URL地址，采集会完整的HTML文档
        /// </summary>
        /// <param name="url">网站地址</param>
        /// <returns></returns>
        public static HtmlAgilityPack.HtmlDocument GetHTML(string url)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //获取Html页面代码
            string html = HTMLHelper.Get_Http(url.Trim());
            //第二步加载html文档
            doc.LoadHtml(html);
            return doc;
        }

        /// <summary>
        /// 合并DataTable
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static DataTable GetAllDataTable(DataSet ds)
        {
            DataTable newDataTable = ds.Tables[0].Clone();                //创建新表 克隆以有表的架构。
            object[] objArray = new object[newDataTable.Columns.Count];   //定义与表列数相同的对象数组 存放表的一行的值。
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    ds.Tables[i].Rows[j].ItemArray.CopyTo(objArray, 0);    //将表的一行的值存放数组中。
                    newDataTable.Rows.Add(objArray);                       //将数组的值添加到新表中。
                }
            }
            return newDataTable;                                           //返回新表。
        }

        /// <summary>
        /// 生成表格，只有表格结构，没有数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmptyDataTable() {
            DataTable dt = new DataTable();
            dt.Columns.Add("type", System.Type.GetType("System.String"));
            dt.Columns.Add("category", System.Type.GetType("System.String"));
            dt.Columns.Add("id", System.Type.GetType("System.String"));
            dt.Columns.Add("cn", System.Type.GetType("System.String"));
            //联营
            dt.Columns.Add("lianying_cn", System.Type.GetType("System.String"));
            dt.Columns.Add("en", System.Type.GetType("System.String"));
            dt.Columns.Add("lianying_en", System.Type.GetType("System.String"));
            return dt;
        }
        #endregion

    }
}
