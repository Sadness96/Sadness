using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Utils.Helper.TXT;

namespace Utils.Helper.PCInformation
{
    /// <summary>
    /// 加密注册码帮助类
    /// 创建日期:2017年6月6日
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        /// 注册请求码
        /// </summary>
        /// <returns>请求码</returns>
        public static string RequestCode()
        {
            try
            {
                // 读取电脑信息
                List<string> listOriginal = new List<string>();
                List<string> listEncryption = new List<string>();
                listOriginal.Add(PCInformationHelper.MAC()[0]);
                listOriginal.Add(PCInformationHelper.CPU()[0]);
                listOriginal.Add(PCInformationHelper.DESK()[0]);
                listOriginal.Add(PCInformationHelper.Memory()[0]);
                listOriginal.Add(PCInformationHelper.Motherboard()[0]);
                // 对信息进行处理
                foreach (string Encryption in listOriginal)
                {
                    string strSecretKey = "";
                    if (Encryption != "")
                    {
                        MD5 md5 = MD5.Create();
                        byte[] bitUtf8 = md5.ComputeHash(Encoding.UTF8.GetBytes("Sadness:" + Encryption));
                        string md5_32 = "";
                        for (int i = 0; i < bitUtf8.Length; i++)
                        {
                            md5_32 = md5_32 + bitUtf8[i].ToString("X");
                        }
                        strSecretKey = md5_32.Substring(6, 1);
                        strSecretKey += md5_32.Substring(2, 1);
                        strSecretKey += md5_32.Substring(3, 1);
                        strSecretKey += md5_32.Substring(1, 1);
                        strSecretKey += md5_32.Substring(5, 1);
                    }
                    else
                    {
                        strSecretKey = "XXXXX";
                    }
                    listEncryption.Add(strSecretKey);
                }
                // 拼接申请码
                string strRequestCode = "";
                foreach (string key in listEncryption)
                {
                    strRequestCode += string.Format("{0}{1}", key, '-');
                }
                return strRequestCode.Substring(0, strRequestCode.Length - 1);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 注册激活码
        /// </summary>
        /// <returns>激活码</returns>
        public static string ActivationCode(string RequestCode)
        {
            try
            {
                // 拆分激活码
                string[] strRequestCode = RequestCode.Split('-');
                // 对信息进行处理
                List<string> listEncryption = new List<string>();
                foreach (string Encryption in strRequestCode)
                {
                    string strSecretKey = "";
                    if (Encryption != "" && Encryption != "XXXXX")
                    {
                        MD5 md5 = MD5.Create();
                        byte[] bitUtf8 = md5.ComputeHash(Encoding.UTF8.GetBytes("Sadness:" + Encryption));
                        string md5_32 = "";
                        for (int i = 0; i < bitUtf8.Length; i++)
                        {
                            md5_32 = md5_32 + bitUtf8[i].ToString("X");
                        }
                        strSecretKey = md5_32.Substring(6, 1);
                        strSecretKey += md5_32.Substring(2, 1);
                        strSecretKey += md5_32.Substring(3, 1);
                        strSecretKey += md5_32.Substring(1, 1);
                        strSecretKey += md5_32.Substring(5, 1);
                    }
                    else
                    {
                        strSecretKey = "XXXXX";
                    }
                    listEncryption.Add(strSecretKey);
                }
                // 拼接激活码
                string strActivationCode = "";
                foreach (string key in listEncryption)
                {
                    strActivationCode += string.Format("{0}{1}", key, '-');
                }
                strActivationCode = strActivationCode.Substring(0, strActivationCode.Length - 1);
                return strActivationCode;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }
    }
}
