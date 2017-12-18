using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
            LoadedCommand = new DelegateCommand<Window>(Loaded);
            //设置软件启动图片
            ImgStartPicture = OperationImage.ByteArrayToBitMapImage(MainLogin.GetImageByteArray("StartPicture"));
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
        /// MainLogin窗体Load事件
        /// </summary>
        /// <param name="window">主窗体</param>
        private void Loaded(Window window)
        {
            //全局获得Window窗体
            this.window = window;
            //启动MainRibbon
            StartMainRibbon();
        }
    }
}
