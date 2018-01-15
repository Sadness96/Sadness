using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Practices.Unity;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using Main.Ribbon.Utils;
using Main.Ribbon.Views;
using Prism.Commands;
using IceElves.Interface;
using IceElves.SQLiteDB.Connect;
using IceElves.SQLiteDB.Utils;
using IceElves.SQLiteDB.Models;

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
                brButtonItem.ItemClick += delegate
                {
                    MessageBox.Show("点击了bAbout");
                };
                ribbonControl.ToolbarItemLinks.Add(brButtonItem);
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
                List<ice_system_plugin_menu> listSystemPluginMenu = OperationEntityList.GetEntityList<ice_system_plugin_menu>("ice_system_plugin_menu", string.Empty);
                //获得 ice_page_home 分组出的数据
                List<string> listPageHome = listSystemPluginMenu.Select(o => o.ice_page_home).Distinct().ToList();
                //遍历添加分组
                foreach (string strPageHome in listPageHome)
                {
                    //创建RibbonPage
                    RibbonPage ribbonPageHome = new RibbonPage();
                    ribbonPageHome.Caption = strPageHome;
                    //获得 ice_page_group 分组出的数据
                    List<string> listPageGroup = listSystemPluginMenu.Where(o => o.ice_page_home == strPageHome).Select(o => o.ice_page_group).Distinct().ToList();
                    //遍历添加分组
                    foreach (string strPageGroup in listPageGroup)
                    {
                        //创建RibbonPageGroup
                        RibbonPageGroup ribbonPageGroup = new RibbonPageGroup();
                        ribbonPageGroup.Caption = strPageGroup;
                        //获得 ice_page_group 分组出的数据
                        List<ice_system_plugin_menu> listPageHomeGroup = listSystemPluginMenu.Where(o => o.ice_page_home == strPageHome && o.ice_page_group == strPageGroup).ToList();
                        //遍历添加按钮
                        foreach (ice_system_plugin_menu itemPluginMenu in listPageHomeGroup)
                        {
                            //创建BarButtonItem
                            BarButtonItem barButton = new BarButtonItem();
                            barButton.RibbonStyle = (RibbonItemStyles)Enum.Parse(typeof(RibbonItemStyles), Enum.GetName(typeof(RibbonItemStyles), itemPluginMenu.ice_button_style));
                            barButton.Content = itemPluginMenu.ice_function_name;
                            barButton.Glyph = OperationImage.ByteArrayToImageSource(itemPluginMenu.ice_image_small);
                            barButton.LargeGlyph = OperationImage.ByteArrayToImageSource(itemPluginMenu.ice_image_large);
                            barButton.ItemClick += delegate
                            {
                                //Test Command
                                OperationReflect.RunMenuPluginClick(itemPluginMenu.ice_dllfile_path, itemPluginMenu.ice_dllfile_class);
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
    }
}
