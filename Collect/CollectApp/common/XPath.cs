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
    }
}
