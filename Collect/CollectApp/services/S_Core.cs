using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CollectApp.services
{
    /// <summary>
    /// 业务逻辑的核心代码
    /// </summary>
    public class S_Core
    {
        /// <summary>
        /// 合并表格，把英文名和中文名合并在一个表格里面
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DataTable MergeTableColumn(DataTable dt1,DataTable dt2) {

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt1.Rows[i]["id"].Equals(dt2.Rows[j]["id"]))
                    {
                        dt1.Rows[i]["en"] = dt2.Rows[j]["en"];
                        dt1.Rows[i]["lianying_en"] = dt2.Rows[j]["lianying_en"];
                        break;
                    }
                }
                //if (i < dt2.Rows.Count)
                //{
                //    dt1.Rows[i]["en"] = dt2.Rows[i]["en"];
                //}
                //else
                //{
                //    dt1.Rows[i]["en"] = "No English Name";
                //}
            }

            return dt1;
        }
    }
}
