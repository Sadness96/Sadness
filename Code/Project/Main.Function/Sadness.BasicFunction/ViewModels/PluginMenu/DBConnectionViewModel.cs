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
using Utils.Helper.TXT;
using Utils.Helper.Image;
using Prism.Mvvm;
using Prism.Commands;
using ADO.Helper.DatabaseConversion;

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
        /// <param name="strDataBaseName">数据库名称 SqlServer/Oracle/MySql/Access/SQLite</param>
        public DBConnectionViewModel(string strDataBaseName)
        {
            //应用程序标题
            Title = "连接数据库";
            //设置软件图标
            MainAppLargeIcon = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
            //赋值全局变量数据库名称
            this.strDataBaseName = strDataBaseName;
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
                    if (strDataBaseName == "SqlServer")
                    {

                    }
                    else if (strDataBaseName == "Oracle")
                    {

                    }
                    else if (strDataBaseName == "MySql")
                    {

                    }
                    else if (strDataBaseName == "Access")
                    {

                    }
                    else if (strDataBaseName == "SQLite")
                    {

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
