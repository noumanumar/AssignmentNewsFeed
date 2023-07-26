using NewsFeedApp;
using NewsFeedApp.Common;
using NewsFeedApp.Controllers;
using NewsFeedApp.Models;
using NewsFeedApp.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedApplication.Controllers
{
    public class NewsController : BaseController
    {
        private INewsService _service;
        private string _userId;
        public NewsController()
        {
            _service = new NewsService();
        }
        public ActionResult Index()
        {
            //var newsList = _service.GetAllNews();
            var searchText = ConfigurationManager.AppSettings["DefaultSearchKeyword"].ToString();
            var rssFeeds = _service.GetRssFeeds(searchText);

            var model = new NewsViewModel 
            {
                //NewsList = newsList,
                RssFeeds = rssFeeds.ToList()
            };
            //model.UserId = Session["UserId"].ToString();
            //_userId = model.UserId;
            return View("~/Views/News/Index.cshtml", model);
        }

        [HttpGet]
        public ActionResult ViewAllNews()
        {
            if (Session["UserId"] == null)
            {
                return View("~/Views/News/Index.cshtml");
            }
            ViewBag.Message = "ViewAllNews";

            var newsList = _service.GetAllNews();
            var model = new NewsViewModel { NewsList = newsList };
            _userId = Session["UserId"].ToString();
            //Session["UserId"] = _userId;
            model.UserId = _userId;
            return View(model);
        }

        [HttpGet]
        public JsonResult SearchNews(string searchText = "")
        {
            if (string.IsNullOrEmpty(searchText))
            {
                searchText = ConfigurationManager.AppSettings["DefaultSearchKeyword"].ToString();
            }

            List<RssFeed> rssFeed = null;
            try
            {
                rssFeed = _service.GetRssFeeds(searchText);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = "Exception occurred" }, JsonRequestBehavior.AllowGet);

            }
            //var response = _service.SaveNews(model.NewsModel);

            if (rssFeed.Any())
            {
                return Json(new { success = true, total = 0, message = "Success", data = rssFeed }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "No data" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}