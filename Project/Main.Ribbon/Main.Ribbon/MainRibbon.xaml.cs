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
using DevExpress.Utils.About;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;

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
            if (base.Height > SystemParameters.VirtualScreenHeight || base.Width > SystemParameters.VirtualScreenWidth)
            {
                base.WindowState = WindowState.Maximized;
            }
            UAlgo.Default.DoEventObject(1, 3, this);
        }

        /// <summary>
        /// MainRibbon 的Load方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainRibbon_Loaded(object sender, RoutedEventArgs e)
        {
            //修改基础样式(默认RibbonStyle.Office2007)
            RibbonControl.RibbonStyle = RibbonStyle.Office2010;
            //设置软件图标
            //RibbonControl.ApplicationButtonSmallIcon = new BitmapImage(new Uri(@"F:\IC#\Ribbon_Code_Demo\Ribbon_Code_Demo\Image\10n.ico", UriKind.Absolute));
            //RibbonControl.ApplicationButtonLargeIcon = new BitmapImage(new Uri(@"F:\IC#\Ribbon_Code_Demo\Ribbon_Code_Demo\Image\10n.ico", UriKind.Absolute));
            //设置对齐方式
            RibbonControl.ApplicationButtonText = "File";
            RibbonControl.PageCategoryAlignment = RibbonPageCategoryCaptionAlignment.Right;
            //创建RibbonPage
            RibbonPage ribbonPageHome = new RibbonPage();
            ribbonPageHome.Caption = "Home";
            //创建RibbonPageGroup
            RibbonPageGroup ribbonPageGroup = new RibbonPageGroup();
            ribbonPageGroup.Caption = "File";
            //创建BarButtonItem
            BarButtonItem barButton = new BarButtonItem();
            barButton.RibbonStyle = RibbonItemStyles.Large;
            barButton.Content = "New";
            //barButton.LargeGlyph = new BitmapImage(new Uri(@"F:\IC#\Ribbon_Code_Demo\Ribbon_Code_Demo\Image\10n.ico", UriKind.Absolute));

            ribbonPageGroup.ItemLinks.Add(barButton);

            ribbonPageHome.Groups.Add(ribbonPageGroup);

            PageCategory.Pages.Add(ribbonPageHome);
        }
    }
}
