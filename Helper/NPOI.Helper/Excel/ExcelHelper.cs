using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using NPOI.Helper.TXT;
using NPOI.SS.Util;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace NPOI.Helper.Excel
{
    /// <summary>
    /// Excel帮助类
    /// 创建日期:2017年5月27日
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 创建Excel(Office2003)
        /// </summary>
        /// <param name="strDataSourcePath">新建Excel的路径.xls</param>
        /// <param name="strSheetName">Sheet名称,如果为空则创建三个默认Sheet页</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateExcel_Office2003(string strDataSourcePath, string strSheetName)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath))
                {
                    return false;
                }
                HSSFWorkbook WorkBook2003 = new HSSFWorkbook();
                if (string.IsNullOrEmpty(strSheetName))
                {
                    WorkBook2003.CreateSheet("Sheet1");
                    WorkBook2003.CreateSheet("Sheet2");
                    WorkBook2003.CreateSheet("Sheet3");
                }
                else
                {
                    WorkBook2003.CreateSheet(strSheetName);
                }
                FileStream fileStream2003 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xls"), FileMode.Create);
                WorkBook2003.Write(fileStream2003);
                fileStream2003.Close();
                WorkBook2003.Close();
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 创建Excel(Office2007)
        /// </summary>
        /// <param name="strDataSourcePath">新建Excel的路径.xlsx</param>
        /// <param name="strSheetName">Sheet名称,如果为空则创建三个默认Sheet页</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateExcel_Office2007(string strDataSourcePath, string strSheetName)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath))
                {
                    return false;
                }
                XSSFWorkbook WorkBook2007 = new XSSFWorkbook();  //新建xlsx工作簿  
                if (string.IsNullOrEmpty(strSheetName))
                {
                    WorkBook2007.CreateSheet("Sheet1");
                    WorkBook2007.CreateSheet("Sheet2");
                    WorkBook2007.CreateSheet("Sheet3");
                }
                else
                {
                    WorkBook2007.CreateSheet(strSheetName);
                }
                FileStream fileStream2007 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xlsx"), FileMode.Create);
                WorkBook2007.Write(fileStream2007);
                fileStream2007.Close();
                WorkBook2007.Close();
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 在指定Excel中添加分页
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径</param>
        /// <param name="strSheetName">需要添加的Sheet名称</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateSheet(string strDataSourcePath, string strSheetName)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || string.IsNullOrEmpty(strSheetName) || !File.Exists(strDataSourcePath))
                {
                    return false;
                }
                IWorkbook iWorkBook = null;
                FileStream fileStream = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                if (System.IO.Path.GetExtension(strDataSourcePath) == ".xls")
                {
                    iWorkBook = new HSSFWorkbook(fileStream);
                    iWorkBook.CreateSheet(strSheetName);
                    FileStream fileStream2003 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xls"), FileMode.Create);
                    iWorkBook.Write(fileStream2003);
                    fileStream2003.Close();
                    iWorkBook.Close();
                }
                else if (System.IO.Path.GetExtension(strDataSourcePath) == ".xlsx")
                {
                    iWorkBook = new XSSFWorkbook(fileStream);
                    iWorkBook.CreateSheet(strSheetName);
                    FileStream fileStream2007 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xlsx"), FileMode.Create);
                    iWorkBook.Write(fileStream2007);
                    fileStream2007.Close();
                    iWorkBook.Close();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 在指定Excel中删除分页(至少有一个Sheet分页文件才能打开)
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径</param>
        /// <param name="strSheetName">需要删除的Sheet名称</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool RemoveSheet(string strDataSourcePath, string strSheetName)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || string.IsNullOrEmpty(strSheetName) || !File.Exists(strDataSourcePath))
                {
                    return false;
                }
                IWorkbook iWorkBook = null;
                FileStream fileStream = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                if (System.IO.Path.GetExtension(strDataSourcePath) == ".xls")
                {
                    iWorkBook = new HSSFWorkbook(fileStream);
                    iWorkBook.RemoveSheetAt(iWorkBook.GetSheetIndex(strSheetName));
                    FileStream fileStream2003 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xls"), FileMode.Create);
                    iWorkBook.Write(fileStream2003);
                    fileStream2003.Close();
                    iWorkBook.Close();
                }
                else if (System.IO.Path.GetExtension(strDataSourcePath) == ".xlsx")
                {
                    iWorkBook = new XSSFWorkbook(fileStream);
                    iWorkBook.RemoveSheetAt(iWorkBook.GetSheetIndex(strSheetName));
                    FileStream fileStream2007 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xlsx"), FileMode.Create);
                    iWorkBook.Write(fileStream2007);
                    fileStream2007.Close();
                    iWorkBook.Close();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获得指定Excel中所有Sheet
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径</param>
        /// <returns>Excel中所有Sheet字典(序号,Sheet名)</returns>
        public static Dictionary<int, string> GetExcelAllSheet(string strDataSourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath))
                {
                    return new Dictionary<int, string>();
                }
                Dictionary<int, string> dicAllSheet = new Dictionary<int, string>();
                IWorkbook iWorkBook = null;
                FileStream fileStream = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                if (System.IO.Path.GetExtension(strDataSourcePath) == ".xls")
                {
                    iWorkBook = new HSSFWorkbook(fileStream);
                }
                else if (System.IO.Path.GetExtension(strDataSourcePath) == ".xlsx")
                {
                    iWorkBook = new XSSFWorkbook(fileStream);
                }
                else
                {
                    return new Dictionary<int, string>();
                }
                for (int iNumberOfSheets = 0; iNumberOfSheets < iWorkBook.NumberOfSheets; iNumberOfSheets++)
                {
                    dicAllSheet.Add(iNumberOfSheets, iWorkBook.GetSheetName(iNumberOfSheets));
                }
                iWorkBook.Close();
                return dicAllSheet;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return new Dictionary<int, string>();
            }
        }

        /// <summary>
        /// 在指定Excel中指定Sheet指定位置填充文本
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径(如果文件不存在则重新创建)</param>
        /// <param name="strSheetName">需要填充的Sheet名称(如果没有则添加,如果冲突则使用冲突Sheet)</param>
        /// <param name="strTXT">需要填充的文本</param>
        /// <param name="iColumn">填充行</param>
        /// <param name="iRows">填充列</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FillString(string strDataSourcePath, string strSheetName, string strTXT, int iColumn, int iRows)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || string.IsNullOrEmpty(strSheetName) || string.IsNullOrEmpty(strTXT))
                {
                    return false;
                }
                if (File.Exists(strDataSourcePath))
                {
                    FileStream fileStream = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                    Dictionary<int, string> dicAllSheet = GetExcelAllSheet(strDataSourcePath);
                    if (System.IO.Path.GetExtension(strDataSourcePath) == ".xls")
                    {
                        IWorkbook iWorkBook = new HSSFWorkbook(fileStream);
                        ISheet iSheet = null;
                        if (dicAllSheet.ContainsValue(strSheetName))
                        {
                            iSheet = iWorkBook.GetSheet(strSheetName);
                        }
                        else
                        {
                            iSheet = iWorkBook.CreateSheet(strSheetName);
                        }
                        IRow iRow = iSheet.CreateRow(iRows);
                        ICell iCell = iRow.CreateCell(iColumn);
                        iCell.SetCellValue(strTXT);
                        FileStream fileStream2003 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xls"), FileMode.Create);
                        iWorkBook.Write(fileStream2003);
                        fileStream2003.Close();
                        iWorkBook.Close();
                    }
                    else if (System.IO.Path.GetExtension(strDataSourcePath) == ".xlsx")
                    {
                        IWorkbook iWorkBook = new XSSFWorkbook(fileStream);
                        ISheet iSheet = null;
                        if (dicAllSheet.ContainsValue(strSheetName))
                        {
                            iSheet = iWorkBook.GetSheet(strSheetName);
                        }
                        else
                        {
                            iSheet = iWorkBook.CreateSheet(strSheetName);
                        }
                        IRow iRow = iSheet.CreateRow(iRows);
                        ICell iCell = iRow.CreateCell(iColumn);
                        iCell.SetCellValue(strTXT);
                        FileStream fileStream2007 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xlsx"), FileMode.Create);
                        iWorkBook.Write(fileStream2007);
                        fileStream2007.Close();
                        iWorkBook.Close();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (System.IO.Path.GetExtension(strDataSourcePath) == ".xls")
                    {
                        bool bCreare = CreateExcel_Office2003(strDataSourcePath, strSheetName);
                        bool bFill = FillString(strDataSourcePath, strSheetName, strTXT, iColumn, iRows);
                        if (bCreare && bFill)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (System.IO.Path.GetExtension(strDataSourcePath) == ".xlsx")
                    {
                        bool bCreare = CreateExcel_Office2007(strDataSourcePath, strSheetName);
                        bool bFill = FillString(strDataSourcePath, strSheetName, strTXT, iColumn, iRows);
                        if (bCreare && bFill)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 在指定Excel中指定Sheet指定位置填充DataTable
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径(如果文件不存在则重新创建)</param>
        /// <param name="strSheetName">需要填充的Sheet名称(如果没有则添加,如果冲突则使用冲突Sheet)</param>
        /// <param name="dtSourceData">DataTable数据</param>
        /// <param name="WhetherThereFieldName">是否有列名(true保留DataTable字段名)</param>
        /// <param name="iRows">起始行</param>
        /// <param name="iColumn">起始列</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool FillDataTable(string strDataSourcePath, string strSheetName, DataTable dtSourceData, bool WhetherThereFieldName, int iRows, int iColumn)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || string.IsNullOrEmpty(strSheetName) || dtSourceData.Rows.Count < 1)
                {
                    return false;
                }
                if (File.Exists(strDataSourcePath))
                {
                    FileStream fileStream = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                    Dictionary<int, string> dicAllSheet = GetExcelAllSheet(strDataSourcePath);
                    if (System.IO.Path.GetExtension(strDataSourcePath) == ".xls")
                    {
                        IWorkbook iWorkBook = new HSSFWorkbook(fileStream);
                        ISheet iSheet = null;
                        if (dicAllSheet.ContainsValue(strSheetName))
                        {
                            iSheet = iWorkBook.GetSheet(strSheetName);
                        }
                        else
                        {
                            iSheet = iWorkBook.CreateSheet(strSheetName);
                        }
                        if (WhetherThereFieldName)
                        {
                            IRow rowDataTableField = iSheet.CreateRow(iRows);
                            for (int iDataTableColumns = 0; iDataTableColumns < dtSourceData.Columns.Count; iDataTableColumns++)
                            {
                                ICell cellErrstatist = rowDataTableField.CreateCell(iDataTableColumns + iColumn);
                                cellErrstatist.SetCellValue(dtSourceData.Columns[iDataTableColumns].ColumnName);
                            }
                            for (int iDataTableRows = 0; iDataTableRows < dtSourceData.Rows.Count; iDataTableRows++)
                            {
                                IRow rowDataTable = iSheet.CreateRow(iDataTableRows + iRows + 1);
                                for (int iDataTableColumns = 0; iDataTableColumns < dtSourceData.Columns.Count; iDataTableColumns++)
                                {
                                    ICell cellErrstatist = rowDataTable.CreateCell(iDataTableColumns + iColumn);
                                    cellErrstatist.SetCellValue(dtSourceData.Rows[iDataTableRows][iDataTableColumns].ToString());
                                }
                            }
                        }
                        else
                        {
                            for (int iDataTableRows = 0; iDataTableRows < dtSourceData.Rows.Count; iDataTableRows++)
                            {
                                IRow rowDataTable = iSheet.CreateRow(iDataTableRows + iRows);
                                for (int iDataTableColumns = 0; iDataTableColumns < dtSourceData.Columns.Count; iDataTableColumns++)
                                {
                                    ICell cellErrstatist = rowDataTable.CreateCell(iDataTableColumns + iColumn);
                                    cellErrstatist.SetCellValue(dtSourceData.Rows[iDataTableRows][iDataTableColumns].ToString());
                                }
                            }
                        }
                        FileStream fileStream2003 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xls"), FileMode.Create);
                        iWorkBook.Write(fileStream2003);
                        fileStream2003.Close();
                        iWorkBook.Close();
                    }
                    else if (System.IO.Path.GetExtension(strDataSourcePath) == ".xlsx")
                    {
                        IWorkbook iWorkBook = new XSSFWorkbook(fileStream);
                        ISheet iSheet = null;
                        if (dicAllSheet.ContainsValue(strSheetName))
                        {
                            iSheet = iWorkBook.GetSheet(strSheetName);
                        }
                        else
                        {
                            iSheet = iWorkBook.CreateSheet(strSheetName);
                        }
                        if (WhetherThereFieldName)
                        {
                            IRow rowDataTableField = iSheet.CreateRow(iRows);
                            for (int iDataTableColumns = 0; iDataTableColumns < dtSourceData.Columns.Count; iDataTableColumns++)
                            {
                                ICell cellErrstatist = rowDataTableField.CreateCell(iDataTableColumns + iColumn);
                                cellErrstatist.SetCellValue(dtSourceData.Columns[iDataTableColumns].ColumnName);
                            }
                            for (int iDataTableRows = 0; iDataTableRows < dtSourceData.Rows.Count; iDataTableRows++)
                            {
                                IRow rowDataTable = iSheet.CreateRow(iDataTableRows + iRows + 1);
                                for (int iDataTableColumns = 0; iDataTableColumns < dtSourceData.Columns.Count; iDataTableColumns++)
                                {
                                    ICell cellErrstatist = rowDataTable.CreateCell(iDataTableColumns + iColumn);
                                    cellErrstatist.SetCellValue(dtSourceData.Rows[iDataTableRows][iDataTableColumns].ToString());
                                }
                            }
                        }
                        else
                        {
                            for (int iDataTableRows = 0; iDataTableRows < dtSourceData.Rows.Count; iDataTableRows++)
                            {
                                IRow rowDataTable = iSheet.CreateRow(iDataTableRows + iRows);
                                for (int iDataTableColumns = 0; iDataTableColumns < dtSourceData.Columns.Count; iDataTableColumns++)
                                {
                                    ICell cellErrstatist = rowDataTable.CreateCell(iDataTableColumns + iColumn);
                                    cellErrstatist.SetCellValue(dtSourceData.Rows[iDataTableRows][iDataTableColumns].ToString());
                                }
                            }
                        }
                        FileStream fileStream2007 = new FileStream(Path.ChangeExtension(strDataSourcePath, "xlsx"), FileMode.Create);
                        iWorkBook.Write(fileStream2007);
                        fileStream2007.Close();
                        iWorkBook.Close();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (System.IO.Path.GetExtension(strDataSourcePath) == ".xls")
                    {
                        bool bCreare = CreateExcel_Office2003(strDataSourcePath, strSheetName);
                        bool bFill = FillDataTable(strDataSourcePath, strSheetName, dtSourceData, WhetherThereFieldName, iColumn, iRows);
                        if (bCreare && bFill)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (System.IO.Path.GetExtension(strDataSourcePath) == ".xlsx")
                    {
                        bool bCreare = CreateExcel_Office2007(strDataSourcePath, strSheetName);
                        bool bFill = FillDataTable(strDataSourcePath, strSheetName, dtSourceData, WhetherThereFieldName, iColumn, iRows);
                        if (bCreare && bFill)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// DataTable转换为Excel
        /// 存在文件则新建DataTableName的分页(如果分页名冲突则或为空则使用默认名称)
        /// 不存在文件则新建(Excel,名称为DataTableName,如果没有则使用默认名称)
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径</param>
        /// <param name="dtSourceData">DataTable数据</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DataTableConversionExcel(string strDataSourcePath, DataTable dtSourceData)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || dtSourceData.Rows.Count < 1)
                {
                    return false;
                }
                Dictionary<int, string> dicAllSheet = GetExcelAllSheet(strDataSourcePath);
                string strTableName = string.IsNullOrEmpty(dtSourceData.TableName) ? string.Format("Sheet{0}", dicAllSheet.Count + 1) : dtSourceData.TableName;
                if (dicAllSheet.ContainsValue(dtSourceData.TableName))
                {
                    RemoveSheet(strDataSourcePath, dtSourceData.TableName);
                }
                if (FillDataTable(strDataSourcePath, strTableName, dtSourceData, true, 0, 0))
                {
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
        /// DataTable转换为Excel
        /// 存在文件则新建DataTableName的分页(如果分页名冲突则或为空则使用默认名称)
        /// 不存在文件则新建(Excel,名称为DataTableName,如果没有则使用默认名称)
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径</param>
        /// <param name="dsSourceData">DataTable数据</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DataSetConversionExcel(string strDataSourcePath, DataSet dsSourceData)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || dsSourceData.Tables.Count < 1)
                {
                    return false;
                }
                foreach (DataTable dtSourceData in dsSourceData.Tables)
                {
                    Dictionary<int, string> dicAllSheet = GetExcelAllSheet(strDataSourcePath);
                    string strTableName = string.IsNullOrEmpty(dtSourceData.TableName) ? string.Format("Sheet{0}", dicAllSheet.Count + 1) : dtSourceData.TableName;
                    if (dicAllSheet.ContainsValue(dtSourceData.TableName))
                    {
                        RemoveSheet(strDataSourcePath, dtSourceData.TableName);
                    }
                    if (!FillDataTable(strDataSourcePath, strTableName, dtSourceData, true, 0, 0))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获得指定Excel指定分页指定起始终止位置的DataTable
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径</param>
        /// <param name="strSheetName">分页Sheet名称</param>
        /// <param name="WhetherThereFieldName">是否有列名(true保留DataTable字段名)</param>
        /// <param name="iStartRows">起始行</param>
        /// <param name="iStartColumn">起始列</param>
        /// <param name="iStopRows">终止行(如果小于等于0则默认Length)</param>
        /// <param name="iStopColumn">终止列(如果小于等于0则默认Length)</param>
        /// <returns>成功返回Excel的DataTable,失败返回NULL</returns>
        public static DataTable GetDataTable(string strDataSourcePath, string strSheetName, bool WhetherThereFieldName, int iStartRows, int iStartColumn, int iStopRows, int iStopColumn)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath))
                {
                    return null;
                }
                DataTable dtTargetData = new DataTable();
                FileStream fileStream = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                IWorkbook iWorkBook = null;
                if (System.IO.Path.GetExtension(strDataSourcePath) == ".xls")
                {
                    iWorkBook = new HSSFWorkbook(fileStream);
                }
                else if (System.IO.Path.GetExtension(strDataSourcePath) == ".xlsx")
                {
                    iWorkBook = new XSSFWorkbook(fileStream);
                }
                ISheet iSheet = null;
                if (string.IsNullOrEmpty(strSheetName))
                {
                    Dictionary<int, string> dicAllSheet = GetExcelAllSheet(strDataSourcePath);
                    iSheet = iWorkBook.GetSheet(dicAllSheet[0]);
                    dtTargetData.TableName = dicAllSheet[0];
                }
                else
                {
                    iSheet = iWorkBook.GetSheet(strSheetName);
                    dtTargetData.TableName = strSheetName;
                }
                if (WhetherThereFieldName)
                {
                    //构建DataTable列(第一行为列名)
                    IRow iRowFirst = iSheet.GetRow(iStartRows);
                    for (int iFirst = iStartColumn; iFirst <= (iStopColumn <= 0 ? (iRowFirst.LastCellNum) : iStopColumn); iFirst++)
                    {
                        ICell iCell = iRowFirst.GetCell(iFirst);
                        if (iCell != null)
                        {
                            if (iCell.StringCellValue != null)
                            {
                                DataColumn dColumn = new DataColumn(iCell.StringCellValue);
                                dtTargetData.Columns.Add(dColumn);
                            }
                        }
                    }
                    //构建DataTable行(第二行往下为数据)
                    for (int iRowNum = iStartRows + 1; iRowNum <= (iStopRows <= 0 ? iSheet.LastRowNum : iStopRows); iRowNum++)
                    {
                        IRow iRowData = iSheet.GetRow(iRowNum);
                        DataRow drTargetData = dtTargetData.NewRow();
                        for (int iRowCell = iStartColumn; iRowCell <= (iStopColumn <= 0 ? (iRowFirst.LastCellNum) : iStopColumn); iRowCell++)
                        {
                            ICell iCell = iRowData.GetCell(iRowCell);
                            if (iCell != null)
                            {
                                if (iCell.StringCellValue != null)
                                {
                                    DataColumn dColumn = new DataColumn(iCell.StringCellValue);
                                    drTargetData[iRowCell - iStartColumn] = dColumn;
                                }
                            }
                        }
                        dtTargetData.Rows.Add(drTargetData);
                    }
                }
                else
                {
                    //构建DataTable列,以读取第一行的长度填充列名(使用默认命名初始化列名Column1)
                    IRow iRowFirst = iSheet.GetRow(iStartRows);
                    for (int iFirst = iStartColumn; iFirst <= (iStopColumn <= 0 ? (iRowFirst.LastCellNum) : iStopColumn); iFirst++)
                    {
                        ICell iCell = iRowFirst.GetCell(iFirst);
                        if (iCell != null)
                        {
                            if (iCell.StringCellValue != null)
                            {
                                dtTargetData.Columns.Add(string.Format("Column{0}", iFirst - iStartColumn));
                            }
                        }
                    }
                    //构建DataTable行(第一行往下为数据)
                    for (int iRowNum = iStartRows; iRowNum <= (iStopRows <= 0 ? iSheet.LastRowNum : iStopRows); iRowNum++)
                    {
                        IRow iRowData = iSheet.GetRow(iRowNum);
                        DataRow drTargetData = dtTargetData.NewRow();
                        for (int iRowCell = iStartColumn; iRowCell <= (iStopColumn <= 0 ? (iRowFirst.LastCellNum) : iStopColumn); iRowCell++)
                        {
                            ICell iCell = iRowData.GetCell(iRowCell);
                            if (iCell != null)
                            {
                                if (iCell.StringCellValue != null)
                                {
                                    DataColumn dColumn = new DataColumn(iCell.StringCellValue);
                                    drTargetData[iRowCell - iStartColumn] = dColumn;
                                }
                            }
                        }
                        dtTargetData.Rows.Add(drTargetData);
                    }
                }
                return dtTargetData;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Excel指定分页转换为DataTable(如果分页为空,默认第一个分页)
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径</param>
        /// <param name="strSheetName">分页Sheet名称</param>
        /// <returns>成功返回Excel的DataTable,失败返回NULL</returns>
        public static DataTable ExcelConversionDataTable(string strDataSourcePath, string strSheetName)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath))
                {
                    return null;
                }
                return GetDataTable(strDataSourcePath, strSheetName, true, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Excel指定分页转换为DataSet
        /// </summary>
        /// <param name="strDataSourcePath">Excel文件路径</param>
        /// <returns>成功返回Excel的DataSet,失败返回NULL</returns>
        public static DataSet ExcelConversionDataSet(string strDataSourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath))
                {
                    return null;
                }
                DataSet dsTargetData = new DataSet();
                Dictionary<int, string> dicAllSheet = GetExcelAllSheet(strDataSourcePath);
                foreach (var vAllSheet in dicAllSheet)
                {
                    DataTable dtTargetData = new DataTable();
                    dtTargetData.TableName = vAllSheet.Value;
                    dtTargetData = ExcelConversionDataTable(strDataSourcePath, vAllSheet.Value);
                    if (dtTargetData == null)
                    {
                        continue;
                    }
                    dsTargetData.Tables.Add(dtTargetData);
                }
                return dsTargetData;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }
    }
}
