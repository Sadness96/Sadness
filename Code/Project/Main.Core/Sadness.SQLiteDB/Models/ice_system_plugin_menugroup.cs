using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadness.SQLiteDB.Models
{
    /// <summary>
    /// 系统菜单插件分组表数据模型
    /// </summary>
    public partial class ice_system_plugin_menugroup
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ice_id { get; set; }
        /// <summary>
        /// RibbonPage 序号
        /// </summary>
        public int ice_number_home { get; set; }
        /// <summary>
        /// RibbonPageGroup 序号
        /// </summary>
        public int ice_number_group { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string ice_page_name { get; set; }
        /// <summary>
        /// True:RibbonPage
        /// False:RibbonPageGroup
        /// </summary>
        public bool ice_page_ishome { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool ice_page_visible { get; set; }
    }
}
