using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NPOI.Helper.TXT;
using NPOI.XWPF.UserModel;
using NPOI.OpenXmlFormats.Dml.WordProcessing;
using NPOI.OpenXmlFormats.Dml;
using Novacode;

namespace NPOI.Helper.Word
{
    /// <summary>
    /// Word帮助类
    /// 创建日期:2017年6月2日
    /// </summary>
    public class WordHelper
    {
        /// <summary>
        /// 创建Word(Office2003)
        /// </summary>
        /// <param name="strDataSourcePath">新建Word的路径.doc</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateWord_Office2003(string strDataSourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath))
                {
                    return false;
                }
                XWPFDocument Word2003 = new XWPFDocument();
                FileStream fileStream2003 = new FileStream(Path.ChangeExtension(strDataSourcePath, "doc"), FileMode.Create);
                Word2003.Write(fileStream2003);
                fileStream2003.Close();
                Word2003.Close();
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 创建Word(Office2007)
        /// </summary>
        /// <param name="strDataSourcePath">新建Word的路径.doc</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool CreateWord_Office2007(string strDataSourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath))
                {
                    return false;
                }
                XWPFDocument Word2007 = new XWPFDocument();
                FileStream fileStream2007 = new FileStream(Path.ChangeExtension(strDataSourcePath, "docx"), FileMode.Create);
                Word2007.Write(fileStream2007);
                fileStream2007.Close();
                Word2007.Close();
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获得Word文档中所有段落
        /// </summary>
        /// <param name="strDataSourcePath">Word文件路径</param>
        /// <returns>段落标签List</returns>
        public static List<string> GetWordParagraphText(string strDataSourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath))
                {
                    return null;
                }
                List<string> listParagraphText = new List<string>();
                FileStream fileStream = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                XWPFDocument wordDocument = new XWPFDocument(fileStream);
                foreach (XWPFParagraph wordParagraph in wordDocument.Paragraphs)
                {
                    listParagraphText.Add(wordParagraph.ParagraphText);
                }
                return listParagraphText;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得Word文档中所有表格
        /// </summary>
        /// <param name="strDataSourcePath">Word文件路径</param>
        /// <returns>段落标签List</returns>
        public static List<string> GetWordTableText(string strDataSourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath))
                {
                    return null;
                }
                List<string> listTableText = new List<string>();
                FileStream fileStream = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                XWPFDocument wordDocument = new XWPFDocument(fileStream);
                foreach (XWPFTable wordTable in wordDocument.Tables)
                {
                    foreach (XWPFTableRow wordTableRow in wordTable.Rows)
                    {
                        foreach (XWPFTableCell wordTableCell in wordTableRow.GetTableCells())
                        {
                            foreach (XWPFParagraph wordParagraph in wordTableCell.Paragraphs)
                            {
                                listTableText.Add(wordParagraph.ParagraphText);
                            }
                        }
                    }
                }
                return listTableText;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得Word文档中所有文本
        /// </summary>
        /// <param name="strDataSourcePath">Word文件路径</param>
        /// <returns>所有文本List</returns>
        public static List<string> GetWordAllText(string strDataSourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath))
                {
                    return null;
                }
                List<string> listAllText = new List<string>();
                List<string> listParagraphText = GetWordParagraphText(strDataSourcePath);
                List<string> listTableText = GetWordTableText(strDataSourcePath);
                if (listParagraphText != null && listParagraphText.Count >= 1)
                {
                    listAllText = listAllText.Union(listParagraphText).ToList<string>();
                }
                if (listTableText != null && listTableText.Count >= 1)
                {
                    listAllText = listAllText.Union(listTableText).ToList<string>();
                }
                return listAllText;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获得Word文档中所有替换标签('{标签}','#标签#')
        /// </summary>
        /// <param name="strDataSourcePath">Word文件路径</param>
        /// <returns>替换标签Dictionary(带标签,不带标签)</returns>
        public static Dictionary<string, string> GetWordAllLable(string strDataSourcePath)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath))
                {
                    return null;
                }
                Dictionary<string, string> dicAllLabel = new Dictionary<string, string>();
                List<string> listAllText = GetWordAllText(strDataSourcePath);
                foreach (string strAllText in listAllText)
                {
                    Dictionary<string, string> dicMatchingLabelK = MatchingReplaceLabel(strAllText, "{", "}");
                    Dictionary<string, string> dicMatchingLabelS = MatchingReplaceLabel(strAllText, "#", "#");
                    if (dicMatchingLabelK != null && dicMatchingLabelK.Count >= 1)
                    {
                        dicAllLabel = dicAllLabel.Union(dicMatchingLabelK).ToDictionary(k => k.Key, v => v.Value);
                    }
                    if (dicMatchingLabelS != null && dicMatchingLabelS.Count >= 1)
                    {
                        dicAllLabel = dicAllLabel.Union(dicMatchingLabelS).ToDictionary(k => k.Key, v => v.Value);
                    }
                }
                return dicAllLabel;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 匹配替换标签
        /// </summary>
        /// <param name="strText">需要匹配的文本</param>
        /// <param name="strStartSymbol">起始符号</param>
        /// <param name="strStopSymbol">终止符号</param>
        /// <returns>匹配成功的标签(带标签,不带标签)</returns>
        public static Dictionary<string, string> MatchingReplaceLabel(string strText, string strStartSymbol, string strStopSymbol)
        {
            try
            {
                if (string.IsNullOrEmpty(strText))
                {
                    return null;
                }
                Dictionary<string, string> dicMatchingLabel = new Dictionary<string, string>();
                string strRegularExpression = string.Format(@"[^{0}]+(?=\{1})", strStartSymbol, strStopSymbol);
                foreach (Match matchText in Regex.Matches(strText, strRegularExpression))
                {
                    if (!dicMatchingLabel.ContainsKey(matchText.Value))
                    {
                        dicMatchingLabel.Add(string.Format("{0}{1}{2}", strStartSymbol, matchText.Value, strStopSymbol), string.Format("{0}", matchText.Value));
                    }
                }
                return dicMatchingLabel;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 替换文本标签
        /// </summary>
        /// <param name="strDataSourcePath">Word文件路径</param>
        /// <param name="strLabelName">标签名称(带标签符号)</param>
        /// <param name="strReplaceLabel">替换标签文本</param>
        /// <returns>成功返回替换数量,失败返回-1</returns>
        public static int ReplaceTextLabel(string strDataSourcePath, string strLabelName, string strReplaceLabel)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath) || string.IsNullOrEmpty(strLabelName) || string.IsNullOrEmpty(strReplaceLabel))
                {
                    return -1;
                }
                int iNumber = 0;
                FileStream fileStreamOpen = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                XWPFDocument wordDocument = new XWPFDocument(fileStreamOpen);
                foreach (XWPFParagraph wordParagraph in wordDocument.Paragraphs)
                {
                    if (wordParagraph.ParagraphText.IndexOf(strLabelName) >= 0)
                    {
                        string strReplaceTextLabel = wordParagraph.ParagraphText.Replace(strLabelName, strReplaceLabel);
                        foreach (XWPFRun wordRun in wordParagraph.Runs)
                        {
                            wordRun.SetText(string.Empty, 0);
                        }
                        wordParagraph.CreateRun().SetText(strReplaceTextLabel, 0);
                        iNumber++;
                    }
                }
                foreach (XWPFTable wordTable in wordDocument.Tables)
                {
                    foreach (XWPFTableRow wordTableRow in wordTable.Rows)
                    {
                        foreach (XWPFTableCell wordTableCell in wordTableRow.GetTableCells())
                        {
                            foreach (XWPFParagraph wordParagraph in wordTableCell.Paragraphs)
                            {
                                if (wordParagraph.ParagraphText.IndexOf(strLabelName) >= 0)
                                {
                                    string strReplaceTextLabel = wordParagraph.ParagraphText.Replace(strLabelName, strReplaceLabel);
                                    foreach (XWPFRun wordRun in wordParagraph.Runs)
                                    {
                                        wordRun.SetText(string.Empty, 0);
                                    }
                                    wordParagraph.CreateRun().SetText(strReplaceTextLabel, 0);
                                    iNumber++;
                                }
                            }
                        }
                    }
                }
                FileStream fileStreamSave = new FileStream(strDataSourcePath, FileMode.Create);
                wordDocument.Write(fileStreamSave);
                fileStreamSave.Close();
                wordDocument.Close();
                return iNumber;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 替换表格标签(DataTable替换)
        /// </summary>
        /// <param name="strDataSourcePath">Word文件路径</param>
        /// <param name="strLabelName">标签名称(带标签符号)</param>
        /// <param name="dtReplaceLabel">替换标签DataTable</param>
        /// <returns>成功返回1,失败返回-1</returns>
        public static int ReplaceDataTableLabel(string strDataSourcePath, string strLabelName, DataTable dtReplaceLabel)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath) || string.IsNullOrEmpty(strLabelName) || dtReplaceLabel == null || dtReplaceLabel.Rows.Count < 1)
                {
                    return -1;
                }
                FileStream fileStreamOpen = new FileStream(strDataSourcePath, FileMode.Open, FileAccess.Read);
                XWPFDocument wordDocument = new XWPFDocument(fileStreamOpen);
                int iLableRowPosition = -1;
                int iLableCellPosition = -1;
                foreach (XWPFTable wordTable in wordDocument.Tables)
                {
                    for (int iTableRow = 0; iTableRow < wordTable.Rows.Count; iTableRow++)
                    {
                        for (int iTableCell = 0; iTableCell < wordTable.Rows[iTableRow].GetTableCells().Count; iTableCell++)
                        {
                            foreach (XWPFParagraph wordParagraph in wordTable.Rows[iTableRow].GetTableCells()[iTableCell].Paragraphs)
                            {
                                if (wordParagraph.ParagraphText.IndexOf(strLabelName) >= 0)
                                {
                                    if (iLableRowPosition < 0 && iLableCellPosition < 0)
                                    {
                                        iLableRowPosition = iTableRow;
                                        iLableCellPosition = iTableCell;
                                    }
                                }
                                if (iLableRowPosition >= 0 && iLableCellPosition >= 0)
                                {
                                    int iCurrentRow = iTableRow - iLableRowPosition;
                                    int iCurrentCell = iTableCell - iLableCellPosition;
                                    if ((iCurrentRow < dtReplaceLabel.Rows.Count && iCurrentRow >= 0) && (iCurrentCell < dtReplaceLabel.Columns.Count && iCurrentCell >= 0))
                                    {
                                        foreach (XWPFRun wordRun in wordParagraph.Runs)
                                        {
                                            wordRun.SetText(string.Empty, 0);
                                        }
                                        wordParagraph.CreateRun().SetText(dtReplaceLabel.Rows[iCurrentRow][iCurrentCell].ToString(), 0);
                                    }
                                }
                            }
                        }
                    }
                }
                FileStream fileStreamSave = new FileStream(strDataSourcePath, FileMode.Create);
                wordDocument.Write(fileStreamSave);
                fileStreamSave.Close();
                wordDocument.Close();
                return 1;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 替换图片标签(使用DocX.dll类库,调用这个方法后NPOI无法读取文档)
        /// </summary>
        /// <param name="strDataSourcePath">Word文件路径</param>
        /// <param name="strLabelName">标签名称(带标签符号)</param>
        /// <param name="strImagePath">替换的图片路径</param>
        /// <param name="iImageWidth">替换的图片宽度(小于0则显示原图宽度)</param>
        /// <param name="iImageHeight">替换的图片高度(小于0则显示原图高度)</param>
        /// <returns>成功返回替换数量,失败返回-1</returns>
        public static int ReplaceImageLabel(string strDataSourcePath, string strLabelName, string strImagePath, int iImageWidth, int iImageHeight)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath) || string.IsNullOrEmpty(strLabelName) || string.IsNullOrEmpty(strImagePath) || !File.Exists(strImagePath))
                {
                    return -1;
                }
                int iNumber = 0;
                //使用DocX.dll类库
                DocX mDocX = DocX.Load(strDataSourcePath);
                //遍历段落
                foreach (Paragraph wordParagraph in mDocX.Paragraphs)
                {
                    if (wordParagraph.Text.IndexOf(strLabelName) >= 0)
                    {
                        //添加图片
                        Novacode.Image pImag = mDocX.AddImage(strImagePath);
                        Picture pPicture = pImag.CreatePicture();
                        //如果传入宽度小于0,则以原始大小插入
                        if (iImageWidth >= 0)
                        {
                            pPicture.Width = iImageWidth;
                        }
                        //如果传入高度小于0,则以原始大小插入
                        if (iImageHeight >= 0)
                        {
                            pPicture.Height = iImageHeight;
                        }
                        //将图像插入到段落后面
                        wordParagraph.InsertPicture(pPicture);
                        //清空文本(清空放在前面会导致替换失败文字消失)
                        wordParagraph.ReplaceText(strLabelName, string.Empty);
                        iNumber++;
                    }
                }
                mDocX.SaveAs(strDataSourcePath);
                return iNumber;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 替换Word文档标签
        /// </summary>
        /// <param name="strDataSourcePath">Word文件路径</param>
        /// <param name="listReplaceLabel">替换标签对应的替换内容(不带标签,替换内容Model)</param>
        /// <returns>成功返回替换行数,失败返回-1</returns>
        public static int ReplaceLabel(string strDataSourcePath, List<ReplaceLabelModel> listReplaceLabel)
        {
            try
            {
                if (string.IsNullOrEmpty(strDataSourcePath) || !File.Exists(strDataSourcePath) || listReplaceLabel == null || listReplaceLabel.Count < 1)
                {
                    return -1;
                }
                int iNumber = 0;
                //由于替换完图片之后会导致NPOI无法读取,所以暂时保存替换图片的标签
                Dictionary<string, ReplaceLabelModel> dicReplaceImageLabel = new Dictionary<string, ReplaceLabelModel>();
                //遍历文档中的标签(先替换文本和表格)
                foreach (var vAllLabel in GetWordAllLable(strDataSourcePath))
                {
                    //获得与文档中匹配的标签
                    List<ReplaceLabelModel> listMatchingLabel = listReplaceLabel.Where(x => x.strLabelName == vAllLabel.Value).ToList();
                    if (listMatchingLabel.Count >= 1)
                    {
                        //使用第一个匹配到的标签替换
                        ReplaceLabelModel ReplaceLabel = listMatchingLabel[0];
                        if (ReplaceLabel.lableType == ReplaceLabelType.LabelType.Text)
                        {
                            ReplaceTextLabel(strDataSourcePath, vAllLabel.Key, ReplaceLabel.strReplaceText);
                            iNumber++;
                        }
                        else if (ReplaceLabel.lableType == ReplaceLabelType.LabelType.DataTable)
                        {
                            ReplaceDataTableLabel(strDataSourcePath, vAllLabel.Key, ReplaceLabel.dtReplaceData);
                            iNumber++;
                        }
                        else if (ReplaceLabel.lableType == ReplaceLabelType.LabelType.Image)
                        {
                            dicReplaceImageLabel.Add(vAllLabel.Key, ReplaceLabel);
                        }
                    }
                }
                //替换图片,这里储存的都是确定Word中存在标签
                foreach (var vReplaceLabel in dicReplaceImageLabel)
                {
                    //默认原图大小,替换前修改图片大小
                    ReplaceImageLabel(strDataSourcePath, vReplaceLabel.Key, vReplaceLabel.Value.strReplaceImagePath, -1, -1);
                    iNumber++;
                }
                return iNumber;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return -1;
            }
        }
    }
}
