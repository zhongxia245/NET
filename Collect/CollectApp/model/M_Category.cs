using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectApp.model
{
    /// <summary>
    /// 类别实体
    /// </summary>
    public class M_Category
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string Param { get; set; }
        /// <summary>
        /// 属于第几期
        /// </summary>
        public int TimePhase { get; set; }

        /// <summary>
        /// 父类别名称
        /// </summary>
        public string ParentTitle { get; set; }
    }
}
