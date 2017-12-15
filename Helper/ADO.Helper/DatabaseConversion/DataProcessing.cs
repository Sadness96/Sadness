using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Reflection;
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

        /// <summary>
        /// DataTable转换为List<T>
        /// </summary>
        /// <typeparam name="T">数据模型</typeparam>
        /// <param name="dtDataSource">源数据(DataTable)</param>
        /// <returns>成功返回List<T>,失败返回null</returns>
        public static List<T> ConvertToList<T>(DataTable dtDataSource) where T : class,new()
        {
            try
            {
                List<T> listT = new List<T>();
                foreach (DataRow drDataSource in dtDataSource.Rows)
                {
                    T t = new T();
                    PropertyInfo[] propertyInfos = t.GetType().GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        string tempName = propertyInfo.Name;
                        if (dtDataSource.Columns.Contains(tempName))
                        {
                            if (!propertyInfo.CanWrite) continue;
                            object value = drDataSource[tempName];
                            if (value != DBNull.Value)
                            {
                                if (propertyInfo.GetMethod.ReturnParameter.ParameterType.Name == "Int32")
                                {
                                    value = Convert.ToInt32(value);
                                }
                                propertyInfo.SetValue(t, value, null);
                            }
                        }
                    }
                    listT.Add(t);
                }
                return listT;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return null;
        }

        /// <summary>
        /// List<T>转换为DataTable
        /// </summary>
        /// <param name="listDataSource">源数据</param>
        /// <returns>成功返回DataTable,失败返回null</returns>
        public static DataTable ConvertDataTable(IList listDataSource)
        {
            try
            {
                DataTable dataTable = new DataTable();
                if (listDataSource.Count > 0)
                {
                    PropertyInfo[] propertyInfos = listDataSource[0].GetType().GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        dataTable.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
                    }
                    foreach (var vDataSource in listDataSource)
                    {
                        ArrayList arrayList = new ArrayList();
                        foreach (PropertyInfo propertyInfo in propertyInfos)
                        {
                            arrayList.Add(propertyInfo.GetValue(vDataSource, null));
                        }
                        dataTable.LoadDataRow(arrayList.ToArray(), true);
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
            }
            return null;
        }
    }
}
