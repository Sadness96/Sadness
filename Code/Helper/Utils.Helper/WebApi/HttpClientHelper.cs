using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utils.Helper.TXT;

namespace Utils.Helper.WebApi
{
    /// <summary>
    /// HttpClient 帮助类
    /// 创建日期:2018年08月27日
    /// </summary>
    public class HttpClientHelper
    {
        /// <summary>
        /// 创建 Get 请求
        /// </summary>
        /// <param name="url">Api 访问地址</param>
        /// <param name="requestUrl">详细方法路径</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>Api 返回值</returns>
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
        /// 创建 Post 请求
        /// </summary>
        /// <param name="url">Api 访问地址</param>
        /// <param name="requestUrl">详细方法路径</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>Api 返回值</returns>
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

        /// <summary>
        /// 单文件下载
        /// </summary>
        /// <param name="baseAddress">Api 访问地址</param>
        /// <param name="requestUrl">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="saveFilePath">保存文件路径</param>
        /// <param name="progressAction">进度回调</param>
        /// <param name="token">Token认证</param>
        /// <returns>是否下载成功</returns>
        public static async Task<bool> DownloadAsync(Uri baseAddress, string requestUrl, IDictionary<string, string> parameters, string saveFilePath, Action<object, HttpProgressEventArgs> progressAction = null, string token = null)
        {
            ProgressMessageHandler progress = new ProgressMessageHandler();
            progress.HttpReceiveProgress += (s, e) =>
            {
                progressAction?.Invoke(s, e);
            };

            HttpClient httpClient = HttpClientFactory.Create(progress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                httpClient.Timeout = TimeSpan.FromMinutes(20);

                // 拼接参数
                StringBuilder builder = new StringBuilder();
                builder.Append(baseAddress);
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

                using (HttpResponseMessage response = await httpClient.GetAsync(builder.ToString()))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // 保存文件
                        using (FileStream fs = File.Create(saveFilePath))
                        {
                            Stream stream = await response.Content.ReadAsStreamAsync();
                            stream.CopyTo(fs);
                            stream.Close();
                            stream.Dispose();
                            fs.Close();
                            fs.Dispose();
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
            finally
            {
                httpClient.Dispose();
                httpClient = null;
                GC.Collect();
            }
        }
    }
}
