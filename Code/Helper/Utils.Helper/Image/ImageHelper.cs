using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Utils.Helper.Image
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 转化图片二进制流到BitmapImage
        /// </summary>
        /// <param name="byteArray">图片二进制流</param>
        /// <returns>成功返回BitmapImage,失败返回null</returns>
        public static BitmapImage ByteArrayToBitMapImage(byte[] byteArray)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 转化图片二进制流到ImageSource
        /// </summary>
        /// <param name="byteArray">图片二进制流</param>
        /// <returns>成功返回ImageSource,失败返回null</returns>
        public static ImageSource ByteArrayToImageSource(byte[] byteArray)
        {
            try
            {
                Bitmap bitmap = ByteArrayToBitmap(byteArray);
                IntPtr hBitmap = bitmap.GetHbitmap();
                ImageSource imageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                return imageSource;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 转化图片二进制流到Bitmap
        /// </summary>
        /// <param name="byteArray">图片二进制流</param>
        /// <returns>成功返回Bitmap,失败返回null</returns>
        public static Bitmap ByteArrayToBitmap(byte[] byteArray)
        {
            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream(byteArray);
                return new Bitmap((System.Drawing.Image)new Bitmap(memoryStream));
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                memoryStream.Close();
            }
        }
    }
}
