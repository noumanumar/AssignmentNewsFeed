using NewsFeedApp.Common;
using NewsFeedApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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