using NewsFeedApp.Common;
using NewsFeedApp.Helper;
using NewsFeedApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace NewsFeedApp.Services
{
    public class NewsService : HttpClientService, INewsService
    {

        //TODO: Implement inversion of control

        private string apiUrl = "http://localhost:50767/";

        private static ApiResponse<T> Send<T>(string uri, HttpMethod method, object input = null)
        {
            var request = new HttpRequestMessage(method, uri);
            if (input != null)
            {
                var content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                request.Content = content;
            };

            //request.Headers.Add("lang", "en-US");

            //request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));

            HttpClient _httpClient = new HttpClient(new HttpClientHandler { UseCookies = false });

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "NewsFeedApp");

            var response = _httpClient.SendAsync(request).Result;
            var apiResponse = new ApiResponse<T>(response.StatusCode);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                apiResponse.Message = "PageNotFound";
            }
            else if (!response.IsSuccessStatusCode)
            {
                var message = response.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrEmpty(message))
                {
                    message = "InternalServerError";
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
        public List<News> GetAllNews()
        {
            var url = apiUrl + "/api/news/getallnews/";
            var response = Get<List<News>>(url);
            if (response != null && response.IsSuccessful && response.Model != null)
            {
                return response.Model;
            }

            return null;
        }

        public List<RssFeed> GetRssFeeds(string searchText)
        {
            WebClient wclient = new WebClient();
            var apiKey = ConfigurationManager.AppSettings["NewApiKey"].ToString();
            var dateFrom = Convert.ToInt32(ConfigurationManager.AppSettings["SearchDateFromDays"].ToString());
            var from = DateTime.Now.AddDays(-dateFrom).ToString("yyyy-MM-dd");
            var url = "https://newsapi.org/v2/everything?q=" + searchText + "&from=" + from + "&sortBy=publishedAt&apiKey=" + apiKey;
            NewsApiResponse response = null;
            try
            {
                //string rssData = wclient.DownloadString(url);
                var data = Send<dynamic>(url, HttpMethod.Get);
                if (data.IsSuccessful)
                {
                    var items = data.Model;
                    string json = JsonConvert.SerializeObject(items);
                    response = JsonConvert.DeserializeObject<NewsApiResponse>(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
            var rssFeedData = new List<RssFeed>();

            if (response != null && response.articles.Any())
            {
                foreach (var x in response.articles)
                {
                    var data = new RssFeed
                    {
                        Author = x.author,
                        Description = x.description,
                        Heading = x.title,
                        Image = x.urlToImage,
                        Link = x.url,
                        PublishDate = x.publishedAt,
                        Content = x.content
                    };
                    rssFeedData.Add(data);
                }
            }

            return rssFeedData;
        }

        public bool SaveNews(News newsDetails)
        {
            var url = apiUrl + "/api/news/savenews";
            var response = Post<bool>(url, newsDetails);

            if (response != null && response.IsSuccessful && response.Model == true)
            {
                return response.Model;
            }

            return false;
        }
        public List<Categories> GetCategories()
        {
            var url = apiUrl + "/api/book/getallcategories";
            var response = Get<List<Categories>>(url);
            if (response != null && response.IsSuccessful && response.Model != null)
            {
                return response.Model;
            }

            return null;
        }
        public News GetNewsInfoById(int newsId)
        {
            var url = apiUrl + string.Format("/api/News/getnewsinfobyid/{0}", newsId);
            var response = Get<News>(url);
            if (response != null && response.IsSuccessful && response.Model != null)
            {
                return response.Model;
            }
            return null;
        }
    }
}