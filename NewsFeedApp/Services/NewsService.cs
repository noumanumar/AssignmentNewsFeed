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
            var apiKey = ConfigurationManager.AppSettings["NewApiKey"].ToString();
            var dateFrom = Convert.ToInt32(ConfigurationManager.AppSettings["SearchDateFromDays"].ToString());
            var from = DateTime.Now.AddDays(-dateFrom).ToString("yyyy-MM-dd");
            var url = "https://newsapi.org/v2/everything?q=" + searchText + "&from=" + from + "&sortBy=publishedAt&apiKey=" + apiKey;
            NewsApiResponse response = null;
            try
            {
                var httpClient = new HttpClientService();
                var data = httpClient.Get<dynamic>(url);
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

            List<RssFeed> rssFeedData = MapNewsData(response);

            return rssFeedData;
        }

        private List<RssFeed> MapNewsData(NewsApiResponse response)
        {
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
                        PublishDate = x.publishedAt.ToString("MMMM dd, yyyy"),
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