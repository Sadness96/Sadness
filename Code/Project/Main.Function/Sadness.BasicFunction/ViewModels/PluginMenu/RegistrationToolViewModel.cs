using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Sadness.SQLiteDB.Connect;
using Utils.Helper.Image;
using Utils.Helper.PCInformation;

namespace Sadness.BasicFunction.ViewModels.PluginMenu
{
    /// <summary>
    /// RegistrationTool.xaml 的视图模型
    /// </summary>
    public class RegistrationToolViewModel
    {
        /// <summary>
        /// RegistrationTool.xaml 的视图模型
        /// </summary>
        public RegistrationToolViewModel()
        {
            //应用程序标题
            Title = "注册工具";
            //设置软件图标
            MainAppLargeIcon = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
        }

        /// <summary>
        /// 应用程序标题
        /// </summary>
        private string _title;
        /// <summary>
        /// 应用程序标题
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        /// <summary>
        /// 应用程序大图标
        /// </summary>
        private ImageSource _mainAppLargeIcon;
        /// <summary>
        /// 应用程序大图标
        /// </summary>
        public ImageSource MainAppLargeIcon
        {
            get
            {
                return _mainAppLargeIcon;
            }
            set
            {
                _mainAppLargeIcon = value;
            }
        }

        /// <summary>
        /// 读取网卡MAC地址
        /// </summary>
        public List<string> ListMAC
        {
            get
            {
                return PCInformationHelper.MAC();
            }
        }

        /// <summary>
        /// 读取CPU-ID
        /// </summary>
        public List<string> ListCPU
        {
            get
            {
                return PCInformationHelper.CPU();
            }
        }

        /// <summary>
        /// 读取硬盘序列号
        /// </summary>
        public List<string> ListDESK
        {
            get
            {
                return PCInformationHelper.DESK();
            }
        }

        /// <summary>
        /// 读取内存序列号
        /// </summary>
        public List<string> ListMemory
        {
            get
            {
                return PCInformationHelper.Memory();
            }
        }

        /// <summary>
        /// 读取主板序列号
        /// </summary>
        public List<string> ListMotherboard
        {
            get
            {
                return PCInformationHelper.Motherboard();
            }
        }

        /// <summary>
        /// 读取BIOS序列号
        /// </summary>
        public List<string> ListBIOS
        {
            get
            {
                return PCInformationHelper.BIOS();
            }
        }

        /// <summary>
        /// 读取显卡信息
        /// </summary>
        public List<string> ListVideo
        {
            get
            {
                return PCInformationHelper.Video();
            }
        }

        /// <summary>
        /// 注册请求码
        /// </summary>
        private string _requestCode { get; set; }
        /// <summary>
        /// 注册请求码
        /// </summary>
        public string RequestCode
        {
            get
            {
                if (string.IsNullOrEmpty(_requestCode))
                {
                    _requestCode = EncryptionHelper.RequestCode();
                }
                return _requestCode;
            }
        }

        /// <summary>
        /// 注册激活码
        /// </summary>
        public string ActivationCode
        {
            get
            {
                return EncryptionHelper.ActivationCode(_requestCode);
            }
        }
    }
}
