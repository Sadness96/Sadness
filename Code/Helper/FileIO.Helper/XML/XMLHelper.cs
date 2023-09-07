using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using FileIO.Helper.TXT;

namespace FileIO.Helper.XML
{
    /// <summary>
    /// XML 文本帮助类
    /// 创建日期:2017年04月28日
    /// </summary>
    public class XMLHelper
    {
        /// <summary>
        /// 创建 XML 文档
        /// </summary>
        /// <param name="xmlFileName">XML 文件路径</param>
        /// <param name="rootNodeName">根节点名称(须指定一个根节点名称)</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateXmlDocument(string xmlFileName, string rootNodeName)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlFileName) || string.IsNullOrEmpty(rootNodeName))
                {
                    return false;
                }
                XmlDocument Document = new XmlDocument();
                XmlNode Node = Document.CreateXmlDeclaration("1.0", "utf-8", "");
                Document.AppendChild(Node);
                XmlNode nodeRoot = Document.CreateElement(rootNodeName);
                Document.AppendChild(nodeRoot);
                Document.Save(xmlFileName);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 在指定节点后添加节点
        /// </summary>
        /// <param name="xmlFileName">XML 文件路径</param>
        /// <param name="strNodeNamePath">指定节点名称路径("NodeNamePath"指定根节点,"//NodeNamePath"指定搜索到的第一个匹配节点)</param>
        /// <param name="strNodeName">添加节点名称</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool AddXmlNode(string xmlFileName, string strNodeNamePath, string strNodeName)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlFileName) || string.IsNullOrEmpty(strNodeNamePath) || string.IsNullOrEmpty(strNodeName))
                {
                    return false;
                }
                XmlDocument Document = new XmlDocument();
                Document.Load(xmlFileName);
                XmlNode nodeData = Document.SelectSingleNode(strNodeNamePath);
                XmlNode addNodet = Document.CreateElement(strNodeName);
                nodeData.AppendChild(addNodet);
                Document.Save(xmlFileName);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 移除指定节点的所有指定属性和子类。删除默认属性。
        /// </summary>
        /// <param name="xmlFileName">XML 文件路径</param>
        /// <param name="strNodeNamePath">指定节点名称路径("NodeNamePath"指定根节点,"//NodeNamePath"指定搜索到的第一个匹配节点)</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool RemoveXmlNode(string xmlFileName, string strNodeNamePath)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlFileName) || string.IsNullOrEmpty(strNodeNamePath))
                {
                    return false;
                }
                XmlDocument Document = new XmlDocument();
                Document.Load(xmlFileName);
                XmlNode nodeData = Document.SelectSingleNode(strNodeNamePath);
                XmlElement elementData = (XmlElement)nodeData;
                elementData.RemoveAll();
                Document.Save(xmlFileName);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 在指定节点上添加属性
        /// </summary>
        /// <param name="xmlFileName">XML 文件路径</param>
        /// <param name="strNodeNamePath">指定节点名称路径("NodeNamePath"指定根节点,"//NodeNamePath"指定搜索到的第一个匹配节点)</param>
        /// <param name="strAttribute">添加属性名称</param>
        /// <param name="strAttributeText">添加属性文本值</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool WriteAttribute(string xmlFileName, string strNodeNamePath, string strAttribute, string strAttributeText)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlFileName) || string.IsNullOrEmpty(strNodeNamePath) || string.IsNullOrEmpty(strAttribute) || string.IsNullOrEmpty(strAttributeText))
                {
                    return false;
                }
                XmlDocument Document = new XmlDocument();
                Document.Load(xmlFileName);
                XmlNode nodeData = Document.SelectSingleNode(strNodeNamePath);
                XmlAttribute addAttribute = Document.CreateAttribute(strAttribute);
                addAttribute.Value = strAttributeText;
                nodeData.Attributes.Append(addAttribute);
                Document.Save(xmlFileName);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 在指定节点上添加文本
        /// </summary>
        /// <param name="xmlFileName">XML 文件路径</param>
        /// <param name="strNodeNamePath">指定节点名称路径("NodeNamePath"指定根节点,"//NodeNamePath"指定搜索到的第一个匹配节点)</param>
        /// <param name="strInnerText">添加文本</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool WriteInnerText(string xmlFileName, string strNodeNamePath, string strInnerText)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlFileName) || string.IsNullOrEmpty(strNodeNamePath) || string.IsNullOrEmpty(strInnerText))
                {
                    return false;
                }
                XmlDocument Document = new XmlDocument();
                Document.Load(xmlFileName);
                XmlNode nodeData = Document.SelectSingleNode(strNodeNamePath);
                nodeData.InnerText = strInnerText;
                Document.Save(xmlFileName);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获得指定节点的指定属性值
        /// </summary>
        /// <param name="xmlFileName">XML 文件路径</param>
        /// <param name="strNodeNamePath">指定节点名称路径("NodeNamePath"指定根节点,"//NodeNamePath"指定搜索到的第一个匹配节点)</param>
        /// <param name="strAttribute">指定属性</param>
        /// <returns>指定节点的指定属性值</returns>
        public static string GetAttributeText(string xmlFileName, string strNodeNamePath, string strAttribute)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlFileName) || string.IsNullOrEmpty(strNodeNamePath) || string.IsNullOrEmpty(strAttribute))
                {
                    return "";
                }
                XmlDocument Document = new XmlDocument();
                Document.Load(xmlFileName);
                XmlNode nodeData = Document.SelectSingleNode(strNodeNamePath);
                return nodeData.Attributes[strAttribute].Value;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// 获得指定节点的文本值
        /// </summary>
        /// <param name="xmlFileName">XML 文件路径</param>
        /// <param name="strNodeNamePath">指定节点名称路径("NodeNamePath"指定根节点,"//NodeNamePath"指定搜索到的第一个匹配节点)</param>
        /// <returns>指定节点的文本值</returns>
        public static string GetInnerText(string xmlFileName, string strNodeNamePath)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlFileName) || string.IsNullOrEmpty(strNodeNamePath))
                {
                    return "";
                }
                XmlDocument Document = new XmlDocument();
                Document.Load(xmlFileName);
                XmlNode nodeData = Document.SelectSingleNode(strNodeNamePath);
                return nodeData.InnerText;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// DataTable 转换为 XML
        /// </summary>
        /// <param name="strSource">XML 文件路径</param>
        /// <param name="dtSourceData">DataTable 数据</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DataTableConversionXML(string strSource, DataTable dtSourceData)
        {
            try
            {
                if (string.IsNullOrEmpty(strSource) || dtSourceData.Rows.Count < 1)
                {
                    return false;
                }
                //创建并保存数据
                XmlDocument Document = new XmlDocument();
                XmlNode Node = Document.CreateXmlDeclaration("1.0", "utf-8", "");
                Document.AppendChild(Node);
                //整体XML是一个DataSet
                XmlNode nodeDataSet = Document.CreateElement("DataSet");
                Document.AppendChild(nodeDataSet);
                //DataSet中包含若干个DataTable
                XmlNode nodeDataTablet = Document.CreateElement("DataTable");
                nodeDataSet.AppendChild(nodeDataTablet);
                //DataTable中包含DataTableName、DataTableField、DataTable
                XmlNode nodeDataTableName = Document.CreateNode(XmlNodeType.Element, "DataTableName", null);
                nodeDataTableName.InnerText = dtSourceData.TableName;
                nodeDataTablet.AppendChild(nodeDataTableName);
                XmlNode nodeDataTableField = Document.CreateNode(XmlNodeType.Element, "DataTableField", null);
                nodeDataTablet.AppendChild(nodeDataTableField);
                foreach (var item in dtSourceData.Columns)
                {
                    //DataTableField中包含Field
                    XmlNode nodeField = Document.CreateNode(XmlNodeType.Element, "Field", null);
                    nodeDataTableField.AppendChild(nodeField);
                    //Field中包含FieldName、FieldType
                    XmlNode nodeFieldName = Document.CreateNode(XmlNodeType.Element, "FieldName", null);
                    nodeFieldName.InnerText = item.ToString();
                    nodeField.AppendChild(nodeFieldName);
                    XmlNode nodeFieldType = Document.CreateNode(XmlNodeType.Element, "FieldType", null);
                    nodeFieldType.InnerText = "";
                    nodeField.AppendChild(nodeFieldType);
                }
                XmlNode nodeDataTableData = Document.CreateNode(XmlNodeType.Element, "DataTableData", null);
                nodeDataTablet.AppendChild(nodeDataTableData);
                foreach (DataRow drSourceData in dtSourceData.Rows)
                {
                    XmlNode nodeDataRow = Document.CreateNode(XmlNodeType.Element, "DataRow", null);
                    nodeDataTableData.AppendChild(nodeDataRow);
                    foreach (var item in dtSourceData.Columns)
                    {
                        XmlNode nodeData = Document.CreateNode(XmlNodeType.Element, item.ToString(), null);
                        nodeData.InnerText = drSourceData[item.ToString()].ToString();
                        nodeDataRow.AppendChild(nodeData);
                    }
                }
                Document.Save(strSource);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// DataTable 转换为 XML(同步保存字段类型)
        /// </summary>
        /// <param name="strSource">XML 文件路径</param>
        /// <param name="dtSourceData">DataTable 数据</param>
        /// <param name="dicFieldNameType">数据库表字段名和字段类型</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DataTableConversionXML(string strSource, DataTable dtSourceData, Dictionary<string, string> dicFieldNameType)
        {
            try
            {
                if (string.IsNullOrEmpty(strSource) || dtSourceData.Rows.Count < 1)
                {
                    return false;
                }
                //创建并保存数据
                XmlDocument Document = new XmlDocument();
                XmlNode Node = Document.CreateXmlDeclaration("1.0", "utf-8", "");
                Document.AppendChild(Node);
                //整体XML是一个DataSet
                XmlNode nodeDataSet = Document.CreateElement("DataSet");
                Document.AppendChild(nodeDataSet);
                //DataSet中包含若干个DataTable
                XmlNode nodeDataTablet = Document.CreateElement("DataTable");
                nodeDataSet.AppendChild(nodeDataTablet);
                //DataTable中包含DataTableName、DataTableField、DataTable
                XmlNode nodeDataTableName = Document.CreateNode(XmlNodeType.Element, "DataTableName", null);
                nodeDataTableName.InnerText = dtSourceData.TableName;
                nodeDataTablet.AppendChild(nodeDataTableName);
                XmlNode nodeDataTableField = Document.CreateNode(XmlNodeType.Element, "DataTableField", null);
                nodeDataTablet.AppendChild(nodeDataTableField);
                foreach (var item in dtSourceData.Columns)
                {
                    //DataTableField中包含Field
                    XmlNode nodeField = Document.CreateNode(XmlNodeType.Element, "Field", null);
                    nodeDataTableField.AppendChild(nodeField);
                    //Field中包含FieldName、FieldType
                    XmlNode nodeFieldName = Document.CreateNode(XmlNodeType.Element, "FieldName", null);
                    nodeFieldName.InnerText = item.ToString();
                    nodeField.AppendChild(nodeFieldName);
                    XmlNode nodeFieldType = Document.CreateNode(XmlNodeType.Element, "FieldType", null);
                    nodeFieldType.InnerText = dicFieldNameType != null && dicFieldNameType.ContainsKey(item.ToString()) ? dicFieldNameType[item.ToString()].ToString() : "";
                    nodeField.AppendChild(nodeFieldType);
                }
                XmlNode nodeDataTableData = Document.CreateNode(XmlNodeType.Element, "DataTableData", null);
                nodeDataTablet.AppendChild(nodeDataTableData);
                foreach (DataRow drSourceData in dtSourceData.Rows)
                {
                    //DataTableData中包含DataRow
                    XmlNode nodeDataRow = Document.CreateNode(XmlNodeType.Element, "DataRow", null);
                    nodeDataTableData.AppendChild(nodeDataRow);
                    foreach (var item in dtSourceData.Columns)
                    {
                        XmlNode nodeData = Document.CreateNode(XmlNodeType.Element, item.ToString(), null);
                        nodeData.InnerText = drSourceData[item.ToString()].ToString();
                        nodeDataRow.AppendChild(nodeData);
                    }
                }
                Document.Save(strSource);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// DataSet 转换为 XML
        /// </summary>
        /// <param name="strSource">XML 文件路径</param>
        /// <param name="dsSourceData">DataSet 数据</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DataSetConversionXML(string strSource, DataSet dsSourceData)
        {
            try
            {
                if (string.IsNullOrEmpty(strSource) || dsSourceData.Tables[0].Rows.Count < 1)
                {
                    return false;
                }
                //创建并保存数据
                XmlDocument Document = new XmlDocument();
                XmlNode Node = Document.CreateXmlDeclaration("1.0", "utf-8", "");
                Document.AppendChild(Node);
                //整体XML是一个DataSet
                XmlNode nodeDataSet = Document.CreateElement("DataSet");
                Document.AppendChild(nodeDataSet);
                //DataSet中包含若干个DataTable
                foreach (DataTable dtSourceData in dsSourceData.Tables)
                {
                    XmlNode nodeDataTablet = Document.CreateElement("DataTable");
                    nodeDataSet.AppendChild(nodeDataTablet);
                    //DataTable中包含DataTableName、DataTableField、DataTable
                    XmlNode nodeDataTableName = Document.CreateNode(XmlNodeType.Element, "DataTableName", null);
                    nodeDataTableName.InnerText = dtSourceData.TableName;
                    nodeDataTablet.AppendChild(nodeDataTableName);
                    XmlNode nodeDataTableField = Document.CreateNode(XmlNodeType.Element, "DataTableField", null);
                    nodeDataTablet.AppendChild(nodeDataTableField);
                    foreach (var item in dtSourceData.Columns)
                    {
                        //DataTableField中包含Field
                        XmlNode nodeField = Document.CreateNode(XmlNodeType.Element, "Field", null);
                        nodeDataTableField.AppendChild(nodeField);
                        //Field中包含FieldName、FieldType
                        XmlNode nodeFieldName = Document.CreateNode(XmlNodeType.Element, "FieldName", null);
                        nodeFieldName.InnerText = item.ToString();
                        nodeField.AppendChild(nodeFieldName);
                        XmlNode nodeFieldType = Document.CreateNode(XmlNodeType.Element, "FieldType", null);
                        nodeFieldType.InnerText = "";
                        nodeField.AppendChild(nodeFieldType);
                    }
                    XmlNode nodeDataTableData = Document.CreateNode(XmlNodeType.Element, "DataTableData", null);
                    nodeDataTablet.AppendChild(nodeDataTableData);
                    foreach (DataRow drSourceData in dtSourceData.Rows)
                    {
                        XmlNode nodeDataRow = Document.CreateNode(XmlNodeType.Element, "DataRow", null);
                        nodeDataTableData.AppendChild(nodeDataRow);
                        foreach (var item in dtSourceData.Columns)
                        {
                            XmlNode nodeData = Document.CreateNode(XmlNodeType.Element, item.ToString(), null);
                            nodeData.InnerText = drSourceData[item.ToString()].ToString();
                            nodeDataRow.AppendChild(nodeData);
                        }
                    }
                }
                Document.Save(strSource);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// DataSet 转换为 XML(同步保存字段类型)
        /// </summary>
        /// <param name="strSource">XML 文件路径</param>
        /// <param name="dsSourceData">DataSet 数据</param>
        /// <param name="diclistFieldNameType">数据库表字段名和字段类型</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool DataSetConversionXML(string strSource, DataSet dsSourceData, Dictionary<string, Dictionary<string, string>> diclistFieldNameType)
        {
            try
            {
                if (string.IsNullOrEmpty(strSource) || dsSourceData.Tables[0].Rows.Count < 1)
                {
                    return false;
                }
                //创建并保存数据
                XmlDocument Document = new XmlDocument();
                XmlNode Node = Document.CreateXmlDeclaration("1.0", "utf-8", "");
                Document.AppendChild(Node);
                //整体XML是一个DataSet
                XmlNode nodeDataSet = Document.CreateElement("DataSet");
                Document.AppendChild(nodeDataSet);
                //DataSet中包含若干个DataTable
                foreach (DataTable dtSourceData in dsSourceData.Tables)
                {
                    Dictionary<string, string> dicFieldNameType = diclistFieldNameType != null && diclistFieldNameType.ContainsKey(dtSourceData.TableName) ? diclistFieldNameType[dtSourceData.TableName] : new Dictionary<string, string>();
                    XmlNode nodeDataTablet = Document.CreateElement("DataTable");
                    nodeDataSet.AppendChild(nodeDataTablet);
                    //DataTable中包含DataTableName、DataTableField、DataTable
                    XmlNode nodeDataTableName = Document.CreateNode(XmlNodeType.Element, "DataTableName", null);
                    nodeDataTableName.InnerText = dtSourceData.TableName;
                    nodeDataTablet.AppendChild(nodeDataTableName);
                    XmlNode nodeDataTableField = Document.CreateNode(XmlNodeType.Element, "DataTableField", null);
                    nodeDataTablet.AppendChild(nodeDataTableField);
                    foreach (var item in dtSourceData.Columns)
                    {
                        //DataTableField中包含Field
                        XmlNode nodeField = Document.CreateNode(XmlNodeType.Element, "Field", null);
                        nodeDataTableField.AppendChild(nodeField);
                        //Field中包含FieldName、FieldType
                        XmlNode nodeFieldName = Document.CreateNode(XmlNodeType.Element, "FieldName", null);
                        nodeFieldName.InnerText = item.ToString();
                        nodeField.AppendChild(nodeFieldName);
                        XmlNode nodeFieldType = Document.CreateNode(XmlNodeType.Element, "FieldType", null);
                        nodeFieldType.InnerText = dicFieldNameType != null && dicFieldNameType.ContainsKey(item.ToString()) ? dicFieldNameType[item.ToString()].ToString() : "";
                        nodeField.AppendChild(nodeFieldType);
                    }
                    XmlNode nodeDataTableData = Document.CreateNode(XmlNodeType.Element, "DataTableData", null);
                    nodeDataTablet.AppendChild(nodeDataTableData);
                    foreach (DataRow drSourceData in dtSourceData.Rows)
                    {
                        XmlNode nodeDataRow = Document.CreateNode(XmlNodeType.Element, "DataRow", null);
                        nodeDataTableData.AppendChild(nodeDataRow);
                        foreach (var item in dtSourceData.Columns)
                        {
                            XmlNode nodeData = Document.CreateNode(XmlNodeType.Element, item.ToString(), null);
                            nodeData.InnerText = drSourceData[item.ToString()].ToString();
                            nodeDataRow.AppendChild(nodeData);
                        }
                    }
                }
                Document.Save(strSource);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// XML 转换为 DataTable
        /// </summary>
        /// <param name="strSource">XML 文件路径</param>
        /// <returns>成功返回XML的DataTable,失败返回NULL</returns>
        public static DataTable XMLConversionDataTable(string strSource)
        {
            try
            {
                if (string.IsNullOrEmpty(strSource) || !File.Exists(strSource))
                {
                    return null;
                }
                DataTable dtTargetData = new DataTable();
                XmlDocument Document = new XmlDocument();
                Document.Load(strSource);
                //遍历DataSet得到每个DataTable
                XmlNodeList nodeListDataSet = Document.SelectNodes("//DataSet");
                foreach (XmlNode nodeDataTable in nodeListDataSet)
                {
                    //遍历Datatable得到DataTableName、DataTableField、DataTable
                    XmlNodeList nodeListDataTable = nodeDataTable.SelectNodes("DataTable");
                    foreach (XmlNode nodeDataTableInfo in nodeListDataTable)
                    {
                        //获得DataTableName,如果没有定义表名,使用Table
                        if (string.IsNullOrEmpty(nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText))
                        {
                            dtTargetData.TableName = string.Format("Table");
                        }
                        else
                        {
                            dtTargetData.TableName = nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText;
                        }
                        //遍历DataTableField得到每个FieldName
                        XmlNodeList nodeListDataTableField = nodeDataTableInfo.SelectNodes("DataTableField");
                        foreach (XmlNode nodeDataTableField in nodeListDataTableField)
                        {
                            XmlNodeList nodeListField = nodeDataTableField.SelectNodes("Field");
                            foreach (XmlNode nodeField in nodeListField)
                            {
                                //遍历DataTableField获得每个FieldName
                                XmlNodeList nodeListFieldName = nodeField.SelectNodes("FieldName");
                                foreach (XmlNode nodeFieldName in nodeListFieldName)
                                {
                                    dtTargetData.Columns.Add(nodeFieldName.InnerText);
                                }
                            }
                        }
                        //遍历DataTableData获得每个DataRow
                        XmlNodeList nodeListDataTableData = nodeDataTableInfo.SelectNodes("DataTableData");
                        foreach (XmlNode nodeDataTableData in nodeListDataTableData)
                        {
                            //遍历DataRow获得每条数据
                            XmlNodeList nodeListDataRow = nodeDataTableData.SelectNodes("DataRow");
                            foreach (XmlNode nodeDataRow in nodeListDataRow)
                            {
                                DataRow drDataTable = dtTargetData.NewRow();
                                //遍历DataTableField得到每个FieldName
                                foreach (XmlNode nodeDataTableField in nodeListDataTableField)
                                {
                                    XmlNodeList nodeListField = nodeDataTableField.SelectNodes("Field");
                                    foreach (XmlNode nodeField in nodeListField)
                                    {
                                        //遍历DataTableField获得每个FieldName
                                        XmlNodeList nodeListFieldName = nodeField.SelectNodes("FieldName");
                                        foreach (XmlNode nodeFieldName in nodeListFieldName)
                                        {
                                            //根据遍历的字段类型,获得每个DataRow中的数据
                                            drDataTable[nodeFieldName.InnerText] = nodeDataRow.SelectSingleNode(nodeFieldName.InnerText).InnerText;
                                        }
                                    }
                                }
                                dtTargetData.Rows.Add(drDataTable);
                            }
                        }
                        //DataTable只能储存一个表,所以退出循环
                        break;
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
        /// XML 转换为 DataTable(返回字段类型)
        /// </summary>
        /// <param name="strSource">XML 文件路径</param>
        /// <param name="dicFieldNameType">返回数据库表字段名和字段类型</param>
        /// <returns>成功返回XML的DataTable,失败返回NULL</returns>
        public static DataTable XMLConversionDataTable(string strSource, out Dictionary<string, string> dicFieldNameType)
        {
            dicFieldNameType = new Dictionary<string, string>();
            try
            {
                if (string.IsNullOrEmpty(strSource) || !File.Exists(strSource))
                {
                    return null;
                }
                DataTable dtTargetData = new DataTable();
                XmlDocument Document = new XmlDocument();
                Document.Load(strSource);
                //遍历DataSet得到每个DataTable
                XmlNodeList nodeListDataSet = Document.SelectNodes("//DataSet");
                foreach (XmlNode nodeDataTable in nodeListDataSet)
                {
                    //遍历Datatable得到DataTableName、DataTableField、DataTable
                    XmlNodeList nodeListDataTable = nodeDataTable.SelectNodes("DataTable");
                    foreach (XmlNode nodeDataTableInfo in nodeListDataTable)
                    {
                        //获得DataTableName,如果没有定义表名,使用Table
                        if (string.IsNullOrEmpty(nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText))
                        {
                            dtTargetData.TableName = string.Format("Table");
                        }
                        else
                        {
                            dtTargetData.TableName = nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText;
                        }
                        //遍历DataTableField得到每个FieldName
                        XmlNodeList nodeListDataTableField = nodeDataTableInfo.SelectNodes("DataTableField");
                        foreach (XmlNode nodeDataTableField in nodeListDataTableField)
                        {
                            XmlNodeList nodeListField = nodeDataTableField.SelectNodes("Field");
                            foreach (XmlNode nodeField in nodeListField)
                            {
                                string strFieldName = "";
                                string strFieldType = "";
                                //遍历DataTableField获得每个FieldName
                                XmlNodeList nodeListFieldName = nodeField.SelectNodes("FieldName");
                                foreach (XmlNode nodeFieldName in nodeListFieldName)
                                {
                                    strFieldName = nodeFieldName.InnerText;
                                    dtTargetData.Columns.Add(nodeFieldName.InnerText);
                                }
                                XmlNodeList nodeListFieldType = nodeField.SelectNodes("FieldType");
                                foreach (XmlNode nodeFieldType in nodeListFieldType)
                                {
                                    strFieldType = nodeFieldType.InnerText;
                                }
                                dicFieldNameType.Add(strFieldName, strFieldType);
                            }
                        }
                        //遍历DataTableData获得每个DataRow
                        XmlNodeList nodeListDataTableData = nodeDataTableInfo.SelectNodes("DataTableData");
                        foreach (XmlNode nodeDataTableData in nodeListDataTableData)
                        {
                            //遍历DataRow获得每条数据
                            XmlNodeList nodeListDataRow = nodeDataTableData.SelectNodes("DataRow");
                            foreach (XmlNode nodeDataRow in nodeListDataRow)
                            {
                                DataRow drDataTable = dtTargetData.NewRow();
                                //遍历DataTableField得到每个FieldName
                                foreach (XmlNode nodeDataTableField in nodeListDataTableField)
                                {
                                    XmlNodeList nodeListField = nodeDataTableField.SelectNodes("Field");
                                    foreach (XmlNode nodeField in nodeListField)
                                    {
                                        //遍历DataTableField获得每个FieldName
                                        XmlNodeList nodeListFieldName = nodeField.SelectNodes("FieldName");
                                        foreach (XmlNode nodeFieldName in nodeListFieldName)
                                        {
                                            //根据遍历的字段类型,获得每个DataRow中的数据
                                            drDataTable[nodeFieldName.InnerText] = nodeDataRow.SelectSingleNode(nodeFieldName.InnerText).InnerText;
                                        }
                                    }
                                }
                                dtTargetData.Rows.Add(drDataTable);
                            }
                        }
                        //DataTable只能储存一个表,所以退出循环
                        break;
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
        /// XML 转换为 DataSet
        /// </summary>
        /// <param name="strSource">XML 文件路径</param>
        /// <returns>成功返回XML的DataTable,失败返回NULL</returns>
        public static DataSet XMLConversionDataSet(string strSource)
        {
            try
            {
                if (string.IsNullOrEmpty(strSource) || !File.Exists(strSource))
                {
                    return null;
                }
                DataSet dsTargetData = new DataSet();
                XmlDocument Document = new XmlDocument();
                Document.Load(strSource);
                //遍历DataSet得到每个DataTable
                XmlNodeList nodeListDataSet = Document.SelectNodes("//DataSet");
                foreach (XmlNode nodeDataTable in nodeListDataSet)
                {
                    //定义没有储存表名的计数
                    int intTableNumber = 0;
                    //遍历Datatable得到DataTableName、DataTableField、DataTable
                    XmlNodeList nodeListDataTable = nodeDataTable.SelectNodes("DataTable");
                    foreach (XmlNode nodeDataTableInfo in nodeListDataTable)
                    {
                        //在遍历Datatable的循环里创建DataTable
                        DataTable dtTargetData = new DataTable();
                        //获得DataTableName,如果没有定义表名,使用Table_intTableNumber
                        if (string.IsNullOrEmpty(nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText))
                        {
                            dtTargetData.TableName = string.Format("Table_") + intTableNumber;
                        }
                        else
                        {
                            dtTargetData.TableName = nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText;
                        }
                        //遍历DataTableField得到每个FieldName
                        XmlNodeList nodeListDataTableField = nodeDataTableInfo.SelectNodes("DataTableField");
                        foreach (XmlNode nodeDataTableField in nodeListDataTableField)
                        {
                            XmlNodeList nodeListField = nodeDataTableField.SelectNodes("Field");
                            foreach (XmlNode nodeField in nodeListField)
                            {
                                //遍历DataTableField获得每个FieldName
                                XmlNodeList nodeListFieldName = nodeField.SelectNodes("FieldName");
                                foreach (XmlNode nodeFieldName in nodeListFieldName)
                                {
                                    dtTargetData.Columns.Add(nodeFieldName.InnerText);
                                }
                            }
                        }
                        //遍历DataTableData获得每个DataRow
                        XmlNodeList nodeListDataTableData = nodeDataTableInfo.SelectNodes("DataTableData");
                        foreach (XmlNode nodeDataTableData in nodeListDataTableData)
                        {
                            //遍历DataRow获得每条数据
                            XmlNodeList nodeListDataRow = nodeDataTableData.SelectNodes("DataRow");
                            foreach (XmlNode nodeDataRow in nodeListDataRow)
                            {
                                DataRow drDataTable = dtTargetData.NewRow();
                                //遍历DataTableField得到每个FieldName
                                foreach (XmlNode nodeDataTableField in nodeListDataTableField)
                                {
                                    XmlNodeList nodeListField = nodeDataTableField.SelectNodes("Field");
                                    foreach (XmlNode nodeField in nodeListField)
                                    {
                                        //遍历DataTableField获得每个FieldName
                                        XmlNodeList nodeListFieldName = nodeField.SelectNodes("FieldName");
                                        foreach (XmlNode nodeFieldName in nodeListFieldName)
                                        {
                                            //根据遍历的字段类型,获得每个DataRow中的数据
                                            drDataTable[nodeFieldName.InnerText] = nodeDataRow.SelectSingleNode(nodeFieldName.InnerText).InnerText;
                                        }
                                    }
                                }
                                dtTargetData.Rows.Add(drDataTable);
                            }
                        }
                        //把每个DataTable存给DataSet
                        dsTargetData.Tables.Add(dtTargetData);
                    }
                }
                return dsTargetData;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// XML 转换为 DataSet(返回字段类型)
        /// </summary>
        /// <param name="strSource">XML 文件路径</param>
        /// <param name="diclistFieldNameType">返回数据库表字段名和字段类型</param>
        /// <returns>成功返回XML的DataTable,失败返回NULL</returns>
        public static DataSet XMLConversionDataSet(string strSource, out Dictionary<string, Dictionary<string, string>> diclistFieldNameType)
        {
            diclistFieldNameType = new Dictionary<string, Dictionary<string, string>>();
            try
            {
                if (string.IsNullOrEmpty(strSource) || !File.Exists(strSource))
                {
                    return null;
                }
                DataSet dsTargetData = new DataSet();
                XmlDocument Document = new XmlDocument();
                Document.Load(strSource);
                //遍历DataSet得到每个DataTable
                XmlNodeList nodeListDataSet = Document.SelectNodes("//DataSet");
                foreach (XmlNode nodeDataTable in nodeListDataSet)
                {
                    //定义没有储存表名的计数
                    int intTableNumber = 0;
                    //遍历Datatable得到DataTableName、DataTableField、DataTable
                    XmlNodeList nodeListDataTable = nodeDataTable.SelectNodes("DataTable");
                    foreach (XmlNode nodeDataTableInfo in nodeListDataTable)
                    {
                        Dictionary<string, string> dicFieldNameType = new Dictionary<string, string>();
                        //在遍历Datatable的循环里创建DataTable
                        DataTable dtTargetData = new DataTable();
                        //获得DataTableName,如果没有定义表名,使用Table_intTableNumber
                        if (string.IsNullOrEmpty(nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText))
                        {
                            dtTargetData.TableName = string.Format("Table_") + intTableNumber;
                        }
                        else
                        {
                            dtTargetData.TableName = nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText;
                        }
                        //遍历DataTableField得到每个FieldName
                        XmlNodeList nodeListDataTableField = nodeDataTableInfo.SelectNodes("DataTableField");
                        foreach (XmlNode nodeDataTableField in nodeListDataTableField)
                        {
                            XmlNodeList nodeListField = nodeDataTableField.SelectNodes("Field");
                            foreach (XmlNode nodeField in nodeListField)
                            {
                                string strFieldName = "";
                                string strFieldType = "";
                                //遍历DataTableField获得每个FieldName
                                XmlNodeList nodeListFieldName = nodeField.SelectNodes("FieldName");
                                foreach (XmlNode nodeFieldName in nodeListFieldName)
                                {
                                    strFieldName = nodeFieldName.InnerText;
                                    dtTargetData.Columns.Add(nodeFieldName.InnerText);
                                }
                                XmlNodeList nodeListFieldType = nodeField.SelectNodes("FieldType");
                                foreach (XmlNode nodeFieldType in nodeListFieldType)
                                {
                                    strFieldType = nodeFieldType.InnerText;
                                }
                                dicFieldNameType.Add(strFieldName, strFieldType);
                            }
                        }
                        //遍历DataTableData获得每个DataRow
                        XmlNodeList nodeListDataTableData = nodeDataTableInfo.SelectNodes("DataTableData");
                        foreach (XmlNode nodeDataTableData in nodeListDataTableData)
                        {
                            //遍历DataRow获得每条数据
                            XmlNodeList nodeListDataRow = nodeDataTableData.SelectNodes("DataRow");
                            foreach (XmlNode nodeDataRow in nodeListDataRow)
                            {
                                DataRow drDataTable = dtTargetData.NewRow();
                                //遍历DataTableField得到每个FieldName
                                foreach (XmlNode nodeDataTableField in nodeListDataTableField)
                                {
                                    XmlNodeList nodeListField = nodeDataTableField.SelectNodes("Field");
                                    foreach (XmlNode nodeField in nodeListField)
                                    {
                                        //遍历DataTableField获得每个FieldName
                                        XmlNodeList nodeListFieldName = nodeField.SelectNodes("FieldName");
                                        foreach (XmlNode nodeFieldName in nodeListFieldName)
                                        {
                                            //根据遍历的字段类型,获得每个DataRow中的数据
                                            drDataTable[nodeFieldName.InnerText] = nodeDataRow.SelectSingleNode(nodeFieldName.InnerText).InnerText;
                                        }
                                    }
                                }
                                dtTargetData.Rows.Add(drDataTable);
                            }
                        }
                        //把每个DataTable存给DataSet、每个dicFieldNameType存给diclistFieldNameType
                        dsTargetData.Tables.Add(dtTargetData);
                        if (string.IsNullOrEmpty(nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText))
                        {
                            diclistFieldNameType.Add(string.Format("Table_") + intTableNumber, dicFieldNameType);
                            intTableNumber++;
                        }
                        else
                        {
                            diclistFieldNameType.Add(nodeDataTableInfo.SelectSingleNode("DataTableName").InnerText, dicFieldNameType);
                        }
                    }
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
