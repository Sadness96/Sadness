using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Sadness.BasicFunction.Views.PluginMenu;
using System.Windows;
using System.Reflection;
using Microsoft.Practices.Unity;
using Sadness.Interface;

namespace Main.Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // 创建工具
            string strBasicFunctionDll = "Sadness.BasicFunction.dll";
            Dictionary<string, string> dicApp = new Dictionary<string, string>();
            dicApp.Add("数据库转换工具", "Sadness.BasicFunction.Command.PluginMenu.DBConversionCommand");
            dicApp.Add("加密解密工具", "Sadness.BasicFunction.Command.PluginMenu.EncryptionCommand");
            dicApp.Add("文件共享工具", "Sadness.BasicFunction.Command.PluginMenu.FileSharingCommand");
            dicApp.Add("生成二维码工具", "Sadness.BasicFunction.Command.PluginMenu.QRCodeCommand");
            dicApp.Add("注册工具", "Sadness.BasicFunction.Command.PluginMenu.RegistrationToolCommand");

            // 控制台提示
            System.Console.WriteLine("输入编号打开对应程序：");
            for (int i = 0; i < dicApp.Count; i++)
            {
                System.Console.WriteLine($"{i}.{dicApp.ElementAt(i).Key}");
            }

            //输入打开程序
            while (true)
            {
                int ReadIndex = -1;
                int.TryParse(System.Console.ReadLine(), out ReadIndex);
                if (ReadIndex >= 0 && ReadIndex < dicApp.Count)
                {
                    RunPluginClick(strBasicFunctionDll, dicApp.ElementAt(ReadIndex).Value);
                }
                else
                {
                    System.Console.WriteLine("输入无效,请重试!");
                }
            }

            System.Console.ReadKey();
        }

        /// <summary>
        /// 运行工具窗体
        /// </summary>
        /// <param name="strDllPath">Dll路径</param>
        /// <param name="strClassName">全类名</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool RunPluginClick(string strDllPath, string strClassName)
        {
            try
            {
                //反射获得Class Type
                Assembly assembly = Assembly.LoadFrom(strDllPath);
                Type type = assembly.GetType(strClassName);
                if (type != null)
                {
                    var container = new UnityContainer();
                    container.RegisterType<MenuPluginInterface>(new ContainerControlledLifetimeManager());
                    container.RegisterType(typeof(MenuPluginInterface), type);
                    var manager = container.Resolve<MenuPluginInterface>();
                    manager.Click();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
