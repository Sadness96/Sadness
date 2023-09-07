using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper.TXT;

namespace Utils.Helper.Encryption
{
    /// <summary>
    /// Base64 加密帮助类
    /// 创建日期:2018年03月30日
    /// </summary>
    public class Base64Helper
    {
        /// <summary>
        /// Base64 加密
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <returns>Base64密文</returns>
        public static string Base64Encrypt(string strPlaintext)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(strPlaintext);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// Base64 解密
        /// </summary>
        /// <param name="strCiphertext">Base64 密文</param>
        /// <returns>明文</returns>
        public static string Base64Decrypt(string strCiphertext)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(strCiphertext);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 图片 Base64 加密
        /// </summary>
        /// <param name="strImagePath">图片路径</param>
        /// <param name="imageFormat">指定图像格式</param>
        /// <returns>Base64 密文</returns>
        public static string ImageBase64Encrypt(string strImagePath, ImageFormat imageFormat)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                Bitmap bitmap = new Bitmap(strImagePath);
                if (imageFormat == null)
                {
                    imageFormat = GetImageFormatFromPath(strImagePath);
                }
                bitmap.Save(memoryStream, imageFormat);
                byte[] bytes = memoryStream.GetBuffer();
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 图片 Base64 解密
        /// </summary>
        /// <param name="strCiphertext">Base64 密文</param>
        /// <param name="strSaveFilePath">解密图片目录</param>
        /// <param name="imageFormat">指定图像格式</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool ImageBase64Decrypt(string strCiphertext, string strSaveFilePath, ImageFormat imageFormat)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(strCiphertext);
                MemoryStream memoryStream = new MemoryStream(bytes);
                Bitmap bitmap = new Bitmap(memoryStream);
                if (imageFormat == null)
                {
                    imageFormat = GetImageFormatFromPath(strSaveFilePath);
                }
                bitmap.Save(strSaveFilePath, imageFormat);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 根据图片路径获得图片格式(缺少MemoryBmp)
        /// </summary>
        /// <param name="strImagePath">图片路径</param>
        /// <returns>图片格式</returns>
        public static ImageFormat GetImageFormatFromPath(string strImagePath)
        {
            try
            {
                string strImageExtension = Path.GetExtension(strImagePath).ToLower();
                if (string.IsNullOrEmpty(strImageExtension))
                {
                    return null;
                }
                else
                {
                    if (strImageExtension.Equals(".bmp") || strImageExtension.Equals(".rle") || strImageExtension.Equals(".dlb"))
                    {
                        return ImageFormat.Bmp;
                    }
                    else if (strImageExtension.Equals(".emf"))
                    {
                        return ImageFormat.Emf;
                    }
                    else if (strImageExtension.Equals(".exif"))
                    {
                        return ImageFormat.Exif;
                    }
                    else if (strImageExtension.Equals(".gif"))
                    {
                        return ImageFormat.Gif;
                    }
                    else if (strImageExtension.Equals(".ico"))
                    {
                        return ImageFormat.Icon;
                    }
                    else if (strImageExtension.Equals(".jpg") || strImageExtension.Equals(".jpeg") || strImageExtension.Equals(".jpe"))
                    {
                        return ImageFormat.Jpeg;
                    }
                    else if (strImageExtension.Equals(".png") || strImageExtension.Equals(".pns"))
                    {
                        return ImageFormat.Png;
                    }
                    else if (strImageExtension.Equals(".tif") || strImageExtension.Equals(".tiff"))
                    {
                        return ImageFormat.Tiff;
                    }
                    else if (strImageExtension.Equals(".wmf"))
                    {
                        return ImageFormat.Wmf;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }
    }
}
