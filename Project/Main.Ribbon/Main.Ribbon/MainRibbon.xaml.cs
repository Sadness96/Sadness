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

namespace Main.Ribbon
{
    /// <summary>
    /// MainRibbon.xaml 的交互逻辑
    /// </summary>
    public partial class MainRibbon : DXRibbonWindow
    {
        /// <summary>
        /// MainRibbon 的构造函数
        /// </summary>
        public MainRibbon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 菜单下分组按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupFile_CaptionButtonClick(object sender, RibbonCaptionButtonClickEventArgs e)
        {

        }
    }
}
