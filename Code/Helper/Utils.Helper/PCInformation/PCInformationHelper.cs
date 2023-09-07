using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;//WMI库
using Utils.Helper.TXT;
using System.Runtime.InteropServices;

namespace Utils.Helper.PCInformation
{
    /// <summary>
    /// 获取电脑信息帮助类
    /// 创建日期:2017年06月06日
    /// </summary>
    public class PCInformationHelper
    {
        /// <summary>
        /// 读取网卡 MAC 地址
        /// </summary>
        /// <returns>成功返回网卡MAC地址,失败返回NULL</returns>
        public static List<string> MAC()
        {

            try
            {
                // 虚拟网卡标识
                List<string> listFictitious = new List<string>
                {
                    "vmnetadapter",
                    "ppoe",
                    "bthpan",
                    "tapvpn",
                    "ndisip",
                    "sinforvnic"
                };
                // 获取并过滤网卡 Mac 信息
                List<string> listMAC = new List<string>();
                foreach (ManagementObject mo in new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances())
                {
                    if ((bool)mo["IPEnabled"] && !listFictitious.Contains(mo["ServiceName"].ToString().ToLower()))
                    {
                        listMAC.Add(mo["MacAddress"].ToString());
                    }
                }
                return listMAC;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 读取 CPU-ID
        /// </summary>
        /// <returns>成功返回CPU-ID,失败返回NULL</returns>
        public static List<string> CPU()
        {
            try
            {
                List<string> listCPU = new List<string>();
                string strMac = "";
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    // Manufacturer = 处理器制造商
                    // Name = 处理器名字
                    // Processorid = CPU-ID
                    strMac = mo["Processorid"].ToString();
                    listCPU.Add(strMac);
                }
                return listCPU;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 读取硬盘序列号
        /// </summary>
        /// <returns>成功返回硬盘序列号,失败返回NULL</returns>
        public static List<string> DESK()
        {

            try
            {
                List<string> listDESK = new List<string>();
                string strMac = "";
                ManagementClass mc = new ManagementClass("win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    // Model = 硬盘信息
                    // SerialNumber = 硬盘序列号
                    strMac = mo["SerialNumber"].ToString();
                    listDESK.Add(strMac);
                }
                return listDESK;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 读取内存序列号
        /// </summary>
        /// <returns>成功返回内存序列号,失败返回NULL</returns>
        public static List<string> Memory()
        {
            try
            {
                List<string> listMemory = new List<string>();
                string strMac = "";
                ManagementClass mc = new ManagementClass("Win32_PhysicalMemory");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    // Manufacturer = 内存生产商
                    // SerialNumber = 序列号
                    strMac = mo["SerialNumber"].ToString();
                    listMemory.Add(strMac);
                }
                return listMemory;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 读取主板序列号
        /// </summary>
        /// <returns>成功返回主板序列号,失败返回NULL</returns>
        public static List<string> Motherboard()
        {
            try
            {
                List<string> listMotherboard = new List<string>();
                string strMac = "";
                ManagementClass mc = new ManagementClass("WIN32_BaseBoard");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    // Manufacturer = 主板制造商
                    // Product = 主板型号
                    // SerialNumber = 序列号
                    strMac = mo["SerialNumber"].ToString();
                    listMotherboard.Add(strMac);
                }
                return listMotherboard;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 读取BIOS序列号
        /// </summary>
        /// <returns>成功返回BIOS序列号,失败返回NULL</returns>
        public static List<string> BIOS()
        {
            try
            {
                List<string> listBIOS = new List<string>();
                string strMac = "";
                ManagementClass mc = new ManagementClass("Win32_BIOS");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    // Manufacturer = BIOS制造商名称
                    // SerialNumber = BIOS序列号
                    // ReleaseDate = 出厂日期
                    // Version = 版本号
                    strMac = mo["SerialNumber"].ToString();
                    listBIOS.Add(strMac);
                }
                return listBIOS;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 读取显卡信息
        /// </summary>
        /// <returns>成功返回显卡信息,失败返回NULL</returns>
        public static List<string> Video()
        {
            try
            {
                List<string> listVideo = new List<string>();
                string strMac = "";
                ManagementClass mc = new ManagementClass("Win32_VideoController");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    // Name = 显卡信息
                    // DriverVersion = 驱动程序版本
                    strMac = mo["Name"].ToString();
                    listVideo.Add(strMac);
                }
                return listVideo;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取系统用户列表
        /// </summary>
        /// <returns></returns>
        public static List<string> UserName()
        {
            var temp = new List<string>();
            NetUserEnum(null, 0, 2, out IntPtr bufPtr, -1, out int EntriesRead, out int TotalEntries, out int Resume);
            if (EntriesRead > 0)
            {
                USER_INFO_0[] Users = new USER_INFO_0[EntriesRead];
                IntPtr iter = bufPtr;
                for (int i = 0; i < EntriesRead; i++)
                {
                    Users[i] = (USER_INFO_0)Marshal.PtrToStructure(iter, typeof(USER_INFO_0));
                    iter = (IntPtr)((int)iter + Marshal.SizeOf(typeof(USER_INFO_0)));
                    temp.Add(Users[i].Username);
                }
                NetApiBufferFree(bufPtr);
            }
            return temp;
        }

        #region 验证操作系统用户名
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct USER_INFO_0
        {
            public string Username;
        }
        [DllImport("Netapi32.dll ")]
        extern static int NetUserEnum([MarshalAs(UnmanagedType.LPWStr)]
        string servername, int level, int filter, out IntPtr bufptr, int prefmaxlen, out int entriesread, out int totalentries, out int resume_handle);

        [DllImport("Netapi32.dll ")]
        extern static int NetApiBufferFree(IntPtr Buffer);
        #endregion
    }
}
