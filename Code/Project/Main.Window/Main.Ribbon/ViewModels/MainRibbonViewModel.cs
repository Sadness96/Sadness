using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors.Settings;
using Main.Ribbon.Models;
using Main.Ribbon.Utils;
using Prism.Commands;
using Sadness.SQLiteDB.Connect;
using Sadness.SQLiteDB.Utils;
using Sadness.SQLiteDB.Models;

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
            //默认最大化窗体
            MainWindowState = WindowState.Maximized;
            //修改基础样式(默认RibbonStyle.Office2007)
            MainRibbonStyle = RibbonStyle.Office2007;
            //设置软件图标
            MainAppSmallIcon = OperationImage.ByteArrayToImageSource(MainImage.GetImageByteArray("AppSmallIcon"));
            MainAppLargeIcon = OperationImage.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
            //设置应用按钮文字
            MainApplicationButtonText = "File";
            //设置对齐方式
            MainRibbonPageCategoryAlignment = RibbonPageCategoryCaptionAlignment.Right;
            //设置应用程序主题
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2013DarkGray.Name;
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
                #region Ribbon控制:页眉的控制
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
                #endregion
                #region Ribbon控制:工具栏项目连接
                //获得 ice_system_plugin_toolbar 表所有数据
                List<ice_system_plugin_toolbar> listSystemPluginToolBar = OperationEntityList.GetEntityList<ice_system_plugin_toolbar>(SystemTables.ice_system_plugin_toolbar, string.Empty);
                foreach (var itemPluginToolBar in listSystemPluginToolBar)
                {
                    //创建BarButtonItem
                    BarButtonItem barButton = new BarButtonItem();
                    barButton.Content = itemPluginToolBar.ice_function_name;
                    barButton.Glyph = OperationImage.ByteArrayToImageSource(itemPluginToolBar.ice_image);
                    barButton.IsVisible = itemPluginToolBar.ice_button_visible;
                    barButton.IsEnabled = itemPluginToolBar.ice_button_enabled;
                    barButton.ItemClick += delegate
                    {
                        //运行菜单插件
                        OperationReflect.RunPluginToolBarClick(itemPluginToolBar.ice_dllfile_path, itemPluginToolBar.ice_dllfile_class);
                    };
                    //添加按钮
                    ribbonControl.ToolbarItemLinks.Add(barButton);
                }
                #endregion
            }
            #endregion
            #region ApplicationMenu(Ribbon应用程序菜单)
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
                //获得 ice_system_plugin_menu 表所有数据
                List<ice_system_plugin_menu> listSystemPluginMenu = OperationEntityList.GetEntityList<ice_system_plugin_menu>(SystemTables.ice_system_plugin_menu, string.Empty);
                //获得 ice_system_plugin_menugroup 表所有数据
                List<ice_system_plugin_menugroup> listSystemPluginMenuGroup = OperationEntityList.GetEntityList<ice_system_plugin_menugroup>(SystemTables.ice_system_plugin_menugroup, string.Empty);
                //遍历添加 Home 分组
                foreach (var itemSystemPluginMenuHome in listSystemPluginMenuGroup.Where(o => o.ice_page_ishome && o.ice_page_visible).OrderBy(o => o.ice_number))
                {
                    //创建 RibbonPage
                    RibbonPage ribbonPageHome = new RibbonPage();
                    ribbonPageHome.IsVisible = itemSystemPluginMenuHome.ice_page_visible;
                    ribbonPageHome.Caption = itemSystemPluginMenuHome.ice_page_name;
                    //遍历添加 Group 分组
                    foreach (var itemSystemPluginMenuGroup in listSystemPluginMenuGroup.Where(o => o.ice_parid == itemSystemPluginMenuHome.ice_id))
                    {
                        //创建 RibbonPageGroup
                        RibbonPageGroup ribbonPageGroup = new RibbonPageGroup();
                        ribbonPageGroup.IsVisible = itemSystemPluginMenuGroup.ice_page_visible;
                        ribbonPageGroup.Caption = itemSystemPluginMenuGroup.ice_page_name;
                        //遍历添加按钮
                        foreach (var itemPluginMenu in listSystemPluginMenu.Where(o => o.ice_page_parid == itemSystemPluginMenuGroup.ice_id).OrderBy(o => o.ice_number))
                        {
                            //创建BarButtonItem
                            BarButtonItem barButton = new BarButtonItem();
                            //按钮样式
                            barButton.RibbonStyle = (RibbonItemStyles)Enum.Parse(typeof(RibbonItemStyles), Enum.GetName(typeof(RibbonItemStyles), itemPluginMenu.ice_button_style));
                            //按钮名称
                            barButton.Content = itemPluginMenu.ice_function_name;
                            //按钮小图片(16x16)
                            barButton.Glyph = OperationImage.ByteArrayToImageSource(itemPluginMenu.ice_image_small);
                            //按钮大图片(32x32)
                            barButton.LargeGlyph = OperationImage.ByteArrayToImageSource(itemPluginMenu.ice_image_large);
                            //按钮是否可见
                            barButton.IsVisible = itemPluginMenu.ice_button_visible;
                            //按钮是否启用
                            barButton.IsEnabled = itemPluginMenu.ice_button_enabled;
                            //按钮提示
                            barButton.Hint = itemPluginMenu.ice_button_hint;
                            //按钮快捷键(不为空且包含"+"则添加)
                            if (!string.IsNullOrEmpty(itemPluginMenu.ice_button_keygesture) && itemPluginMenu.ice_button_keygesture.Contains("+"))
                            {
                                barButton.KeyGesture = ((new KeyGestureValueSerializer().ConvertFromString(itemPluginMenu.ice_button_keygesture, null)) as KeyGesture);
                            }
                            //按钮运行菜单插件
                            barButton.ItemClick += delegate
                            {
                                OperationReflect.RunPluginMenuClick(itemPluginMenu.ice_dllfile_path, itemPluginMenu.ice_dllfile_class);
                            };
                            //添加按钮
                            ribbonPageGroup.ItemLinks.Add(barButton);
                        }
                        //添加 PageGroup 分组
                        ribbonPageHome.Groups.Add(ribbonPageGroup);
                    }
                    //添加 PageHome 分组
                    defaultPageCategory.Pages.Add(ribbonPageHome);
                }
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

        /// <summary>
        /// 指定是最小化、最大化还是还原窗口
        /// </summary>
        private WindowState _mainWindowState;
        /// <summary>
        /// 指定是最小化、最大化还是还原窗口
        /// </summary>
        public WindowState MainWindowState
        {
            get
            {
                return _mainWindowState;
            }
            set
            {
                _mainWindowState = value;
            }
        }
    }
}
