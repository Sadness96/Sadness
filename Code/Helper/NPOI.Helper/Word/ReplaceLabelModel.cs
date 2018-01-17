using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPOI.Helper.Word
{
    /// <summary>
    /// 替换Word模板Model
    /// 创建日期:2017年6月15日
    /// </summary>
    public class ReplaceLabelModel
    {
        /// <summary>
        /// 替换标签名称
        /// </summary>
        public string strLabelName { get; set; }

        /// <summary>
        /// 替换标签类型
        /// </summary>
        public ReplaceLabelType.LabelType lableType { get; set; }

        /// <summary>
        /// 替换的文本
        /// </summary>
        public string strReplaceText { get; set; }

        /// <summary>
        /// 替换的图片路径
        /// </summary>
        public string strReplaceImagePath { get; set; }

        /// <summary>
        /// 替换的表格
        /// </summary>
        public DataTable dtReplaceData { get; set; }
    }
}
