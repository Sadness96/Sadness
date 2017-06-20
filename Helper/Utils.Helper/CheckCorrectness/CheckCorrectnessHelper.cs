using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Utils.Helper.TXT;

namespace Utils.Helper.CheckCorrectness
{
    /// <summary>
    /// 效验正确性帮助类
    /// 创建日期:2017年6月18日
    /// </summary>
    public class CheckCorrectnessHelper
    {
        /// <summary>
        /// 效验中国大陆手机号码
        /// </summary>
        /// <param name="strPhoneNumber">中国大陆手机号码</param>
        /// <returns>效验通过返回true,失败返回false</returns>
        public static bool CheckPhoneNumber(string strPhoneNumber)
        {
            try
            {
                //+86替换成空(只考虑中国大陆手机号)
                if (strPhoneNumber.Length == 14)
                {
                    strPhoneNumber.Replace("+86", string.Empty);
                }
                //中国电信正则表达式匹配
                string strRegexChinaTelecom = @"^1[3578][01379]\d{8}$";
                Regex regexChinaTelecom = new Regex(strRegexChinaTelecom);
                //中国移动正则表达式匹配
                string strRegexChinaMobile = @"^(134[012345678]\d{7}|1[34578][012356789]\d{8})$";
                Regex regexChinaMobile = new Regex(strRegexChinaMobile);
                //中国联通正则表达式匹配
                string strRegexChinaUnicom = @"^1[34578][01256]\d{8}$";
                Regex regexChinaUnicom = new Regex(strRegexChinaUnicom);
                //验证手机号
                if (regexChinaTelecom.IsMatch(strPhoneNumber) || regexChinaMobile.IsMatch(strPhoneNumber) || regexChinaUnicom.IsMatch(strPhoneNumber))
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
        /// 效验E-Mail
        /// </summary>
        /// <param name="strEMail">E-Mail</param>
        /// <returns>效验通过返回true,失败返回false</returns>
        public static bool CheckEMail(string strEMail)
        {
            try
            {
                //邮箱正则表达式匹配
                string strRegexEMail = @"^\s*([A-Za-z0-9_-]+(\.\w+)*@(\w+\.)+\w{2,5})\s*$";
                Regex regexEMail = new Regex(strRegexEMail);
                if (regexEMail.IsMatch(strEMail))
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
        /// 效验身份证号码
        /// </summary>
        /// <param name="strIDNumber">身份证号码</param>
        /// <returns>效验通过返回true,失败返回false</returns>
        public static bool CheckIDNumber(string strIDNumber)
        {
            try
            {
                if (strIDNumber.Length == 15 && CheckIDCard15(strIDNumber))
                {
                    return true;
                }
                else if (strIDNumber.Length == 18 && CheckIDCard18(strIDNumber))
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
        /// 15位身份证号验证
        /// </summary>
        /// <param name="idNumber">身份证号</param>
        /// <returns>效验通过返回true,失败返回false</returns>
        private static bool CheckIDCard15(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            return true;//符合15位身份证标准  
        }

        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        /// <param name="idNumber">身份证号</param>  
        /// <returns>效验通过返回true,失败返回false</returns>  
        private static bool CheckIDCard18(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证    
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证    
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证    
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            Console.WriteLine("Y的理论值: " + y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证    
            }
            return true;//符合GB11643-1999标准    
        }
    }
}
