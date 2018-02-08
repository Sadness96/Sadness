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
        /// 关联ID : RibbonPageGroup 关联至 RibbonPage
        /// </summary>
        public int ice_parid { get; set; }
        /// <summary>
        /// 功能序号：
        /// 1.ice_page_ishome = true : RibbonPage序号
        /// 2.ice_page_ishome = false : RibbonPageGroup序号
        /// </summary>
        public int ice_number { get; set; }
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
