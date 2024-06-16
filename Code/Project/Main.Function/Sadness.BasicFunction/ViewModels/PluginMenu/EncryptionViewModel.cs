using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Drawing.Imaging;
using Microsoft.Win32;
using Sadness.SQLiteDB.Connect;
using Utils.Helper.TXT;
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
                    OpenFileDialog dialog = new OpenFileDialog();
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
                    OpenFileDialog dialog = new OpenFileDialog();
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
                    OpenFileDialog dialog = new OpenFileDialog();
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
                    OpenFileDialog dialog = new OpenFileDialog();
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
        /// <summary>
        /// Rsa加密源
        /// </summary>
        private string _textRsaSource;
        /// <summary>
        /// Rsa加密源
        /// </summary>
        public string TextRsaSource
        {
            get
            {
                return _textRsaSource;
            }
            set
            {
                SetProperty(ref _textRsaSource, value);
            }
        }

        /// <summary>
        /// Rsa加密目标
        /// </summary>
        private string _textRsaTarget;
        /// <summary>
        /// Rsa加密目标
        /// </summary>
        public string TextRsaTarget
        {
            get
            {
                return _textRsaTarget;
            }
            set
            {
                SetProperty(ref _textRsaTarget, value);
            }
        }

        /// <summary>
        /// Rsa加密公钥
        /// </summary>
        private string _textRsaPublicKey;
        /// <summary>
        /// Rsa加密公钥
        /// </summary>
        public string TextRsaPublicKey
        {
            get
            {
                return _textRsaPublicKey;
            }
            set
            {
                SetProperty(ref _textRsaPublicKey, value);
            }
        }

        /// <summary>
        /// Rsa加密私钥
        /// </summary>
        private string _textRsaPrivateKey;
        /// <summary>
        /// Rsa加密私钥
        /// </summary>
        public string TextRsaPrivateKey
        {
            get
            {
                return _textRsaPrivateKey;
            }
            set
            {
                SetProperty(ref _textRsaPrivateKey, value);
            }
        }

        /// <summary>
        /// Rsa生成秘钥
        /// </summary>
        public DelegateCommand RsaGenerateKey
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    string strRsaPublicKey = string.Empty;
                    string strRsaPrivateKey = string.Empty;
                    RSAHelper.RSAKey(out strRsaPublicKey, out strRsaPrivateKey);
                    TextRsaPublicKey = strRsaPublicKey;
                    TextRsaPrivateKey = strRsaPrivateKey;
                });
            }
        }

        /// <summary>
        /// Rsa加密
        /// </summary>
        public DelegateCommand RsaEncrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (string.IsNullOrEmpty(_textRsaPublicKey))
                    {
                        MessageBox.Show("请输入公钥!");
                        return;
                    }
                    TextRsaTarget = RSAHelper.RSAEncrypt(TextRsaSource, TextRsaPublicKey);
                });
            }
        }

        /// <summary>
        /// Rsa解密
        /// </summary>
        public DelegateCommand RsaDecrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (string.IsNullOrEmpty(_textRsaPrivateKey))
                    {
                        MessageBox.Show("请输入私钥!");
                        return;
                    }
                    TextRsaTarget = RSAHelper.RSADecrypt(TextRsaSource, TextRsaPrivateKey);
                });
            }
        }
        #endregion

        #region Base64加密 Tab分页
        /// <summary>
        /// Base64加密源
        /// </summary>
        private string _textBase64Source;
        /// <summary>
        /// Base64加密源
        /// </summary>
        public string TextBase64Source
        {
            get
            {
                return _textBase64Source;
            }
            set
            {
                SetProperty(ref _textBase64Source, value);
            }
        }

        /// <summary>
        /// Base64加密目标
        /// </summary>
        private string _textBase64Target;
        /// <summary>
        /// Base64加密目标
        /// </summary>
        public string TextBase64Target
        {
            get
            {
                return _textBase64Target;
            }
            set
            {
                SetProperty(ref _textBase64Target, value);
            }
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        public DelegateCommand Base64Encrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    TextBase64Target = Base64Helper.Base64Encrypt(TextBase64Source);
                });
            }
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        public DelegateCommand Base64Decrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    TextBase64Target = Base64Helper.Base64Decrypt(TextBase64Source);
                });
            }
        }

        /// <summary>
        /// Base64加密图片
        /// </summary>
        public DelegateCommand Base64EncryptImage
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Title = "选择文件";
                    dialog.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|" +
                                    "Windows Bitmap(*.bmp)|*.bmp|" +
                                    "Windows Icon(*.ico)|*.ico|" +
                                    "Graphics Interchange Format (*.gif)|*.gif|" +
                                    "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphics (*.png)|*.png|" +
                                    "Tag Image File Format (*.tif)|*.tif;*.tiff|" +
                                    "所有文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strFilePath = dialog.FileName;
                        ImageFormat imfImage = Base64Helper.GetImageFormatFromPath(strFilePath);
                        if (imfImage != null)
                        {
                            TextBase64Target = Base64Helper.ImageBase64Encrypt(strFilePath, imfImage);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Base64解密图片
        /// </summary>
        public DelegateCommand Base64DecryptImage
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (string.IsNullOrEmpty(_textBase64Source))
                    {
                        MessageBox.Show("数据源为空!");
                        return;
                    }
                    Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
                    dialog.Title = "保存文件";
                    dialog.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|" +
                                    "Windows Bitmap(*.bmp)|*.bmp|" +
                                    "Windows Icon(*.ico)|*.ico|" +
                                    "Graphics Interchange Format (*.gif)|*.gif|" +
                                    "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphics (*.png)|*.png|" +
                                    "Tag Image File Format (*.tif)|*.tif;*.tiff|" +
                                    "所有文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strFilePath = dialog.FileName;
                        ImageFormat imfImage = Base64Helper.GetImageFormatFromPath(strFilePath);
                        if (Base64Helper.ImageBase64Decrypt(TextBase64Source, strFilePath, imfImage))
                        {
                            TextBase64Target = "true";
                        }
                        else
                        {
                            TextBase64Target = "false";
                        }
                    }
                });
            }
        }
        #endregion

        #region MD5加密 Tab分页
        /// <summary>
        /// MD5加密源
        /// </summary>
        private string _textMD5Source;
        /// <summary>
        /// MD5加密源
        /// </summary>
        public string TextMD5Source
        {
            get
            {
                return _textMD5Source;
            }
            set
            {
                SetProperty(ref _textMD5Source, value);
            }
        }

        /// <summary>
        /// MD5加密目标
        /// </summary>
        private string _textMD5Target;
        /// <summary>
        /// MD5加密目标
        /// </summary>
        public string TextMD5Target
        {
            get
            {
                return _textMD5Target;
            }
            set
            {
                SetProperty(ref _textMD5Target, value);
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        public DelegateCommand MD5Encrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    StringBuilder strTarget = new StringBuilder();
                    strTarget.Append(string.Format("16位小写:{0}\n", MD5Helper.MD5Encrypt16(TextMD5Source, true)));
                    strTarget.Append(string.Format("16位大写:{0}\n", MD5Helper.MD5Encrypt16(TextMD5Source, false)));
                    strTarget.Append(string.Format("32位小写:{0}\n", MD5Helper.MD5Encrypt32(TextMD5Source, true)));
                    strTarget.Append(string.Format("32位大写:{0}", MD5Helper.MD5Encrypt32(TextMD5Source, false)));
                    TextMD5Target = strTarget.ToString();
                });
            }
        }

        /// <summary>
        /// MD5加密文件
        /// </summary>
        public DelegateCommand MD5EncryptFile
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Title = "选择文件";
                    dialog.Filter = "所有文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strFilePath = dialog.FileName;
                        StringBuilder strTarget = new StringBuilder();
                        strTarget.Append(string.Format("32位小写:{0}\n", MD5Helper.FileMD5Encrypt32(strFilePath, true)));
                        strTarget.Append(string.Format("32位大写:{0}", MD5Helper.FileMD5Encrypt32(strFilePath, false)));
                        TextMD5Target = strTarget.ToString();
                    }
                });
            }
        }
        #endregion

        #region SHA1加密 Tab分页
        /// <summary>
        /// SHA1加密源
        /// </summary>
        private string _textSHA1Source;
        /// <summary>
        /// SHA1加密源
        /// </summary>
        public string TextSHA1Source
        {
            get
            {
                return _textSHA1Source;
            }
            set
            {
                SetProperty(ref _textSHA1Source, value);
            }
        }

        /// <summary>
        /// SHA1加密目标
        /// </summary>
        private string _textSHA1Target;
        /// <summary>
        /// SHA1加密目标
        /// </summary>
        public string TextSHA1Target
        {
            get
            {
                return _textSHA1Target;
            }
            set
            {
                SetProperty(ref _textSHA1Target, value);
            }
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        public DelegateCommand SHA1Encrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    StringBuilder strTarget = new StringBuilder();
                    strTarget.Append(string.Format("40位小写:{0}\n", SHA1Helper.SHA1Encrypt_40Lower(TextSHA1Source)));
                    strTarget.Append(string.Format("40位大写:{0}", SHA1Helper.SHA1Encrypt_40Upper(TextSHA1Source)));
                    TextSHA1Target = strTarget.ToString();
                });
            }
        }

        /// <summary>
        /// SHA1加密文件
        /// </summary>
        public DelegateCommand SHA1EncryptFile
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Title = "选择文件";
                    dialog.Filter = "所有文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strFilePath = dialog.FileName;
                        StringBuilder strTarget = new StringBuilder();
                        strTarget.Append(string.Format("40位小写:{0}\n", SHA1Helper.FileSHA1Encrypt_40Lower(strFilePath)));
                        strTarget.Append(string.Format("40位大写:{0}", SHA1Helper.FileSHA1Encrypt_40Upper(strFilePath)));
                        TextSHA1Target = strTarget.ToString();
                    }
                });
            }
        }
        #endregion

        #region CRC32加密 Tab分页
        /// <summary>
        /// CRC32加密源
        /// </summary>
        private string _textCRC32Source;
        /// <summary>
        /// SHA1加密源
        /// </summary>
        public string TextCRC32Source
        {
            get
            {
                return _textCRC32Source;
            }
            set
            {
                SetProperty(ref _textCRC32Source, value);
            }
        }

        /// <summary>
        /// CRC32加密目标
        /// </summary>
        private string _textCRC32Target;
        /// <summary>
        /// CRC32加密目标
        /// </summary>
        public string TextCRC32Target
        {
            get
            {
                return _textCRC32Target;
            }
            set
            {
                SetProperty(ref _textCRC32Target, value);
            }
        }

        /// <summary>
        /// CRC32加密
        /// </summary>
        public DelegateCommand CRC32Encrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    StringBuilder strTarget = new StringBuilder();
                    strTarget.Append(string.Format("8位小写:{0}\n", CRC32Helper.CRC32Encrypt_8Lower(TextCRC32Source)));
                    strTarget.Append(string.Format("8位大写:{0}", CRC32Helper.CRC32Encrypt_8Upper(TextCRC32Source)));
                    TextCRC32Target = strTarget.ToString();
                });
            }
        }

        /// <summary>
        /// CRC32加密文件
        /// </summary>
        public DelegateCommand CRC32EncryptFile
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Title = "选择文件";
                    dialog.Filter = "所有文件|*.*";
                    if (dialog.ShowDialog() == true)
                    {
                        string strFilePath = dialog.FileName;
                        StringBuilder strTarget = new StringBuilder();
                        strTarget.Append(string.Format("8位小写:{0}\n", CRC32Helper.FileCRC32Encrypt_8Lower(strFilePath)));
                        strTarget.Append(string.Format("8位大写:{0}", CRC32Helper.FileCRC32Encrypt_8Upper(strFilePath)));
                        TextCRC32Target = strTarget.ToString();
                    }
                });
            }
        }
        #endregion

        #region Folder加密 Tab分页
        /// <summary>
        /// Folder加密秘钥
        /// </summary>
        private string _textFolderKey;
        /// <summary>
        /// Folder加密秘钥
        /// </summary>
        public string TextFolderKey
        {
            get
            {
                return _textFolderKey;
            }
            set
            {
                SetProperty(ref _textFolderKey, value);
            }
        }

        /// <summary>
        /// Folder加密
        /// </summary>
        public DelegateCommand FolderEncrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string strFolderPath = folderBrowserDialog.SelectedPath;
                        if (FolderHelper.FolderEncrypt(strFolderPath, FolderHelper.Lock))
                        {
                            MessageBox.Show("文件夹加密成功");
                        }
                        else
                        {
                            MessageBox.Show("文件夹加密失败");
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Folder解密
        /// </summary>
        public DelegateCommand FolderDecrypt
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string strFolderPath = folderBrowserDialog.SelectedPath;
                        if (FolderHelper.FolderDecrypt(strFolderPath))
                        {
                            MessageBox.Show("文件夹解密成功");
                        }
                        else
                        {
                            MessageBox.Show("文件夹解密失败");
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Folder加密(带密码)
        /// </summary>
        public DelegateCommand FolderEncryptPassword
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (string.IsNullOrEmpty(_textFolderKey))
                    {
                        MessageBox.Show("请输入秘钥!");
                        return;
                    }
                    System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string strFolderPath = folderBrowserDialog.SelectedPath;
                        if (FolderHelper.FolderEncrypt(strFolderPath, FolderHelper.Lock, TextFolderKey))
                        {
                            MessageBox.Show("文件夹加密成功");
                        }
                        else
                        {
                            MessageBox.Show("文件夹加密失败");
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Folder解密(带密码)
        /// </summary>
        public DelegateCommand FolderDecryptPassword
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (string.IsNullOrEmpty(_textFolderKey))
                    {
                        MessageBox.Show("请输入秘钥!");
                        return;
                    }
                    System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string strFolderPath = folderBrowserDialog.SelectedPath;
                        if (FolderHelper.FolderDecrypt(strFolderPath, TextFolderKey))
                        {
                            MessageBox.Show("文件夹解密成功");
                        }
                        else
                        {
                            MessageBox.Show("文件夹解密失败");
                        }
                    }
                });
            }
        }
        #endregion
    }
}
