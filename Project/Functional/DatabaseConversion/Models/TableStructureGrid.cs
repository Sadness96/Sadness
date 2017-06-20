using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvvmToolkit;

namespace DatabaseConversion.Models
{
    /// <summary>
    /// 表字段Grid数据模型
    /// </summary>
    public class TableStructureGrid : ModelBase<TableStructureGrid>
    {
        /// <summary>
        /// 选择
        /// </summary>
        public bool Choose { get; set; }

        /// <summary>
        /// 源表列名(不允许修改)
        /// </summary>
        public string SourceFieldName { get; set; }

        /// <summary>
        /// 目标表列名
        /// </summary>
        public string TargetFieldName { get; set; }

        /// <summary>
        /// 表字段类型
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// 表字段列长度
        /// </summary>
        public string FieldLength { get; set; }

        /// <summary>
        /// 表字段列小数位数
        /// </summary>
        public string DecimalLength { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool PrimaryKey { get; set; }
    }
}
