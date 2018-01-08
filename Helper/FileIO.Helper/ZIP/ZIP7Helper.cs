using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileIO.Helper.TXT;
using SevenZip;

namespace FileIO.Helper.ZIP
{
    /// <summary>
    /// 7ZIP压缩文件帮助类
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
        /// 压缩7-ZIP文件
        /// </summary>
        /// <param name="strZipPath">ZIP压缩文件保存位置</param>
        /// <param name="listFolderOrFilePath">需要压缩的文件夹或文件</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool Compression7Zip(string strZipPath, List<string> listFolderOrFilePath)
        {
            try
            {
                //动态链接7ZIP类库
                if (IntPtr.Size == 4)
                {
                    SevenZipExtractor.SetLibraryPath(strX86_DllPath);
                }
                else
                {
                    SevenZipExtractor.SetLibraryPath(strX64_DllPath);
                }
                //压缩7-ZIP文件
                SevenZipCompressor sevenZipCompressor = new SevenZipCompressor();
                //sevenZipCompressor
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
