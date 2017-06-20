using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SimpleMvvmToolkit;
using SimpleMvvmToolkit.ModelExtensions;
using System.Collections.ObjectModel;
using DatabaseConversion.Models;
using DatabaseConversion.Views;
using ADO.Helper.SqlServer;
using ADO.Helper.Oracle;
using ADO.Helper.MySql;
using ADO.Helper.Access;
using ADO.Helper.SQLite;
using ADO.Helper.TXT;

namespace DatabaseConversion.ViewModels
{
    /// <summary>
    /// DBConnection.xaml 的视图模型
    /// </summary>
    public class DBConnectionViewModel : ViewModelBase<DBConnectionViewModel>
    {
        /// <summary>
        /// 数据库名称(打开窗口时传值)
        /// SqlServer/Oracle/MySql/Access/SQLite
        /// </summary>
        public string strDataBaseName { get; set; }

        /// <summary>
        /// 源数据或目标数据(打开窗口时传值)
        /// Source/Target
        /// </summary>
        public string strSourceOrTarget { get; set; }

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
                _DisplaySqlServer = value;
                NotifyPropertyChanged(x => x.DisplaySqlServer);
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
                _DisplayOracle = value;
                NotifyPropertyChanged(x => x.DisplayOracle);
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
                _DisplayMySql = value;
                NotifyPropertyChanged(x => x.DisplayMySql);
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
                _DisplayAccess = value;
                NotifyPropertyChanged(x => x.DisplayAccess);
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
                _DisplaySQLite = value;
                NotifyPropertyChanged(x => x.DisplaySQLite);
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
                _strIPAddress = value;
                NotifyPropertyChanged(x => x.strIPAddress);
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
                _strDataBase = value;
                NotifyPropertyChanged(x => x.strDataBase);
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
                _strUserName = value;
                NotifyPropertyChanged(x => x.strUserName);
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
                _strPassword = value;
                NotifyPropertyChanged(x => x.strPassword);
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
                _strPath = value;
                NotifyPropertyChanged(x => x.strPath);
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
                    Dictionary<string, string> dicColourAndString = new Dictionary<string, string>();
                    dicColourAndString.Add("SourceOrTarget", strSourceOrTarget);
                    dicColourAndString.Add("Colour", "#FF3FF831");
                    if (strDataBaseName == "SqlServer")
                    {
                        SqlServerHelper sqlHelper = new SqlServerHelper();
                        sqlHelper.SqlServerConnectionString(strIPAddress, strDataBase, strUserName, strPassword);
                        if (sqlHelper.Open() == 0)
                        {
                            //连接成功
                            sqlHelper.Close();
                            dicColourAndString.Add("String", sqlHelper.GetConnectionString());
                            SendMessage<Dictionary<string, string>>("ColourAndString", new NotificationEventArgs<Dictionary<string, string>>("", dicColourAndString));
                            CloseWindow();
                        }
                        else
                        {
                            //连接失败
                            MessageBox.Show("连接失败!!!");
                        }
                    }
                    else if (strDataBaseName == "Oracle")
                    {
                        OracleHelper sqlHelper = new OracleHelper();
                        sqlHelper.OracleConnectionString(strIPAddress, strUserName, strPassword);
                        if (sqlHelper.Open() == 0)
                        {
                            //连接成功
                            sqlHelper.Close();
                            dicColourAndString.Add("String", sqlHelper.GetConnectionString());
                            SendMessage<Dictionary<string, string>>("ColourAndString", new NotificationEventArgs<Dictionary<string, string>>("", dicColourAndString));
                            CloseWindow();
                        }
                        else
                        {
                            //连接失败
                            MessageBox.Show("连接失败!!!");
                        }
                    }
                    else if (strDataBaseName == "MySql")
                    {
                        MySqlHelper sqlHelper = new MySqlHelper();
                        sqlHelper.MySqlConnectionString(strIPAddress, strUserName, strPassword, strDataBase);
                        if (sqlHelper.Open() == 0)
                        {
                            //连接成功
                            sqlHelper.Close();
                            dicColourAndString.Add("String", sqlHelper.GetConnectionString());
                            SendMessage<Dictionary<string, string>>("ColourAndString", new NotificationEventArgs<Dictionary<string, string>>("", dicColourAndString));
                            CloseWindow();
                        }
                        else
                        {
                            //连接失败
                            MessageBox.Show("连接失败!!!");
                        }
                    }
                    else if (strDataBaseName == "Access")
                    {
                        AccessHelper sqlHelper = new AccessHelper();
                        //判断是否需要新建数据库
                        if (!System.IO.File.Exists(strPath))
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
                            dicColourAndString.Add("String", sqlHelper.GetConnectionString());
                            SendMessage<Dictionary<string, string>>("ColourAndString", new NotificationEventArgs<Dictionary<string, string>>("", dicColourAndString));
                            CloseWindow();
                        }
                        else
                        {
                            //连接失败
                            MessageBox.Show("连接失败!!!");
                        }
                    }
                    else if (strDataBaseName == "SQLite")
                    {
                        SQLiteHelper sqlHelper = new SQLiteHelper();
                        //判断是否需要新建数据库
                        if (!System.IO.File.Exists(strPath))
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
                            dicColourAndString.Add("String", sqlHelper.GetConnectionString());
                            SendMessage<Dictionary<string, string>>("ColourAndString", new NotificationEventArgs<Dictionary<string, string>>("", dicColourAndString));
                            CloseWindow();
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
        public DelegateCommand Close
        {
            get
            {
                return new DelegateCommand(CloseWindow);
            }
        }

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        private void CloseWindow()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.IsActive)
                {
                    win.Close();
                    return;
                }
            }
        }
    }
}
