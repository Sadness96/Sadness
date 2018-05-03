using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sadness.Interface;

namespace Sadness.BasicFunction.Command.PluginStartup
{
    /// <summary>
    /// 启动项测试Command
    /// </summary>
    public class StartupTestCommand : StartupPluginInterface
    {
        /// <summary>
        /// 菜单单击事件
        /// </summary>
        public void StartupItem()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 启动顺序
        /// </summary>
        public int iStartupIndex
        {
            get { return 1; }
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string strFunctionName
        {
            get { return "测试启动项"; }
        }
    }
}
