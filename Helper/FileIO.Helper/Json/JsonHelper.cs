using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using FileIO.Helper.TXT;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FileIO.Helper.Json
{
    /// <summary>
    /// Json文本帮助类
    /// 创建日期:2017年5月22日
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="value">要序列化的对象</param>
        /// <returns>对象的JSON字符串表示形式</returns>
        public static string SerializeObject(object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">反序列化对象的类型</typeparam>
        /// <param name="value">反序列化的JSON</param>
        /// <returns>反序列化对象的JSON字符串</returns>
        public static T DeserializeObject<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return default(T);
            }
        }

        /// <summary>
        /// DataTable转换为Json
        /// </summary>
        /// <param name="dtSourceData">DataTable数据</param>
        /// <param name="dicFieldNameType">数据库表字段名和字段类型</param>
        /// <returns>对象的JSON字符串表示形式</returns>
        public static string DataTableConversionJson(DataTable dtSourceData, Dictionary<string, string> dicFieldNameType)
        {
            try
            {
                if (dtSourceData == null || dtSourceData.Rows.Count < 1)
                {
                    //dtSourceData是基础数据,名称和字段类型可以为空
                    return string.Empty;
                }
                DataTableModel dtmValue = new DataTableModel();
                dtmValue.TableName = dtSourceData.TableName;
                dtmValue.dicFieldNameType = dicFieldNameType;
                dtmValue.dtSourceData = dtSourceData;
                return JsonConvert.SerializeObject(dtmValue);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// DataSet转换为Json
        /// </summary>
        /// <param name="dsSourceData">DataTable数据</param>
        /// <param name="diclistFieldNameType">数据库表字段名和字段类型</param>
        /// <returns>对象的JSON字符串表示形式</returns>
        public static string DataSetConversionJson(DataSet dsSourceData, Dictionary<string, Dictionary<string, string>> diclistFieldNameType)
        {
            try
            {
                if (dsSourceData == null || dsSourceData.Tables.Count < 1)
                {
                    //dtSourceData是基础数据,名称和字段类型可以为空
                    return string.Empty;
                }
                List<DataTableModel> listDataTableModel = new List<DataTableModel>();
                foreach (DataTable dtSourceData in dsSourceData.Tables)
                {
                    DataTableModel dtmValue = new DataTableModel();
                    dtmValue.TableName = dtSourceData.TableName;
                    dtmValue.dicFieldNameType = diclistFieldNameType[dtSourceData.TableName];
                    dtmValue.dtSourceData = dtSourceData;
                    listDataTableModel.Add(dtmValue);
                }
                return JsonConvert.SerializeObject(listDataTableModel);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// Json转换为DataTable
        /// </summary>
        /// <param name="strJson">反序列化对象</param>
        /// <param name="dicFieldNameType">返回Json的数据库表字段名和字段类型</param>
        /// <returns>成功返回Json的DataTable,失败返回NULL</returns>
        public static DataTable JsonConversionDataTable(string strJson, out Dictionary<string, string> dicFieldNameType)
        {
            dicFieldNameType = new Dictionary<string, string>();
            try
            {
                if (string.IsNullOrEmpty(strJson))
                {
                    return null;
                }
                DataTableModel dtmValue = JsonConvert.DeserializeObject<DataTableModel>(strJson);
                dicFieldNameType = dtmValue.dicFieldNameType;
                DataTable dtSourceData = dtmValue.dtSourceData;
                dtSourceData.TableName = dtmValue.TableName;
                return dtSourceData;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Json转换为DataSet
        /// </summary>
        /// <param name="strJson">反序列化对象</param>
        /// <param name="diclistFieldNameType">返回Json的数据库表字段名和字段类型</param>
        /// <returns>成功返回Json的DataSet,失败返回NULL</returns>
        public static DataSet JsonConversionDataSet(string strJson, out Dictionary<string, Dictionary<string, string>> diclistFieldNameType)
        {
            diclistFieldNameType = new Dictionary<string, Dictionary<string, string>>();
            try
            {
                if (string.IsNullOrEmpty(strJson))
                {
                    return null;
                }
                List<DataTableModel> listDtmValue = JsonConvert.DeserializeObject<List<DataTableModel>>(strJson);
                DataSet dsSourceData = new DataSet();
                foreach (DataTableModel dtmValue in listDtmValue)
                {
                    DataTable dtSourceData = dtmValue.dtSourceData;
                    dtSourceData.TableName = dtmValue.TableName;
                    dsSourceData.Tables.Add(dtSourceData);
                    diclistFieldNameType.Add(dtmValue.TableName, dtmValue.dicFieldNameType);
                }
                return dsSourceData;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }
    }
}
