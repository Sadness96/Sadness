using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Main.Ribbon.Views;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using Prism.Commands;
using Microsoft.Practices.Unity;

namespace Main.Ribbon.ViewModels
{
    /// <summary>
    /// MainRibbon.xaml 的视图模型
    /// </summary>
    public class MainRibbonViewModel
    {
        /// <summary>
        /// MainRibbon.xaml 的视图模型
        /// </summary>
        public MainRibbonViewModel()
        {
            //委托Load方法
            LoadedCommand = new DelegateCommand<Window>(Loaded);
            //修改基础样式(默认RibbonStyle.Office2007)
            MainRibbonStyle = RibbonStyle.Office2010;
            //设置软件图标
            //RibbonControl.ApplicationButtonSmallIcon = new BitmapImage(new Uri(@"F:\IC#\Ribbon_Code_Demo\Ribbon_Code_Demo\Image\10n.ico", UriKind.Absolute));
            //RibbonControl.ApplicationButtonLargeIcon = new BitmapImage(new Uri(@"F:\IC#\Ribbon_Code_Demo\Ribbon_Code_Demo\Image\10n.ico", UriKind.Absolute));
            //设置对齐方式
            MainApplicationButtonText = "File";
            MainRibbonPageCategoryAlignment = RibbonPageCategoryCaptionAlignment.Right;
        }

        /// <summary>
        /// MainRibbon窗体Load事件
        /// </summary>
        /// <param name="window">主窗体</param>
        private void Loaded(Window window)
        {
            //生成Ribbon控件
            RibbonDefaultPageCategory PageCategory = window.FindName("PageCategory") as RibbonDefaultPageCategory;
            if (PageCategory != null)
            {
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

        /// <summary>
        /// 主界面Ribbon风格
        /// </summary>
        private RibbonStyle _mainRibbonStyle;
        /// <summary>
        /// 主界面Ribbon风格
        /// </summary>
        public RibbonStyle MainRibbonStyle
        {
            get
            {
                return _mainRibbonStyle;
            }
            set
            {
                _mainRibbonStyle = value;
            }
        }

        /// <summary>
        /// 应用按钮文字
        /// </summary>
        private string _mainApplicationButtonText;
        /// <summary>
        /// 应用按钮文字
        /// </summary>
        public string MainApplicationButtonText
        {
            get
            {
                return _mainApplicationButtonText;
            }
            set
            {
                _mainApplicationButtonText = value;
            }
        }

        /// <summary>
        /// 类别对齐
        /// </summary>
        private RibbonPageCategoryCaptionAlignment _mainRibbonPageCategoryAlignment;
        /// <summary>
        /// 类别对齐
        /// </summary>
        public RibbonPageCategoryCaptionAlignment MainRibbonPageCategoryAlignment
        {
            get
            {
                return _mainRibbonPageCategoryAlignment;
            }
            set
            {
                _mainRibbonPageCategoryAlignment = value;
            }
        }

        /// <summary>
        /// Load命令
        /// </summary>
        private ICommand _loadedCommand;
        /// <summary>
        /// Load命令
        /// </summary>
        public ICommand LoadedCommand
        {
            get
            {
                return _loadedCommand;
            }
            set
            {
                _loadedCommand = value;
            }
        }
    }
}
