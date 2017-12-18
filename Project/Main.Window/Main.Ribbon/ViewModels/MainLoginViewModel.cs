using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Main.Ribbon.Utils;
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
    }
}
