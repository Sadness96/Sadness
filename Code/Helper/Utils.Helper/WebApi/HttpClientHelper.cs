using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper.TXT;

namespace Utils.Helper.WebApi
{
    /// <summary>
    /// HttpClient帮助类
    /// 创建日期:2018年8月27日
    /// </summary>
    public class HttpClientHelper
    {
        /// <summary>
        /// 创建Get请求
        /// </summary>
        /// <param name="url">Api访问地址</param>
        /// <param name="requestUrl">详细方法路径</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>Api返回值</returns>
        public static string CreateGetHttpClient(string url, string requestUrl, IDictionary<string, string> parameters)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(url);
                builder.Append(requestUrl);
                if (parameters != null && parameters.Count >= 1)
                {
                    builder.Append("?");
                    int i = 0;
                    foreach (var item in parameters)
                    {
                        if (i > 0)
                        {
                            builder.Append("&");
                        }
                        builder.AppendFormat("{0}={1}", item.Key, item.Value);
                        i++;
                    }
                }
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(url);
                var result = httpClient.GetAsync(builder.ToString()).Result;
                return result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// 创建Post请求
        /// </summary>
        /// <param name="url">Api访问地址</param>
        /// <param name="requestUrl">详细方法路径</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>Api返回值</returns>
        public static string CreatePostHttpClient(string url, string requestUrl, IDictionary<string, string> parameters)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(url);
                var result = httpClient.PostAsync(requestUrl, new FormUrlEncodedContent(parameters)).Result;
                return result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return "";
            }
        }
    }
}
