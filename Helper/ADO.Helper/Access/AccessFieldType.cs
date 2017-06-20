using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Helper.Access
{
    /// <summary>
    /// Access字段类型枚举类
    /// 创建日期:2016年12月22日
    /// </summary>
    public class AccessFieldType
    {
        /// <summary>
        /// Access特定的数据类型。
        /// </summary>
        public enum FieldType
        {
            EMPTY = 0,
            SMALLINT = 2,
            INTEGER = 3,
            SINGLE = 4,
            DOUBLE = 5,
            CURRENCY = 6,
            DATE = 7,
            BSTR = 8,
            IDISPATCH = 9,
            ERROR = 10,
            BOOLEAN = 11,
            VARIANT = 12,
            IUNKNOWN = 13,
            DECIMAL = 14,
            TINYINT = 16,
            UNSIGNEDTINYINT = 17,
            UNSIGNEDSMALLINT = 18,
            UNSIGNEDINT = 19,
            BIGINT = 20,
            UNSIGNEDBIGINT = 21,
            FILETIME = 64,
            GUID = 72,
            BINARY = 128,
            CHAR = 129,
            WCHAR = 130,
            NUMERIC = 131,
            USERDEFINED = 132,
            DBDATE = 133,
            DBTIME = 134,
            DBTIMESTAMP = 135,
            CHAPTER = 136,
            PROPVARIANT = 138,
            VARNUMERIC = 139,
            VARCHAR = 200,
            LONGVARWCHAR = 203,
            VARBINARY = 204,
            LONGVARCHAR = 205,
            IMAGE = 206
        }

        /// <summary>
        /// 从字段类型Code获得字段类型
        /// </summary>
        /// <param name="strFieldTypeCode">字段类型Code</param>
        /// <returns>成功返回字段类型,失败返回-1</returns>
        public static string GetFieldType(string strFieldTypeCode)
        {
            switch (strFieldTypeCode)
            {
                case "0": return "EMPTY";
                case "2": return "SMALLINT";
                case "3": return "INTEGER";
                case "4": return "SINGLE";
                case "5": return "DOUBLE";
                case "6": return "CURRENCY";
                case "7": return "DATE";
                case "8": return "BSTR";
                case "9": return "IDISPATCH";
                case "10": return "ERROR";
                case "11": return "BINARY";//BOOLEAN
                case "12": return "VARIANT";
                case "13": return "IUNKNOWN";
                case "14": return "DECIMAL";
                case "16": return "TINYINT";
                case "17": return "UNSIGNEDTINYINT";
                case "18": return "UNSIGNEDSMALLINT";
                case "19": return "UNSIGNEDINT";
                case "20": return "BIGINT";
                case "21": return "UNSIGNEDBIGINT";
                case "64": return "FILETIME";
                case "72": return "GUID";
                case "128": return "BINARY";
                case "129": return "CHAR";
                case "130": return "CHAR";//WCHAR
                case "131": return "NUMERIC";
                case "132": return "USERDEFINED";
                case "133": return "DBDATE";
                case "134": return "DBTIME";
                case "135": return "DBTIMESTAMP";
                case "136": return "CHAPTER";
                case "138": return "PROPVARIANT";
                case "139": return "VARNUMERIC";
                case "200": return "VARCHAR";
                case "203": return "LONGVARWCHAR";
                case "204": return "VARBINARY";
                case "205": return "LONGVARCHAR";
                default: return "CHAR";
            }
        }
    }
}
