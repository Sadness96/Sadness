using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utils.Helper.WaitingMessage
{
    /// <summary>
    /// 等待窗体
    /// </summary>
    public partial class WaitForm : Form
    {
        /// <summary>
        /// WaitForm 的构造函数
        /// </summary>
        public WaitForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.ShowInTaskbar = false;
        }

        /// <summary>
        /// 处理进度信息
        /// </summary>
        public string ProgressInfo
        {
            get { return lblProgressInfo.Text; }
            set { lblProgressInfo.Text = value; }
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        public override void Refresh()
        {
            Application.DoEvents();
        }
    }
}
