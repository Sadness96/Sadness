using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Helper.Registry
{
    /// <summary>
    /// 二级菜单数据模型
    /// 创建日期:2017年06月22日
    /// </summary>
    public class SecondaryMenuModel
    {
        /// <summary>
        /// 二级菜单键值名称
        /// </summary>
        public string strSecondaryMenuName { get; set; }

        /// <summary>
        /// 二级菜单显示名称
        /// </summary>
        public string strDisplayName { get; set; }

        /// <summary>
        /// 二级菜单软件启动路径
        /// </summary>
        public string strSoftwarePath { get; set; }

        /// <summary>
        /// 二级菜单显示图片路径
        /// </summary>
        public string strIcoPath { get; set; }
    }
}
