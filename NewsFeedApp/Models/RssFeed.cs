using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsFeedApp.Models
{
    public class NewsApiResponse
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<Articles> articles { get; set; }

    }
    public class Articles
    {
        public Source source { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string content { get; set; }
    }
    public class Source
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class RssFeed
    {
        public string Name { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime PublishDate { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
    }
    public class NewsFeeds
    {
        public List<RssFeed> RssFeeds { get; set; }
    }
}