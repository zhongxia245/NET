using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectApp.config
{
    public class Config
    {
        /// <summary>
        /// 采集网站的基本地址,中文版
        /// </summary>
        public static string BaseURL_CN = "http://i.cantonfair.org.cn/cn/expexhibitorlist.aspx";

        /// <summary>
        /// 采集网站的基本地址,英文版
        /// </summary>
        public static string BaseURL_EN = "http://i.cantonfair.org.cn/en/expexhibitorlist.aspx";

        /// <summary>
        /// 出口基本地址，中文
        /// </summary>
        public static string BaseURL_CN_Imp = "http://i.cantonfair.org.cn/cn/impexhibitorlist.aspx";

        /// <summary>
        /// 出口，基本地址，英文
        /// </summary>
        public static string BaseURL_EN_Imp = "http://i.cantonfair.org.cn/en/impexhibitorlist.aspx";

        /// <summary>
        /// 每页显示的条数,用来判断每次是否采集完成，不完成重新采集
        /// </summary>
        public static int PageSize = 10;
            
    }
}
