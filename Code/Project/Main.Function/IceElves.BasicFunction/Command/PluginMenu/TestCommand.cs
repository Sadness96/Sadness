using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IceElves.Interface;

namespace IceElves.BasicFunction.Command.PluginMenu
{
    /// <summary>
    /// 测试Command
    /// </summary>
    public class TestCommand : MenuPluginInterface
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
            get { return "测试功能"; }
        }

        /// <summary>
        /// 功能分组
        /// </summary>
        public string strFunctionGroup
        {
            get { return "测试分组"; }
        }
    }
}
