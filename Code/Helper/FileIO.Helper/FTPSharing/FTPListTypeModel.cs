using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO.Helper.FTPSharing
{
    /// <summary>
    /// FTP-FTP-LIST 命令返回参数数据模型
    /// 创建日期:2017年05月25日
    /// </summary>
    public class FTPListTypeModel
    {
        /// <summary>
        /// 文件类型 位数(1)
        /// d 文件夹/- 普通文件/l 链接/b 块设备文件/p 管道文件/c 字符设备文件/s 套接口文件
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件属主拥有权限 位数(2-4)
        /// r 读/w 写/x 执行
        /// </summary>
        public string UserOwnership { get; set; }

        /// <summary>
        /// 文件属主同一组用户拥有权限 位数(5-7)
        /// r 读/w 写/x 执行
        /// </summary>
        public string GroupUserOwnership { get; set; }

        /// <summary>
        /// 其他用户拥有权限 位数(8-10)
        /// r 读/w 写/x 执行
        /// </summary>
        public string OtherUserOwnership { get; set; }

        /// <summary>
        /// 未知参数1 (1)
        /// </summary>
        public string UnknownParameter1 { get; set; }

        /// <summary>
        /// 未知参数2 (user)
        /// </summary>
        public string UnknownParameter2 { get; set; }

        /// <summary>
        /// 未知参数3 (group)
        /// </summary>
        public string UnknownParameter3 { get; set; }

        /// <summary>
        /// 文件大小(文件夹为0)
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 文件月份
        /// </summary>
        public string FileMonth { get; set; }

        /// <summary>
        /// 文件日期
        /// </summary>
        public string FileDay { get; set; }

        /// <summary>
        /// 文件年份或时间
        /// </summary>
        public string FileYearOrTime { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
    }
}
