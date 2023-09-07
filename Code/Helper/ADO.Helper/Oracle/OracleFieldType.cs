using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Helper.Oracle
{
    /// <summary>
    /// Oracle 字段类型枚举类
    /// 创建日期:2016年12月16日
    /// </summary>
    public class OracleFieldType
    {
        /// <summary>
        /// Oracle 特定的数据类型。
        /// </summary>
        public enum FieldType
        {
            CHAR = 0,
            NCHAR = 1,
            VARCHAR2 = 2,
            VARCHAR = 3,
            NVARCHAR2 = 4,
            CLOB = 5,
            NCLOB = 6,
            LONG = 7,
            NUMBER = 8,
            BINARY_FLOAT = 9,
            BINARY_DOUBLE = 10,
            DATE = 11,
            INTERVAL_DAY_TO_SECOND = 12,
            INTERVAL_YEAR_TO_MONTH = 13,
            TIMESTAMP = 14,
            TIMESTAMP_WITH_TIME_ZONE = 15,
            TIMESTAMP_WITH_LOCAL_TIME_ZONE = 16,
            BLOB = 17,
            BFILE = 18,
            RAW = 19,
            LONG_RAW = 20,
            ROWID = 21,
            CHARACTER = 22,
            CHARACTER_VARYING = 23,
            CHAR_VARYING = 24,
            NATIONAL_CHARACTER = 25,
            NATIONAL_CHAR = 26,
            NATIONAL_CHARACTER_VARYING = 27,
            NATIONAL_CHAR_VARYING = 28,
            NCHAR_VARYING = 29,
            NUMERIC = 30,
            DECIMAL = 31,
            INTEGER = 32,
            INT = 33,
            SMALLINT = 34,
            FLOAT = 35,
            DOUBLE_PRECISION = 36,
            REAL = 37,
            COMPLEX = 38
        }
    }
}
