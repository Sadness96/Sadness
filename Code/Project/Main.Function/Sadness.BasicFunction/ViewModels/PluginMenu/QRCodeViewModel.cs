using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Sadness.SQLiteDB.Connect;
using Utils.Helper.Image;
using Utils.Helper.QRCode;
using Prism.Mvvm;
using Prism.Commands;
using Sadness.BasicFunction.Views.PluginMenu;

namespace Sadness.BasicFunction.ViewModels.PluginMenu
{
    /// <summary>
    /// QRCode.xaml 的视图模型
    /// </summary>
    public class QRCodeViewModel : BindableBase
    {
        /// <summary>
        /// QRCode.xaml 的视图模型
        /// </summary>
        public QRCodeViewModel()
        {
            //应用程序标题
            Title = "生成二维码工具";
            //设置软件图标
            MainAppLargeIcon = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
            //默认不使用LOGO
            BISExistenceLogo = false;
            //设置初始二维码LOGO
            QRCodeLogo = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("QRCodeLogo"));
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
        /// 二维码LOGO
        /// </summary>
        private ImageSource _QRCodeLogo;
        /// <summary>
        /// 二维码LOGO
        /// </summary>
        public ImageSource QRCodeLogo
        {
            get
            {
                return _QRCodeLogo;
            }
            set
            {
                SetProperty(ref _QRCodeLogo, value);
            }
        }

        /// <summary>
        /// 二维码
        /// </summary>
        private ImageSource _QRCodeSource;
        /// <summary>
        /// 二维码
        /// </summary>
        public ImageSource QRCodeSource
        {
            get
            {
                return _QRCodeSource;
            }
            set
            {
                SetProperty(ref _QRCodeSource, value);
            }
        }

        /// <summary>
        /// 二维码文本
        /// </summary>
        private string _QRCodeText;
        /// <summary>
        /// 二维码文本
        /// </summary>
        public string QRCodeText
        {
            get
            {
                return _QRCodeText;
            }
            set
            {
                SetProperty(ref _QRCodeText, value);
            }
        }

        /// <summary>
        /// 是否使用LOGO图片
        /// </summary>
        private bool _bISExistenceLogo;
        /// <summary>
        /// 是否使用LOGO图片
        /// </summary>
        public bool BISExistenceLogo
        {
            get
            {
                return _bISExistenceLogo;
            }
            set
            {
                SetProperty(ref _bISExistenceLogo, value);
            }
        }

        /// <summary>
        /// 二维码LOGO图片
        /// </summary>
        private string _QRCodeLogoPath;
        /// <summary>
        /// 二维码LOGO图片
        /// </summary>
        public string QRCodeLogoPath
        {
            get
            {
                return _QRCodeLogoPath;
            }
            set
            {
                SetProperty(ref _QRCodeLogoPath, value);
            }
        }

        /// <summary>
        /// 全局拿到二维码点阵图(用于生成图片文件)
        /// </summary>
        private Bitmap _bitmapQRCode;
        /// <summary>
        /// 全局拿到二维码点阵图(用于生成图片文件)
        /// </summary>
        public Bitmap BitmapQRCode
        {
            get
            {
                return _bitmapQRCode;
            }
            set
            {
                SetProperty(ref _bitmapQRCode, value);
            }
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        public DelegateCommand GenerateQRCode
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    if (string.IsNullOrEmpty(QRCodeText))
                    {
                        MessageBox.Show("请输入文本!");
                        return;
                    }
                    if (BISExistenceLogo)
                    {
                        //使用LOGO
                        BitmapQRCode = QRCodeHelper.GetQRCode_logo(QRCodeText, 500, 500, QRCodeLogoPath);
                        QRCodeSource = QRCodeHelper.GetImageSourceFromBitmap(BitmapQRCode);
                    }
                    else
                    {
                        //不使用LOGO
                        BitmapQRCode = QRCodeHelper.GetQRCode(QRCodeText, 500, 500);
                        QRCodeSource = QRCodeHelper.GetImageSourceFromBitmap(BitmapQRCode);
                    }
                });
            }
        }

        /// <summary>
        /// 选择图片
        /// </summary>
        public DelegateCommand ChoicePicture
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    dialog.Title = "选择logo";
                    dialog.Filter = "图片文件(*.jpg,*png,*.gif,*.bmp)|*.jpg;*.png;*.gif;*.bmp";
                    if (dialog.ShowDialog() == true)
                    {
                        //使用LOGO
                        BISExistenceLogo = true;
                        //保存图片路径
                        QRCodeLogoPath = dialog.FileName;
                        //修改LOGO图片
                        QRCodeLogo = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
                    }
                });
            }
        }

        /// <summary>
        /// 清空图片
        /// </summary>
        public DelegateCommand EmptyPicture
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    //不使用LOGO
                    BISExistenceLogo = false;
                    //设置初始二维码LOGO
                    QRCodeLogo = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("QRCodeLogo"));
                });
            }
        }

        /// <summary>
        /// 导出二维码
        /// </summary>
        public DelegateCommand ExportQRCode
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    if (BitmapQRCode == null)
                    {
                        MessageBox.Show("请先生成二维码!");
                        return;
                    }
                    //默认保存为PNG图片
                    Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                    dialog.Title = "保存输出文件";
                    dialog.Filter = "PNG文件|*.png";
                    if (dialog.ShowDialog() == true)
                    {
                        QRCodeHelper.SaveBitmap(dialog.FileName, BitmapQRCode);
                    }
                });
            }
        }

        /// <summary>
        /// 图片识别
        /// </summary>
        public DelegateCommand PictureDistinguish
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    dialog.Filter = "二维码图片|*.JPG;*.PNG;*.BMP|全部文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strQRCode = QRCodeHelper.BarcodeReader(dialog.FileName);
                        if (string.IsNullOrEmpty(strQRCode))
                        {
                            MessageBox.Show("图像未识别");
                        }
                        else
                        {
                            MessageBox.Show(strQRCode);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 动态识别
        /// </summary>
        public DelegateCommand DynamicDistinguish
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    RecognitionQRCode form = new RecognitionQRCode();
                    form.ShowDialog();
                });
            }
        }
    }
}
