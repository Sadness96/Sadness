using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper.TXT;

namespace Utils.Helper.Reflect
{
    /// <summary>
    /// 反射帮助类(调用参考,没有价值)
    /// 创建日期:2017年08月16日
    /// </summary>
    public class ReflectHelper
    {
        /// <summary>
        /// 加载指定路径上的程序集文件的内容
        /// </summary>
        /// <param name="strFilePath">指定类库路径</param>
        /// <returns>程序集</returns>
        public static Assembly LoadAssembly(string strFilePath)
        {
            try
            {
                if (File.Exists(strFilePath) && Path.GetExtension(strFilePath).IndexOf(".dll") > -1)
                {
                    return Assembly.LoadFile(strFilePath);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得指定程序集类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>程序集类型</returns>
        public static Type[] GetAssemblyType(Assembly assembly)
        {
            try
            {
                if (assembly != null)
                {
                    return assembly.GetExportedTypes();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 判断程序集类型一是否实现了程序集类型二接口
        /// </summary>
        /// <param name="typeOne">程序集类型一(方法)</param>
        /// <param name="typeTWO">程序集类型二(接口)</param>
        /// <returns>实现接口返回true,未实现返回false</returns>
        public static bool IsAssignableFrom(Type typeOne, Type typeTWO)
        {
            try
            {
                if (typeTWO.IsAssignableFrom(typeOne) && !typeOne.IsAbstract)
                {
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

        /// <summary>
        /// 使用指定类型的默认构造函数来创建该类型的实例。
        /// </summary>
        /// <param name="type">要创建的对象的类型</param>
        /// <returns>对新创建对象的引用</returns>
        public static object CreateInstance(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
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
    }
}
