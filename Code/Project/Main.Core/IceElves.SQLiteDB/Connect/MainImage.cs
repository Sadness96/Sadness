using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SQLite;
using IceElves.SQLiteDB.Models;
using IceElves.SQLiteDB.Utils;

namespace IceElves.SQLiteDB.Connect
{
    /// <summary>
    /// 主方法图片操作
    /// </summary>
    public class MainImage
    {
        /// <summary>
        /// 保存图片到数据库
        /// </summary>
        /// <param name="strImageKey">图片Key值</param>
        /// <param name="strIamgePath">新增图片路径</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool SaveImageByteArray(string strImageKey, string strIamgePath)
        {
            return OperationByte.SaveByteArray("ice_system_images", "ice_sys_value", strIamgePath, string.Format("ice_sys_key = '{0}'", strImageKey));
        }

        /// <summary>
        /// 从数据库获得图片byte[]
        /// </summary>
        /// <param name="strImageKey">图片Key值</param>
        /// <returns>数据库中二进制图片</returns>
        public static byte[] GetImageByteArray(string strImageKey)
        {
            return OperationByte.GetByteArray("ice_system_images", "ice_sys_value", string.Format("ice_sys_key = '{0}'", strImageKey));
        }
    }
}
