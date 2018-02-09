using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadness.Interface
{
    /// <summary>
    /// 启动项插件接口
    /// </summary>
    public interface StartupPluginInterface
    {
        /// <summary>
        /// 菜单单击事件
        /// </summary>
        void StartupItem();

        /// <summary>
        /// 启动顺序
        /// </summary>
        int iStartupIndex { get; }

        /// <summary>
        /// 功能名称
        /// </summary>
        string strFunctionName { get; }
    }
}
