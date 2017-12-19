using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SQLite;
using ADO.Helper.TXT;

namespace IceElves.SQLiteDB.Utils
{
    /// <summary>
    /// SQLite操作二进制方法
    /// </summary>
    public class OperationByte
    {
        /// <summary>
        /// 初始化获得连接串
        /// </summary>
        public static string strSQLiteConn = ConfigurationManager.AppSettings["SQLiteConnString"];

        /// <summary>
        /// 保存二进制文件到数据库中
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strSaveField">保存列名</param>
        /// <param name="strSaveFilePath">文件路径</param>
        /// <param name="strWhere">查询Where条件</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool SaveByteArray(string strTableName, string strSaveField, string strSaveFilePath, string strWhere)
        {
            try
            {
                //读取文件为Byte[]
                FileStream fileStream = new FileStream(strSaveFilePath, FileMode.Open);
                byte[] byteFile = new byte[(int)fileStream.Length];
                fileStream.Read(byteFile, 0, byteFile.Length);
                fileStream.Close();
                //保存到数据库
                SQLiteConnection SQLConnection = new SQLiteConnection(strSQLiteConn);
                SQLConnection.Open();
                string strSQL = string.Format("UPDATE {0} SET {1} = @file where {2}", strTableName, strSaveField, strWhere);
                SQLiteCommand SQLCommand = new SQLiteCommand(strSQL, SQLConnection);
                SQLCommand.Parameters.Add("@file", DbType.Binary, byteFile.Length);
                SQLCommand.Parameters["@file"].Value = byteFile;
                SQLCommand.ExecuteNonQuery();
                SQLConnection.Close();
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获得数据库中二进制文件
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strByteFieldName">二进制列名</param>
        /// <param name="strWhere">查询Where条件</param>
        /// <returns>成功返回指定位置二进制文件,失败返回NULL</returns>
        public static byte[] GetByteArray(string strTableName, string strByteFieldName, string strWhere)
        {
            try
            {
                SQLiteConnection SQLConnection = new SQLiteConnection(strSQLiteConn);
                string sqlSelect = string.Format("SELECT {1} FROM {0} WHERE {2}", strTableName, strByteFieldName, strWhere);
                SQLiteDataAdapter SQLDataAdapter = new SQLiteDataAdapter(sqlSelect, SQLConnection);
                DataTable dtImage = new DataTable();
                SQLDataAdapter.Fill(dtImage);
                if (dtImage != null && dtImage.Rows.Count >= 1)
                {
                    return (byte[])dtImage.Rows[0][0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }
    }
}
