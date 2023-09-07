using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Utils.Helper.TXT;

namespace Utils.Helper.Encryption
{
    /// <summary>
    /// 文件夹加密解密帮助类(可破解)
    /// 创建日期:2017年12月04日
    /// </summary>
    public class FolderHelper
    {
        /// <summary>
        /// 加密文件
        /// </summary>
        public static string Lock = ".{2559a1f2-21d7-11d4-bdaf-00c04f60b9f0}";
        /// <summary>
        /// 控制面板
        /// </summary>
        public static string Control = ".{21EC2020-3AEA-1069-A2DD-08002B30309D}";
        /// <summary>
        /// RunIE
        /// </summary>
        public static string RunIE = ".{2559a1f4-21d7-11d4-bdaf-00c04f60b9f0}";
        /// <summary>
        /// 回收站
        /// </summary>
        public static string Recycle = ".{645FF040-5081-101B-9F08-00AA002F954E}";
        /// <summary>
        /// Help
        /// </summary>
        public static string Help = ".{2559a1f1-21d7-11d4-bdaf-00c04f60b9f0}";
        /// <summary>
        /// NetWork
        /// </summary>
        public static string NetWork = ".{7007ACC7-3202-11D1-AAD2-00805FC1270E}";

        /// <summary>
        /// 文件夹加密(可破解)
        /// </summary>
        /// <param name="strFolderPath">文件夹路径</param>
        /// <param name="strClsid">Clsid类型</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FolderEncrypt(string strFolderPath, string strClsid)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(strFolderPath);
                directoryInfo.MoveTo(directoryInfo.Parent.FullName + "\\" + directoryInfo.Name + strClsid);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 文件夹解密
        /// 理论上可以解密所有该方法加密的文件夹
        /// </summary>
        /// <param name="strFolderPath">文件夹路径</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FolderDecrypt(string strFolderPath)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(strFolderPath);
                directoryInfo.MoveTo(strFolderPath.Substring(0, strFolderPath.LastIndexOf(".")));
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 文件夹加密(带密码)(可破解)
        /// </summary>
        /// <param name="strFolderPath">文件夹路径</param>
        /// <param name="strClsid">Clsid类型</param>
        /// <param name="strPassword">加密密码</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FolderEncrypt(string strFolderPath, string strClsid, string strPassword)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(strFolderPath);
                XmlDocument xmlDocument = new XmlDocument();
                XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xmlDocument.AppendChild(xmlNode);
                XmlElement xmlElement = xmlDocument.CreateElement("", "ROOT", "");
                XmlText xmlText = xmlDocument.CreateTextNode(strPassword);
                xmlElement.AppendChild(xmlText);
                xmlDocument.AppendChild(xmlElement);
                xmlDocument.Save(strFolderPath + "\\Lock.xml");
                directoryInfo.MoveTo(directoryInfo.Parent.FullName + "\\" + directoryInfo.Name + strClsid);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 文件夹解密(带密码)
        /// </summary>
        /// <param name="strFolderPath">文件夹路径</param>
        /// <param name="strPassword">加密密码</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FolderDecrypt(string strFolderPath, string strPassword)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(strFolderPath);
                bool bIsPassword = false;
                XmlTextReader xmlTextReader = new XmlTextReader(strFolderPath + "\\Lock.xml");
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.NodeType == XmlNodeType.Text)
                    {
                        if (xmlTextReader.Value == strPassword)
                        {
                            bIsPassword = true;
                            break;
                        }
                    }
                }
                xmlTextReader.Close();
                if (bIsPassword)
                {
                    File.Delete(strFolderPath + "\\Lock.xml");
                    directoryInfo.MoveTo(strFolderPath.Substring(0, strFolderPath.LastIndexOf(".")));
                    return true;
                }
                else
                {
                    return false;
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
