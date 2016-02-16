using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectApp.common
{
    public class XPath
    {
        /// <summary>
        /// 解析公司名称【中英文一样】
        /// </summary>
        public static  string COMPANYNAME = "//div[@class='exhibitor_list']/div[@class='company']/h3/a";
        /// <summary>
        /// 解析公司名称总页数
        /// </summary>
        public static  string TOTALPAGE = "//div[@id='AspNetPager1']/div[1]";

        /// <summary>
        /// 解析每个类别的数据总条数
        /// </summary>
        public static string CATEGORYDATATOTAL = "//div[@class='list_info']";

        /// <summary>
        /// 第一期类别
        /// </summary>
        public static string ONE = "//ul[@id='phase1']/li/a";

        /// <summary>
        /// 第二期类别
        /// </summary>
        public static string TWO = "//ul[@id='phase2']/li/a";

        /// <summary>
        /// 第三期类别
        /// </summary>
        public static string THREE = "//ul[@id='phase3']/li/a";

    }
}
