using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadness.Interface
{
    /// <summary>
    /// 工具栏插件接口
    /// </summary>
    public interface ToolBarPluginInterface
    {
        /// <summary>
        /// 菜单单击事件
        /// </summary>
        void Click();

        /// <summary>
        /// 功能名称
        /// </summary>
        string strFunctionName { get; }
    }
}
