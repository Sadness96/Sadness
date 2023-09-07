using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileIO.Helper.TXT;
using ICSharpCode.SharpZipLib.Zip;

namespace FileIO.Helper.ZIP
{
    /// <summary>
    /// ZIP 压缩文件帮助类
    /// 创建日期:2017年05月25日
    /// </summary>
    public class ZIPHelper
    {
        /// <summary>
        /// 压缩 ZIP 文件
        /// </summary>
        /// <param name="strZipPath">ZIP 压缩文件保存位置</param>
        /// <param name="listFolderOrFilePath">需要压缩的文件夹或文件</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CompressionZip(string strZipPath, List<string> listFolderOrFilePath)
        {
            return CompressionZip(strZipPath, listFolderOrFilePath, string.Empty);
        }

        /// <summary>
        /// 压缩 ZIP 文件(加密)
        /// </summary>
        /// <param name="strZipPath">ZIP 压缩文件保存位置</param>
        /// <param name="listFolderOrFilePath">需要压缩的文件夹或文件</param>
        /// <param name="strPassword">压缩文件密码</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CompressionZip(string strZipPath, List<string> listFolderOrFilePath, string strPassword)
        {
            try
            {
                ZipOutputStream ComStream = new ZipOutputStream(File.Create(strZipPath));
                // 压缩等级(0-9)
                ComStream.SetLevel(9);
                // 压缩文件加密
                if (!string.IsNullOrEmpty(strPassword))
                {
                    ComStream.Password = strPassword;
                }
                foreach (string strFolderOrFilePath in listFolderOrFilePath)
                {
                    if (Directory.Exists(strFolderOrFilePath))
                    {
                        // 如果路径是文件目录
                        CompressionZipDirectory(strFolderOrFilePath, ComStream, string.Empty);
                    }
                    else if (File.Exists(strFolderOrFilePath))
                    {
                        // 如果路径是文件路径
                        FileStream fileStream = File.OpenRead(strFolderOrFilePath);
                        byte[] btsLength = new byte[fileStream.Length];
                        fileStream.Read(btsLength, 0, btsLength.Length);
                        ZipEntry zipEntry = new ZipEntry(new FileInfo(strFolderOrFilePath).Name);
                        ComStream.PutNextEntry(zipEntry);
                        ComStream.Write(btsLength, 0, btsLength.Length);
                    }
                }
                ComStream.Finish();
                ComStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 压缩 ZIP 文件夹
        /// </summary>
        /// <param name="strRootPath">根目录路径</param>
        /// <param name="ComStream">ZIP 文件输出流</param>
        /// <param name="strSubPath">子目录路径</param>
        private static void CompressionZipDirectory(string strRootPath, ZipOutputStream ComStream, string strSubPath)
        {
            try
            {
                // 创建当前文件夹
                ZipEntry zipEntry = new ZipEntry(Path.Combine(strSubPath, Path.GetFileName(strRootPath) + "/"));
                ComStream.PutNextEntry(zipEntry);
                ComStream.Flush();
                // 遍历压缩目录
                foreach (string strFolder in Directory.GetDirectories(strRootPath))
                {
                    CompressionZipDirectory(strFolder, ComStream, Path.Combine(strSubPath, Path.GetFileName(strRootPath)));
                }
                // 遍历压缩文件
                foreach (string strFileName in Directory.GetFiles(strRootPath))
                {
                    FileStream fileStream = File.OpenRead(strFileName);
                    byte[] btsLength = new byte[fileStream.Length];
                    fileStream.Read(btsLength, 0, btsLength.Length);
                    zipEntry = new ZipEntry(Path.Combine(strSubPath, Path.GetFileName(strRootPath) + "/" + Path.GetFileName(strFileName)));
                    ComStream.PutNextEntry(zipEntry);
                    ComStream.Write(btsLength, 0, btsLength.Length);
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                    }
                }
                if (zipEntry != null)
                {
                    zipEntry = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
        }

        /// <summary>
        /// 解压缩 ZIP 文件
        /// </summary>
        /// <param name="strZipPath">ZIP 压缩文件保存位置</param>
        /// <param name="strDeCompressionPath">需要解压到的指定位置</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DeCompressionZip(string strZipPath, string strDeCompressionPath)
        {
            return DeCompressionZip(strZipPath, strDeCompressionPath, string.Empty);
        }

        /// <summary>
        /// 解压缩 ZIP 文件(加密)
        /// </summary>
        /// <param name="strZipPath">ZIP 压缩文件保存位置</param>
        /// <param name="strDeCompressionPath">需要解压到的指定位置</param>
        /// <param name="strPassword">压缩文件密码</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DeCompressionZip(string strZipPath, string strDeCompressionPath, string strPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(strZipPath) || !File.Exists(strZipPath))
                {
                    return false;
                }
                ZipInputStream inputStream = new ZipInputStream(File.OpenRead(strZipPath));
                // 压缩文件解密
                if (!string.IsNullOrEmpty(strPassword))
                {
                    inputStream.Password = strPassword;
                }
                ZipEntry zipEntry = null;
                while ((zipEntry = inputStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(zipEntry.Name))
                    {
                        string strFileName = Path.Combine(strDeCompressionPath, zipEntry.Name);
                        string strDirectory = Path.GetDirectoryName(strFileName);
                        if (!Directory.Exists(strDirectory))
                        {
                            Directory.CreateDirectory(strDirectory);
                        }
                        strFileName = strFileName.Replace('/', '\\');
                        if (strFileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(strFileName);
                        }
                        else
                        {
                            FileStream fileStream = null;
                            int intSize = 2048;
                            byte[] btsData = new byte[intSize];
                            while (true)
                            {
                                intSize = inputStream.Read(btsData, 0, btsData.Length);
                                if (fileStream == null)
                                {
                                    fileStream = File.Create(strFileName);
                                }
                                if (intSize > 0)
                                {
                                    fileStream.Write(btsData, 0, btsData.Length);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (fileStream != null)
                            {
                                fileStream.Close();
                                fileStream.Dispose();
                            }
                        }
                    }
                }
                if (zipEntry != null)
                {
                    zipEntry = null;
                }
                if (inputStream != null)
                {
                    inputStream.Close();
                    inputStream.Dispose();
                }
                GC.Collect();
                GC.Collect(1);
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
