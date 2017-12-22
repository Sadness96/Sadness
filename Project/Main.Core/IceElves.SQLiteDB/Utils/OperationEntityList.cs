using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADO.Helper.DatabaseConversion;
using ADO.Helper.SQLite;
using ADO.Helper.TXT;

namespace IceElves.SQLiteDB.Utils
{
    public class OperationEntityList
    {
        /// <summary>
        /// 初始化获得连接串
        /// </summary>
        public static string strSQLiteConn = ConfigurationManager.AppSettings["SQLiteConnString"];

        /// <summary>
        /// 获得EntityList类型数据
        /// </summary>
        /// <typeparam name="T">数据模型</typeparam>
        /// <param name="strTableName">表名</param>
        /// <param name="strWhere">过滤条件</param>
        /// <returns>成功返回List<T>,失败返回null</returns>
        public static List<T> GetEntityList<T>(string strTableName, string strWhere) where T : class,new()
        {
            try
            {
                List<T> listT = new List<T>();
                SQLiteHelper sqlHelper = new SQLiteHelper();
                sqlHelper.SQLiteConnectionString(strSQLiteConn);
                sqlHelper.Open();
                if (string.IsNullOrEmpty(strWhere))
                {
                    listT = DataProcessing.ConvertToList<T>(sqlHelper.GetDataTable(string.Format("SELECT * FROM {0}", strTableName)));
                }
                else
                {
                    listT = DataProcessing.ConvertToList<T>(sqlHelper.GetDataTable(string.Format("SELECT * FROM {0} WHERE {1}", strTableName, strWhere)));
                }
                sqlHelper.Close();
                return listT;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }
    }
}
