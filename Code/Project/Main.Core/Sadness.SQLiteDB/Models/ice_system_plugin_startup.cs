using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadness.SQLiteDB.Models
{
    /// <summary>
    /// 启动项表数据模型
    /// </summary>
    public partial class ice_system_plugin_startup
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ice_id { get; set; }
        /// <summary>
        /// 启动顺序
        /// </summary>
        public int ice_number { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string ice_function_name { get; set; }
        /// <summary>
        /// 类库文件路径
        /// </summary>
        public string ice_dllfile_path { get; set; }
        /// <summary>
        /// 类库文件 Class
        /// </summary>
        public string ice_dllfile_class { get; set; }
    }
}
