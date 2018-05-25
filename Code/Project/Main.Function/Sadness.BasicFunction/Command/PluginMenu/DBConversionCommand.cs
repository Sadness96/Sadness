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
    /// 数据转换
    /// </summary>
    public class DBConversionCommand : MenuPluginInterface
    {
        /// <summary>
        /// Click Command
        /// </summary>
        public void Click()
        {
            DBConversion form = new DBConversion();
            form.ShowDialog();
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string strFunctionName
        {
            get { return "数据转换"; }
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
