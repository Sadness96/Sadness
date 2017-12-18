using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SQLite;
using IceElves.SQLiteDB.Utils;
using ADO.Helper.TXT;

namespace IceElves.SQLiteDB.Connect
{
    /// <summary>
    /// MainLogin 连接方法
    /// </summary>
    public class MainLogin
    {
        /// <summary>
        /// 从数据库获得
        /// </summary>
        /// <param name="strImageKey">图片Key值</param>
        /// <returns>数据库中二进制图片</returns>
        public static byte[] GetImageByteArray(string strImageKey)
        {
            return OperationByte.GetByteArray("ice_system_images", "ice_sys_value", string.Format("ice_sys_key = '{0}'", strImageKey));
        }
    }
}
