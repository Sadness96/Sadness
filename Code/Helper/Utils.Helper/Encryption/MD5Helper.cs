using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Utils.Helper.TXT;

namespace Utils.Helper.Encryption
{
    /// <summary>
    /// MD5 加密帮助类
    /// 创建日期:2017年06月16日
    /// </summary>
    public class MD5Helper
    {
        /// <summary>
        /// MD5 加密(16位)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <param name="isLower">是否小写</param>
        /// <returns>MD5 密文(16位)</returns>
        public static string MD5Encrypt16(string strPlaintext, bool isLower = true)
        {
            try
            {
                MD5 md5Crypto = MD5.Create();
                string strCiphertext = BitConverter.ToString(md5Crypto.ComputeHash(Encoding.Default.GetBytes(strPlaintext)), 4, 8);
                strCiphertext = strCiphertext.Replace("-", "");
                strCiphertext = isLower ? strCiphertext.ToLower() : strCiphertext.ToUpper();
                return strCiphertext;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// MD5 加密(32位)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <param name="isLower">是否小写</param>
        /// <returns>MD5 密文(32位)</returns>
        public static string MD5Encrypt32(string strPlaintext, bool isLower = true)
        {
            try
            {
                MD5 md5Crypto = MD5.Create();
                string strCiphertext = BitConverter.ToString(md5Crypto.ComputeHash(Encoding.Default.GetBytes(strPlaintext)));
                strCiphertext = strCiphertext.Replace("-", "");
                strCiphertext = isLower ? strCiphertext.ToLower() : strCiphertext.ToUpper();
                return strCiphertext;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取文件 MD5 值(32位)
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="isLower">是否小写</param>
        /// <returns>文件 MD5 值(32位)</returns>
        public static string FileMD5Encrypt32(Stream stream, bool isLower = true)
        {
            try
            {
                MD5 md5 = MD5.Create();
                byte[] byteHash = md5.ComputeHash(stream);
                var stringBuilder = new StringBuilder();
                for (int i = 0; i < byteHash.Length; i++)
                {
                    stringBuilder.Append(byteHash[i].ToString(isLower ? "x2" : "X2"));
                }
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取文件 MD5 值(32位)
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="isLower">是否小写</param>
        /// <returns>文件 MD5 值(32位)</returns>
        public static string FileMD5Encrypt32(string strFilePath, bool isLower = true)
        {
            var fileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
            try
            {
                return FileMD5Encrypt32(fileStream, isLower);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
            finally
            {
                fileStream.Close();
            }
        }
    }
}
