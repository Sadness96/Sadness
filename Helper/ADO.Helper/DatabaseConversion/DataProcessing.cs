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
    /// 数据处理类
    /// 创建日期:2016年12月22日
    /// </summary>
    public class DataProcessing
    {
        /// <summary>
        /// 对DataTable进行筛选过滤
        /// </summary>
        /// <param name="dtDataSource">源数据(DataTable)</param>
        /// <param name="sqlFilterConditions">过滤条件(WHERE语句)</param>
        /// <returns>筛选过滤后的DataTable</returns>
        public static DataTable DataTableFiltered(DataTable dtDataSource, string sqlFilterConditions)
        {
            DataTable dtDataResulting = new DataTable();
            try
            {
                if (dtDataSource.Rows.Count < 1 || string.IsNullOrEmpty(sqlFilterConditions)) return null;
                dtDataResulting = dtDataSource.Select(sqlFilterConditions).CopyToDataTable();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return dtDataResulting;
        }

        /// <summary>
        /// 删除DataTable中的空行
        /// 弱引用,可直接修改参数
        /// </summary>
        /// <param name="dtDataSource">源数据(DataTable)</param>
        /// <returns>删除空行后的DataTable</returns>
        public static DataTable RemoveEmpty(DataTable dtDataSource)
        {
            try
            {
                List<DataRow> listRemove = new List<DataRow>();
                for (int i = 0; i < dtDataSource.Rows.Count; i++)
                {
                    bool IsNull = true;
                    for (int j = 0; j < dtDataSource.Columns.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(dtDataSource.Rows[i][j].ToString().Trim()))
                        {
                            IsNull = false;
                        }
                    }
                    if (IsNull)
                    {
                        listRemove.Add(dtDataSource.Rows[i]);
                    }
                }
                for (int i = 0; i < listRemove.Count; i++)
                {
                    dtDataSource.Rows.Remove(listRemove[i]);
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return dtDataSource;
        }
    }
}
