using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Threading;
using Sadness.SQLiteDB.Connect;
using Utils.Helper.Image;
using Utils.Helper.QRCode;
using Prism.Commands;
using Prism.Mvvm;

namespace Sadness.BasicFunction.ViewModels.PluginMenu
{
    /// <summary>
    /// RecognitionQRCode.xaml 的视图模型
    /// </summary>
    public class RecognitionQRCodeViewModel : BindableBase
    {
        /// <summary>
        /// RecognitionQRCode.xaml 的视图模型
        /// </summary>
        public RecognitionQRCodeViewModel()
        {
            //委托Load方法
            LoadedCommand = new DelegateCommand<Window>(Window_Loaded);
            //应用程序标题
            Title = "识别二维码";
            //设置软件图标
            MainAppLargeIcon = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
            //关闭按钮图片
            QRCodeClose = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("QRCodeClose"));
            //FPS默认为60
            TextFPS = "60";
            //识别二维码按钮
            RecognitionBtn = "识别二维码";
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="window">主窗体</param>
        private void Window_Loaded(Window window)
        {
            //全局获得Window窗体
            this.window = window;
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

        /// <summary>
        /// FPS
        /// </summary>
        private string _textFPS;
        /// <summary>
        /// FPS
        /// </summary>
        public string TextFPS
        {
            get
            {
                return _textFPS;
            }
            set
            {
                SetProperty(ref _textFPS, value);
            }
        }

        /// <summary>
        /// 识别二维码按钮
        /// </summary>
        private string _recognitionBtn;
        /// <summary>
        /// 识别二维码按钮
        /// </summary>
        public string RecognitionBtn
        {
            get
            {
                return _recognitionBtn;
            }
            set
            {
                SetProperty(ref _recognitionBtn, value);
            }
        }

        /// <summary>
        /// 关闭按钮图片
        /// </summary>
        private ImageSource _QRCodeClose;
        /// <summary>
        /// 关闭按钮图片
        /// </summary>
        public ImageSource QRCodeClose
        {
            get
            {
                return _QRCodeClose;
            }
            set
            {
                _QRCodeClose = value;
            }
        }

        /// <summary>
        /// 全局计时器
        /// </summary>
        private DispatcherTimer timer = null;

        /// <summary>
        /// 动态识别
        /// </summary>
        public DelegateCommand RecognitionQRCode
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    //设置定时器
                    if (RecognitionBtn == "识别二维码")
                    {
                        int intFPS = 60;
                        int.TryParse(TextFPS, out intFPS);
                        timer = new DispatcherTimer();
                        timer.Interval = new TimeSpan(TimeSpan.TicksPerMinute / intFPS);
                        timer.Tick += new EventHandler(Timer_RecognitionQRCode);
                        timer.Start();
                        RecognitionBtn = "正在识别...";
                    }
                    else
                    {
                        timer.Stop();
                        RecognitionBtn = "识别二维码";
                    }
                });
            }
        }

        /// <summary>
        /// 计时器方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_RecognitionQRCode(object sender, EventArgs e)
        {
            string strQRCode = QRCodeHelper.BarcodeReader(GetWindowBitmap());
            if (string.IsNullOrEmpty(strQRCode))
            {
                //MessageBox.Show("图像未识别");
            }
            else
            {
                timer.Stop();
                RecognitionBtn = "识别二维码";
                MessageBox.Show(strQRCode);
                return;
            }
        }

        /// <summary>
        /// 获取窗口范围内的图像(Bitmap)
        /// </summary>
        /// <returns>图像Bitmap</returns>
        private System.Drawing.Bitmap GetWindowBitmap()
        {
            System.Drawing.Bitmap bit = new System.Drawing.Bitmap((int)window.Width - 20, (int)window.Height - 60);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bit);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.CopyFromScreen((int)window.Left + 10, (int)window.Top + 25, (int)0, (int)0, new System.Drawing.Size((int)window.Width, (int)window.Height));
            return bit;
        }
    }
}
