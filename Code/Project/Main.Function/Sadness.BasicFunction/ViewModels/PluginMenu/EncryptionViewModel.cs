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
                return new DelegateCommand(delegate ()
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
                return new DelegateCommand(delegate ()
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
                return new DelegateCommand(delegate ()
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
                return new DelegateCommand(delegate ()
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
        /// <summary>
        /// Des加密源
        /// </summary>
        private string _textDesSource;
        /// <summary>
        /// Des加密源
        /// </summary>
        public string TextDesSource
        {
            get
            {
                return _textDesSource;
            }
            set
            {
                SetProperty(ref _textDesSource, value);
            }
        }

        /// <summary>
        /// Des加密目标
        /// </summary>
        private string _textDesTarget;
        /// <summary>
        /// Des加密目标
        /// </summary>
        public string TextDesTarget
        {
            get
            {
                return _textDesTarget;
            }
            set
            {
                SetProperty(ref _textDesTarget, value);
            }
        }

        /// <summary>
        /// Des加密秘钥
        /// </summary>
        private string _textDesKey;
        /// <summary>
        /// Des加密秘钥
        /// </summary>
        public string TextDesKey
        {
            get
            {
                return _textDesKey;
            }
            set
            {
                SetProperty(ref _textDesKey, value);
            }
        }

        /// <summary>
        /// Des加密向量
        /// </summary>
        private string _textDesIV;
        /// <summary>
        /// Des加密向量
        /// </summary>
        public string TextDesIV
        {
            get
            {
                return _textDesIV;
            }
            set
            {
                SetProperty(ref _textDesIV, value);
            }
        }

        /// <summary>
        /// Des加密
        /// </summary>
        public DelegateCommand DesEncrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (string.IsNullOrEmpty(_textDesKey) || string.IsNullOrEmpty(_textDesIV))
                    {
                        MessageBox.Show("请输入秘钥和向量!");
                        return;
                    }
                    TextDesTarget = DESHelper.DESEncrypt(TextDesSource, TextDesKey, TextDesIV);
                });
            }
        }

        /// <summary>
        /// Des解密
        /// </summary>
        public DelegateCommand DesDecrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (string.IsNullOrEmpty(_textDesKey) || string.IsNullOrEmpty(_textDesIV))
                    {
                        MessageBox.Show("请输入秘钥和向量!");
                        return;
                    }
                    TextDesTarget = DESHelper.DESDecrypt(TextDesSource, TextDesKey, TextDesIV);
                });
            }
        }

        /// <summary>
        /// Des加密文件
        /// </summary>
        public DelegateCommand DesEncryptFile
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (string.IsNullOrEmpty(_textDesKey) || string.IsNullOrEmpty(_textDesIV))
                    {
                        MessageBox.Show("请输入秘钥和向量!");
                        return;
                    }
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    dialog.Title = "选择文件";
                    dialog.Filter = "所有文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strFilePath = dialog.FileName;
                        string strSaveFilePath = string.Format("{0}\\{1}(Encrypt){2}", Path.GetDirectoryName(strFilePath), Path.GetFileNameWithoutExtension(strFilePath), Path.GetExtension(strFilePath));
                        if (DESHelper.FileDESEncrypt(strFilePath, strSaveFilePath, TextDesKey, TextDesIV))
                        {
                            MessageBox.Show("Des文件加密成功!");
                        }
                        else
                        {
                            MessageBox.Show("Des文件加密失败!");
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Des解密文件
        /// </summary>
        public DelegateCommand DesDecryptFile
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (string.IsNullOrEmpty(_textDesKey) || string.IsNullOrEmpty(_textDesIV))
                    {
                        MessageBox.Show("请输入秘钥和向量!");
                        return;
                    }
                    Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                    dialog.Title = "选择文件";
                    dialog.Filter = "所有文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strFilePath = dialog.FileName;
                        string strSaveFilePath = string.Format("{0}\\{1}(Decrypt){2}", Path.GetDirectoryName(strFilePath), Path.GetFileNameWithoutExtension(strFilePath), Path.GetExtension(strFilePath));
                        if (DESHelper.FileDESDecrypt(strFilePath, strSaveFilePath, TextDesKey, TextDesIV))
                        {
                            MessageBox.Show("Des文件解密成功!");
                        }
                        else
                        {
                            MessageBox.Show("Des文件解密失败!");
                        }
                    }
                });
            }
        }
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
