using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Main.Ribbon.Utils
{
    /// <summary>
    /// 操作图片方法
    /// </summary>
    public class OperationImage
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
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
