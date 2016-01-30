using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
        /// <param name="url"></param>
        /// <returns></returns>
        public static DataTable GetAllCompanyName_DataTable(string url)
        {

            DataSet ds = new DataSet();
            DataTable dt;

            //获取文档
            HtmlAgilityPack.HtmlDocument doc = GetHTML(url);

            //获取当前类别总页数
            int totalPage = GetTotalPage(doc, XPath.TOTALPAGE);

            for (int i = 1; i <= totalPage; i++)
            {
                string url_page = url + "&page=" + i;
                ds.Tables.Add(GetDataTableByUrl(url_page, XPath.COMPANYNAME));
            }

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
        /// 根据需要采集的地址，和解析规则，采集回相对应的数据[列表数据 DataTable]
        /// </summary>
        /// <param name="url">需要采集的地址</param>
        /// <param name="xPath">解析规则[HtmlAgilityPack类库的解析规则]</param>
        /// <returns></returns>
        public static DataTable GetDataTableByUrl(string url, string xPath)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cn", System.Type.GetType("System.String"));
            dt.Columns.Add("en", System.Type.GetType("System.String"));

            HtmlAgilityPack.HtmlDocument doc = GetHTML(url);
            //获取所有公司的名称
            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(xPath);

            StringBuilder sb = new StringBuilder();
            if (collection != null)
            {
                foreach (HtmlAgilityPack.HtmlNode item in collection)
                {
                    DataRow dr = dt.NewRow();
                    dr["cn"] = item.InnerText;
                    dr["en"] = item.InnerText;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
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
        #endregion

    }
}
