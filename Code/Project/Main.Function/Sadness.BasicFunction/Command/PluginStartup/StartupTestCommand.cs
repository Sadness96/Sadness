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

        public void StartupItem()
        {
            throw new NotImplementedException();
        }

        public int iStartupIndex
        {
            get { return 1; }
        }

        public string strFunctionName
        {
            get { return "测试启动项"; }
        }
    }
}
