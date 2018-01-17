using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileIO.Helper.TXT;

namespace FileIO.Helper.BinaryFile
{
    /// <summary>
    /// 二进制文件帮助类
    /// 创建日期:2017年6月2日
    /// </summary>
    public class BinaryFileHelper
    {
        /// <summary>
        /// 通过文件路径获得二进制文件
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>二进制文件</returns>
        public static Byte[] GetBinaryDataFromFilePath(string strFilePath)
        {
            try
            {
                Byte[] BinaryData = null;
                if (!string.IsNullOrEmpty(strFilePath) && File.Exists(strFilePath))
                {
                    System.IO.FileStream fileStream = new System.IO.FileStream(strFilePath, FileMode.Open, FileAccess.Read);
                    BinaryData = new Byte[fileStream.Length];
                    fileStream.Read(BinaryData, 0, BinaryData.Length);
                    fileStream.Close();
                }
                return BinaryData;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 通过二进制文件保存文件到指定路径
        /// </summary>
        /// <param name="BinaryData">二进制文件</param>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool GetFilePathFromBinaryData(Byte[] BinaryData, string strFilePath)
        {
            try
            {
                if (BinaryData.Length < 1 || string.IsNullOrEmpty(strFilePath))
                {
                    return false;
                }
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(strFilePath)))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(strFilePath));
                }
                FileStream fileStream = new FileStream(strFilePath, FileMode.Create);
                BinaryWriter pBinaryWriter = new BinaryWriter(fileStream);
                pBinaryWriter.Write(BinaryData, 0, BinaryData.Length);
                pBinaryWriter.Close();
                fileStream.Close();
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
