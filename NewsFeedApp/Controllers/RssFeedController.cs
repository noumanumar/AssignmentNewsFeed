using NewsFeedApp;
using NewsFeedApp.Common;
using NewsFeedApp.Controllers;
using NewsFeedApp.Models;
using NewsFeedApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace NewsFeedApplication.Controllers
{
    public class RssFeedController : BaseController
    {
        private INewsService _service;
        private string _userId;
        public RssFeedController()
        {
            _service = new NewsService();
        }
        public ActionResult Index()
        {
            WebClient wclient = new WebClient();
            string RSSData = wclient.DownloadString("https://newsapi.org/");

            XDocument xml = XDocument.Parse(RSSData);
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RssFeed
                               {
                                   Heading = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Details = ((string)x.Element("description")),
                                   PublishDate = ((string)x.Element("pubDate")),
                                   Image = ((string)x.Element("image"))
                               });
            ViewBag.RSSFeed = RSSFeedData;
            //ViewBag.URL = RSSURL;
            return View();
        }
       
    }
}