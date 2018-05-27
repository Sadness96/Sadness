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
using Sadness.BasicFunction.ViewModels.PluginMenu;
using Prism.Events;

namespace Sadness.BasicFunction.Views.PluginMenu
{
    /// <summary>
    /// DBConnection.xaml 的交互逻辑
    /// </summary>
    public partial class DBConnection : Window
    {
        /// <summary>
        /// DBConnection.xaml 的构造函数
        /// </summary>
        public DBConnection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DBConnection.xaml 的构造函数
        /// </summary>
        /// <param name="eventAggregator">Defines an interface to get instances of an event type.</param>
        /// <param name="strDataBaseName">数据库名称 SqlServer/Oracle/MySql/Access/SQLite</param>
        public DBConnection(IEventAggregator eventAggregator, string strDataBaseName)
        {
            InitializeComponent();
            this.DataContext = new DBConnectionViewModel(eventAggregator, strDataBaseName);
        }
    }
}
