using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Helper.MySql
{
    /// <summary>
    /// MySql字段类型枚举类
    /// 创建日期:2016年12月19日
    /// </summary>
    public class MySqlFieldType
    {
        /// <summary>
        /// MySql特定的数据类型。
        /// </summary>
        public enum FieldType
        {
            TINYINT = 0,
            SMALLINT = 1,
            MEDIUMINT = 2,
            INT = 3,
            INTEGER = 4,
            BIGINT = 5,
            BIT = 6,
            REAL = 7,
            DOUBLE = 8,
            FLOAT = 9,
            DECIMAL = 10,
            NUMERIC = 11,
            CHAR = 12,
            VARCHAR = 13,
            DATE = 14,
            TIME = 15,
            YEAR = 16,
            TIMESTAMP = 17,
            DATETIME = 18,
            TINYBLOB = 19,
            BLOB = 20,
            MEDIUMBLOB = 21,
            LONGBLOB = 22,
            TINYTEXT = 23,
            TEXT = 24,
            MEDIUMTEXT = 25,
            LONGTEXT = 26,
            ENUM = 27,
            SET = 28,
            BINARY = 29,
            VARBINARY = 30,
            POINT = 31,
            LINESTRING = 32,
            POLYGON = 33,
            GEOMETRY = 34,
            MULTIPOINT = 35,
            MULTILINESTRING = 36,
            MULTIPOLYGON = 37,
            GEOMETRYCOLLECTION = 38
        }
    }
}
