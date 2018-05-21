using System;
using System.Collections.Generic;
using System.Data;
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

namespace Sadness.BasicFunction.Views.PluginMenu
{
    /// <summary>
    /// FileSharingSettings.xaml 的交互逻辑
    /// </summary>
    public partial class FileSharingSettings : Window
    {
        /// <summary>
        /// FileSharingSettings.xaml 的构造函数
        /// </summary>
        public FileSharingSettings()
        {
            InitializeComponent();
            this.DataContext = new FileSharingSettingsViewModel();
        }

        /// <summary>
        /// FileSharingSettings.xaml 的构造函数
        /// </summary>
        /// <param name="mySelectedElement">选中数据</param>
        public FileSharingSettings(DataRowView mySelectedElement)
        {
            InitializeComponent();
            this.DataContext = new FileSharingSettingsViewModel(mySelectedElement);
        }
    }
}
