using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Main.Ribbon.Utils;
using Prism.Commands;
using IceElves.SQLiteDB.Connect;

namespace Main.Ribbon.ViewModels
{
    /// <summary>
    /// MainLogin.xaml 的视图模型
    /// </summary>
    public class MainLoginViewModel
    {
        /// <summary>
        /// MainLogin.xaml 的视图模型
        /// </summary>
        public MainLoginViewModel()
        {
            //委托Load方法
            LoadedCommand = new DelegateCommand<Window>(Window_Loaded);
            //设置软件标题
            MainTitle = "MainLogin";
            //设置软件图标
            MainAppIcon = OperationImage.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
            //设置软件启动图片
            ImgStartPicture = OperationImage.ByteArrayToBitMapImage(MainImage.GetImageByteArray("StartPicture"));
        }

        /// <summary>
        /// MainLogin窗体Load事件
        /// </summary>
        /// <param name="window">主窗体</param>
        private void Window_Loaded(Window window)
        {
            //全局获得Window窗体
            this.window = window;
            //启动MainRibbon
            StartMainRibbon();
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
        /// 应用程序标题
        /// </summary>
        private string _mainTitle;
        /// <summary>
        /// 应用程序标题
        /// </summary>
        public string MainTitle
        {
            get
            {
                return _mainTitle;
            }
            set
            {
                _mainTitle = value;
            }
        }

        /// <summary>
        /// 应用程序图标
        /// </summary>
        private ImageSource _mainAppIcon;
        /// <summary>
        /// 应用程序图标
        /// </summary>
        public ImageSource MainAppIcon
        {
            get
            {
                return _mainAppIcon;
            }
            set
            {
                _mainAppIcon = value;
            }
        }

        /// <summary>
        /// 软件启动图片
        /// </summary>
        private BitmapImage _imgStartPicture;
        /// <summary>
        /// 软件启动图片
        /// </summary>
        public BitmapImage ImgStartPicture
        {
            get
            {
                return _imgStartPicture;
            }
            set
            {
                _imgStartPicture = value;
            }
        }

        /// <summary>
        /// 启动MainRibbon
        /// </summary>
        private void StartMainRibbon()
        {
            Main.Ribbon.Views.MainRibbon form = new Main.Ribbon.Views.MainRibbon();
            form.CloseEvent += new Main.Ribbon.Views.MainRibbon.CloseDelegate(CloseEvent);
            form.ShowDialog();
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        private void CloseEvent()
        {
            window.Close();
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
