using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileIO.Helper.TXT;

namespace FileIO.Helper.Folder
{
    /// <summary>
    /// Folder文件夹帮助类
    /// 创建日期:2017年5月8日
    /// </summary>
    public class FolderHelper
    {
        /// <summary>
        /// 获得指定路径下文件的路径和文件名
        /// </summary>
        /// <param name="strPath">文件夹路径</param>
        /// <returns>成功返回文件全路径或文件名,失败返回NULL</returns>
        public static List<string> GetSpecifiedDirectoryFiles(string strPath)
        {
            try
            {
                List<string> listAllFiles = new List<string>();
                DirectoryInfo TheFolder = new DirectoryInfo(strPath);
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    listAllFiles.Add(NextFile.FullName);
                }
                return listAllFiles;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得指定路径下所有文件的路径和文件名
        /// </summary>
        /// <param name="strPath">文件夹路径</param>
        /// <returns>成功返回文件全路径或文件名,失败返回NULL</returns>
        public static List<string> GetSpecifiedDirectoryAllFiles(string strPath)
        {
            try
            {
                List<string> listAllFiles = new List<string>();
                DirectoryInfo TheFolder = new DirectoryInfo(strPath);
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    listAllFiles.Add(NextFile.FullName);
                }
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    foreach (string strLayer in GetSpecifiedDirectoryAllFiles(NextFolder.FullName))
                    {
                        listAllFiles.Add(strLayer);
                    }
                }
                return listAllFiles;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得指定路径下文件的路径和文件名(限定后缀名)
        /// </summary>
        /// <param name="strPath">文件夹路径</param>
        /// <param name="strSuffixName">限定后缀名,如:"*.txt"、"*.xml"</param>
        /// <returns>成功返回文件全路径或文件名,失败返回NULL</returns>
        public static List<string> GetSpecifiedDirectoryFiles(string strPath, string strSuffixName)
        {
            try
            {
                List<string> listAllFiles = new List<string>();
                DirectoryInfo TheFolder = new DirectoryInfo(strPath);
                FileInfo[] TheFile;
                if (string.IsNullOrEmpty(strSuffixName))
                {
                    TheFile = TheFolder.GetFiles();
                }
                else
                {
                    TheFile = TheFolder.GetFiles(strSuffixName);
                }
                foreach (FileInfo NextFile in TheFile)
                {
                    listAllFiles.Add(NextFile.FullName);
                }
                return listAllFiles;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得指定路径下文件的路径和文件名(限定后缀名)
        /// </summary>
        /// <param name="strPath">文件夹路径</param>
        /// <param name="strSuffixName">限定后缀名,如:"*.txt"、"*.xml"</param>
        /// <returns>成功返回文件全路径或文件名,失败返回NULL</returns>
        public static List<string> GetSpecifiedDirectoryAllFiles(string strPath, string strSuffixName)
        {
            try
            {
                List<string> listAllFiles = new List<string>();
                DirectoryInfo TheFolder = new DirectoryInfo(strPath);
                FileInfo[] TheFile;
                if (string.IsNullOrEmpty(strSuffixName))
                {
                    TheFile = TheFolder.GetFiles();
                }
                else
                {
                    TheFile = TheFolder.GetFiles(strSuffixName);
                }
                foreach (FileInfo NextFile in TheFile)
                {
                    listAllFiles.Add(NextFile.FullName);
                }
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    foreach (string strLayer in GetSpecifiedDirectoryAllFiles(NextFolder.FullName, strSuffixName))
                    {
                        listAllFiles.Add(strLayer);
                    }
                }
                return listAllFiles;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得指定路径下文件夹的路径和文件夹名
        /// </summary>
        /// <param name="strPath">文件夹路径</param>
        /// <returns>成功返回文件全路径或文件名,失败返回NULL</returns>
        public static List<string> GetSpecifiedDirectoryFolders(string strPath)
        {
            try
            {
                List<string> listAllFolders = new List<string>();
                DirectoryInfo TheFolder = new DirectoryInfo(strPath);
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    listAllFolders.Add(NextFolder.FullName);
                }
                return listAllFolders;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得指定路径下所有文件夹的路径和文件夹名
        /// </summary>
        /// <param name="strPath">文件夹路径</param>
        /// <returns>成功返回文件全路径或文件名,失败返回NULL</returns>
        public static List<string> GetSpecifiedDirectoryAllFolders(string strPath)
        {
            try
            {
                List<string> listAllFolders = new List<string>();
                DirectoryInfo TheFolder = new DirectoryInfo(strPath);
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    listAllFolders.Add(NextFolder.FullName);
                    foreach (string strLayer in GetSpecifiedDirectoryAllFolders(NextFolder.FullName))
                    {
                        listAllFolders.Add(strLayer);
                    }
                }
                return listAllFolders;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }
    }
}
