using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using Prism.Commands;

namespace Main.Config.ViewModels
{
    /// <summary>
    /// MainConfig.xaml 的视图模型
    /// </summary>
    public class MainConfigViewModel
    {
        /// <summary>
        /// MainConfig.xaml 的视图模型
        /// </summary>
        public MainConfigViewModel()
        {
            //委托Load方法
            LoadedCommand = new DelegateCommand<Window>(Window_Loaded);
        }

        /// <summary>
        /// MainConfig窗体Load事件
        /// </summary>
        /// <param name="window">主窗体</param>
        private void Window_Loaded(Window window)
        {
            //全局获得Window窗体
            this.window = window;
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
