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
    /// SHA1 加密帮助类
    /// 创建日期:2017年06月16日
    /// </summary>
    public class SHA1Helper
    {
        /// <summary>
        /// SHA1 加密(40位小写)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <returns>SHA1 密文(40位小写)</returns>
        public static string SHA1Encrypt_40Lower(string strPlaintext)
        {
            try
            {
                SHA1 sha1Crypto = new SHA1CryptoServiceProvider();
                byte[] bytes_sha1_in = Encoding.Default.GetBytes(strPlaintext);
                byte[] bytes_sha1_out = sha1Crypto.ComputeHash(bytes_sha1_in);
                string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
                str_sha1_out = str_sha1_out.Replace("-", "");
                str_sha1_out = str_sha1_out.ToLower();
                return str_sha1_out;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// SHA1 加密(40位大写)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <returns>SHA1 密文(40位大写)</returns>
        public static string SHA1Encrypt_40Upper(string strPlaintext)
        {
            try
            {
                SHA1 sha1Crypto = new SHA1CryptoServiceProvider();
                byte[] bytes_sha1_in = Encoding.Default.GetBytes(strPlaintext);
                byte[] bytes_sha1_out = sha1Crypto.ComputeHash(bytes_sha1_in);
                string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
                str_sha1_out = str_sha1_out.Replace("-", "");
                str_sha1_out = str_sha1_out.ToUpper();
                return str_sha1_out;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取文件 SHA1 值(40位小写)
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>文件 SHA1 值(40位小写)</returns>
        public static string FileSHA1Encrypt_40Lower(string strFilePath)
        {
            try
            {
                FileStream fileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] byteHash = sha1.ComputeHash(fileStream);
                fileStream.Close();
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < byteHash.Length; i++)
                {
                    stringBuilder.Append(byteHash[i].ToString("x2"));
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
        /// 获取文件 SHA1 值(40位大写)
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>文件 SHA1 值(40位大写)</returns>
        public static string FileSHA1Encrypt_40Upper(string strFilePath)
        {
            try
            {
                FileStream fileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] byteHash = sha1.ComputeHash(fileStream);
                fileStream.Close();
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < byteHash.Length; i++)
                {
                    stringBuilder.Append(byteHash[i].ToString("X2"));
                }
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }
    }
}
