using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadness.SQLiteDB.Models
{
    /// <summary>
    /// 系统菜单插件表数据模型
    /// </summary>
    public partial class ice_system_plugin_menu
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
        /// 功能分组
        /// </summary>
        public string ice_function_group { get; set; }
        /// <summary>
        /// 按钮样式
        /// </summary>
        public int ice_button_style { get; set; }
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
        /// RibbonPage 名称
        /// </summary>
        public string ice_page_home { get; set; }
        /// <summary>
        /// RibbonPageGroup 名称
        /// </summary>
        public string ice_page_group { get; set; }
        /// <summary>
        /// 按钮小图片(16x16)
        /// </summary>
        public byte[] ice_image_small { get; set; }
        /// <summary>
        /// 按钮大图片(32x32)
        /// </summary>
        public byte[] ice_image_large { get; set; }
    }
}
