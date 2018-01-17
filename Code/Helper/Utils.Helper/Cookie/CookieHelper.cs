using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Utils.Helper.TXT;

namespace Utils.Helper.Cookie
{
    /// <summary>
    /// 浏览器缓存帮助类
    /// 创建日期:2017年7月31日
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="strCookieName">Cookie名</param>
        /// <param name="strCookieValue">Cookie值</param>
        /// <param name="dtExpires">到期时间(如果为空默认为7天)</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool SetCookie(string strCookieName, string strCookieValue, DateTime dtExpires)
        {
            try
            {
                if (string.IsNullOrEmpty(strCookieName) || string.IsNullOrEmpty(strCookieValue))
                {
                    return false;
                }
                //如果指定Cookie不存在则创建,存在则修改
                if (HttpContext.Current.Request.Cookies[strCookieName] == null)
                {
                    HttpCookie httpCookie = new HttpCookie(strCookieName);
                    httpCookie.Value = HttpUtility.UrlEncode(strCookieValue);
                    httpCookie.Expires = dtExpires;
                    HttpContext.Current.Response.Cookies.Add(httpCookie);
                }
                else
                {
                    ModifyCookie(strCookieName, strCookieValue);
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
        /// 添加带有子键的Cookie
        /// </summary>
        /// <param name="strCookieName">Cookie名</param>
        /// <param name="dicCookieValue">Cookie子键值</param>
        /// <param name="dtExpires">到期时间(如果为空默认为7天)</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool SetCookie(string strCookieName, Dictionary<string, string> dicCookieValue, DateTime dtExpires)
        {
            try
            {
                if (string.IsNullOrEmpty(strCookieName) || dicCookieValue.Count < 1)
                {
                    return false;
                }
                //如果指定Cookie不存在则创建,存在则修改
                if (HttpContext.Current.Request.Cookies[strCookieName] == null)
                {
                    HttpCookie httpCookie = new HttpCookie(strCookieName);
                    foreach (var vCookieValue in dicCookieValue)
                    {
                        httpCookie[vCookieValue.Key] = HttpUtility.UrlEncode(vCookieValue.Value);
                    }
                    httpCookie.Expires = dtExpires;
                    HttpContext.Current.Response.Cookies.Add(httpCookie);
                }
                else
                {
                    foreach (var vCookieValue in dicCookieValue)
                    {
                        ModifyCookie(strCookieName, vCookieValue.Key, vCookieValue.Value);
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
        /// 获取Cookie
        /// </summary>
        /// <param name="strCookieName">Cookie名</param>
        /// <returns>指定Cookie的Value值</returns>
        public static string GetCookie(string strCookieName)
        {
            try
            {
                if (string.IsNullOrEmpty(strCookieName) || HttpContext.Current.Request.Cookies[strCookieName] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName].Value);
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取子键Cookie
        /// </summary>
        /// <param name="strCookieName">Cookie名</param>
        /// <param name="strCookieSubkey">Cookie子键名</param>
        /// <returns>指定Cookie子键的Value值</returns>
        public static string GetCookie(string strCookieName, string strCookieSubkey)
        {
            try
            {
                if (string.IsNullOrEmpty(strCookieName) || string.IsNullOrEmpty(strCookieSubkey) || HttpContext.Current.Request.Cookies[strCookieName] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName].Values[strCookieSubkey]);
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return string.Empty;
            }
        }

        /// <summary>
        /// 修改指定Cookie
        /// </summary>
        /// <param name="strCookieName">Cookie名</param>
        /// <param name="strCookieValue">Cookie值</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool ModifyCookie(string strCookieName, string strCookieValue)
        {
            try
            {
                if (string.IsNullOrEmpty(strCookieName) || string.IsNullOrEmpty(strCookieValue))
                {
                    return false;
                }
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strCookieName];
                if (httpCookie != null)
                {
                    httpCookie.Value = strCookieValue;
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
        /// 修改指定子键Cookie
        /// </summary>
        /// <param name="strCookieName">Cookie名</param>
        /// <param name="strCookieSubkey">Cookie子键名</param>
        /// <param name="strCookieValue">Cookie子键值</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool ModifyCookie(string strCookieName, string strCookieSubkey, string strCookieValue)
        {
            try
            {
                if (string.IsNullOrEmpty(strCookieName) || string.IsNullOrEmpty(strCookieValue))
                {
                    return false;
                }
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strCookieName];
                if (httpCookie != null)
                {
                    httpCookie.Values[strCookieSubkey] = strCookieValue;
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
        /// 移除所有Cookie
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool RemoveAllCookie()
        {
            try
            {
                HttpCookieCollection httpCCollection = HttpContext.Current.Request.Cookies;
                foreach (string strKeys in httpCCollection.AllKeys)
                {
                    RemoveCookie(strKeys);
                }
                HttpContext.Current.Request.Cookies.Clear();
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 移除指定Cookie(测试无效)
        /// </summary>
        /// <param name="strCookieName">Cookie名</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool RemoveCookie(string strCookieName)
        {
            try
            {
                if (string.IsNullOrEmpty(strCookieName))
                {
                    return false;
                }
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strCookieName];
                if (httpCookie != null)
                {
                    httpCookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Set(httpCookie);
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
    }
}
