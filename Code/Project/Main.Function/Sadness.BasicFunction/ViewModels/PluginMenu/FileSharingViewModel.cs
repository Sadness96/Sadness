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

namespace Sadness.BasicFunction.ViewModels.PluginMenu
{
    /// <summary>
    /// FileSharing.xaml 的视图模型
    /// </summary>
    public class FileSharingViewModel : BindableBase
    {
        /// <summary>
        /// FileSharing.xaml 的视图模型
        /// </summary>
        public FileSharingViewModel()
        {
            //应用程序标题
            Title = "文件共享工具";
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
        /// 共享信息
        /// </summary>
        private DataView _gridShareInfo;
        /// <summary>
        /// 共享信息
        /// </summary>
        public DataView GridShareInfo
        {
            get
            {
                _gridShareInfo = FileSharingHelper.InquireShareFile().DefaultView;
                return _gridShareInfo;
            }
            set
            {
                SetProperty(ref _gridShareInfo, value);
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
        /// 新增共享
        /// </summary>
        public DelegateCommand NewShare
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    new FileSharingSettings().ShowDialog();
                    GridShareInfo = FileSharingHelper.InquireShareFile().DefaultView;
                });
            }
        }

        /// <summary>
        /// 修改共享
        /// </summary>
        public DelegateCommand ModifyShare
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    if (SelectedItemRow == null)
                    {
                        MessageBox.Show("请选择共享文件项");
                        return;
                    }
                    if (SelectedItemRow.Row["name"].ToString().IndexOf('$') > -1)
                    {
                        MessageBox.Show("系统文件,禁止修改");
                        return;
                    }
                    new FileSharingSettings(SelectedItemRow).ShowDialog();
                    GridShareInfo = FileSharingHelper.InquireShareFile().DefaultView;
                });
            }
        }

        /// <summary>
        /// 删除共享
        /// </summary>
        public DelegateCommand DeleteShare
        {
            get
            {
                return new DelegateCommand(delegate()
                {
                    if (SelectedItemRow == null)
                    {
                        MessageBox.Show("请选择共享文件项");
                        return;
                    }
                    string strFolderName = SelectedItemRow.Row["name"].ToString();
                    string strFolderPath = SelectedItemRow.Row["path"].ToString();
                    if (strFolderName.IndexOf('$') > -1)
                    {
                        MessageBox.Show("系统文件,禁止删除");
                        return;
                    }
                    if (FileSharingHelper.DeleteShareFolder(strFolderPath))
                    {
                        MessageBox.Show(string.Format("{0} 已经删除", strFolderPath));
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} 删除失败", strFolderPath));
                    }
                    GridShareInfo = FileSharingHelper.InquireShareFile().DefaultView;
                });
            }
        }

        /// <summary>
        /// 表格选中事件
        /// </summary>
        public DelegateCommand<DataRowView> GridSelectionChanged
        {
            get
            {
                return new DelegateCommand<DataRowView>((eDataRow) =>
                {
                    if (eDataRow != null)
                    {
                        SelectedItemRow = eDataRow;
                    }
                });
            }
        }

        /// <summary>
        /// 表格双击事件
        /// </summary>
        public DelegateCommand<DataRowView> GridMouseDoubleClick
        {
            get
            {
                return new DelegateCommand<DataRowView>((eDataRow) =>
                {
                    if (eDataRow != null)
                    {
                        SelectedItemRow = eDataRow;
                        string result = eDataRow.Row["path"].ToString();
                        if (string.IsNullOrEmpty(result))
                        {
                            MessageBox.Show("选中路径为空");
                            return;
                        }
                        System.Diagnostics.Process.Start(result);
                    }
                });
            }
        }
    }
}
