using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceElves.Interface
{
    /// <summary>
    /// 菜单插件接口
    /// </summary>
    public interface MenuPluginInterface
    {
        /// <summary>
        /// 菜单单击事件
        /// </summary>
        void Click();

        /// <summary>
        /// 功能名称
        /// </summary>
        string strFunctionName { get; }

        /// <summary>
        /// 功能分组
        /// </summary>
        string strFunctionGroup { get; }
    }
}
