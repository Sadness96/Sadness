using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils.Helper.TXT;

namespace Utils.Helper.WaitingMessage
{
    public class WaitMessage
    {
        /// <summary>
        /// 创建窗体
        /// </summary>
        private static WaitForm wait = null;
        /// <summary>
        /// 打开进展
        /// </summary>
        private static bool openprogress = false;

        /// <summary>
        /// 显示等待状态
        /// </summary>
        /// <param name="title">标题</param>
        public static void Show(string title)
        {
            innerShow(title, new Size());
        }

        /// <summary>
        /// 显示等待状态
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="size">大小</param>
        public static void Show(string title, Size size)
        {
            innerShow(title, size);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public static void Close()
        {
            try
            {
                openprogress = false;
                if (wait != null)
                {
                    wait.Close();
                }
                wait = null;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                try
                {
                    if (wait != null)
                    {
                        wait.Close();
                    }
                    wait = null;
                }
                catch (Exception)
                { }
            }
        }

        /// <summary>
        /// 等待状态
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="showtime">显示时间</param>
        /// <param name="size">窗体大小</param>
        internal static void innerShow(string title, Size size)
        {
            if (wait == null)
            {
                wait = new WaitForm();
            }

            if (size != null && size.Width != 0 && size.Height != 0)
            {
                wait.Width = size.Width;
                wait.Height = size.Height;
            }

            if (wait != null)
            {
                wait.ProgressInfo = title;
            }

            if (!openprogress)
            {
                ThreadStart threadStart = innerShowForm;
                Thread thread = new Thread(threadStart) { IsBackground = true };
                thread.Start();
                openprogress = true;
            }
        }

        /// <summary>
        /// 线程上执行的方法
        /// </summary>
        internal static void innerShowForm()
        {
            try
            {
                if (wait != null)
                {
                    wait.Show();
                }
                if (wait != null)
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        wait.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
        }
    }
}
