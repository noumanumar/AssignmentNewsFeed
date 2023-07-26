using NewsFeedApp.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace NewsFeedApp.Helper
{
    public class HttpClientService
    {
        static HttpClient _httpClient = new HttpClient(new HttpClientHandler { UseCookies = false });

        static HttpClientService()
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public ApiResponse<T> Get<T>(string uri) where T : class
        {
            return Send<T>(uri, HttpMethod.Get);
        }
        public ApiResponse<T> Post<T>(string uri, object input = null) //where T : BaseModel, new()
        {
            return Send<T>(uri, HttpMethod.Post, input);
        }

        private static ApiResponse<T> Send<T>(string uri, HttpMethod method, object input = null)
        {
            var userID = string.Empty;
            var request = new HttpRequestMessage(method, uri);
            if (input != null)
            {
                var content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                request.Content = content;
            };

            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "NewsFeedApp");

            var response = _httpClient.SendAsync(request).Result;
            var apiResponse = new ApiResponse<T>(response.StatusCode);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                apiResponse.Message = "Not Found";
            }
            else if (!response.IsSuccessStatusCode)
            {
                var message = response.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrEmpty(message))
                {
                    message = "Internal Server Error";
                }

                apiResponse.Message = message;
            }
            else
            {
                if (typeof(T) == typeof(byte[]))
                {
                    apiResponse.Model = (T)(object)response.Content.ReadAsByteArrayAsync().Result;
                }
                else if (typeof(T) == typeof(HtmlString))
                {
                    apiResponse.Model = (T)(object)new HtmlString(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    apiResponse.Model = JsonConvert.DeserializeObject<T>(result);
                }
            }
            return apiResponse;
        }
    }
}