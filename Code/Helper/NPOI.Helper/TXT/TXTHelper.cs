using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPOI.Helper.TXT
{
    /// <summary>
    /// TXT 文本帮助类
    /// 创建日期:2016年12月8日
    /// </summary>
    public class TXTHelper
    {
        /// <summary>
        /// 写入文本到TXT(如果有文件不进行操作)
        /// </summary>
        /// <param name="strPath">TXT储存路径</param>
        /// <param name="strTXT">文本内容</param>
        /// <returns>成功返回0,失败返回-1</returns>
        public static int WriteFile(string strPath, string strTXT)
        {
            try
            {
                //如果路径下的文件不存在,自动创建
                string strFolderPath = Path.GetDirectoryName(strPath);
                if (!Directory.Exists(strFolderPath))
                {
                    Directory.CreateDirectory(strFolderPath);
                }
                //写入文本到TXT(如果文件存在异常处理)
                FileStream filestream = new FileStream(strPath, FileMode.CreateNew, FileAccess.Write);
                StreamWriter streamwriter = new StreamWriter(filestream);
                streamwriter.Write(strTXT);
                streamwriter.Close();
                filestream.Close();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 写入文本到TXT(选择是否覆盖)
        /// </summary>
        /// <param name="strPath">TXT储存路径</param>
        /// <param name="strTXT">文本内容</param>
        /// <param name="boolCover">(true)如果冲突覆盖文件,(false)如果冲突不进行操作</param>
        /// <returns>成功返回0,失败返回-1</returns>
        public static int WriteFile(string strPath, string strTXT, bool boolCover)
        {
            try
            {
                //如果路径下的文件不存在,自动创建
                string strFolderPath = Path.GetDirectoryName(strPath);
                if (!Directory.Exists(strFolderPath))
                {
                    Directory.CreateDirectory(strFolderPath);
                }
                //写入文本到TXT(选择是否覆盖)
                if (boolCover == true)
                {
                    FileStream filestream = new FileStream(strPath, FileMode.Create, FileAccess.Write);
                    StreamWriter streamwriter = new StreamWriter(filestream);
                    streamwriter.Write(strTXT);
                    streamwriter.Close();
                    filestream.Close();
                }
                else
                {
                    FileStream filestream = new FileStream(strPath, FileMode.CreateNew, FileAccess.Write);
                    StreamWriter streamwriter = new StreamWriter(filestream);
                    streamwriter.Write(strTXT);
                    streamwriter.Close();
                    filestream.Close();
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 写入文本到TXT,追加写入文件(如果不存在自动创建)
        /// </summary>
        /// <param name="strPath">TXT储存路径</param>
        /// <param name="strTXT">文本内容</param>
        /// <returns>成功返回0,失败返回-1</returns>
        public static int AppendFile(string strPath, string strTXT)
        {
            try
            {
                //如果路径下的文件不存在,自动创建
                string strFolderPath = Path.GetDirectoryName(strPath);
                if (!Directory.Exists(strFolderPath))
                {
                    Directory.CreateDirectory(strFolderPath);
                }
                //写入文本到TXT,追加写入文件(如果不存在自动创建)
                FileStream filestream = new FileStream(strPath, FileMode.Append, FileAccess.Write);
                StreamWriter streamwriter = new StreamWriter(filestream);
                streamwriter.Write(strTXT);
                streamwriter.Close();
                filestream.Close();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 写入文本到TXT,追加写入文件(如果不存在自动创建)(是否换行)
        /// </summary>
        /// <param name="strPath">TXT储存路径</param>
        /// <param name="strTXT">文本内容</param>
        /// <param name="boolWrap">(true)换行,(false)不换行</param>
        /// <returns>成功返回0,失败返回-1</returns>
        public static int AppendFile(string strPath, string strTXT, bool boolWrap)
        {
            try
            {
                //如果路径下的文件不存在,自动创建
                string strFolderPath = Path.GetDirectoryName(strPath);
                if (!Directory.Exists(strFolderPath))
                {
                    Directory.CreateDirectory(strFolderPath);
                }
                //写入文本到TXT,追加写入文件(如果不存在自动创建)(是否换行)
                FileStream filestream = new FileStream(strPath, FileMode.Append, FileAccess.Write);
                StreamWriter streamwriter = new StreamWriter(filestream);
                if (boolWrap == true)
                    streamwriter.Write("\r\n");
                streamwriter.Write(strTXT);
                streamwriter.Close();
                filestream.Close();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 帮助类日志文件
        /// </summary>
        /// <param name="strLogs">异常信息</param>
        /// <returns>成功返回0,失败返回-1</returns>
        public static void Logs(string strLogs)
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string strDateTime = DateTime.Now.ToString();
            string strFolderPath = string.Format("{0}\\Logs", System.Environment.CurrentDirectory);
            string strFilePath = string.Format("{0}\\Logs\\{1}.txt", System.Environment.CurrentDirectory, strDate);
            if (!File.Exists(strFilePath))
            {
                AppendFile(strFilePath, string.Format("{0}:", strDateTime), false);
                AppendFile(strFilePath, strLogs, true);
            }
            else
            {
                //错误日志不重复,已有的错误不在显示
                if (GetFileString(strFilePath).IndexOf(strLogs) > -1)
                {
                    return;
                }
                AppendFile(strFilePath, "", true);
                AppendFile(strFilePath, string.Format("{0}:", strDateTime), true);
                AppendFile(strFilePath, strLogs, true);
            }
        }

        /// <summary>
        /// 读取TXT文件中的文本
        /// </summary>
        /// <param name="strPath">TXT文件路径</param>
        /// <returns>TXT文件中的文本</returns>
        public static string GetFileString(string strPath)
        {
            string strText = "";
            try
            {
                strText = File.ReadAllText(strPath);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return strText;
        }

        /// <summary>
        /// 读取TXT文件中的文本(按照每行存到string[]中)
        /// </summary>
        /// <param name="strPath">TXT文件路径</param>
        /// <returns>TXT文件中的文本(string[])</returns>
        public static string[] GetFileArray(string strPath)
        {
            string[] strText = null;
            try
            {
                strText = File.ReadAllLines(strPath);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return strText;
        }

        /// <summary>
        /// 读取TXT文件中的文本(按照每行存到listString中)
        /// </summary>
        /// <param name="strPath">TXT文件路径</param>
        /// <returns>TXT文件中的文本(listString)</returns>
        public static List<string> GetFileList(string strPath)
        {
            string[] strText = null;
            List<string> listText = new List<string>();
            try
            {
                strText = File.ReadAllLines(strPath);
                foreach (string strLine in strText)
                {
                    listText.Add(strLine);
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return listText;
        }
    }
}
