using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ADO.Helper.DatabaseConversion
{
    /// <summary>
    /// Sql语句处理类
    /// 创建日期:2017年4月24日
    /// </summary>
    public class SqlProcessing
    {
        /// <summary>
        /// 去除非法字符'\\ufeff'
        /// </summary>
        /// <param name="strSource">数据源</param>
        /// <returns>修正后的字符</returns>
        public static string RemoveIllegal(string strSource)
        {
            return UnicodeToString(StringToUnicode(strSource));
        }

        /// <summary>
        /// String转Unicode,并去除'\\ufeff'非法字符
        /// </summary>
        /// <param name="strSource">数据源</param>
        /// <returns>Unicode编码字符</returns>
        public static string StringToUnicode(string strSource)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //先把字符串转换成 UTF-16 的Btye数组
            byte[] bytes = Encoding.Unicode.GetBytes(strSource);
            for (int i = 0; i < bytes.Length; i += 2)
            {
                //根据Unicode规则，每两个byte表示一个汉字，并且后前顺序，英文前面补00
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            //去掉'?'的Unicode码,?=003f,Unicode以\u开头,\\为转义\
            return stringBuilder.Replace("\\ufeff", string.Empty).ToString();
        }

        /// <summary>
        /// Unicode转String
        /// </summary>
        /// <param name="strSource">数据源</param>
        /// <returns>String类型编码字符</returns>
        public static string UnicodeToString(string strSource)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(strSource, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
    }
}
