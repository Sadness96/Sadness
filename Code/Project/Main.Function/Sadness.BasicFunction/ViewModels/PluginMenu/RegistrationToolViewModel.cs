using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sadness.BasicFunction.ViewModels.PluginMenu
{
    /// <summary>
    /// RegistrationTool.xaml 的视图模型
    /// </summary>
    public class RegistrationToolViewModel
    {
        /// <summary>
        /// RegistrationTool.xaml 的视图模型
        /// </summary>
        public RegistrationToolViewModel()
        {
            //应用程序标题
            Title = "注册工具";
            //应用程序背景
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#505050"));
        }

        /// <summary>
        /// 应用程序标题
        /// </summary>
        private string _Title;
        /// <summary>
        /// 应用程序标题
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        /// <summary>
        /// 应用程序背景
        /// </summary>
        private Brush _background;
        /// <summary>
        /// 应用程序背景
        /// </summary>
        public Brush Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }
    }
}
