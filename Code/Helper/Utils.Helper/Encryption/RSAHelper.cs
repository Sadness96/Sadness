using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Utils.Helper.TXT;

namespace Utils.Helper.Encryption
{
    /// <summary>
    /// RSA加密解密帮助类
    /// 创建日期:2017年6月16日
    /// </summary>
    public class RSAHelper
    {
        /// <summary>
        /// RSA产生秘钥
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="xmlPrivateKey">私钥</param>
        public static void RSAKey(out string xmlPublicKey, out string xmlPrivateKey)
        {
            try
            {
                RSACryptoServiceProvider rsaCrypto = new RSACryptoServiceProvider();
                xmlPublicKey = rsaCrypto.ToXmlString(false);
                xmlPrivateKey = rsaCrypto.ToXmlString(true);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                xmlPublicKey = string.Empty;
                xmlPrivateKey = string.Empty;
            }
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="strPlaintext">明文</param>
        /// <param name="xmlPublicKey">公钥</param>
        /// <returns>RSA密文</returns>
        public static string RSAEncrypt(string strPlaintext, string xmlPublicKey)
        {
            try
            {
                RSACryptoServiceProvider rsaCrypto = new RSACryptoServiceProvider();
                rsaCrypto.FromXmlString(xmlPublicKey);
                UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
                byte[] byteBaseUnicode = unicodeEncoding.GetBytes(strPlaintext);
                byte[] byteBaseEncrypt = rsaCrypto.Encrypt(byteBaseUnicode, false);
                string strRSAEncrypt = Convert.ToBase64String(byteBaseEncrypt);
                return strRSAEncrypt;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="strCiphertext">RSA密文</param>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <returns>明文</returns>
        public static string RSADecrypt(string strCiphertext, string xmlPrivateKey)
        {
            try
            {
                RSACryptoServiceProvider rsaCrypto = new RSACryptoServiceProvider();
                rsaCrypto.FromXmlString(xmlPrivateKey);
                byte[] byteBase64 = Convert.FromBase64String(strCiphertext);
                byte[] byteBaseDecrypt = rsaCrypto.Decrypt(byteBase64, false);
                string strRSADecrypt = (new UnicodeEncoding()).GetString(byteBaseDecrypt);
                return strRSADecrypt;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }
    }
}
