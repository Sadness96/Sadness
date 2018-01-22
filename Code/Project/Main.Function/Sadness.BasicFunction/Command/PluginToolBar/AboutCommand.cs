using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sadness.Interface;

namespace Sadness.BasicFunction.Command.PluginToolBar
{
    /// <summary>
    /// 关于Command
    /// </summary>
    public class AboutCommand : ToolBarPluginInterface
    {
        /// <summary>
        /// Click Command
        /// </summary>
        public void Click()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string strFunctionName
        {
            get { return "关于"; }
        }
    }
}
