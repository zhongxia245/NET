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
        public static string BaseURL_EN_Imp = "http://i.cantonfair.org.cn/cn/impexhibitorlist.aspx";
        /// <summary>
        /// 获取第一期类别
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetOne(){
             Hashtable ht = new Hashtable();
             ht.Add(411, "电子消费品及信息产品");
             ht.Add(412, "电子电气产品");
             ht.Add(413, "动力、电力设备");
             ht.Add(414, "新能源");
             ht.Add(415, "家用电器");
             ht.Add(416, "照明产品");
             ht.Add(417, "建筑及装饰材料");
             ht.Add(418, "自行车");
             ht.Add(419, "摩托车");
             ht.Add(420, "汽车配件");
             ht.Add(421, "车辆");
             ht.Add(422, "通用机械、小型加工机械及工业零部件");
             ht.Add(423, "大型机械及设备");
             ht.Add(424, "工程农机（室内）");
             ht.Add(425, "电子消费品及信息产品");
             ht.Add(426, "电子消费品及信息产品");
             return ht;
        }
            
    }
}
