using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Utils.Helper.TXT;

namespace Utils.Helper.Shutdown
{
    /// <summary>
    /// 加密注册码帮助类
    /// 创建日期:2017年6月6日
    /// </summary>
    public class ShutdownHelper
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        { public int Count; public long Luid; public int Attr;}
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int flg, int rea);
        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        internal const int EWX_LOGOFF = 0x00000000;
        internal const int EWX_SHUTDOWN = 0x00000001;
        internal const int EWX_REBOOT = 0x00000002;
        internal const int EWX_FORCE = 0x00000004;
        internal const int EWX_POWEROFF = 0x00000008;
        internal const int EWX_FORCEIFHUNG = 0x00000010;

        /// <summary>
        /// 关闭Windows
        /// </summary>
        /// <param name="flg"></param>
        private static void DoExitWin(int flg)
        {
            bool ok;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1; tp.Luid = 0; tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }

        /// <summary>
        /// 关闭计算机
        /// </summary>
        public static void Shutdown()
        {
            try
            {
                DoExitWin(EWX_SHUTDOWN);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
        }

        /// <summary>
        /// 注销计算机
        /// </summary>
        public static void Logoff()
        {
            try
            {
                DoExitWin(EWX_LOGOFF);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
        }

        /// <summary>
        /// 重启计算机
        /// </summary>
        public static void Reboot()
        {
            try
            {
                DoExitWin(EWX_REBOOT);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
        }
    }
}
