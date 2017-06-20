using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
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
using ADO.Helper.DatabaseConversion;

namespace DatabaseConversion.ViewModels
{
    /// <summary>
    /// DBConversion.xaml 的视图模型
    /// </summary>
    public class DBConversionViewModel : ViewModelBase<DBConversionViewModel>
    {
        /// <summary>
        /// DBConversionViewModel 的构造函数
        /// </summary>
        public DBConversionViewModel()
        {
            SourceConnectionColour = "#FF909090";
            TargetConnectionColour = "#FF909090";
            RegisterToReceiveMessages<Dictionary<string, string>>("ColourAndString", ColourAndString);
        }

        /// <summary>
        /// 消息机制(传送颜色和连接字符串)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColourAndString(object sender, NotificationEventArgs<Dictionary<string, string>> e)
        {
            if (e.Data["SourceOrTarget"] == "Source")
            {
                SourceConnectionColour = e.Data["Colour"];
                SourceConnectionString = e.Data["String"];
            }
            else
            {
                TargetConnectionColour = e.Data["Colour"];
                TargetConnectionString = e.Data["String"];
            }
        }

        /// <summary>
        /// 表名Tree绑定数据
        /// </summary>
        private ObservableCollection<Models.TablesTree> _TreeList;
        /// <summary>
        /// 表名Tree绑定数据
        /// </summary>
        public ObservableCollection<Models.TablesTree> TreeList
        {
            get
            {
                if (_TreeList == null)
                {
                    _TreeList = new ObservableCollection<Models.TablesTree>();
                }
                return _TreeList;
            }
            set
            {
                _TreeList = value;
                NotifyPropertyChanged(x => x.TreeList);
            }
        }

        /// <summary>
        /// 表字段Grid绑定数据
        /// </summary>
        private ObservableCollection<Models.TableStructureGrid> _GridList;
        /// <summary>
        /// 表字段Grid绑定数据
        /// </summary>
        public ObservableCollection<Models.TableStructureGrid> GridList
        {
            get
            {
                if (_GridList == null)
                {
                    _GridList = new ObservableCollection<Models.TableStructureGrid>();
                }
                return _GridList;
            }
            set
            {
                _GridList = value;
                NotifyPropertyChanged(x => x.GridList);
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
                _FieldTypeList = value;
                NotifyPropertyChanged(x => x.FieldTypeList);
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
                return _SourceConnectionColour;
            }
            set
            {
                _SourceConnectionColour = value;
                NotifyPropertyChanged(x => x.SourceConnectionColour);
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
                return _TargetConnectionColour;
            }
            set
            {
                _TargetConnectionColour = value;
                NotifyPropertyChanged(x => x.TargetConnectionColour);
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
                _IsBusy = value;
                NotifyPropertyChanged(x => x.IsBusy);
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
                        SourceConnectionColour = "#FF909090";
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
                        TargetConnectionColour = "#FF909090";
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
                    if (IsSourceString == "SqlServer")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "SqlServer";
                        dbcViewModel.strSourceOrTarget = "Source";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Visible;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Hidden;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
                    else if (IsSourceString == "Oracle")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "Oracle";
                        dbcViewModel.strSourceOrTarget = "Source";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Visible;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Hidden;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
                    else if (IsSourceString == "MySql")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "MySql";
                        dbcViewModel.strSourceOrTarget = "Source";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Visible;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Hidden;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
                    else if (IsSourceString == "Access")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "Access";
                        dbcViewModel.strSourceOrTarget = "Source";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Visible;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Hidden;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
                    else if (IsSourceString == "SQLite")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "SQLite";
                        dbcViewModel.strSourceOrTarget = "Source";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Visible;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
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
                    if (IsTargetString == "SqlServer")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "SqlServer";
                        dbcViewModel.strSourceOrTarget = "Target";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Visible;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Hidden;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
                    else if (IsTargetString == "Oracle")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "Oracle";
                        dbcViewModel.strSourceOrTarget = "Target";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Visible;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Hidden;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
                    else if (IsTargetString == "MySql")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "MySql";
                        dbcViewModel.strSourceOrTarget = "Target";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Visible;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Hidden;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
                    else if (IsTargetString == "Access")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "Access";
                        dbcViewModel.strSourceOrTarget = "Target";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Visible;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Hidden;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
                    else if (IsTargetString == "SQLite")
                    {
                        DBConnectionViewModel dbcViewModel = new DBConnectionViewModel();
                        dbcViewModel.strDataBaseName = "SQLite";
                        dbcViewModel.strSourceOrTarget = "Target";
                        dbcViewModel.DisplaySqlServer = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayOracle = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayMySql = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplayAccess = System.Windows.Visibility.Hidden;
                        dbcViewModel.DisplaySQLite = System.Windows.Visibility.Visible;
                        DBConnection dbcView = new DBConnection();
                        dbcView.DataContext = dbcViewModel;
                        dbcView.ShowDialog();
                    }
                });
            }
        }

        /// <summary>
        /// 读取结构信息
        /// </summary>
        public DelegateCommand ReadStructureInformation
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    //判断是否符合转换要求
                    if (SourceConnectionColour == "#FF3FF831" && TargetConnectionColour == "#FF3FF831")
                    {
                        //初始化视图模型
                        TreeList = new ObservableCollection<Models.TablesTree>();
                        DicTablesStructure = new Dictionary<string, ObservableCollection<Models.TableStructureGrid>>();
                        //对不同数据库进行判断
                        if (IsSourceString == "SqlServer")
                        {
                            SqlServerHelper sqlHelper = new SqlServerHelper();
                            sqlHelper.SqlServerConnectionString(SourceConnectionString);
                            sqlHelper.Open();
                            List<string> listAllTableName = sqlHelper.GetAllTableName();
                            if (listAllTableName == null || listAllTableName.Count < 1)
                            {
                                return;
                            }
                            foreach (string strTableName in listAllTableName)
                            {
                                //添加TreeList
                                Models.TablesTree mtt = new Models.TablesTree();
                                mtt.Choose = true;
                                mtt.Table = strTableName;
                                TreeList.Add(mtt);
                                //添加DicTablesStructure
                                Dictionary<string, string> AllFieldNameType = sqlHelper.GetAllFieldNameType(strTableName);
                                if (AllFieldNameType == null || AllFieldNameType.Count < 1)
                                {
                                    continue;
                                }
                                if (IsSourceString != IsTargetString)
                                {
                                    //如果源数据库和目标数据库类型不一致,调用FieldTypeProcessing进行类型转换
                                    AllFieldNameType = ADO.Helper.DatabaseConversion.TypeProcessing.FieldTypeProcessing(AllFieldNameType, DBTypeGoEnum(IsTargetString));
                                }
                                DicTablesStructure.Add(strTableName, ParsingTableInformation(AllFieldNameType));
                            }
                            sqlHelper.Close();
                        }
                        else if (IsSourceString == "Oracle")
                        {
                            OracleHelper sqlHelper = new OracleHelper();
                            sqlHelper.OracleConnectionString(SourceConnectionString);
                            sqlHelper.Open();
                            List<string> listAllTableName = sqlHelper.GetAllTableName();
                            if (listAllTableName == null || listAllTableName.Count < 1)
                            {
                                return;
                            }
                            foreach (string strTableName in listAllTableName)
                            {
                                //添加TreeList
                                Models.TablesTree mtt = new Models.TablesTree();
                                mtt.Choose = true;
                                mtt.Table = strTableName;
                                TreeList.Add(mtt);
                                //添加DicTablesStructure
                                Dictionary<string, string> AllFieldNameType = sqlHelper.GetAllFieldNameType(strTableName);
                                if (AllFieldNameType == null || AllFieldNameType.Count < 1)
                                {
                                    continue;
                                }
                                if (IsSourceString != IsTargetString)
                                {
                                    //如果源数据库和目标数据库类型不一致,调用FieldTypeProcessing进行类型转换
                                    AllFieldNameType = ADO.Helper.DatabaseConversion.TypeProcessing.FieldTypeProcessing(AllFieldNameType, DBTypeGoEnum(IsTargetString));
                                }
                                DicTablesStructure.Add(strTableName, ParsingTableInformation(AllFieldNameType));
                            }
                            sqlHelper.Close();
                        }
                        else if (IsSourceString == "MySql")
                        {
                            MySqlHelper sqlHelper = new MySqlHelper();
                            sqlHelper.MySqlConnectionString(SourceConnectionString);
                            sqlHelper.Open();
                            List<string> listAllTableName = sqlHelper.GetAllTableName();
                            if (listAllTableName == null || listAllTableName.Count < 1)
                            {
                                return;
                            }
                            foreach (string strTableName in listAllTableName)
                            {
                                //添加TreeList
                                Models.TablesTree mtt = new Models.TablesTree();
                                mtt.Choose = true;
                                mtt.Table = strTableName;
                                TreeList.Add(mtt);
                                //添加DicTablesStructure
                                Dictionary<string, string> AllFieldNameType = sqlHelper.GetAllFieldNameType(strTableName);
                                if (AllFieldNameType == null || AllFieldNameType.Count < 1)
                                {
                                    continue;
                                }
                                if (IsSourceString != IsTargetString)
                                {
                                    //如果源数据库和目标数据库类型不一致,调用FieldTypeProcessing进行类型转换
                                    AllFieldNameType = ADO.Helper.DatabaseConversion.TypeProcessing.FieldTypeProcessing(AllFieldNameType, DBTypeGoEnum(IsTargetString));
                                }
                                DicTablesStructure.Add(strTableName, ParsingTableInformation(AllFieldNameType));
                            }
                            sqlHelper.Close();
                        }
                        else if (IsSourceString == "Access")
                        {
                            AccessHelper sqlHelper = new AccessHelper();
                            sqlHelper.AccessConnectionString(SourceConnectionString);
                            sqlHelper.Open();
                            List<string> listAllTableName = sqlHelper.GetAllTableName();
                            if (listAllTableName == null || listAllTableName.Count < 1)
                            {
                                return;
                            }
                            foreach (string strTableName in listAllTableName)
                            {
                                //添加TreeList
                                Models.TablesTree mtt = new Models.TablesTree();
                                mtt.Choose = true;
                                mtt.Table = strTableName;
                                TreeList.Add(mtt);
                                //添加DicTablesStructure
                                Dictionary<string, string> AllFieldNameType = sqlHelper.GetAllFieldNameType(strTableName);
                                if (AllFieldNameType == null || AllFieldNameType.Count < 1)
                                {
                                    continue;
                                }
                                if (IsSourceString != IsTargetString)
                                {
                                    //如果源数据库和目标数据库类型不一致,调用FieldTypeProcessing进行类型转换
                                    AllFieldNameType = ADO.Helper.DatabaseConversion.TypeProcessing.FieldTypeProcessing(AllFieldNameType, DBTypeGoEnum(IsTargetString));
                                }
                                DicTablesStructure.Add(strTableName, ParsingTableInformation(AllFieldNameType));
                            }
                            sqlHelper.Close();
                        }
                        else if (IsSourceString == "SQLite")
                        {
                            SQLiteHelper sqlHelper = new SQLiteHelper();
                            sqlHelper.SQLiteConnectionString(SourceConnectionString);
                            sqlHelper.Open();
                            List<string> listAllTableName = sqlHelper.GetAllTableName();
                            if (listAllTableName == null || listAllTableName.Count < 1)
                            {
                                return;
                            }
                            foreach (string strTableName in listAllTableName)
                            {
                                //sqlite特有系统表跳过
                                if (strTableName == "sqlite_sequence")
                                {
                                    continue;
                                }
                                //添加TreeList
                                Models.TablesTree mtt = new Models.TablesTree();
                                mtt.Choose = true;
                                mtt.Table = strTableName;
                                TreeList.Add(mtt);
                                //添加DicTablesStructure
                                Dictionary<string, string> AllFieldNameType = sqlHelper.GetAllFieldNameType(strTableName);
                                if (AllFieldNameType == null || AllFieldNameType.Count < 1)
                                {
                                    continue;
                                }
                                if (IsSourceString != IsTargetString)
                                {
                                    //如果源数据库和目标数据库类型不一致,调用FieldTypeProcessing进行类型转换
                                    AllFieldNameType = ADO.Helper.DatabaseConversion.TypeProcessing.FieldTypeProcessing(AllFieldNameType, DBTypeGoEnum(IsTargetString));
                                }
                                DicTablesStructure.Add(strTableName, ParsingTableInformation(AllFieldNameType));
                            }
                            sqlHelper.Close();
                        }
                        //字段类型列绑定数据
                        FieldTypeList = DBTypeGoFieldType(IsTargetString);
                        //重新读取后清空GridList
                        GridList = new ObservableCollection<TableStructureGrid>();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("请连接源数据和目标数据!!!");
                    }
                });
            }
        }

        /// <summary>
        /// 保存/转换
        /// </summary>
        public DelegateCommand SaveConversion
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    //判断是否符合转换要求
                    if (SourceConnectionColour == "#FF3FF831" && TargetConnectionColour == "#FF3FF831")
                    {
                        //显示等待控件并设置窗口空闲
                        IsBusy = true;
                        //开始转换
                        foreach (var item in _TreeList.Where(x => x.Choose).ToList())
                        {
                            //当前数据库表名
                            string strTableName = item.Table;
                            //弹窗提示
                            DatabaseConversion.Views.WaitingMessage.WaitMessage.Show(string.Format("正在导出表：{0}", strTableName));
                            //刷新界面
                            DatabaseConversion.Common.App.DoEvents();
                            //创建数据库表(GRID拼写出建表字典)
                            Dictionary<string, string> dicFieldNameType = GridModelGoGridDic(DicTablesStructure[strTableName]);
                            //获得目标与源的字段名对应关系
                            Dictionary<string, string> dicCorrespondenceBetween = GetTargetAndSourceCorrespondenceBetween(DicTablesStructure[strTableName]);
                            //获得源数据表中的数据(如果数据为空退出当前循环)
                            DataTable dtSourceData = GetSourceDataTable(strTableName);
                            if (dtSourceData.Rows.Count < 1) { continue; }
                            //创建目标数据库连接:对不同数据库进行判断
                            if (IsTargetString == "SqlServer")
                            {
                                SqlServerHelper sqlHelper = new SqlServerHelper();
                                sqlHelper.SqlServerConnectionString(TargetConnectionString);
                                sqlHelper.Open();
                                //创建数据库表(执行建表语句)
                                if (sqlHelper.CreateTable(strTableName, dicFieldNameType) == -1)
                                {
                                    //创建数据库表失败
                                    sqlHelper.Close();
                                    continue;
                                }
                                //向数据库表写入数据,开启事务(DataTable保存到目标数据库)
                                sqlHelper.BeginTransaction();
                                int iPerformNumber = sqlHelper.SaveByteData(strTableName, dtSourceData, dicCorrespondenceBetween);
                                if (iPerformNumber > 0)
                                {
                                    sqlHelper.CommitTransaction();
                                }
                                else
                                {
                                    sqlHelper.RollbackTransaction();
                                }
                                sqlHelper.Close();
                            }
                            else if (IsTargetString == "Oracle")
                            {
                                OracleHelper sqlHelper = new OracleHelper();
                                sqlHelper.OracleConnectionString(TargetConnectionString);
                                sqlHelper.Open();
                                if (sqlHelper.CreateTable(strTableName, dicFieldNameType) == -1)
                                {
                                    sqlHelper.Close();
                                    continue;
                                }
                                sqlHelper.BeginTransaction();
                                int iPerformNumber = sqlHelper.SaveByteData(strTableName, dtSourceData, dicCorrespondenceBetween);
                                if (iPerformNumber > 0)
                                {
                                    sqlHelper.CommitTransaction();
                                }
                                else
                                {
                                    sqlHelper.RollbackTransaction();
                                }
                                sqlHelper.Close();
                            }
                            else if (IsTargetString == "MySql")
                            {
                                MySqlHelper sqlHelper = new MySqlHelper();
                                sqlHelper.MySqlConnectionString(TargetConnectionString);
                                sqlHelper.Open();
                                if (sqlHelper.CreateTable(strTableName, dicFieldNameType) == -1)
                                {
                                    sqlHelper.Close();
                                    continue;
                                }
                                sqlHelper.BeginTransaction();
                                int iPerformNumber = sqlHelper.SaveByteData(strTableName, dtSourceData, dicCorrespondenceBetween);
                                if (iPerformNumber > 0)
                                {
                                    sqlHelper.CommitTransaction();
                                }
                                else
                                {
                                    sqlHelper.RollbackTransaction();
                                }
                                sqlHelper.Close();
                            }
                            else if (IsTargetString == "Access")
                            {
                                AccessHelper sqlHelper = new AccessHelper();
                                sqlHelper.AccessConnectionString(TargetConnectionString);
                                sqlHelper.Open();
                                if (sqlHelper.CreateTable(strTableName, dicFieldNameType) == -1)
                                {
                                    sqlHelper.Close();
                                    continue;
                                }
                                sqlHelper.BeginTransaction();
                                int iPerformNumber = sqlHelper.SaveByteData(strTableName, dtSourceData, dicCorrespondenceBetween);
                                if (iPerformNumber > 0)
                                {
                                    sqlHelper.CommitTransaction();
                                }
                                else
                                {
                                    sqlHelper.RollbackTransaction();
                                }
                                sqlHelper.Close();
                            }
                            else if (IsTargetString == "SQLite")
                            {
                                SQLiteHelper sqlHelper = new SQLiteHelper();
                                sqlHelper.SQLiteConnectionString(TargetConnectionString);
                                sqlHelper.Open();
                                if (sqlHelper.CreateTable(strTableName, dicFieldNameType) == -1)
                                {
                                    sqlHelper.Close();
                                    continue;
                                }
                                sqlHelper.BeginTransaction();
                                int iPerformNumber = sqlHelper.SaveByteData(strTableName, dtSourceData, dicCorrespondenceBetween);
                                if (iPerformNumber > 0)
                                {
                                    sqlHelper.CommitTransaction();
                                }
                                else
                                {
                                    sqlHelper.RollbackTransaction();
                                }
                                sqlHelper.Close();
                            }
                        }
                        DatabaseConversion.Views.WaitingMessage.WaitMessage.Close();
                        IsBusy = false;//关闭显示等待控件并设置窗口空闲
                        System.Windows.MessageBox.Show("保存/转换成功!!!");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("请连接源数据和目标数据!!!");
                    }
                });
            }
        }

        /// <summary>
        /// 表名Tree单击事件
        /// </summary>
        public DelegateCommand<object> TreeClickEvents
        {
            get
            {
                return new DelegateCommand<object>(delegate(object eData)
                {
                    Models.TablesTree SelectedItem = eData as Models.TablesTree;
                    try
                    {
                        GridList = DicTablesStructure[SelectedItem.Table];
                    }
                    catch (Exception ex)
                    {
                        //切换目标数据库后重新读取结构信息会触发错误
                        ADO.Helper.TXT.TXTHelper.Logs(ex.ToString());
                    }
                });
            }
        }

        /// <summary>
        /// 解析表信息(表字段类型字典解析为Models.TableStructureGrid)
        /// 目前长度不支持小数位数的读取
        /// </summary>
        /// <returns>表结构信息ObservableCollection集合</returns>
        private ObservableCollection<Models.TableStructureGrid> ParsingTableInformation(Dictionary<string, string> AllFieldNameType)
        {
            ObservableCollection<Models.TableStructureGrid> listTableStructure = new ObservableCollection<Models.TableStructureGrid>();
            foreach (var varFieldNameType in AllFieldNameType)
            {
                //字段类型
                string subFieldType = varFieldNameType.Value.Substring(0, varFieldNameType.Value.IndexOf('(') == -1 ? varFieldNameType.Value.Length : varFieldNameType.Value.IndexOf('('));
                //字段长度(长度,小数位数)
                string subLength = varFieldNameType.Value.IndexOf('(') == -1 ? "" : varFieldNameType.Value.Substring(varFieldNameType.Value.IndexOf('(') + 1, varFieldNameType.Value.IndexOf(')') - varFieldNameType.Value.IndexOf('(') - 1);
                //字段长度
                string subFieldLength = subLength.Substring(0, subLength.IndexOf(',') == -1 ? subLength.Length : subLength.IndexOf(','));
                //小数位数
                string subDecimalLength = subLength.IndexOf(',') == -1 ? "" : subLength.Substring(subLength.IndexOf(',') + 1, subLength.Length - subLength.IndexOf(',') - 1);
                //TableStructureGrid模型添加到集合
                Models.TableStructureGrid TableStructure = new Models.TableStructureGrid();
                TableStructure.Choose = true;
                TableStructure.SourceFieldName = varFieldNameType.Key;
                TableStructure.TargetFieldName = varFieldNameType.Key;
                TableStructure.FieldType = subFieldType;
                TableStructure.FieldLength = subFieldLength;
                TableStructure.DecimalLength = subDecimalLength;
                TableStructure.PrimaryKey = false;
                listTableStructure.Add(TableStructure);
            }
            return listTableStructure;
        }

        /// <summary>
        /// 根据数据库类型得到枚举类型
        /// ADO.Helper.DatabaseConversion.TypeProcessing.DataBase
        /// SqlServer/Oracle/MySql/Access/SQLite
        /// </summary>
        /// <param name="strDBType">数据库类型</param>
        /// <returns>DataBase枚举类型</returns>
        private ADO.Helper.DatabaseConversion.TypeProcessing.DataBase DBTypeGoEnum(string strDBType)
        {
            ADO.Helper.DatabaseConversion.TypeProcessing.DataBase DBType = new ADO.Helper.DatabaseConversion.TypeProcessing.DataBase();
            if (strDBType == "SqlServer")
            {
                DBType = ADO.Helper.DatabaseConversion.TypeProcessing.DataBase.SqlServer;
            }
            else if (strDBType == "Oracle")
            {
                DBType = ADO.Helper.DatabaseConversion.TypeProcessing.DataBase.Oracle;
            }
            else if (strDBType == "MySql")
            {
                DBType = ADO.Helper.DatabaseConversion.TypeProcessing.DataBase.MySql;
            }
            else if (strDBType == "Access")
            {
                DBType = ADO.Helper.DatabaseConversion.TypeProcessing.DataBase.Access;
            }
            else if (strDBType == "SQLite")
            {
                DBType = ADO.Helper.DatabaseConversion.TypeProcessing.DataBase.SQLite;
            }
            return DBType;
        }

        /// <summary>
        /// 根据数据库类型得到字段类型
        /// </summary>
        /// <param name="strDBType">数据库类型</param>
        /// <returns>目标数据库字段类型</returns>
        private ObservableCollection<string> DBTypeGoFieldType(string strDBType)
        {
            ObservableCollection<string> listFieldType = new ObservableCollection<string>();
            if (strDBType == "SqlServer")
            {
                foreach (ADO.Helper.SqlServer.SqlServerFieldType.FieldType item in Enum.GetValues(typeof(ADO.Helper.SqlServer.SqlServerFieldType.FieldType)))
                {
                    listFieldType.Add(item.ToString());
                }
            }
            else if (strDBType == "Oracle")
            {
                foreach (ADO.Helper.Oracle.OracleFieldType.FieldType item in Enum.GetValues(typeof(ADO.Helper.Oracle.OracleFieldType.FieldType)))
                {
                    listFieldType.Add(item.ToString());
                }
            }
            else if (strDBType == "MySql")
            {
                foreach (ADO.Helper.MySql.MySqlFieldType.FieldType item in Enum.GetValues(typeof(ADO.Helper.MySql.MySqlFieldType.FieldType)))
                {
                    listFieldType.Add(item.ToString());
                }
            }
            else if (strDBType == "Access")
            {
                foreach (ADO.Helper.Access.AccessFieldType.FieldType item in Enum.GetValues(typeof(ADO.Helper.Access.AccessFieldType.FieldType)))
                {
                    listFieldType.Add(item.ToString());
                }
            }
            else if (strDBType == "SQLite")
            {
                foreach (ADO.Helper.SQLite.SQLiteFieldType.FieldType item in Enum.GetValues(typeof(ADO.Helper.SQLite.SQLiteFieldType.FieldType)))
                {
                    listFieldType.Add(item.ToString());
                }
            }
            return listFieldType;
        }

        /// <summary>
        /// 根据Grid视图模型得到Grid字段类型字典
        /// </summary>
        /// <param name="listTableStructure">Grid视图模型</param>
        /// <returns>Grid字段类型字典</returns>
        private Dictionary<string, string> GridModelGoGridDic(ObservableCollection<Models.TableStructureGrid> listTableStructure)
        {
            Dictionary<string, string> dicFieldNameType = new Dictionary<string, string>();
            foreach (var vTableStructure in listTableStructure.Where(x => x.Choose == true).ToList())
            {
                string strFieldName = vTableStructure.TargetFieldName;
                string strFieldType = vTableStructure.FieldType;
                string strFieldLength = vTableStructure.FieldLength;
                string strDecimalLength = vTableStructure.DecimalLength;
                dicFieldNameType.Add(strFieldName, string.IsNullOrEmpty(strFieldLength) ? string.Format("{0}", strFieldType) : string.IsNullOrEmpty(strDecimalLength) ? string.Format("{0}({1})", strFieldType, strFieldLength) : string.Format("{0}({1},{2})", strFieldType, strFieldLength, strDecimalLength));
            }
            return dicFieldNameType;
        }

        /// <summary>
        /// 获得目标与源的字段名对应关系
        /// </summary>
        /// <param name="listTableStructure">Grid视图模型</param>
        /// <returns>目标与源的字段名对应关系(目标字段名/源字段名)</returns>
        private Dictionary<string, string> GetTargetAndSourceCorrespondenceBetween(ObservableCollection<Models.TableStructureGrid> listTableStructure)
        {
            Dictionary<string, string> CorrespondenceBetween = new Dictionary<string, string>();
            foreach (var vTableStructure in listTableStructure.Where(x => x.Choose == true).ToList())
            {
                CorrespondenceBetween.Add(SqlProcessing.RemoveIllegal(vTableStructure.TargetFieldName), SqlProcessing.RemoveIllegal(vTableStructure.SourceFieldName));
            }
            return CorrespondenceBetween;
        }

        /// <summary>
        /// 得到源数据表的DataTable
        /// </summary>
        /// <param name="strTableName">源数据表名</param>
        /// <returns>源数据表的DataTable</returns>
        private DataTable GetSourceDataTable(string strTableName)
        {
            DataTable dtSourceData = new DataTable();
            dtSourceData.TableName = strTableName;
            string sqlSelect = string.Format("SELECT * FROM {0}", strTableName);
            //对不同数据库进行判断
            if (IsSourceString == "SqlServer")
            {
                SqlServerHelper sqlHelper = new SqlServerHelper();
                sqlHelper.SqlServerConnectionString(SourceConnectionString);
                sqlHelper.Open();
                dtSourceData = sqlHelper.GetDataTable(sqlSelect);
                sqlHelper.Close();
            }
            else if (IsSourceString == "Oracle")
            {
                OracleHelper sqlHelper = new OracleHelper();
                sqlHelper.OracleConnectionString(SourceConnectionString);
                sqlHelper.Open();
                dtSourceData = sqlHelper.GetDataTable(sqlSelect);
                sqlHelper.Close();
            }
            else if (IsSourceString == "MySql")
            {
                MySqlHelper sqlHelper = new MySqlHelper();
                sqlHelper.MySqlConnectionString(SourceConnectionString);
                sqlHelper.Open();
                dtSourceData = sqlHelper.GetDataTable(sqlSelect);
                sqlHelper.Close();
            }
            else if (IsSourceString == "Access")
            {
                AccessHelper sqlHelper = new AccessHelper();
                sqlHelper.AccessConnectionString(SourceConnectionString);
                sqlHelper.Open();
                dtSourceData = sqlHelper.GetDataTable(sqlSelect);
                sqlHelper.Close();
            }
            else if (IsSourceString == "SQLite")
            {
                SQLiteHelper sqlHelper = new SQLiteHelper();
                sqlHelper.SQLiteConnectionString(SourceConnectionString);
                sqlHelper.Open();
                dtSourceData = sqlHelper.GetDataTable(sqlSelect);
                sqlHelper.Close();
            }
            return dtSourceData;
        }
    }
}