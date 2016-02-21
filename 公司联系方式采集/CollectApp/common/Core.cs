using CollectApp.model;
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
        #region 1.业务逻辑方法

        /// <summary>
        /// 把数据添加到表格中
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="item"></param>
        public static void Add2Dt(DataTable dt, Data item)
        {
            //数据清洗
            item = DataHandler(item);

            //添加到表格中
            if (item.CompanyName.Trim().Length != 0)
            {
                DataRow dr = dt.NewRow();
                dr["CompanyName"] = item.CompanyName;
                dr["Phase"] = item.Phase;
                dr["Contacts"] = item.Contacts;
                dr["Post"] = item.Post;
                dr["Telephone"] = item.Telephone;
                dr["MobilePhone"] = item.MobilePhone;
                dr["Fax"] = item.Fax;
                dr["Address"] = item.Address;
                dr["Country_Region"] = item.Country_Region;
                dr["Province"] = item.Province;
                dr["City"] = item.City;
                dr["Facebook"] = item.Facebook;
                dr["URL"] = item.URL;
                dt.Rows.Add(dr);
            }
        }

        /// <summary>
        /// 数据清洗
        /// </summary>
        /// <param name="item"></param>
        public static Data DataHandler(Data item)
        {
            Data item_new = new Data();
            item_new.CompanyName = item.CompanyName.Trim();

            //期数,正则匹配出第几期
            string phase = "";

            string pattern = "Phase[0-9]";//是字母或数字 至少出现一次
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            System.Text.RegularExpressions.MatchCollection mc = regex.Matches(item.Phase.Trim());
            for (int i = 0; i < mc.Count; i++)
            {
                if (phase.IndexOf(mc[i].Value) == -1)
                {
                    phase = mc[i].Value + "," + phase;
                }
            }

            if (phase.Length != 0)
            {
                phase = phase.Substring(0, phase.Length - 1);
            }
            phase = phase.Replace("Phase", "");
            item_new.Phase = phase;


            item_new.Contacts = item.Contacts.Trim();
            item_new.Post = item.Post.Trim();
            item_new.Telephone = item.Telephone.Trim().Split(new char[] { ':' })[1].Trim();
            item_new.MobilePhone = item.MobilePhone.Trim().Split(new char[] { ':' })[1].Trim();
            item_new.Fax = item.Fax.Trim().Split(new char[] { ':' })[1].Trim();
            item_new.Address = item.Address.Trim().Split(new char[] { ':' })[1].Trim();
            item_new.Country_Region = item.Country_Region.Split(new char[] { ':' })[1].Trim();
            item_new.Province = item.Province.Trim().Split(new char[] { ':' })[1].Trim();
            item_new.City = item.City.Trim().Split(new char[] { ':' })[1].Trim();
            item_new.Facebook = item.Facebook.Trim().Split(new char[] { ':' })[1].Trim();
            item_new.URL = item.URL.Trim();

            return item_new;
        }

        /// <summary>
        /// 根据需要采集的地址，和解析规则，采集回相对应的数据[列表数据 DataTable]
        /// </summary>
        /// <param name="url">需要采集的地址</param>
        /// <param name="xPath">解析规则[HtmlAgilityPack类库的解析规则]</param>
        /// <returns></returns>
        public static Data GetDataByUrl(string url)
        {
            Data data = new Data();
            HtmlAgilityPack.HtmlDocument doc = GetHTML(url);

            data.CompanyName = GetContentByXpath(doc, XPath.COMPANYNAME);

            Console.WriteLine(String.Format("url==>{0},company==>{1}", url, data.CompanyName));

            data.Phase = GetContentByXpath(doc, XPath.PHASE);
            data.Contacts = GetContentByXpath(doc, XPath.CONTACTS);
            data.Post = GetContentByXpath(doc, XPath.POST);

            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(XPath.TELEPHONE);
            data.Telephone = GetContentByCollection(collection, 0);
            data.MobilePhone = GetContentByCollection(collection, 1);
            data.Fax = GetContentByCollection(collection, 2);
            data.Address = GetContentByCollection(collection, 3);
            data.Country_Region = GetContentByCollection(collection, 4);
            data.Province = GetContentByCollection(collection, 5);
            data.City = GetContentByCollection(collection, 6);
            data.Facebook = GetContentByCollection(collection, 7);
            data.URL = url;
            return data;
        }

        /// <summary>
        /// 根据解析规则，获取文档里面的数据
        /// </summary>
        /// <param name="doc">HTML文档</param>
        /// <param name="xpath">解析规则</param>
        /// <param name="index">默认第几个下标的值</param>
        /// <returns>字符串数据</returns>
        public static string GetContentByXpath(HtmlAgilityPack.HtmlDocument doc, string xpath, int index = 0)
        {
            string result = "";
            HtmlAgilityPack.HtmlNodeCollection collection = doc.DocumentNode.SelectNodes(xpath);
            if (collection != null)
            {
                result = collection[index].InnerText;
            }
            return result;
        }

        /// <summary>
        /// 根据解析出来的集合，返回指定下标的值
        /// </summary>
        /// <param name="collection">匹配出的集合</param>
        /// <param name="index">下标</param>
        /// <returns></returns>
        public static string GetContentByCollection(HtmlAgilityPack.HtmlNodeCollection collection, int index = 0)
        {
            string result = "";
            if (collection != null)
            {
                result = collection[index].InnerText;
            }
            return result;
        }

        /// <summary>
        /// 生成表格，只有表格结构，没有数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmptyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CompanyName", System.Type.GetType("System.String"));
            dt.Columns.Add("Phase", System.Type.GetType("System.String"));
            dt.Columns.Add("Contacts", System.Type.GetType("System.String"));
            dt.Columns.Add("Post", System.Type.GetType("System.String"));
            dt.Columns.Add("Telephone", System.Type.GetType("System.String"));
            dt.Columns.Add("MobilePhone", System.Type.GetType("System.String"));
            dt.Columns.Add("Fax", System.Type.GetType("System.String"));
            dt.Columns.Add("Address", System.Type.GetType("System.String"));
            dt.Columns.Add("Country_Region", System.Type.GetType("System.String"));
            dt.Columns.Add("Province", System.Type.GetType("System.String"));
            dt.Columns.Add("City", System.Type.GetType("System.String"));
            dt.Columns.Add("Facebook", System.Type.GetType("System.String"));
            dt.Columns.Add("URL", System.Type.GetType("System.String"));
            return dt;
        }

        #endregion
        #region 2.基础核心方法

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
