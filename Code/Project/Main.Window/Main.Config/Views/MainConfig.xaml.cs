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
using Main.Config.ViewModels;

namespace Main.Config.Views
{
    /// <summary>
    /// MainConfig.xaml 的交互逻辑
    /// </summary>
    public partial class MainConfig : Window
    {
        /// <summary>
        /// MainConfig.xaml 的构造函数
        /// </summary>
        public MainConfig()
        {
            InitializeComponent();
            this.DataContext = new MainConfigViewModel();
        }
    }
}
