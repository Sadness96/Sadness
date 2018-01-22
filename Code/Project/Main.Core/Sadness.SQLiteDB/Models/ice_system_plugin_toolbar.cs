using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadness.SQLiteDB.Models
{
    /// <summary>
    /// 工具栏插件表数据模型
    /// </summary>
    public partial class ice_system_plugin_toolbar
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ice_id { get; set; }
        /// <summary>
        /// 显示序号
        /// </summary>
        public int ice_number { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string ice_function_name { get; set; }
        /// <summary>
        /// 功能别名
        /// </summary>
        public string ice_function_alias { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool ice_button_visible { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool ice_button_enabled { get; set; }
        /// <summary>
        /// 类库文件路径
        /// </summary>
        public string ice_dllfile_path { get; set; }
        /// <summary>
        /// 类库文件 Class
        /// </summary>
        public string ice_dllfile_class { get; set; }
        /// <summary>
        /// 按钮图片(16x16)
        /// </summary>
        public byte[] ice_image { get; set; }
    }
}
