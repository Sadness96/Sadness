using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;
using Sadness.SQLiteDB.Connect;
using Sadness.BasicFunction.Models;
using Sadness.BasicFunction.Views.PluginMenu;
using Utils.Helper.TXT;
using Utils.Helper.Image;
using Prism.Mvvm;
using Prism.Commands;
using ADO.Helper.DatabaseConversion;

namespace Sadness.BasicFunction.ViewModels.PluginMenu
{
    /// <summary>
    /// DBConversion.xaml 的视图模型
    /// </summary>
    public class DBConversionViewModel : BindableBase
    {
        /// <summary>
        /// DBConversion.xaml 的视图模型
        /// </summary>
        public DBConversionViewModel()
        {
            //应用程序标题
            Title = "数据库转换工具";
            //设置软件图标
            MainAppLargeIcon = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
        }

        /// <summary>
        /// 应用程序标题
        /// </summary>
        private string _title;
        /// <summary>
        /// 应用程序标题
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
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
        /// 表名Tree绑定数据
        /// </summary>
        private ObservableCollection<TablesTree> _TreeList;
        /// <summary>
        /// 表名Tree绑定数据
        /// </summary>
        public ObservableCollection<TablesTree> TreeList
        {
            get
            {
                if (_TreeList == null)
                {
                    _TreeList = new ObservableCollection<TablesTree>();
                }
                return _TreeList;
            }
            set
            {
                SetProperty(ref _TreeList, value);
            }
        }

        /// <summary>
        /// 表字段Grid绑定数据
        /// </summary>
        private ObservableCollection<TableStructureGrid> _GridList;
        /// <summary>
        /// 表字段Grid绑定数据
        /// </summary>
        public ObservableCollection<TableStructureGrid> GridList
        {
            get
            {
                if (_GridList == null)
                {
                    _GridList = new ObservableCollection<TableStructureGrid>();
                }
                return _GridList;
            }
            set
            {
                SetProperty(ref _GridList, value);
            }
        }

        /// <summary>
        /// 字段类型列绑定数据
        /// </summary>
        private ObservableCollection<string> _FieldTypeList;
        /// <summary>
        /// 字段类型列绑定数据
        /// </summary>
        public ObservableCollection<string> FieldTypeList
        {
            get
            {
                if (_FieldTypeList == null)
                {
                    _FieldTypeList = new ObservableCollection<string>();
                }
                return _FieldTypeList;
            }
            set
            {
                SetProperty(ref _FieldTypeList, value);
            }
        }

        /// <summary>
        /// 表结构信息字典<表名,字段信息>
        /// </summary>
        public Dictionary<string, ObservableCollection<Models.TableStructureGrid>> DicTablesStructure { get; set; }

        /// <summary>
        /// 源数据连接颜色
        /// </summary>
        private string _SourceConnectionColour;
        /// <summary>
        /// 源数据连接颜色
        /// </summary>
        public string SourceConnectionColour
        {
            get
            {
                if (string.IsNullOrEmpty(SourceConnectionString))
                {
                    _SourceConnectionColour = "#FF909090";
                }
                else
                {
                    _SourceConnectionColour = "#FF3FF831";
                }
                return _SourceConnectionColour;
            }
            set
            {
                SetProperty(ref _SourceConnectionColour, value);
            }
        }

        /// <summary>
        /// 目标数据连接颜色
        /// </summary>
        private string _TargetConnectionColour;
        /// <summary>
        /// 目标数据连接颜色
        /// </summary>
        public string TargetConnectionColour
        {
            get
            {
                if (string.IsNullOrEmpty(TargetConnectionString))
                {
                    _TargetConnectionColour = "#FF909090";
                }
                else
                {
                    _TargetConnectionColour = "#FF3FF831";
                }
                return _TargetConnectionColour;
            }
            set
            {
                SetProperty(ref _TargetConnectionColour, value);
            }
        }

        /// <summary>
        /// 是否空闲
        /// </summary>
        private bool _IsBusy;
        /// <summary>
        /// 是否空闲
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                SetProperty(ref _IsBusy, value);
            }
        }

        /// <summary>
        /// 源数据库连接字符串
        /// </summary>
        public string SourceConnectionString { get; set; }

        /// <summary>
        /// 目标数据库连接字符串
        /// </summary>
        public string TargetConnectionString { get; set; }

        /// <summary>
        /// 源数据库单选按钮
        /// </summary>
        public string IsSourceString { get; set; }

        /// <summary>
        /// 源数据库单选按钮(单击事件)
        /// </summary>
        public DelegateCommand<string> RadioButtonSource
        {
            get
            {
                return new DelegateCommand<string>(delegate(string eData)
                {
                    if (eData != IsSourceString)
                    {
                        SourceConnectionString = string.Empty;
                    }
                    IsSourceString = eData;
                });
            }
        }

        /// <summary>
        /// 目标数据库单选按钮
        /// </summary>
        public string IsTargetString { get; set; }

        /// <summary>
        /// 目标数据库单选按钮(单击事件)
        /// </summary>
        public DelegateCommand<string> RadioButtonTarget
        {
            get
            {
                return new DelegateCommand<string>(delegate(string eData)
                {
                    if (eData != IsTargetString)
                    {
                        TargetConnectionString = string.Empty;
                    }
                    IsTargetString = eData;
                });
            }
        }

        /// <summary>
        /// 连接源数据
        /// </summary>
        public DelegateCommand ConnectSourceData
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    DBConnection dbcView = new DBConnection(IsSourceString);
                    dbcView.ShowDialog();
                });
            }
        }

        /// <summary>
        /// 连接目标数据
        /// </summary>
        public DelegateCommand ConnectTargetData
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    DBConnection dbcView = new DBConnection(IsTargetString);
                    dbcView.ShowDialog();
                });
            }
        }
    }
}
