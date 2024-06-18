using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utils.Helper.TXT;

namespace Utils.Helper.Image
{
    /// <summary>
    /// 图像帮助类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 以只读方式加载图片
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <returns></returns>
        public static BitmapImage ImageToBitmapImage(string imagePath)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                return bitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 转化图片二进制流到 BitmapImage
        /// </summary>
        /// <param name="byteArray">图片二进制流</param>
        /// <returns>成功返回BitmapImage,失败返回null</returns>
        public static BitmapImage ByteArrayToBitMapImage(byte[] byteArray)
        {
            try
            {
                using (var memoryStream = new MemoryStream(byteArray))
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
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 转化图片二进制流到 ImageSource
        /// </summary>
        /// <param name="byteArray">图片二进制流</param>
        /// <returns>成功返回ImageSource,失败返回null</returns>
        public static ImageSource ByteArrayToImageSource(byte[] byteArray)
        {
            try
            {
                if (byteArray != null)
                {
                    Bitmap bitmap = ByteArrayToBitmap(byteArray);
                    IntPtr hBitmap = bitmap.GetHbitmap();
                    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 转化图片二进制流到 Bitmap
        /// </summary>
        /// <param name="byteArray">图片二进制流</param>
        /// <returns>成功返回Bitmap,失败返回null</returns>
        public static Bitmap ByteArrayToBitmap(byte[] byteArray)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    return new Bitmap(memoryStream);
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 注销对象方法API
        /// </summary>
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// Bitmap 转 ImageSource
        /// </summary>
        /// <param name="bitmap">位图</param>
        /// <returns>ImageSource</returns>
        public static ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            try
            {
                IntPtr hBitmap = bitmap.GetHbitmap();
                BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(hBitmap);
                return bitmapSource;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        ///  Bitmap 转 图片二进制流
        /// </summary>
        /// <param name="bitmap">位图</param>
        /// <param name="imageFormat">文件格式(Null默认Bmp)</param>
        /// <returns>图片二进制流</returns>
        public static byte[] BitmapToByteArray(Bitmap bitmap, ImageFormat imageFormat)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                bitmap.Save(memoryStream, imageFormat == null ? ImageFormat.Bmp : imageFormat);
                return memoryStream.GetBuffer();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
            finally
            {
                memoryStream.Close();
            }
        }
    }
}
