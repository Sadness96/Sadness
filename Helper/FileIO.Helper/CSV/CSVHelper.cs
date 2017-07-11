using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileIO.Helper.TXT;

namespace FileIO.Helper.CSV
{
    /// <summary>
    /// CSV逗号分隔值帮助类
    /// 创建日期:2017年6月21日
    /// </summary>
    public class CSVHelper
    {
        /// <summary>
        /// DataTable转换为CSV
        /// </summary>
        /// <param name="strSource">CSV文件路径</param>
        /// <param name="dtSourceData">DataTable数据</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DataTableConversionCSV(string strSource, DataTable dtSourceData)
        {
            try
            {
                if (string.IsNullOrEmpty(strSource) || dtSourceData.Rows.Count < 1)
                {
                    return false;
                }
                FileStream fileStream = new FileStream(Path.ChangeExtension(strSource, "csv"), FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //记录当前读取到的一行数据
                string strRowOfData = string.Empty;
                //循环保存列名
                for (int iColumnsName = 0; iColumnsName < dtSourceData.Columns.Count; iColumnsName++)
                {
                    strRowOfData += string.Format("{0}{1}{0}", "\"", dtSourceData.Columns[iColumnsName].ColumnName.ToString());
                    if (iColumnsName < dtSourceData.Columns.Count - 1)
                    {
                        strRowOfData += ",";
                    }
                }
                streamWriter.WriteLine(strRowOfData);
                //循环保存数据
                for (int iRow = 0; iRow < dtSourceData.Rows.Count; iRow++)
                {
                    strRowOfData = string.Empty;
                    for (int iColumns = 0; iColumns < dtSourceData.Columns.Count; iColumns++)
                    {
                        strRowOfData += string.Format("{0}{1}{0}", "\"", dtSourceData.Rows[iRow][iColumns].ToString());
                        if (iColumns < dtSourceData.Columns.Count - 1)
                        {
                            strRowOfData += ",";
                        }
                    }
                    streamWriter.WriteLine(strRowOfData);
                }
                streamWriter.Close();
                fileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// CSV转换为DataTable(默认 UTF8 编码)
        /// </summary>
        /// <param name="strSource">CSV文件路径</param>
        /// <returns>成功返回CSV的DataTable,失败返回NULL</returns>
        public static DataTable CSVConversionDataTable(string strSource)
        {
            try
            {
                if (string.IsNullOrEmpty(strSource) || !File.Exists(strSource))
                {
                    return null;
                }
                DataTable dtTargetData = new DataTable();
                //初始化 System.IO.FileStream 类的新实例
                FileStream fileStreamOpen = new FileStream(strSource, FileMode.Open, FileAccess.Read);
                //从当前流中读取一行字符并将数据作为字符串返回
                StreamReader streamReader = new StreamReader(fileStreamOpen, Encoding.UTF8);
                //记录当前读取到的一行数据
                string strRowOfData;
                //记录当前是否为标题行
                bool boolIsFirst = true;
                //循环获得CSV文件数据
                while ((strRowOfData = streamReader.ReadLine()) != null)
                {
                    //从当前 System.String 对象中移除所有前导和尾随空白字符
                    strRowOfData.Trim();
                    //替换两遍连续两个 ,, 为 ,"",(希望数据里不存在两个逗号相连的情况)
                    strRowOfData = strRowOfData.Replace(",,", ",\"\",");
                    strRowOfData = strRowOfData.Replace(",,", ",\"\",");
                    //根据CSV规则分割字符串
                    string strRegexCSV = string.Format("[^\",]+|\"(?:[^\"]|\"\")*\"");
                    Regex regexCSV = new Regex(strRegexCSV);
                    MatchCollection matchCollection = regexCSV.Matches(strRowOfData);
                    //判断是否为标题行
                    if (boolIsFirst)
                    {
                        foreach (Match mColumnValue in matchCollection)
                        {
                            dtTargetData.Columns.Add(InterceptionQuotes(mColumnValue.Value));
                        }
                        boolIsFirst = false;
                    }
                    else
                    {
                        DataRow drTargetData = dtTargetData.NewRow();
                        for (int iColumn = 0; iColumn < dtTargetData.Columns.Count && iColumn < matchCollection.Count; iColumn++)
                        {
                            drTargetData[iColumn] = InterceptionQuotes(matchCollection[iColumn].Value);
                        }
                        dtTargetData.Rows.Add(drTargetData);
                    }
                }
                streamReader.Close();
                fileStreamOpen.Close();
                return dtTargetData;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// CSV转换为DataTable
        /// </summary>
        /// <param name="strSource">CSV文件路径</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <returns>成功返回CSV的DataTable,失败返回NULL</returns>
        public static DataTable CSVConversionDataTable(string strSource, Encoding encoding)
        {
            try
            {
                if (string.IsNullOrEmpty(strSource) || !File.Exists(strSource))
                {
                    return null;
                }
                DataTable dtTargetData = new DataTable();
                //初始化 System.IO.FileStream 类的新实例
                FileStream fileStreamOpen = new FileStream(strSource, FileMode.Open, FileAccess.Read);
                //从当前流中读取一行字符并将数据作为字符串返回
                StreamReader streamReader = new StreamReader(fileStreamOpen, encoding);
                //记录当前读取到的一行数据
                string strRowOfData;
                //记录当前是否为标题行
                bool boolIsFirst = true;
                //循环获得CSV文件数据
                while ((strRowOfData = streamReader.ReadLine()) != null)
                {
                    //从当前 System.String 对象中移除所有前导和尾随空白字符
                    strRowOfData.Trim();
                    //替换两遍连续两个 ,, 为 ,"",(希望数据里不存在两个逗号相连的情况)
                    strRowOfData = strRowOfData.Replace(",,", ",\"\",");
                    strRowOfData = strRowOfData.Replace(",,", ",\"\",");
                    //根据CSV规则分割字符串
                    string strRegexCSV = string.Format("[^\",]+|\"(?:[^\"]|\"\")*\"");
                    Regex regexCSV = new Regex(strRegexCSV);
                    MatchCollection matchCollection = regexCSV.Matches(strRowOfData);
                    //判断是否为标题行
                    if (boolIsFirst)
                    {
                        foreach (Match mColumnValue in matchCollection)
                        {
                            dtTargetData.Columns.Add(InterceptionQuotes(mColumnValue.Value));
                        }
                        boolIsFirst = false;
                    }
                    else
                    {
                        DataRow drTargetData = dtTargetData.NewRow();
                        for (int iColumn = 0; iColumn < dtTargetData.Columns.Count && iColumn < matchCollection.Count; iColumn++)
                        {
                            drTargetData[iColumn] = InterceptionQuotes(matchCollection[iColumn].Value);
                        }
                        dtTargetData.Rows.Add(drTargetData);
                    }
                }
                streamReader.Close();
                fileStreamOpen.Close();
                return dtTargetData;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 截取字符串前后双引号
        /// </summary>
        /// <param name="strSource">源字符串</param>
        /// <returns>截取后字符串</returns>
        private static string InterceptionQuotes(string strSource)
        {
            try
            {
                if (strSource[0] == '\"' && strSource[strSource.Length - 1] == '\"')
                {
                    return strSource.Substring(1, strSource.Length - 2);
                }
                else
                {
                    return strSource;
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }
    }
}
