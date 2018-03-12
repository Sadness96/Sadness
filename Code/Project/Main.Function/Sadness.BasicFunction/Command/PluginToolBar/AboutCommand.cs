using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sadness.Interface;
using Sadness.BasicFunction.Views.PluginToolBar;

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
            About form = new About();
            form.ShowDialog();
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
