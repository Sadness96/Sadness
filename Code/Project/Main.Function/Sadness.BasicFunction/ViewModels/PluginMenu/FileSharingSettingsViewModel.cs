using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Utils.Helper.Image;
using FileIO.Helper.FileSharing;
using Sadness.SQLiteDB.Connect;
using Sadness.BasicFunction.Views.PluginMenu;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Forms;

namespace Sadness.BasicFunction.ViewModels.PluginMenu
{
    /// <summary>
    /// FileSharingSettings.xaml 的视图模型
    /// </summary>
    public class FileSharingSettingsViewModel : BindableBase
    {
        /// <summary>
        /// FileSharingSettings.xaml 的视图模型
        /// </summary>
        public FileSharingSettingsViewModel()
        {
            //应用程序标题
            Title = "文件共享设置";
            //设置软件图标
            MainAppLargeIcon = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
            //初始化数据
            SetSharingIndex = 0;
        }

        /// <summary>
        /// FileSharingSettings.xaml 的视图模型
        /// </summary>
        /// <param name="mySelectedElement">选中数据</param>
        public FileSharingSettingsViewModel(DataRowView mySelectedElement)
        {
            //应用程序标题
            Title = "文件共享设置";
            //设置软件图标
            MainAppLargeIcon = ImageHelper.ByteArrayToImageSource(MainImage.GetImageByteArray("AppLargeIcon"));
            //赋值选中数据
            this.SelectedItemRow = mySelectedElement;
            //初始化数据
            SetSharingIndex = 0;
            StrSharingPath = mySelectedElement.Row["path"].ToString();
            StrSharingName = mySelectedElement.Row["name"].ToString();
            SetSharingValue = mySelectedElement.Row["permissions"].ToString();
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
        /// 当前表格选中行数据
        /// </summary>
        private DataRowView _SelectedItemRow;
        /// <summary>
        /// 当前表格选中行数据
        /// </summary>
        public DataRowView SelectedItemRow
        {
            get
            {
                return _SelectedItemRow;
            }
            set
            {
                SetProperty(ref _SelectedItemRow, value);
            }
        }

        /// <summary>
        /// 设置共享
        /// </summary>
        private List<string> _setSharing;
        /// <summary>
        /// 设置共享
        /// </summary>
        public List<string> SetSharing
        {
            get
            {
                _setSharing = new List<string>();
                _setSharing.Add("完全控制");
                _setSharing.Add("只读");
                _setSharing.Add("读取/写入");
                return _setSharing;
            }
            set
            {
                SetProperty(ref _setSharing, value);
            }
        }

        /// <summary>
        /// 设置共享索引
        /// </summary>
        private int _setSharingIndex;
        /// <summary>
        /// 设置共享索引
        /// </summary>
        public int SetSharingIndex
        {
            get
            {
                return _setSharingIndex;
            }
            set
            {
                SetProperty(ref _setSharingIndex, value);
            }
        }

        /// <summary>
        /// 设置共享值
        /// </summary>
        private string _setSharingValue;
        /// <summary>
        /// 设置共享值
        /// </summary>
        public string SetSharingValue
        {
            get
            {
                return _setSharingValue;
            }
            set
            {
                SetProperty(ref _setSharingValue, value);
            }
        }

        /// <summary>
        /// 共享路径
        /// </summary>
        private string _strSharingPath;
        /// <summary>
        /// 共享路径
        /// </summary>
        public string StrSharingPath
        {
            get
            {
                return _strSharingPath;
            }
            set
            {
                SetProperty(ref _strSharingPath, value);
            }
        }

        /// <summary>
        /// 共享名称
        /// </summary>
        private string _strSharingName;
        /// <summary>
        /// 共享名称
        /// </summary>
        public string StrSharingName
        {
            get
            {
                return _strSharingName;
            }
            set
            {
                SetProperty(ref _strSharingName, value);
            }
        }

        /// <summary>
        /// 选择文件路径
        /// </summary>
        public DelegateCommand SelectPath
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string strFolderPath = folderBrowserDialog.SelectedPath.Trim();
                        StrSharingPath = strFolderPath;
                        StrSharingName = System.IO.Path.GetFileName(strFolderPath);
                    }
                });
            }
        }

        /// <summary>
        /// 新增共享文件
        /// </summary>
        public DelegateCommand<Window> AddFile
        {
            get
            {
                return new DelegateCommand<Window>((window) =>
                {
                    //如果修改共享,则先删除后新增
                    if (SelectedItemRow != null)
                    {
                        if (SelectedItemRow == null)
                        {
                            System.Windows.MessageBox.Show("请选择共享文件项");
                            return;
                        }
                        string strFolderName = SelectedItemRow.Row["name"].ToString();
                        string strFolderPath = SelectedItemRow.Row["path"].ToString();
                        if (strFolderName.IndexOf('$') > -1)
                        {
                            System.Windows.MessageBox.Show("系统文件,禁止删除");
                            return;
                        }
                        FileSharingHelper.DeleteShareFolder(strFolderPath);
                    }
                    //效验是否有值
                    if (string.IsNullOrEmpty(StrSharingPath))
                    {
                        System.Windows.MessageBox.Show("请选择共享路径!");
                        return;
                    }
                    if (string.IsNullOrEmpty(StrSharingName))
                    {
                        System.Windows.MessageBox.Show("请输入共享名称!");
                        return;
                    }
                    if (string.IsNullOrEmpty(SetSharingValue))
                    {
                        System.Windows.MessageBox.Show("请输选择共享类型!");
                        return;
                    }
                    string Permissions = "";
                    if (SetSharingValue.Equals("完全控制"))
                    {
                        Permissions = "FULL";
                    }
                    else if (SetSharingValue.Equals("只读"))
                    {
                        Permissions = "READ";
                    }
                    else if (SetSharingValue.Equals("读取/写入"))
                    {
                        Permissions = "CHANGE";
                    }
                    if (FileSharingHelper.AddShareFolder(StrSharingPath, StrSharingName, Permissions))
                    {
                        System.Windows.MessageBox.Show(string.Format("{0} 共享成功", StrSharingPath));
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(string.Format("{0} 共享失败", StrSharingPath));
                    }
                    //关闭窗体
                    if (window != null)
                    {
                        window.Close();
                    }
                });
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public DelegateCommand<Window> Close
        {
            get
            {
                return new DelegateCommand<Window>((window) =>
                {
                    if (window != null)
                    {
                        window.Close();
                    }
                });
            }
        }
    }
}
