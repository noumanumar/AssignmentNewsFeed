using NewsFeedApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedApp.Models
{
    public class NewsViewModel
    {
        public List<News> NewsList { get; set; }
        public string UserId { get; set; }
    }

    public class AddNewsViewModel
    {
        public string UserId { get; set; }
        public News NewsModel { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
    }   
}