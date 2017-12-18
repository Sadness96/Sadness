using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using Main.Ribbon.ViewModels;

namespace Main.Ribbon.Views
{
    /// <summary>
    /// MainRibbon.xaml 的交互逻辑
    /// </summary>
    public partial class MainRibbon : DXRibbonWindow
    {
        /// <summary>
        /// MainRibbon.xaml 的构造函数
        /// </summary>
        public MainRibbon()
        {
            InitializeComponent();
            this.Loaded += delegate
            {
                CloseEventDelegate();
            };
            this.DataContext = new MainRibbonViewModel();
        }

        /// <summary>
        /// 关闭委托
        /// </summary>
        public delegate void CloseDelegate();
        /// <summary>
        /// 关闭事件
        /// </summary>
        public event CloseDelegate CloseEvent;

        /// <summary>
        /// 关闭登录窗口路由通知
        /// </summary>
        private void CloseEventDelegate()
        {
            if (CloseEvent != null)
            {
                CloseEvent.Invoke();
            }
        }
    }
}
