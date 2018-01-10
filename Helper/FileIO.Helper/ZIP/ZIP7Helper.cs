using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileIO.Helper.TXT;
using SevenZip;

namespace FileIO.Helper.ZIP
{
    /// <summary>
    /// 7ZIP压缩文件帮助类(不支持分卷压缩)
    /// 创建日期:2018年1月8日
    /// </summary>
    public class ZIP7Helper
    {
        /// <summary>
        /// 获得当前系统X86架构7ZIP类库路径
        /// </summary>
        public static string strX86_DllPath
        {
            get
            {
                return string.Format(@"{0}\x86\7z.dll", System.Environment.CurrentDirectory);
            }
        }

        /// <summary>
        /// 获得当前系统X64架构7ZIP类库路径
        /// </summary>
        public static string strX64_DllPath
        {
            get
            {
                return string.Format(@"{0}\x64\7z.dll", System.Environment.CurrentDirectory);
            }
        }

        /// <summary>
        /// 动态链接7ZIP类库
        /// </summary>
        private static void SetLibraryPath7z()
        {
            //动态链接7ZIP类库
            if (IntPtr.Size == 8)
            {
                SevenZipExtractor.SetLibraryPath(strX64_DllPath);
            }
            else
            {
                SevenZipExtractor.SetLibraryPath(strX86_DllPath);
            }
        }

        /// <summary>
        /// 压缩7-ZIP文件
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件保存位置</param>
        /// <param name="fileFullNames">需要压缩的文件</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool Compression7Zip(string strZipPath, params string[] fileFullNames)
        {
            try
            {
                //动态链接7ZIP类库
                SetLibraryPath7z();
                //默认格式为(*.7z)
                strZipPath = Path.ChangeExtension(strZipPath, "7z");
                //压缩7-ZIP文件
                SevenZipCompressor sevenZipCompressor = new SevenZipCompressor();
                //压缩等级(默认正常值)
                sevenZipCompressor.CompressionLevel = CompressionLevel.Normal;
                //压缩格式(默认7z压缩)
                sevenZipCompressor.ArchiveFormat = OutArchiveFormat.SevenZip;
                //是否保持目录结构(默认为true)
                sevenZipCompressor.DirectoryStructure = true;
                //是否包含空目录(默认true)  
                sevenZipCompressor.IncludeEmptyDirectories = true;
                //压缩目录时是否使用顶层目录(默认false)  
                sevenZipCompressor.PreserveDirectoryRoot = false;
                //加密7z头(默认false)  
                sevenZipCompressor.EncryptHeaders = false;
                //文件加密算法
                sevenZipCompressor.ZipEncryptionMethod = ZipEncryptionMethod.ZipCrypto;
                //尽快压缩(不会触发*Started事件,仅触发*Finished事件)  
                sevenZipCompressor.FastCompression = false;
                //压缩文件
                sevenZipCompressor.CompressFiles(strZipPath, fileFullNames);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 压缩7-ZIP文件(加密)
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件保存位置</param>
        /// <param name="strPassword">压缩文件密码</param>
        /// <param name="fileFullNames">需要压缩的文件</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool Compression7Zip(string strZipPath, string strPassword, params string[] fileFullNames)
        {
            try
            {
                //动态链接7ZIP类库
                SetLibraryPath7z();
                //默认格式为(*.7z)
                strZipPath = Path.ChangeExtension(strZipPath, "7z");
                //压缩7-ZIP文件
                SevenZipCompressor sevenZipCompressor = new SevenZipCompressor();
                //压缩等级(默认正常值)
                sevenZipCompressor.CompressionLevel = CompressionLevel.Normal;
                //压缩格式(默认7z压缩)
                sevenZipCompressor.ArchiveFormat = OutArchiveFormat.SevenZip;
                //是否保持目录结构(默认为true)
                sevenZipCompressor.DirectoryStructure = true;
                //是否包含空目录(默认true)  
                sevenZipCompressor.IncludeEmptyDirectories = true;
                //压缩目录时是否使用顶层目录(默认false)  
                sevenZipCompressor.PreserveDirectoryRoot = false;
                //加密7z头(默认false)  
                sevenZipCompressor.EncryptHeaders = true;
                //文件加密算法
                sevenZipCompressor.ZipEncryptionMethod = ZipEncryptionMethod.ZipCrypto;
                //尽快压缩(不会触发*Started事件,仅触发*Finished事件)  
                sevenZipCompressor.FastCompression = false;
                //压缩文件
                sevenZipCompressor.CompressFilesEncrypted(strZipPath, strPassword, fileFullNames);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 压缩7-ZIP文件夹
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件夹</param>
        /// <param name="strDirectory">需要压缩的文件夹</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool Compression7ZipDirectory(string strZipPath, string strDirectory)
        {
            try
            {
                //动态链接7ZIP类库
                SetLibraryPath7z();
                //默认格式为(*.7z)
                strZipPath = Path.ChangeExtension(strZipPath, "7z");
                //压缩7-ZIP文件
                SevenZipCompressor sevenZipCompressor = new SevenZipCompressor();
                //压缩等级(默认正常值)
                sevenZipCompressor.CompressionLevel = CompressionLevel.Normal;
                //压缩格式(默认7z压缩)
                sevenZipCompressor.ArchiveFormat = OutArchiveFormat.SevenZip;
                //是否保持目录结构(默认为true)
                sevenZipCompressor.DirectoryStructure = true;
                //是否包含空目录(默认true)  
                sevenZipCompressor.IncludeEmptyDirectories = true;
                //压缩目录时是否使用顶层目录(默认false)  
                sevenZipCompressor.PreserveDirectoryRoot = false;
                //加密7z头(默认false)  
                sevenZipCompressor.EncryptHeaders = false;
                //文件加密算法
                sevenZipCompressor.ZipEncryptionMethod = ZipEncryptionMethod.ZipCrypto;
                //尽快压缩(不会触发*Started事件,仅触发*Finished事件)  
                sevenZipCompressor.FastCompression = false;
                //压缩文件
                sevenZipCompressor.CompressDirectory(strDirectory, strZipPath);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 压缩7-ZIP文件夹(加密)
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件夹</param>
        /// <param name="strPassword">压缩文件密码</param>
        /// <param name="strDirectory">需要压缩的文件夹</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool Compression7ZipDirectory(string strZipPath, string strPassword, string strDirectory)
        {
            try
            {
                //动态链接7ZIP类库
                SetLibraryPath7z();
                //默认格式为(*.7z)
                strZipPath = Path.ChangeExtension(strZipPath, "7z");
                //压缩7-ZIP文件
                SevenZipCompressor sevenZipCompressor = new SevenZipCompressor();
                //压缩等级(默认正常值)
                sevenZipCompressor.CompressionLevel = CompressionLevel.Normal;
                //压缩格式(默认7z压缩)
                sevenZipCompressor.ArchiveFormat = OutArchiveFormat.SevenZip;
                //是否保持目录结构(默认为true)
                sevenZipCompressor.DirectoryStructure = true;
                //是否包含空目录(默认true)  
                sevenZipCompressor.IncludeEmptyDirectories = true;
                //压缩目录时是否使用顶层目录(默认false)  
                sevenZipCompressor.PreserveDirectoryRoot = false;
                //加密7z头(默认false)  
                sevenZipCompressor.EncryptHeaders = true;
                //文件加密算法
                sevenZipCompressor.ZipEncryptionMethod = ZipEncryptionMethod.ZipCrypto;
                //尽快压缩(不会触发*Started事件,仅触发*Finished事件)  
                sevenZipCompressor.FastCompression = false;
                //压缩文件
                sevenZipCompressor.CompressDirectory(strDirectory, strZipPath, strPassword);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 解压缩7-ZIP文件
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件保存位置</param>
        /// <param name="strDeCompressionPath">需要解压到的指定位置</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DeCompression7Zip(string strZipPath, string strDeCompressionPath)
        {
            try
            {
                if (string.IsNullOrEmpty(strZipPath) || !File.Exists(strZipPath))
                {
                    return false;
                }
                //动态链接7ZIP类库
                SetLibraryPath7z();
                //创建目录
                if (!Directory.Exists(strDeCompressionPath))
                {
                    Directory.CreateDirectory(strDeCompressionPath);
                }
                //解压数据
                SevenZipExtractor sevenZipExtractor = new SevenZipExtractor(strZipPath);
                foreach (ArchiveFileInfo itemArchiveFileInfo in sevenZipExtractor.ArchiveFileData)
                {
                    sevenZipExtractor.ExtractFiles(strDeCompressionPath, itemArchiveFileInfo.Index);
                }
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 解压缩7-ZIP文件(加密)
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件保存位置</param>
        /// <param name="strDeCompressionPath">需要解压到的指定位置</param>
        /// <param name="strPassword">压缩文件密码</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DeCompression7Zip(string strZipPath, string strDeCompressionPath, string strPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(strZipPath) || !File.Exists(strZipPath))
                {
                    return false;
                }
                //动态链接7ZIP类库
                SetLibraryPath7z();
                //创建目录
                if (!Directory.Exists(strDeCompressionPath))
                {
                    Directory.CreateDirectory(strDeCompressionPath);
                }
                //解压数据
                SevenZipExtractor sevenZipExtractor = new SevenZipExtractor(strZipPath, strPassword);
                foreach (ArchiveFileInfo itemArchiveFileInfo in sevenZipExtractor.ArchiveFileData)
                {
                    sevenZipExtractor.ExtractFiles(strDeCompressionPath, itemArchiveFileInfo.Index);
                }
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
