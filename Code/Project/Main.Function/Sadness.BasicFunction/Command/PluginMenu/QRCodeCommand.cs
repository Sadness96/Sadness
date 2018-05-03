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
    /// 二维码生成
    /// </summary>
    public class QRCodeCommand : MenuPluginInterface
    {
        /// <summary>
        /// Click Command
        /// </summary>
        public void Click()
        {
            QRCode form = new QRCode();
            form.ShowDialog();
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string strFunctionName
        {
            get { return "二维码生成"; }
        }

        /// <summary>
        /// 功能分组
        /// </summary>
        public string strFunctionGroup
        {
            get { return "二维码生成"; }
        }
    }
}
