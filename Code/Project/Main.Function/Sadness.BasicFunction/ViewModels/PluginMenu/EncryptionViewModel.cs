using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Sadness.SQLiteDB.Connect;
using Utils.Helper.Image;
using Utils.Helper.Encryption;
using Prism.Mvvm;
using Prism.Commands;

namespace Sadness.BasicFunction.ViewModels.PluginMenu
{
    /// <summary>
    /// Encryption.xaml 的视图模型
    /// </summary>
    public class EncryptionViewModel : BindableBase
    {
        /// <summary>
        /// Encryption.xaml 的视图模型
        /// </summary>
        public EncryptionViewModel()
        {
            //应用程序标题
            Title = "加密解密工具";
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

        #region AES加密 Tab分页
        /// <summary>
        /// Aes加密源
        /// </summary>
        private string _textAesSource;
        /// <summary>
        /// Aes加密源
        /// </summary>
        public string TextAesSource
        {
            get
            {
                return _textAesSource;
            }
            set
            {
                SetProperty(ref _textAesSource, value);
            }
        }

        /// <summary>
        /// Aes加密目标
        /// </summary>
        private string _textAesTarget;
        /// <summary>
        /// Aes加密目标
        /// </summary>
        public string TextAesTarget
        {
            get
            {
                return _textAesTarget;
            }
            set
            {
                SetProperty(ref _textAesTarget, value);
            }
        }

        /// <summary>
        /// Aes加密秘钥
        /// </summary>
        private string _textAesKey;
        /// <summary>
        /// Aes加密秘钥
        /// </summary>
        public string TextAesKey
        {
            get
            {
                return _textAesKey;
            }
            set
            {
                SetProperty(ref _textAesKey, value);
            }
        }

        /// <summary>
        /// Aes加密
        /// </summary>
        public DelegateCommand AesEncrypt
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    if (string.IsNullOrEmpty(_textAesKey))
                    {
                        MessageBox.Show("请输入秘钥!");
                        return;
                    }
                    TextAesTarget = AESHelper.AESEncrypt(TextAesSource, TextAesKey);
                });
            }
        }

        /// <summary>
        /// Aes解密
        /// </summary>
        public DelegateCommand AesDecrypt
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    if (string.IsNullOrEmpty(_textAesKey))
                    {
                        MessageBox.Show("请输入秘钥!");
                        return;
                    }
                    TextAesTarget = AESHelper.AESDecrypt(TextAesSource, TextAesKey);
                });
            }
        }

        /// <summary>
        /// Aes加密文件
        /// </summary>
        public DelegateCommand AesEncryptFile
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    if (string.IsNullOrEmpty(_textAesKey))
                    {
                        MessageBox.Show("请输入秘钥!");
                        return;
                    }
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    dialog.Title = "选择文件";
                    dialog.Filter = "所有文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strFilePath = dialog.FileName;
                        string strSaveFilePath = string.Format("{0}\\{1}(Encrypt){2}", Path.GetDirectoryName(strFilePath), Path.GetFileNameWithoutExtension(strFilePath), Path.GetExtension(strFilePath));
                        if (AESHelper.FileAESEncrypt(strFilePath, strSaveFilePath, TextAesKey))
                        {
                            MessageBox.Show("Aes文件加密成功!");
                        }
                        else
                        {
                            MessageBox.Show("Aes文件加密失败!");
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Aes解密文件
        /// </summary>
        public DelegateCommand AesDecryptFile
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    if (string.IsNullOrEmpty(_textAesKey))
                    {
                        MessageBox.Show("请输入秘钥!");
                        return;
                    }
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    dialog.Title = "选择文件";
                    dialog.Filter = "所有文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strFilePath = dialog.FileName;
                        string strSaveFilePath = string.Format("{0}\\{1}(Decrypt){2}", Path.GetDirectoryName(strFilePath), Path.GetFileNameWithoutExtension(strFilePath), Path.GetExtension(strFilePath));
                        if (AESHelper.FileAESDecrypt(strFilePath, strSaveFilePath, TextAesKey))
                        {
                            MessageBox.Show("Aes文件解密成功!");
                        }
                        else
                        {
                            MessageBox.Show("Aes文件解密失败!");
                        }
                    }
                });
            }
        }
        #endregion

        #region DES加密 Tab分页

        #endregion

        #region RSA加密 Tab分页

        #endregion

        #region Base64加密 Tab分页

        #endregion

        #region MD5加密 Tab分页

        #endregion

        #region SHA1加密 Tab分页

        #endregion

        #region CRC32加密 Tab分页

        #endregion

        #region Folder加密 Tab分页

        #endregion
    }
}
