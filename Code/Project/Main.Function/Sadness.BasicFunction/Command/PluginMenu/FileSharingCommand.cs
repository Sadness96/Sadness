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
    /// 文件共享
    /// </summary>
    public class FileSharingCommand : MenuPluginInterface
    {
        /// <summary>
        /// Click Command
        /// </summary>
        public void Click()
        {
            FileSharing form = new FileSharing();
            form.ShowDialog();
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string strFunctionName
        {
            get { return "文件共享"; }
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
