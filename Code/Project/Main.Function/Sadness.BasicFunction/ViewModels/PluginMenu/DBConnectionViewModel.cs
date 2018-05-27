using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;
using Sadness.SQLiteDB.Connect;
using Sadness.BasicFunction.Models;
using Prism.Mvvm;
using Prism.Events;
using Prism.Commands;
using ADO.Helper.DatabaseConversion;
using ADO.Helper.SqlServer;
using ADO.Helper.Oracle;
using ADO.Helper.MySql;
using ADO.Helper.Access;
using ADO.Helper.SQLite;
using Utils.Helper.TXT;
using Utils.Helper.Image;

namespace Sadness.BasicFunction.ViewModels.PluginMenu
{
    /// <summary>
    /// DBConnection.xaml 的视图模型
    /// </summary>
    public class DBConnectionViewModel : BindableBase
    {
        /// <summary>
        /// DBConnection.xaml 的视图模型
        /// </summary>
        /// <param name="eventAggregator">Defines an interface to get instances of an event type.</param>
        /// <param name="strDataBaseName">数据库名称 SqlServer/Oracle/MySql/Access/SQLite</param>
        public DBConnectionViewModel(IEventAggregator eventAggregator, string strDataBaseName)
        {
            //委托Load方法
            LoadedCommand = new DelegateCommand<Window>(Window_Loaded);
            //应用程序标题
            Title = "连接数据库";
            //设置软件图标
            MainAppLargeIcon = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
            //赋值全局变量数据库名称
            this.strDataBaseName = strDataBaseName;
            //赋值全局变量消息机制
            this.eventAggregator = eventAggregator;
            //初始化控件
            if (strDataBaseName.Equals(TypeProcessing.DataBase.SqlServer.ToString()))
            {
                DisplaySqlServer = Visibility.Visible;
                DisplayOracle = Visibility.Hidden;
                DisplayMySql = Visibility.Hidden;
                DisplayAccess = Visibility.Hidden;
                DisplaySQLite = Visibility.Hidden;
            }
            else if (strDataBaseName.Equals(TypeProcessing.DataBase.Oracle.ToString()))
            {
                DisplaySqlServer = Visibility.Hidden;
                DisplayOracle = Visibility.Visible;
                DisplayMySql = Visibility.Hidden;
                DisplayAccess = Visibility.Hidden;
                DisplaySQLite = Visibility.Hidden;
            }
            else if (strDataBaseName.Equals(TypeProcessing.DataBase.MySql.ToString()))
            {
                DisplaySqlServer = Visibility.Hidden;
                DisplayOracle = Visibility.Hidden;
                DisplayMySql = Visibility.Visible;
                DisplayAccess = Visibility.Hidden;
                DisplaySQLite = Visibility.Hidden;
            }
            else if (strDataBaseName.Equals(TypeProcessing.DataBase.Access.ToString()))
            {
                DisplaySqlServer = Visibility.Hidden;
                DisplayOracle = Visibility.Hidden;
                DisplayMySql = Visibility.Hidden;
                DisplayAccess = Visibility.Visible;
                DisplaySQLite = Visibility.Hidden;
            }
            else if (strDataBaseName.Equals(TypeProcessing.DataBase.SQLite.ToString()))
            {
                DisplaySqlServer = Visibility.Hidden;
                DisplayOracle = Visibility.Hidden;
                DisplayMySql = Visibility.Hidden;
                DisplayAccess = Visibility.Hidden;
                DisplaySQLite = Visibility.Visible;
            }
        }

        /// <summary>
        /// Window窗体Load事件
        /// </summary>
        /// <param name="window">主窗体</param>
        private void Window_Loaded(Window window)
        {
            //全局获得Window窗体
            this.window = window;
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
        /// 消息机制传递数据库连接信息
        /// </summary>
        private IEventAggregator eventAggregator;

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
        /// 数据库名称
        /// SqlServer/Oracle/MySql/Access/SQLite
        /// </summary>
        private string strDataBaseName { get; set; }

        /// <summary>
        /// 显示SqlServer
        /// </summary>
        private System.Windows.Visibility _DisplaySqlServer;
        /// <summary>
        /// 显示SqlServer
        /// </summary>
        public System.Windows.Visibility DisplaySqlServer
        {
            get
            {
                return _DisplaySqlServer;
            }
            set
            {
                SetProperty(ref _DisplaySqlServer, value);
            }
        }

        /// <summary>
        /// 显示Oracle
        /// </summary>
        private System.Windows.Visibility _DisplayOracle;
        /// <summary>
        /// 显示Oracle
        /// </summary>
        public System.Windows.Visibility DisplayOracle
        {
            get
            {
                return _DisplayOracle;
            }
            set
            {
                SetProperty(ref _DisplayOracle, value);
            }
        }

        /// <summary>
        /// 显示MySql
        /// </summary>
        private System.Windows.Visibility _DisplayMySql;
        /// <summary>
        /// 显示MySql
        /// </summary>
        public System.Windows.Visibility DisplayMySql
        {
            get
            {
                return _DisplayMySql;
            }
            set
            {
                SetProperty(ref _DisplayMySql, value);
            }
        }

        /// <summary>
        /// 显示Access
        /// </summary>
        private System.Windows.Visibility _DisplayAccess;
        /// <summary>
        /// 显示Access
        /// </summary>
        public System.Windows.Visibility DisplayAccess
        {
            get
            {
                return _DisplayAccess;
            }
            set
            {
                SetProperty(ref _DisplayAccess, value);
            }
        }

        /// <summary>
        /// 显示SQLite
        /// </summary>
        private System.Windows.Visibility _DisplaySQLite;
        /// <summary>
        /// 显示SQLite
        /// </summary>
        public System.Windows.Visibility DisplaySQLite
        {
            get
            {
                return _DisplaySQLite;
            }
            set
            {
                SetProperty(ref _DisplaySQLite, value);
            }
        }

        /// <summary>
        /// IP地址/数据库
        /// </summary>
        private string _strIPAddress;
        /// <summary>
        /// IP地址/数据库
        /// </summary>
        public string strIPAddress
        {
            get
            {
                return _strIPAddress;
            }
            set
            {
                SetProperty(ref _strIPAddress, value);
            }
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        private string _strDataBase;
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string strDataBase
        {
            get
            {
                return _strDataBase;
            }
            set
            {
                SetProperty(ref _strDataBase, value);
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string _strUserName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string strUserName
        {
            get
            {
                return _strUserName;
            }
            set
            {
                SetProperty(ref _strUserName, value);
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string _strPassword;
        /// <summary>
        /// 密码
        /// </summary>
        public string strPassword
        {
            get
            {
                return _strPassword;
            }
            set
            {
                SetProperty(ref _strPassword, value);
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        private string _strPath;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string strPath
        {
            get
            {
                return _strPath;
            }
            set
            {
                SetProperty(ref _strPath, value);
            }
        }

        /// <summary>
        /// Access数据库单选按钮
        /// </summary>
        public string IsOffaceString { get; set; }
        /// <summary>
        /// Access数据库单选按钮(单击事件)
        /// </summary>
        public DelegateCommand<string> RadioButtonOfface
        {
            get
            {
                return new DelegateCommand<string>(delegate(string eData)
                {
                    IsOffaceString = eData;
                });
            }
        }

        /// <summary>
        /// 选择文件路径
        /// </summary>
        public DelegateCommand SelectPath
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    if (strDataBaseName == "Access")
                    {
                        dialog.Filter = "Microsoft Access 数据库|*.mdb;*.accdb";
                    }
                    else if (strDataBaseName == "SQLite")
                    {
                        dialog.Filter = "SQLite 数据库|*.db;*.sqlite|全部文件|*.*";
                    }
                    else
                    {
                        dialog.Filter = "全部文件|*.*";
                    }
                    if (dialog.ShowDialog() == true)
                    {
                        strPath = dialog.FileName;
                    }
                });
            }
        }

        /// <summary>
        /// 选择文件保存路径
        /// </summary>
        public DelegateCommand SavePath
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                    dialog.Title = "新建数据库";
                    if (strDataBaseName == "Access")
                    {
                        dialog.Filter = "Microsoft Access 数据库|*.mdb;*.accdb";
                    }
                    else if (strDataBaseName == "SQLite")
                    {
                        dialog.Filter = "SQLite 数据库|*.db;*.sqlite|全部文件|*.*";
                    }
                    else
                    {
                        dialog.Filter = "全部文件|*.*";
                    }
                    if (dialog.ShowDialog() == true)
                    {
                        strPath = dialog.FileName;
                    }
                });
            }
        }

        /// <summary>
        /// 连接数据库(测试连接)
        /// </summary>
        public DelegateCommand ConnectionDB
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    if (strDataBaseName.Equals(TypeProcessing.DataBase.SqlServer.ToString()))
                    {
                        SqlServerHelper sqlHelper = new SqlServerHelper();
                        sqlHelper.SqlServerConnectionString(strIPAddress, strDataBase, strUserName, strPassword);
                        if (sqlHelper.Open() == 0)
                        {
                            //连接成功
                            sqlHelper.Close();
                            eventAggregator.GetEvent<EventConnection>().Publish(new Models.EventArgs() { strDataBaseName = TypeProcessing.DataBase.SqlServer.ToString(), strSqlConnection = sqlHelper.GetConnectionString() });
                            this.window.Close();
                        }
                        else
                        {
                            //连接失败
                            MessageBox.Show("连接失败!!!");
                        }
                    }
                    else if (strDataBaseName.Equals(TypeProcessing.DataBase.Oracle.ToString()))
                    {
                        OracleHelper sqlHelper = new OracleHelper();
                        sqlHelper.OracleConnectionString(strIPAddress, strUserName, strPassword);
                        if (sqlHelper.Open() == 0)
                        {
                            //连接成功
                            sqlHelper.Close();
                            eventAggregator.GetEvent<EventConnection>().Publish(new Models.EventArgs() { strDataBaseName = TypeProcessing.DataBase.Oracle.ToString(), strSqlConnection = sqlHelper.GetConnectionString() });
                            this.window.Close();
                        }
                        else
                        {
                            //连接失败
                            MessageBox.Show("连接失败!!!");
                        }
                    }
                    else if (strDataBaseName.Equals(TypeProcessing.DataBase.MySql.ToString()))
                    {
                        MySqlHelper sqlHelper = new MySqlHelper();
                        sqlHelper.MySqlConnectionString(strIPAddress, strUserName, strPassword, strDataBase);
                        if (sqlHelper.Open() == 0)
                        {
                            //连接成功
                            sqlHelper.Close();
                            eventAggregator.GetEvent<EventConnection>().Publish(new Models.EventArgs() { strDataBaseName = TypeProcessing.DataBase.MySql.ToString(), strSqlConnection = sqlHelper.GetConnectionString() });
                            this.window.Close();
                        }
                        else
                        {
                            //连接失败
                            MessageBox.Show("连接失败!!!");
                        }
                    }
                    else if (strDataBaseName.Equals(TypeProcessing.DataBase.Access.ToString()))
                    {
                        AccessHelper sqlHelper = new AccessHelper();
                        //判断是否需要新建数据库
                        if (!string.IsNullOrEmpty(strPath) && !System.IO.File.Exists(strPath))
                        {
                            if (sqlHelper.CreateDataBase(strPath) == -1)
                            {
                                System.Windows.MessageBox.Show("创建数据库失败!!!");
                            }
                        }
                        //连接数据库
                        if (string.IsNullOrEmpty(IsOffaceString) || IsOffaceString == "offace2003")
                        {
                            sqlHelper.AccessConnectionPath_Office2003(strPath);
                        }
                        else
                        {
                            sqlHelper.AccessConnectionPath_Office2007(strPath);
                        }
                        if (sqlHelper.Open() == 0)
                        {
                            //连接成功
                            sqlHelper.Close();
                            eventAggregator.GetEvent<EventConnection>().Publish(new Models.EventArgs() { strDataBaseName = TypeProcessing.DataBase.Access.ToString(), strSqlConnection = sqlHelper.GetConnectionString() });
                            this.window.Close();
                        }
                        else
                        {
                            //连接失败
                            MessageBox.Show("连接失败!!!");
                        }
                    }
                    else if (strDataBaseName.Equals(TypeProcessing.DataBase.SQLite.ToString()))
                    {
                        SQLiteHelper sqlHelper = new SQLiteHelper();
                        //判断是否需要新建数据库
                        if (!string.IsNullOrEmpty(strPath) && !System.IO.File.Exists(strPath))
                        {
                            if (sqlHelper.CreateDataBase(strPath) == -1)
                            {
                                System.Windows.MessageBox.Show("创建数据库失败!!!");
                            }
                        }
                        //连接数据库
                        sqlHelper.SQLiteConnectionPath(strPath);
                        if (sqlHelper.Open() == 0)
                        {
                            //连接成功
                            sqlHelper.Close();
                            eventAggregator.GetEvent<EventConnection>().Publish(new Models.EventArgs() { strDataBaseName = TypeProcessing.DataBase.SQLite.ToString(), strSqlConnection = sqlHelper.GetConnectionString() });
                            this.window.Close();
                        }
                        else
                        {
                            //连接失败
                            MessageBox.Show("连接失败!!!");
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public DelegateCommand<Window> Close
        {
            get
            {
                return new DelegateCommand<Window>((window) =>
                {
                    if (window != null)
                    {
                        window.Close();
                    }
                });
            }
        }
    }
}
