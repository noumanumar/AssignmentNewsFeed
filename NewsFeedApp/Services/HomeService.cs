using NewsFeedApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsFeedApp.Services
{
    public class HomeService : HttpClientService, IHomeService
    {
        private string apiUrl = "http://localhost:50767/";

        public string IsValidLogin(string userId, string password)
        {
            var url = apiUrl + string.Format("/api/home/IsValidLogin/{0}/{1}", userId, password);
            var response = Get<string>(url);
            if (response != null && response.IsSuccessful)
            {
                return response.Model;
            }

            return null;
        }
    }
}