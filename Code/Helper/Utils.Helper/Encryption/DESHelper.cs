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
    /// DES 加密解密帮助类
    /// 创建日期:2017年06月16日
    /// </summary>
    public class DESHelper
    {
        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <param name="strKey">秘钥(8位)</param>
        /// <param name="strIV">向量(8位)</param>
        /// <returns>DES密文</returns>
        public static string DESEncrypt(string strPlaintext, string strKey, string strIV)
        {
            try
            {
                DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
                desCrypto.Key = Encoding.Default.GetBytes(strKey);
                desCrypto.IV = Encoding.UTF8.GetBytes(strIV);
                using (ICryptoTransform cryptoTransform = desCrypto.CreateEncryptor())
                {
                    byte[] byteBaseUTF8 = Encoding.UTF8.GetBytes(strPlaintext);
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(byteBaseUTF8, 0, byteBaseUTF8.Length);
                            cryptoStream.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="strCiphertext">DES 密文</param>
        /// <param name="strKey">秘钥(8位)</param>
        /// <param name="strIV">向量(8位)</param>
        /// <returns>明文</returns>
        public static string DESDecrypt(string strCiphertext, string strKey, string strIV)
        {
            try
            {
                DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
                desCrypto.Key = Encoding.Default.GetBytes(strKey);
                desCrypto.IV = Encoding.UTF8.GetBytes(strIV);
                using (ICryptoTransform cryptoTransform = desCrypto.CreateDecryptor())
                {
                    byte[] byteBase64 = Convert.FromBase64String(strCiphertext);
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(byteBase64, 0, byteBase64.Length);
                            cryptoStream.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 文件 DES 加密
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="strSaveFilePath">加密文件目录</param>
        /// <param name="strKey">秘钥(8位)</param>
        /// <param name="strIV">向量(8位)</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FileDESEncrypt(string strFilePath, string strSaveFilePath, string strKey, string strIV)
        {
            try
            {
                DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
                desCrypto.Key = Encoding.Default.GetBytes(strKey);
                desCrypto.IV = Encoding.UTF8.GetBytes(strIV);
                FileStream fileStream = File.OpenRead(strFilePath);
                byte[] byteFileStream = new byte[fileStream.Length];
                fileStream.Read(byteFileStream, 0, (int)fileStream.Length);
                fileStream.Close();
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, desCrypto.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(byteFileStream, 0, byteFileStream.Length);
                        cryptoStream.FlushFinalBlock();
                        fileStream = File.OpenWrite(strSaveFilePath);
                        foreach (byte byteMemoryStream in memoryStream.ToArray())
                        {
                            fileStream.WriteByte(byteMemoryStream);
                        }
                        fileStream.Close();
                        cryptoStream.Close();
                        memoryStream.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 文件 DES 解密
        /// </summary>
        /// <param name="strFilePath">被加密的文件路径</param>
        /// <param name="strSaveFilePath">解密文件目录</param>
        /// <param name="strKey">秘钥(8位)</param>
        /// <param name="strIV">向量(8位)</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FileDESDecrypt(string strFilePath, string strSaveFilePath, string strKey, string strIV)
        {
            try
            {
                DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
                desCrypto.Key = Encoding.Default.GetBytes(strKey);
                desCrypto.IV = Encoding.UTF8.GetBytes(strIV);
                FileStream fileStream = File.OpenRead(strFilePath);
                byte[] byteFileStream = new byte[fileStream.Length];
                fileStream.Read(byteFileStream, 0, (int)fileStream.Length);
                fileStream.Close();
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, desCrypto.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(byteFileStream, 0, byteFileStream.Length);
                        cryptoStream.FlushFinalBlock();
                        fileStream = File.OpenWrite(strSaveFilePath);
                        foreach (byte byteMemoryStream in memoryStream.ToArray())
                        {
                            fileStream.WriteByte(byteMemoryStream);
                        }
                        fileStream.Close();
                        cryptoStream.Close();
                        memoryStream.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }
    }
}
