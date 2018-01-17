using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper.TXT;
using Utils.Helper.ClosedForm;

namespace Utils.Helper.HookProc
{
    /// <summary>
    /// 钩子程序帮助类
    /// 创建日期:2017年6月7日
    /// </summary>
    public class HookProcHelper
    {
        /// <summary>
        /// 启动钩子测试程序
        /// </summary>
        public static void StartTestHookListeners()
        {
            TestFormHookListeners form = new TestFormHookListeners();
            form.Show();
        }

        /// <summary>
        /// 停止钩子测试程序
        /// </summary>
        public static void StopTestHookListeners()
        {
            ClosedFormHelper.ByHandleIsClosedForm(null, "Test for the class HookManager");
        }

        /// <summary>
        /// 启动记录电脑操作
        /// </summary>
        public static void StartRecordOperation()
        {
            RecordOperation form = new RecordOperation();
            form.Show();
        }

        /// <summary>
        /// 停止记录电脑操作(测试开启后无法关闭,直到所有线程关闭)
        /// </summary>
        public static void StopRecordOperation()
        {
            ClosedFormHelper.ByHandleIsClosedForm(null, "RecordOperation");
        }
    }
}
