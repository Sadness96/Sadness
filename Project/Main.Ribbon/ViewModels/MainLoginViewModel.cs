using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
            byte[] byteArray = File.ReadAllBytes(@"..\Project\Main.Ribbon\Images\LoginImage.png");
            BitmapImage bitmapImage = ByteArrayToBitMapImage(byteArray);
            ImgStartPicture = bitmapImage;
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
        /// 转化图片二进制流到BitMapImage
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private BitmapImage ByteArrayToBitMapImage(byte[] byteArray)
        {
            using (var memoryStream = new System.IO.MemoryStream(byteArray))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
    }
}
