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
        /// MD5 加密(16位小写)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <returns>MD5 密文(16位小写)</returns>
        public static string MD5Encrypt_16Lower(string strPlaintext)
        {
            try
            {
                MD5 md5Crypto = MD5.Create();
                string strCiphertext = BitConverter.ToString(md5Crypto.ComputeHash(Encoding.Default.GetBytes(strPlaintext)), 4, 8);
                strCiphertext = strCiphertext.Replace("-", "");
                strCiphertext = strCiphertext.ToLower();
                return strCiphertext;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// MD5 加密(16位大写)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <returns>MD5 密文(16位小写)</returns>
        public static string MD5Encrypt_16Upper(string strPlaintext)
        {
            try
            {
                MD5 md5Crypto = MD5.Create();
                string strCiphertext = BitConverter.ToString(md5Crypto.ComputeHash(Encoding.Default.GetBytes(strPlaintext)), 4, 8);
                strCiphertext = strCiphertext.Replace("-", "");
                strCiphertext = strCiphertext.ToUpper();
                return strCiphertext;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// MD5 加密(32位小写)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <returns>MD5 密文(32位小写)</returns>
        public static string MD5Encrypt_32Lower(string strPlaintext)
        {
            try
            {
                MD5 md5Crypto = MD5.Create();
                string strCiphertext = BitConverter.ToString(md5Crypto.ComputeHash(Encoding.Default.GetBytes(strPlaintext)));
                strCiphertext = strCiphertext.Replace("-", "");
                strCiphertext = strCiphertext.ToLower();
                return strCiphertext;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// MD5 加密(32位大写)
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <returns>MD5 密文(32位小写)</returns>
        public static string MD5Encrypt_32Upper(string strPlaintext)
        {
            try
            {
                MD5 md5Crypto = MD5.Create();
                string strCiphertext = BitConverter.ToString(md5Crypto.ComputeHash(Encoding.Default.GetBytes(strPlaintext)));
                strCiphertext = strCiphertext.Replace("-", "");
                strCiphertext = strCiphertext.ToUpper();
                return strCiphertext;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取文件 MD5 值(32位小写)
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>文件 MD5 值(32位小写)</returns>
        public static string FileMD5Encrypt_32Lower(string strFilePath)
        {
            try
            {
                FileStream fileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
                MD5 md5 = MD5.Create();
                byte[] byteHash = md5.ComputeHash(fileStream);
                fileStream.Close();
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < byteHash.Length; i++)
                {
                    stringBuilder.Append(byteHash[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取文件 MD5 值(32位大写)
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>文件 MD5 值(32位大写)</returns>
        public static string FileMD5Encrypt_32Upper(string strFilePath)
        {
            try
            {
                FileStream fileStream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
                MD5 md5 = MD5.Create();
                byte[] byteHash = md5.ComputeHash(fileStream);
                fileStream.Close();
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < byteHash.Length; i++)
                {
                    stringBuilder.Append(byteHash[i].ToString("X2"));
                }
                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
