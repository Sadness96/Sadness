using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO.Helper.FileSharing
{
    /// <summary>
    /// 共享文件访问权限枚举类
    /// 创建日期:2017年5月23日
    /// </summary>
    public class SharingPermissions
    {
        /// <summary>
        /// 共享文件访问权限。
        /// </summary>
        public enum PermissionsType
        {
            /// <summary>
            /// 完全控制
            /// </summary>
            FULL = 0,
            /// <summary>
            /// 只读
            /// </summary>
            READ = 1,
            /// <summary>
            /// 读取/写入
            /// </summary>
            CHANGE = 2
        }
    }
}
