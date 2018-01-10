using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper.TXT;

namespace Utils.Helper.Encryption
{
    /// <summary>
    /// AES加密解密帮助类
    /// 创建日期:2018年1月10日
    /// </summary>
    public class AESHelper
    {
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <param name="strKey">秘钥</param>
        /// <returns>AES密文</returns>
        public static string AESEncrypt(string strPlaintext, string strKey)
        {
            try
            {
                if (string.IsNullOrEmpty(strPlaintext))
                {
                    return string.Empty;
                }
                strKey = strKey.Length < 32 ? strKey.PadRight(32, '0') : strKey.Substring(0, 32);
                Byte[] toEncryptArray = Encoding.UTF8.GetBytes(strPlaintext);
                RijndaelManaged rijndaelManaged = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(strKey),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform pCryptoTransform = rijndaelManaged.CreateEncryptor();
                Byte[] resultArray = pCryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="strCiphertext">AES密文</param>
        /// <param name="strKey">秘钥</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string strCiphertext, string strKey)
        {
            try
            {
                if (string.IsNullOrEmpty(strCiphertext))
                {
                    return string.Empty;
                }
                strKey = strKey.Length < 32 ? strKey.PadRight(32, '0') : strKey.Substring(0, 32);
                Byte[] toEncryptArray = Convert.FromBase64String(strCiphertext);
                RijndaelManaged rijndaelManaged = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(strKey),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform pCryptoTransform = rijndaelManaged.CreateDecryptor();
                Byte[] resultArray = pCryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 文件AES加密
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="strSaveFilePath">加密文件目录</param>
        /// <param name="strKey">秘钥</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FileAESEncrypt(string strFilePath, string strSaveFilePath, string strKey)
        {
            try
            {
                //设置Aes秘钥和格式
                strKey = strKey.Length < 32 ? strKey.PadRight(32, '0') : strKey.Substring(0, 32);
                RijndaelManaged rijndaelManaged = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(strKey),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                //读取文本加密数据
                FileStream fileStream = File.OpenRead(strFilePath);
                byte[] byteFileStream = new byte[fileStream.Length];
                fileStream.Read(byteFileStream, 0, (int)fileStream.Length);
                fileStream.Close();
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
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
        /// 文件AES解密
        /// </summary>
        /// <param name="strFilePath">被加密的文件路径</param>
        /// <param name="strSaveFilePath">解密文件目录</param>
        /// <param name="strKey">秘钥</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FileAESDecrypt(string strFilePath, string strSaveFilePath, string strKey)
        {
            try
            {
                strKey = strKey.Length < 32 ? strKey.PadRight(32, '0') : strKey.Substring(0, 32);
                RijndaelManaged rijndaelManaged = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(strKey),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                FileStream fileStream = File.OpenRead(strFilePath);
                byte[] byteFileStream = new byte[fileStream.Length];
                fileStream.Read(byteFileStream, 0, (int)fileStream.Length);
                fileStream.Close();
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
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
