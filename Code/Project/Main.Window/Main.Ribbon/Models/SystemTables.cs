using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Ribbon.Models
{
    /// <summary>
    /// 系统表模型
    /// </summary>
    public static class SystemTables
    {
        /// <summary>
        /// 系统图片表
        /// </summary>
        public static string ice_system_images { get { return "ice_system_images"; } }

        /// <summary>
        /// 系统菜单插件表
        /// </summary>
        public static string ice_system_plugin_menu { get { return "ice_system_plugin_menu"; } }

        /// <summary>
        /// 系统菜单插件分组表
        /// </summary>
        public static string ice_system_plugin_menugroup { get { return "ice_system_plugin_menugroup"; } }

        /// <summary>
        /// 工具栏插件表
        /// </summary>
        public static string ice_system_plugin_toolbar { get { return "ice_system_plugin_toolbar"; } }
    }
}
