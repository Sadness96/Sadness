using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadness.BasicFunction.Models
{
    /// <summary>
    /// 消息机制传递数据库连接信息
    /// </summary>
    public class EventConnection : PubSubEvent<EventArgs> { }

    /// <summary>
    /// 事件集合
    /// </summary>
    public class EventArgs
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public string strSourceOrTarget { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string strDataBaseName { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string strSqlConnection { get; set; }
    }
}
