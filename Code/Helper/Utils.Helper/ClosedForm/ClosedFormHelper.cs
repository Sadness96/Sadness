using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Utils.Helper.TXT;

namespace Utils.Helper.ClosedForm
{
    /// <summary>
    /// 关闭窗体帮助类
    /// 创建日期:2017年06月06日
    /// </summary>
    public class ClosedFormHelper
    {
        /// <summary>
        /// 通过句柄关闭窗体
        /// </summary>
        /// <param name="lpClassName">指向包含了窗口类名的空中止(C语言)字串的指针;或设为零,表示接收任何类</param>
        /// <param name="lpWindowName">指向包含了窗口文本(或标签)的空中止(C语言)字串的指针;或设为零,表示接收任何窗口标题</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool ByHandleIsClosedForm(string lpClassName, string lpWindowName)
        {
            try
            {
                int WM_CLOSE = 0x0010;
                IntPtr hwndCalc = FindWindow(lpClassName, lpWindowName);
                SendMessage(hwndCalc, WM_CLOSE, 0, 0);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获得指定窗口类名或窗口名的窗口的句柄
        /// </summary>
        /// <param name="lpClassName">指向包含了窗口类名的空中止(C语言)字串的指针;或设为零,表示接收任何类</param>
        /// <param name="lpWindowName">指向包含了窗口文本(或标签)的空中止(C语言)字串的指针;或设为零,表示接收任何窗口标题</param>
        /// <returns>执行成功,则返回值是拥有指定窗口类名或窗口名的窗口的句柄;执行失败,则返回值为NULL</returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 该函数将指定的消息发送到一个或多个窗口.此函数为指定的窗口调用窗口程序,直到窗口程序处理完消息再返回.而和函数PostMessage不同,PostMessage是将一个消息寄送到一个线程的消息队列后就立即返回.
        /// </summary>
        /// <param name="hWnd">其窗口程序将接收消息的窗口的句柄.如果此参数为HWND_BROADCAST,则消息将被发送到系统中所有顶层窗口,包括无效或不可见的非自身拥有的窗口、被覆盖的窗口和弹出式窗口,但消息不被发送到子窗口</param>
        /// <param name="Msg">指定被发送的消息</param>
        /// <param name="wParam">指定附加的消息特定信息</param>
        /// <param name="lParam">指定附加的消息特定信息</param>
        /// <returns>返回值指定消息处理的结果，依赖于所发送的消息</returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    }
}
