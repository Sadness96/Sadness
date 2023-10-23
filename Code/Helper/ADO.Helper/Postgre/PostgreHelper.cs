using ADO.Helper.TXT;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.Helper.Postgre
{
    /// <summary>
    /// PostgreSQL 数据库帮助类
    /// 创建日期:2023年10月23日
    /// 对比其他数据库帮助类，仅保留必要的事务与执行非查询SQL与查询SQL
    /// </summary>
    public class PostgreHelper
    {
        /// <summary>
        /// PostgreSQL 连接字符串
        /// </summary>
        private static string strPostgreConnection { get; set; }

        /// <summary>
        /// 表示一个到 PostgreSQL 数据库的打开的连接
        /// </summary>
        NpgsqlConnection Connection = null;

        /// <summary>
        /// 表示要在 PostgreSQL 数据库中处理的 Transact-SQL 事务
        /// </summary>
        NpgsqlTransaction Transaction = null;

        /// <summary>
        /// 直接传给帮助类 PostgreSQL 连接字符串
        /// </summary>
        /// <param name="PostgreConnection">PostgreSQL 连接字符串</param>
        public void PostgreConnectionString(string PostgreConnection)
        {
            strPostgreConnection = PostgreConnection;
        }

        /// <summary>
        /// 传给帮助类 PostgreSQL 连接字符串需要的信息(端口号默认5432)
        /// </summary>
        /// <param name="host">IP</param>
        /// <param name="database">数据库名</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void PostgreConnectionString(string host, string database, string username, string password)
        {
            strPostgreConnection = $"Host={host};Port=5432;Database={database};Username={username};Password={password}";
        }

        /// <summary>
        /// 传给帮助类 PostgreSQL 连接字符串需要的信息
        /// </summary>
        /// <param name="host">IP</param>
        /// <param name="port">端口号</param>
        /// <param name="database">数据库名</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void PostgreConnectionString(string host, int port, string database, string username, string password)
        {
            strPostgreConnection = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
        }

        /// <summary>
        /// 得到数据库连接字符串
        /// </summary>
        /// <returns>数据库连接字符串</returns>
        public string GetConnectionString()
        {
            return strPostgreConnection;
        }

        /// <summary>
        /// 使用所指定的属性设置打开数据库连接
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public bool Open()
        {
            try
            {
                Connection = new NpgsqlConnection(strPostgreConnection);
                Connection.Open();
                return Connection.State == ConnectionState.Open;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 关闭与数据库的连接
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public bool Close()
        {
            try
            {
                Connection.Close();
                return Connection.State == ConnectionState.Closed;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public bool BeginTransaction()
        {
            try
            {
                Transaction = Connection.BeginTransaction();
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public bool CommitTransaction()
        {
            try
            {
                if (Transaction != null)
                {
                    Transaction.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public bool RollbackTransaction()
        {
            try
            {
                if (Transaction != null)
                {
                    Transaction.Rollback();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="sqlExecuteNonQuery">非查询SQL</param>
        /// <returns>成功返回受影响的行数,失败或非数据操作成功返回-1</returns>
        public int ExecuteNonQuery(string sqlExecuteNonQuery)
        {
            int intNumberIfAffectedRows = 0;
            try
            {
                if (string.IsNullOrEmpty(sqlExecuteNonQuery))
                {
                    return -1;
                }
                NpgsqlCommand Command = new NpgsqlCommand(sqlExecuteNonQuery, Connection);
                if (Transaction != null)
                {
                    Command.Transaction = Transaction;
                }
                intNumberIfAffectedRows = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
            return intNumberIfAffectedRows;
        }

        /// <summary>
        /// 执行非查询SQL语句(批量执行)
        /// </summary>
        /// <param name="listExecuteNonQuery">SQL语句集合</param>
        /// <returns>成功返回执行条数,失败返回-1</returns>
        public int ExecuteNonQuery(List<string> listExecuteNonQuery)
        {
            int intNumberIfAffectedRows = 0;
            try
            {
                if (listExecuteNonQuery.Count < 1)
                {
                    return -1;
                }
                foreach (string sqlExecuteNonQuery in listExecuteNonQuery)
                {
                    NpgsqlCommand Command = new NpgsqlCommand(sqlExecuteNonQuery, Connection);
                    if (Transaction != null)
                    {
                        Command.Transaction = Transaction;
                    }
                    if (Command.ExecuteNonQuery() > 0)
                    {
                        intNumberIfAffectedRows++;
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
            return intNumberIfAffectedRows;
        }

        /// <summary>
        /// 执行查询SQL语句(存到DataSet)
        /// </summary>
        /// <param name="sqlSelect">查询SQL</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string sqlSelect)
        {
            DataSet dsSelect = new DataSet();
            try
            {
                if (string.IsNullOrEmpty(sqlSelect)) return null;
                NpgsqlDataAdapter DataAdapter = new NpgsqlDataAdapter(sqlSelect, Connection);
                if (Transaction != null)
                {
                    DataAdapter.SelectCommand.Transaction = Transaction;
                }
                DataAdapter.Fill(dsSelect);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return dsSelect;
        }

        /// <summary>
        /// 执行查询SQL语句(存到DataSet)(多表)
        /// </summary>
        /// <param name="listSelect">查询SQL集合</param>
        /// <returns>DataSet(多表)</returns>
        public DataSet GetDataSet(List<string> listSelect)
        {
            DataSet dsSelect = new DataSet();
            string strTableName = string.Format("Table_");
            int intTableNumber = 0;
            try
            {
                if (listSelect.Count < 1) return null;
                foreach (string sqlSelect in listSelect)
                {
                    NpgsqlDataAdapter DataAdapter = new NpgsqlDataAdapter(sqlSelect, Connection);
                    if (Transaction != null)
                    {
                        DataAdapter.SelectCommand.Transaction = Transaction;
                    }
                    DataAdapter.Fill(dsSelect, strTableName + intTableNumber++);
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return dsSelect;
        }

        /// <summary>
        /// 执行查询SQL语句(存到DataTable)
        /// </summary>
        /// <param name="sqlSelect">查询SQL</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string sqlSelect)
        {
            DataTable dtSelect = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(sqlSelect)) return null;
                NpgsqlDataAdapter DataAdapter = new NpgsqlDataAdapter(sqlSelect, Connection);
                if (Transaction != null)
                {
                    DataAdapter.SelectCommand.Transaction = Transaction;
                }
                DataAdapter.Fill(dtSelect);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return dtSelect;
        }
    }
}
