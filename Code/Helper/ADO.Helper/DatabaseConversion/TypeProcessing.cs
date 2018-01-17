using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ADO.Helper.TXT;

namespace ADO.Helper.DatabaseConversion
{
    /// <summary>
    /// 数据类型处理类
    /// 创建日期:2016年12月22日
    /// </summary>
    public class TypeProcessing
    {
        /// <summary>
        /// 数据库枚举
        /// </summary>
        public enum DataBase
        {
            SqlServer = 0,
            Oracle = 1,
            MySql = 2,
            Access = 3,
            SQLite = 4
        }

        /// <summary>
        /// 字段类型修改
        /// 根据不同的数据库修改为改数据库可用的字段类型
        /// </summary>
        /// <param name="dicDataSource">源字段名和字段类型</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>修改后字段名和字段类型</returns>
        public static Dictionary<string, string> FieldTypeProcessing(Dictionary<string, string> dicDataSource, DataBase dbType)
        {
            Dictionary<string, string> dicFieldTypeProcessing = new Dictionary<string, string>();
            try
            {
                if (dicDataSource.Count < 1 || !Enum.IsDefined(typeof(DataBase), dbType)) return null;
                if (dbType == DataBase.SqlServer)
                {
                    foreach (var varDataSource in dicDataSource)
                    {
                        string strKey = varDataSource.Key;
                        string strValue = varDataSource.Value.ToUpper();
                        string strFieldType = "";
                        string strMaxLength = "";
                        SplitTypeAndLength(strValue, out strFieldType, out strMaxLength);
                        string strFieldValue = FieldTypeModify_SqlServer(strFieldType, strMaxLength);
                        dicFieldTypeProcessing.Add(strKey, strFieldValue);
                    }
                }
                else if (dbType == DataBase.Oracle)
                {
                    foreach (var varDataSource in dicDataSource)
                    {
                        string strKey = varDataSource.Key;
                        string strValue = varDataSource.Value.ToUpper();
                        string strFieldType = "";
                        string strMaxLength = "";
                        SplitTypeAndLength(strValue, out strFieldType, out strMaxLength);
                        string strFieldValue = FieldTypeModify_Oracle(strFieldType, strMaxLength);
                        dicFieldTypeProcessing.Add(strKey, strFieldValue);
                    }
                }
                else if (dbType == DataBase.MySql)
                {
                    foreach (var varDataSource in dicDataSource)
                    {
                        string strKey = varDataSource.Key;
                        string strValue = varDataSource.Value.ToUpper();
                        string strFieldType = "";
                        string strMaxLength = "";
                        SplitTypeAndLength(strValue, out strFieldType, out strMaxLength);
                        string strFieldValue = FieldTypeModify_MySql(strFieldType, strMaxLength);
                        dicFieldTypeProcessing.Add(strKey, strFieldValue);
                    }
                }
                else if (dbType == DataBase.Access)
                {
                    foreach (var varDataSource in dicDataSource)
                    {
                        string strKey = varDataSource.Key;
                        string strValue = varDataSource.Value.ToUpper();
                        string strFieldType = "";
                        string strMaxLength = "";
                        SplitTypeAndLength(strValue, out strFieldType, out strMaxLength);
                        string strFieldValue = FieldTypeModify_Access(strFieldType, strMaxLength);
                        dicFieldTypeProcessing.Add(strKey, strFieldValue);
                    }
                }
                else if (dbType == DataBase.SQLite)
                {
                    foreach (var varDataSource in dicDataSource)
                    {
                        string strKey = varDataSource.Key;
                        string strValue = varDataSource.Value.ToUpper();
                        string strFieldType = "";
                        string strMaxLength = "";
                        SplitTypeAndLength(strValue, out strFieldType, out strMaxLength);
                        string strFieldValue = FieldTypeModify_SQLite(strFieldType, strMaxLength);
                        dicFieldTypeProcessing.Add(strKey, strFieldValue);
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return dicFieldTypeProcessing;
        }

        /// <summary>
        /// 拆分字段类型的类型和长度,或是主外键是否为空
        /// </summary>
        /// <param name="sqlTypeAndLength">源字符串</param>
        /// <param name="strFieldType">拆分后的字段类型</param>
        /// <param name="strMaxLength">拆分后的字段长度或是主外键是否为空</param>
        private static void SplitTypeAndLength(string sqlTypeAndLength, out string strFieldType, out string strMaxLength)
        {
            strFieldType = "";
            strMaxLength = "";
            if (string.IsNullOrEmpty(sqlTypeAndLength)) return;
            sqlTypeAndLength = sqlTypeAndLength.ToUpper();
            if (sqlTypeAndLength.IndexOf("(") > -1)
            {
                int intIndexOf = sqlTypeAndLength.IndexOf("(");
                strFieldType = sqlTypeAndLength.Substring(0, intIndexOf);
                strMaxLength = sqlTypeAndLength.Substring(intIndexOf, sqlTypeAndLength.Length - intIndexOf);
            }
            else if (sqlTypeAndLength.IndexOf(" ") > -1)
            {
                int intIndexOf = sqlTypeAndLength.IndexOf(" ");
                strFieldType = sqlTypeAndLength.Substring(0, intIndexOf);
                strMaxLength = sqlTypeAndLength.Substring(intIndexOf, sqlTypeAndLength.Length - intIndexOf);
            }
            else
            {
                strFieldType = sqlTypeAndLength;
            }
        }

        /// <summary>
        /// 字段类型修改_SqlServer
        /// </summary>
        /// <param name="strFieldType">源字段类型</param>
        /// <param name="strMaxLength">源字段长度</param>
        /// <returns>修改后字段类型</returns>
        private static string FieldTypeModify_SqlServer(string strFieldType, string strMaxLength)
        {
            if (strFieldType.IndexOf("INT") > -1 || strFieldType.IndexOf("FLOAT") > -1 || strFieldType.IndexOf("DOUBLE") > -1 || strFieldType.IndexOf("NUMBER") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("REAL") > -1 || strFieldType.IndexOf("CURRENCY") > -1 || strFieldType.IndexOf("MONEY") > -1)
            {
                strFieldType = "NUMERIC";
            }
            if (strFieldType.IndexOf("CHAR") > -1 || strFieldType.IndexOf("TEXT") > -1 || strFieldType == "LONG")
            {
                strFieldType = "NVARCHAR";
                if (string.IsNullOrEmpty(strMaxLength))
                {
                    strMaxLength = "(255)";
                }
            }
            if (strFieldType.IndexOf("BINARY") > -1 || strFieldType.IndexOf("IMAGE") > -1 || strFieldType.IndexOf("RAW") > -1 || strFieldType.IndexOf("BLOB") > -1 || strFieldType.IndexOf("CLOB") > -1 || strFieldType.IndexOf("NROWID") > -1 || strFieldType.IndexOf("BIT") > -1)
            {
                strFieldType = "VARBINARY";
                if (string.IsNullOrEmpty(strMaxLength))
                {
                    strMaxLength = "(MAX)";
                }
            }
            if (strFieldType.IndexOf("DATE") > -1)
            {
                strFieldType = "DATETIME";
            }
            if (string.IsNullOrEmpty(strFieldType))
            {
                strFieldType = "VARCHAR";
                strMaxLength = "(255)";
            }
            return string.Format("{0}{1}", strFieldType, strMaxLength);
        }

        /// <summary>
        /// 字段类型修改_Oracle
        /// </summary>
        /// <param name="strFieldType">源字段类型</param>
        /// <param name="strMaxLength">源字段长度</param>
        /// <returns>修改后字段类型</returns>
        private static string FieldTypeModify_Oracle(string strFieldType, string strMaxLength)
        {
            if (strFieldType.IndexOf("INT") > -1 || strFieldType.IndexOf("FLOAT") > -1 || strFieldType.IndexOf("DOUBLE") > -1 || strFieldType.IndexOf("NUMBER") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("REAL") > -1 || strFieldType.IndexOf("CURRENCY") > -1 || strFieldType.IndexOf("MONEY") > -1)
            {
                strFieldType = "NUMBER";
            }
            if (strFieldType.IndexOf("CHAR") > -1 || strFieldType.IndexOf("TEXT") > -1 || strFieldType == "LONG")
            {
                strFieldType = "VARCHAR2";
                if (string.IsNullOrEmpty(strMaxLength))
                {
                    strMaxLength = "(255)";
                }
            }
            if (strFieldType.IndexOf("BINARY") > -1 || strFieldType.IndexOf("IMAGE") > -1 || strFieldType.IndexOf("RAW") > -1 || strFieldType.IndexOf("BLOB") > -1 || strFieldType.IndexOf("CLOB") > -1 || strFieldType.IndexOf("NROWID") > -1 || strFieldType.IndexOf("BIT") > -1)
            {
                strFieldType = "BLOB";
            }
            if (strFieldType.IndexOf("DATE") > -1)
            {
                strFieldType = "DATE";
            }
            if (string.IsNullOrEmpty(strFieldType))
            {
                strFieldType = "VARCHAR";
                strMaxLength = "(255)";
            }
            return string.Format("{0}{1}", strFieldType, strMaxLength);
        }

        /// <summary>
        /// 字段类型修改_MySql
        /// </summary>
        /// <param name="strFieldType">源字段类型</param>
        /// <param name="strMaxLength">源字段长度</param>
        /// <returns>修改后字段类型</returns>
        private static string FieldTypeModify_MySql(string strFieldType, string strMaxLength)
        {
            if (strFieldType.IndexOf("INT") > -1)
            {
                strFieldType = "INT";
                strMaxLength = string.Empty;
            }
            if (strFieldType.IndexOf("FLOAT") > -1 || strFieldType.IndexOf("DOUBLE") > -1 || strFieldType.IndexOf("NUMBER") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("REAL") > -1 || strFieldType.IndexOf("CURRENCY") > -1 || strFieldType.IndexOf("MONEY") > -1)
            {
                strFieldType = "DOUBLE";
                if (string.IsNullOrEmpty(strMaxLength))
                {
                    strMaxLength = "(18,0)";
                }
            }
            if (strFieldType.IndexOf("CHAR") > -1 || strFieldType.IndexOf("TEXT") > -1 || strFieldType == "LONG")
            {
                strFieldType = "VARCHAR";
                if (string.IsNullOrEmpty(strMaxLength))
                {
                    strMaxLength = "(255)";
                }
            }
            if (strFieldType.IndexOf("BINARY") > -1 || strFieldType.IndexOf("IMAGE") > -1 || strFieldType.IndexOf("RAW") > -1 || strFieldType.IndexOf("BLOB") > -1 || strFieldType.IndexOf("CLOB") > -1 || strFieldType.IndexOf("NROWID") > -1 || strFieldType.IndexOf("BIT") > -1)
            {
                strFieldType = "LONGBLOB";
            }
            if (strFieldType.IndexOf("DATE") > -1)
            {
                strFieldType = "DATETIME";
            }
            if (string.IsNullOrEmpty(strFieldType))
            {
                strFieldType = "VARCHAR";
                strMaxLength = "(255)";
            }
            return string.Format("{0}{1}", strFieldType, strMaxLength);
        }

        /// <summary>
        /// 字段类型修改_Access
        /// </summary>
        /// <param name="strFieldType">源字段类型</param>
        /// <param name="strMaxLength">源字段长度</param>
        /// <returns>修改后字段类型</returns>
        private static string FieldTypeModify_Access(string strFieldType, string strMaxLength)
        {
            if (strFieldType.IndexOf("INT") > -1 || strFieldType.IndexOf("FLOAT") > -1 || strFieldType.IndexOf("DOUBLE") > -1 || strFieldType.IndexOf("NUMBER") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("REAL") > -1 || strFieldType.IndexOf("CURRENCY") > -1 || strFieldType.IndexOf("MONEY") > -1)
            {
                strFieldType = "INTEGER";
                strMaxLength = string.Empty;
            }
            if (strFieldType.IndexOf("CHAR") > -1 || strFieldType.IndexOf("TEXT") > -1 || strFieldType == "LONG")
            {
                strFieldType = "CHAR";
                if (string.IsNullOrEmpty(strMaxLength))
                {
                    strMaxLength = "(255)";
                }
            }
            if (strFieldType.IndexOf("BINARY") > -1 || strFieldType.IndexOf("IMAGE") > -1 || strFieldType.IndexOf("RAW") > -1 || strFieldType.IndexOf("BLOB") > -1 || strFieldType.IndexOf("CLOB") > -1 || strFieldType.IndexOf("NROWID") > -1 || strFieldType.IndexOf("BIT") > -1)
            {
                strFieldType = "IMAGE";
                strMaxLength = string.Empty;
            }
            if (strFieldType.IndexOf("DATE") > -1)
            {
                strFieldType = "DATE";
            }
            if (string.IsNullOrEmpty(strFieldType))
            {
                strFieldType = "VARCHAR";
                strMaxLength = "(255)";
            }
            return string.Format("{0}{1}", strFieldType, strMaxLength);
        }

        /// <summary>
        /// 字段类型修改_SQLite
        /// </summary>
        /// <param name="strFieldType">源字段类型</param>
        /// <param name="strMaxLength">源字段长度</param>
        /// <returns>修改后字段类型</returns>
        private static string FieldTypeModify_SQLite(string strFieldType, string strMaxLength)
        {
            if (strFieldType.IndexOf("INT") > -1 || strFieldType.IndexOf("FLOAT") > -1 || strFieldType.IndexOf("DOUBLE") > -1 || strFieldType.IndexOf("NUMBER") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("DECIMAL") > -1 || strFieldType.IndexOf("REAL") > -1 || strFieldType.IndexOf("CURRENCY") > -1 || strFieldType.IndexOf("MONEY") > -1)
            {
                strFieldType = "INTEGER";
            }
            if (strFieldType.IndexOf("CHAR") > -1 || strFieldType.IndexOf("TEXT") > -1 || strFieldType == "LONG")
            {
                strFieldType = "TEXT";
                if (string.IsNullOrEmpty(strMaxLength))
                {
                    strMaxLength = "(255)";
                }
            }
            if (strFieldType.IndexOf("BINARY") > -1 || strFieldType.IndexOf("IMAGE") > -1 || strFieldType.IndexOf("RAW") > -1 || strFieldType.IndexOf("BLOB") > -1 || strFieldType.IndexOf("CLOB") > -1 || strFieldType.IndexOf("NROWID") > -1 || strFieldType.IndexOf("BIT") > -1)
            {
                strFieldType = "BLOB";
            }
            if (strFieldType.IndexOf("DATE") > -1)
            {
                strFieldType = "DATETIME";
            }
            if (string.IsNullOrEmpty(strFieldType))
            {
                strFieldType = "VARCHAR";
                strMaxLength = "(255)";
            }
            return string.Format("{0}{1}", strFieldType, strMaxLength);
        }
    }
}
