using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO.Helper.Json
{
    /// <summary>
    /// DataTable数据模型
    /// 创建日期:2017年5月23日
    /// </summary>
    public class DataTableModel
    {
        /// <summary>
        /// DataTable表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 数据库表字段名和字段类型
        /// </summary>
        public Dictionary<string, string> dicFieldNameType { get; set; }

        /// <summary>
        /// DataTable数据
        /// </summary>
        public DataTable dtSourceData { get; set; }
    }
}
