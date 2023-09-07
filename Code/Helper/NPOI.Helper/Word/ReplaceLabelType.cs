using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPOI.Helper.Word
{
    /// <summary>
    /// 替换 Word 模板数据类型枚举类
    /// 创建日期:2017年06月05日
    /// </summary>
    public class ReplaceLabelType
    {
        public enum LabelType
        {
            /// <summary>
            /// 文本类型,一个标签替换一个文本
            /// </summary>
            Text = 0,
            /// <summary>
            /// DataTable表格类型,替换表格中的一组数据
            /// </summary>
            DataTable = 1,
            /// <summary>
            /// 图片类型,一个标签替换一张图片
            /// </summary>
            Image = 2
        }
    }
}
