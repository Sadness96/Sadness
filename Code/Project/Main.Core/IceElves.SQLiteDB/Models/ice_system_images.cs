using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceElves.SQLiteDB.Models
{
    /// <summary>
    /// 系统图片表数据模型
    /// </summary>
    public partial class ice_system_images
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ice_id { get; set; }
        /// <summary>
        /// Image_Key
        /// </summary>
        public string ice_sys_key { get; set; }
        /// <summary>
        /// Image_Value
        /// </summary>
        public byte[] ice_sys_value { get; set; }
    }
}
