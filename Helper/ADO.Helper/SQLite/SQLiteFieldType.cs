using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Helper.SQLite
{
    /// <summary>
    /// SQLite字段类型枚举类
    /// 创建日期:2016年12月19日
    /// </summary>
    public class SQLiteFieldType
    {
        /// <summary>
        /// SQLite特定的数据类型。
        /// </summary>
        public enum FieldType
        {
            INT = 0,//亲和类型:INTEGER
            INTEGER = 1,
            TINYINT = 2,
            SMALLINT = 3,
            MEDIUMINT = 4,
            BIGINT = 5,
            UNSIGNED_BIG_INT = 6,
            INT2 = 7,
            INT8 = 8,
            CHARACTER = 9,//亲和类型:TEXT
            VARCHAR = 10,
            VARYING_CHARACTER = 11,
            NCHAR = 12,
            NATIVE_CHARACTER = 13,
            NVARCHAR = 14,
            TEXT = 15,
            CLOB = 16,
            BLOB = 17,//亲和类型:NONE
            NO_DATATYPE_SPECIFIED = 18,
            REAL = 19,//亲和类型:REAL
            DOUBLE = 20,
            DOUBLE_PRECISION = 21,
            FLOAT = 22,
            NUMERIC = 23,//亲和类型:NUMERIC
            DECIMAL = 24,
            BOOLEAN = 25,
            DATE = 26,
            DATETIME = 27
        }
    }
}
