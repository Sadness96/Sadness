using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sadness.Interface;
using Sadness.BasicFunction.Views.PluginMenu;

namespace Sadness.BasicFunction.Command.PluginMenu
{
    /// <summary>
    /// 注册工具
    /// </summary>
    public class RegistrationToolCommand : MenuPluginInterface
    {
        /// <summary>
        /// Click Command
        /// </summary>
        public void Click()
        {
            RegistrationTool form = new RegistrationTool();
            form.ShowDialog();
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string strFunctionName
        {
            get { return "注册工具"; }
        }

        /// <summary>
        /// 功能分组
        /// </summary>
        public string strFunctionGroup
        {
            get { return "基础功能"; }
        }
    }
}
