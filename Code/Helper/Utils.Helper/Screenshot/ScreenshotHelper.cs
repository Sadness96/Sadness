using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utils.Helper.TXT;

namespace Utils.Helper.Screenshot
{
    /// <summary>
    /// 屏幕截图帮助类
    /// 创建日期:2017年06月21日
    /// </summary>
    public class ScreenshotHelper
    {
        /// <summary>
        /// 全屏幕截图
        /// </summary>
        /// <returns>截图Bitmap</returns>
        public static Bitmap ScreenshotFullScreen()
        {
            try
            {
                // 得到屏幕工作区域宽度
                // double x = SystemParameters.WorkArea.Width;
                // 得到屏幕工作区域高度
                // double y = SystemParameters.WorkArea.Height;
                // 得到屏幕整体宽度
                double dPrimaryScreenWidth = SystemParameters.PrimaryScreenWidth;
                // 得到屏幕整体高度
                double dPrimaryScreenHeight = SystemParameters.PrimaryScreenHeight;
                // 初始化使用指定的大小(屏幕大小)的 System.Drawing.Bitmap 类的新实例.
                Bitmap bitmapScreenshot = new Bitmap((int)dPrimaryScreenWidth, (int)dPrimaryScreenHeight);
                // 从指定的载入原创建新的 System.Drawing.Graphics.
                Graphics graphicsScreenshot = Graphics.FromImage(bitmapScreenshot);
                // 获取或设置绘制到此 System.Drawing.Graphics 的渲染质量:高质量 低速度合成.
                graphicsScreenshot.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                // 截取电脑屏幕:从屏幕到 System.Drawing.Graphics 的绘图图面.
                graphicsScreenshot.CopyFromScreen((int)0, (int)0, (int)0, (int)0, new System.Drawing.Size((int)dPrimaryScreenWidth, (int)dPrimaryScreenHeight));
                return bitmapScreenshot;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 截取指定位置截图
        /// </summary>
        /// <param name="iStartX">截取起始坐标 X</param>
        /// <param name="iStartY">截取起始坐标 Y</param>
        /// <param name="iInterceptWidth">截取宽度</param>
        /// <param name="iInterceptHeight">截取高度</param>
        /// <returns>截图Bitmap</returns>
        public static Bitmap ScreenshotsSpecifyLocation(int iStartX, int iStartY, int iInterceptWidth, int iInterceptHeight)
        {
            try
            {
                // 初始化使用指定的大小(屏幕大小)的 System.Drawing.Bitmap 类的新实例.
                Bitmap bitmapScreenshot = new Bitmap((int)iInterceptWidth, (int)iInterceptHeight);
                // 从指定的载入原创建新的 System.Drawing.Graphics.
                Graphics graphicsScreenshot = Graphics.FromImage(bitmapScreenshot);
                // 获取或设置绘制到此 System.Drawing.Graphics 的渲染质量:高质量 低速度合成.
                graphicsScreenshot.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                // 截取电脑屏幕:从屏幕到 System.Drawing.Graphics 的绘图图面.
                graphicsScreenshot.CopyFromScreen(iStartX, iStartY, (int)0, (int)0, new System.Drawing.Size((int)iInterceptWidth, (int)iInterceptHeight));
                return bitmapScreenshot;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 保存屏幕截图位图到指定位置
        /// </summary>
        /// <param name="strSavePath">文件保存位置</param>
        /// <param name="bitmapScreenshot">屏幕截图位图</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool SaveBitmap(string strSavePath, Bitmap bitmapScreenshot)
        {
            try
            {
                ImageFormat imageFormat;
                switch (System.IO.Path.GetExtension(strSavePath))
                {
                    case ".bmp": imageFormat = ImageFormat.Bmp; break;
                    case ".emf": imageFormat = ImageFormat.Emf; break;
                    case ".exif": imageFormat = ImageFormat.Exif; break;
                    case ".gif": imageFormat = ImageFormat.Gif; break;
                    case ".icon": imageFormat = ImageFormat.Icon; break;
                    case ".jpeg": imageFormat = ImageFormat.Jpeg; break;
                    case ".jpg": imageFormat = ImageFormat.Jpeg; break;
                    case ".memorybmp": imageFormat = ImageFormat.MemoryBmp; break;
                    case ".png": imageFormat = ImageFormat.Png; break;
                    case ".tiff": imageFormat = ImageFormat.Tiff; break;
                    case ".wmf": imageFormat = ImageFormat.Wmf; break;
                    default: imageFormat = ImageFormat.Png; break;
                }
                bitmapScreenshot.Save(strSavePath, imageFormat);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }
    }
}
