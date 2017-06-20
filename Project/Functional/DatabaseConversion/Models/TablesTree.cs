using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConversion.Models
{
    /// <summary>
    /// 表名Tree数据模型
    /// </summary>
    public class TablesTree
    {
        /// <summary>
        /// 选择
        /// </summary>
        public bool Choose { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string Table { get; set; }
    }
}
