using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using Main.Ribbon.Utils;
using Main.Ribbon.Views;
using Prism.Commands;
using IceElves.SQLiteDB.Connect;

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
            LoadedCommand = new DelegateCommand<Window>(Window_Loaded);
            //设置软件标题
            MainTitle = "MainWindow";
            //修改基础样式(默认RibbonStyle.Office2007)
            MainRibbonStyle = RibbonStyle.Office2007;
            //设置软件图标
            MainAppSmallIcon = OperationImage.ByteArrayToImageSource(MainImage.GetImageByteArray("AppSmallIcon"));
            MainAppLargeIcon = OperationImage.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
            //设置应用按钮文字
            MainApplicationButtonText = "File";
            //设置对齐方式
            MainRibbonPageCategoryAlignment = RibbonPageCategoryCaptionAlignment.Right;
        }

        /// <summary>
        /// MainRibbon窗体Load事件
        /// </summary>
        /// <param name="window">主窗体</param>
        private void Window_Loaded(Window window)
        {
            //全局获得Window窗体
            this.window = window;
            #region RibbonControl(Ribbon控制)
            RibbonControl ribbonControl = window.FindName("RibbonControl") as RibbonControl;
            if (ribbonControl != null)
            {
                //Ribbon控制:页眉的控制
                BarEditItem barItemStyle = new BarEditItem();
                barItemStyle.Name = "eRibbonStyle";
                barItemStyle.Content = "Ribbon Style:";
                barItemStyle.EditWidth = 100;
                barItemStyle.ClosePopupOnChangingEditValue = true;
                barItemStyle.EditValue = ribbonControl.RibbonStyle;
                ComboBoxEditSettings comboBoxEditSettings = new ComboBoxEditSettings();
                comboBoxEditSettings.IsTextEditable = false;
                comboBoxEditSettings.PopupMaxHeight = 250;
                comboBoxEditSettings.ItemsSource = Enum.GetValues(typeof(RibbonStyle));
                barItemStyle.EditSettings = comboBoxEditSettings;
                barItemStyle.SetBinding(BarEditItem.EditValueProperty, new Binding("MainRibbonStyle"));
                ribbonControl.PageHeaderItemLinks.Add(barItemStyle);
                //Ribbon控制:工具栏项目连接
                BarButtonItem brButtonItem = new BarButtonItem();
                brButtonItem.Name = "bAbout";
                brButtonItem.Content = "About";
                ribbonControl.ToolbarItemLinks.Add(brButtonItem);
            }
            #endregion
            #region ApplicationMenu(Ribbon控制:应用程序菜单)
            ApplicationMenu applicationMenu = window.FindName("ApplicationMenu") as ApplicationMenu;
            if (applicationMenu != null)
            {
            }
            #endregion
            #region DefaultPageCategory(Ribbon默认页面类别)
            //生成Ribbon控件
            RibbonDefaultPageCategory defaultPageCategory = window.FindName("DefaultPageCategory") as RibbonDefaultPageCategory;
            if (defaultPageCategory != null)
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

                defaultPageCategory.Pages.Add(ribbonPageHome);
            }
            #endregion
        }

        /// <summary>
        /// Window窗体
        /// </summary>
        private Window _window;
        /// <summary>
        /// Window窗体
        /// </summary>
        public Window window
        {
            get
            {
                return _window;
            }
            set
            {
                _window = value;
            }
        }

        /// <summary>
        /// 应用程序标题
        /// </summary>
        private string _mainTitle;
        /// <summary>
        /// 应用程序标题
        /// </summary>
        public string MainTitle
        {
            get
            {
                return _mainTitle;
            }
            set
            {
                _mainTitle = value;
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
        /// 应用程序小图标
        /// </summary>
        private ImageSource _mainAppSmallIcon;
        /// <summary>
        /// 应用程序小图标
        /// </summary>
        public ImageSource MainAppSmallIcon
        {
            get
            {
                return _mainAppSmallIcon;
            }
            set
            {
                _mainAppSmallIcon = value;
            }
        }

        /// <summary>
        /// 应用程序大图标
        /// </summary>
        private ImageSource _mainAppLargeIcon;
        /// <summary>
        /// 应用程序大图标
        /// </summary>
        public ImageSource MainAppLargeIcon
        {
            get
            {
                return _mainAppLargeIcon;
            }
            set
            {
                _mainAppLargeIcon = value;
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
