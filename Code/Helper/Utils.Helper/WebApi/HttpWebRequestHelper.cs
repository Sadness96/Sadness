using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper.TXT;

namespace Utils.Helper.WebApi
{
    /// <summary>
    /// HttpWebRequest 帮助类
    /// 创建日期:2018年08月27日
    /// </summary>
    public class HttpWebRequestHelper
    {
        /// <summary>
        /// 创建 Get 请求
        /// </summary>
        /// <param name="url">Api 访问地址</param>
        /// <param name="requestUrl">详细方法路径</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>Api 返回值</returns>
        public static string CreateGetHttpWebRequest(string url, string requestUrl, IDictionary<string, string> parameters)
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(builder.ToString());
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// 创建 Get 请求
        /// </summary>
        /// <param name="url">Api 访问地址</param>
        /// <param name="requestUrl">详细方法路径</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="timout ">请求超时前等待的毫秒数,默认值是 100,000 毫秒（100 秒）</param>
        /// <returns>Api 返回值</returns>
        public static string CreateGetHttpWebRequest(string url, string requestUrl, IDictionary<string, string> parameters, Encoding encoding, int timout)
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(builder.ToString());
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = timout;
                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), encoding);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// 创建 Post 请求
        /// </summary>
        /// <param name="url">Api 访问地址</param>
        /// <param name="requestUrl">详细方法路径</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>Api返回值</returns>
        public static string CreatePostHttpWebRequest(string url, string requestUrl, IDictionary<string, string> parameters)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url + requestUrl) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                // 如果需要 POST 数据
                if (!(parameters == null || parameters.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                    byte[] data = Encoding.GetEncoding("utf-8").GetBytes(buffer.ToString());
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// 创建 Post 请求
        /// </summary>
        /// <param name="url">Api 访问地址</param>
        /// <param name="requestUrl">详细方法路径</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="timout ">请求超时前等待的毫秒数,默认值是 100,000 毫秒（100 秒）</param>
        /// <returns>Api 返回值</returns>
        public static string CreatePostHttpWebRequest(string url, string requestUrl, IDictionary<string, string> parameters, Encoding encoding, int timout)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url + requestUrl) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = timout;
                // 如果需要 POST 数据
                if (!(parameters == null || parameters.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                    byte[] data = encoding.GetBytes(buffer.ToString());
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return "";
            }
        }
    }
}
