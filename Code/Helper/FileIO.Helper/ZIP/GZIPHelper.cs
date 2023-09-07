using FileIO.Helper.TXT;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO.Helper.ZIP
{
    /// <summary>
    /// GZIP 压缩帮助类
    /// 创建日期:2018年09月26日
    /// </summary>
    public class GZIPHelper
    {
        /// <summary>
        /// 压缩 GZIP 数据
        /// </summary>
        /// <param name="bytesSourceData">源数据</param>
        /// <returns>压缩数据</returns>
        public static byte[] CompressionGZIP(byte[] bytesSourceData)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                GZipStream compressedzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true);
                compressedzipStream.Write(bytesSourceData, 0, bytesSourceData.Length);
                compressedzipStream.Close();
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 解压缩 GZIP 数据
        /// </summary>
        /// <param name="bytesSourceData">源数据</param>
        /// <returns>解压缩数据</returns>
        public static byte[] DeCompressionGZIP(byte[] bytesSourceData)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(bytesSourceData);
                GZipStream compressedzipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
                MemoryStream outBuffer = new MemoryStream();
                byte[] block = new byte[1024];
                while (true)
                {
                    int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                    if (bytesRead <= 0)
                        break;
                    else
                        outBuffer.Write(block, 0, bytesRead);
                }
                compressedzipStream.Close();
                return outBuffer.ToArray();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 压缩 String 类型 GZIP 数据
        /// </summary>
        /// <param name="strSourceData">源数据</param>
        /// <returns>压缩数据(Base64)</returns>
        public static string CompressionStringGZIP(string strSourceData)
        {
            try
            {
                if (!string.IsNullOrEmpty(strSourceData))
                {
                    byte[] rawData = Encoding.UTF8.GetBytes(strSourceData);
                    byte[] zippedData = CompressionGZIP(rawData);
                    return Convert.ToBase64String(zippedData);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 解压缩 String 类型 GZIP 数据
        /// </summary>
        /// <param name="strSourceData">源数据(Base64)</param>
        /// <returns>解压缩数据</returns>
        public static string DeCompressionStringGZIP(string strSourceData)
        {
            try
            {
                if (!string.IsNullOrEmpty(strSourceData))
                {
                    byte[] zippedData = Convert.FromBase64String(strSourceData.ToString());
                    return Encoding.UTF8.GetString(DeCompressionGZIP(zippedData));
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }
    }
}
