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
    /// ZIP压缩文件帮助类
    /// 创建日期:2017年5月25日
    /// </summary>
    public class ZIPHelper
    {
        /// <summary>
        /// 压缩ZIP文件
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件保存位置</param>
        /// <param name="listFolderOrFilePath">需要压缩的文件夹或文件</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CompressionZip(string strZipPath, List<string> listFolderOrFilePath)
        {
            try
            {
                ZipOutputStream ComStream = new ZipOutputStream(File.Create(strZipPath));
                //压缩等级(0-9)
                ComStream.SetLevel(9);
                foreach (string strFolderOrFilePath in listFolderOrFilePath)
                {
                    if (Directory.Exists(strFolderOrFilePath))
                    {
                        //如果路径是文件目录
                        CompressionZipDirectory(strFolderOrFilePath, ComStream, strFolderOrFilePath);
                    }
                    else if (File.Exists(strFolderOrFilePath))
                    {
                        //如果路径是文件路径
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
        /// 压缩ZIP文件(加密)
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件保存位置</param>
        /// <param name="listFolderOrFilePath">需要压缩的文件夹或文件</param>
        /// <param name="strPassword">压缩文件密码</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CompressionZip(string strZipPath, List<string> listFolderOrFilePath, string strPassword)
        {
            try
            {
                ZipOutputStream ComStream = new ZipOutputStream(File.Create(strZipPath));
                //压缩等级(0-9)
                ComStream.SetLevel(9);
                //压缩文件加密
                if (!string.IsNullOrEmpty(strPassword))
                {
                    ComStream.Password = strPassword;
                }
                foreach (string strFolderOrFilePath in listFolderOrFilePath)
                {
                    if (Directory.Exists(strFolderOrFilePath))
                    {
                        //如果路径是文件目录
                        CompressionZipDirectory(strFolderOrFilePath, ComStream, strFolderOrFilePath);
                    }
                    else if (File.Exists(strFolderOrFilePath))
                    {
                        //如果路径是文件路径
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
        /// 压缩ZIP文件夹
        /// </summary>
        /// <param name="strRootPath">根目录路径</param>
        /// <param name="ComStream">ZIP文件输出流</param>
        /// <param name="strSubPath">子目录路径</param>
        private static void CompressionZipDirectory(string strRootPath, ZipOutputStream ComStream, string strSubPath)
        {
            try
            {
                foreach (FileSystemInfo item in new DirectoryInfo(strSubPath).GetFileSystemInfos())
                {
                    if (Directory.Exists(item.FullName))
                    {
                        //如果路径是文件目录
                        CompressionZipDirectory(strRootPath, ComStream, item.FullName);
                    }
                    else if (File.Exists(item.FullName))
                    {
                        //如果路径是文件路径
                        DirectoryInfo dirInfo = new DirectoryInfo(strRootPath);
                        string strFullName = new FileInfo(item.FullName).FullName;
                        string strRelativePath = dirInfo.Name + strFullName.Substring(dirInfo.FullName.Length, strFullName.Length - dirInfo.FullName.Length);
                        FileStream fileStream = File.OpenRead(strFullName);
                        byte[] btsLength = new byte[fileStream.Length];
                        fileStream.Read(btsLength, 0, btsLength.Length);
                        ZipEntry zipEntry = new ZipEntry(strRelativePath);
                        ComStream.PutNextEntry(zipEntry);
                        ComStream.Write(btsLength, 0, btsLength.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
        }

        /// <summary>
        /// 解压缩ZIP文件
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件保存位置</param>
        /// <param name="strDeCompressionPath">需要解压到的指定位置</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DeCompressionZip(string strZipPath, string strDeCompressionPath)
        {
            try
            {
                if (string.IsNullOrEmpty(strZipPath) || !File.Exists(strZipPath))
                {
                    return false;
                }
                ZipInputStream inputStream = new ZipInputStream(File.OpenRead(strZipPath));
                ZipEntry zipEntry = null;
                while ((zipEntry = inputStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(zipEntry.Name))
                    {
                        string strFileName = Path.Combine(strDeCompressionPath, zipEntry.Name);
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

        /// <summary>
        /// 解压缩ZIP文件(加密)
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件保存位置</param>
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
                //压缩文件解密
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
