using NewsFeedApp;
using NewsFeedApp.Common;
using NewsFeedApp.Controllers;
using NewsFeedApp.Models;
using NewsFeedApp.Services;
using System;
using System.Collections.Generic;
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
            if (Session["UserId"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            var newsList = _service.GetAllNews();
            var model = new NewsViewModel { NewsList = newsList };
            model.UserId = Session["UserId"].ToString();
            _userId = model.UserId;
            return View("~/Views/News/ViewAllNews.cshtml", model);
        }

        [HttpGet]
        public ActionResult ViewAllNews()
        {
            if (Session["UserId"] == null)
            {
                return View("~/Views/Home/Index.cshtml");
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
        public ActionResult AddNews()
        {
            if (Session["UserId"] == null || Session["IsAdmin"].ToString() == "false")
            {
                return View("~/Views/Home/Index.cshtml");
            }
            ViewBag.Message = "Add News";
            var model = new AddNewsViewModel();
            //var categories = _service.GetCategories();
            //var categoryList = new List<SelectListItem>();
            //if (categories != null)
            //{
            //    var category = categories.Select(
            //        x => new SelectListItem()
            //        {
            //            Value = x.CategoryId.ToString(),
            //            Text = x.CategoryName
            //        }).ToList();
            //    categoryList.AddRange(category);
            //}

            model.CategoriesList = null;// categoryList;
            model.NewsModel = new News();
            _userId = Session["UserId"].ToString();
            //Session["UserId"] = _userId;
            model.UserId = _userId;

            return View(model);
        }

        [HttpPost]
        public JsonResult AddNews(AddNewsViewModel model)
        {
            if (Session["UserId"] == null || Session["IsAdmin"].ToString() == "false")
            {
                return Json(new { success = false, message = "Please login to Add News" }, JsonRequestBehavior.AllowGet);
            }
            if (model == null || model.NewsModel == null)
            {
                return Json(new { success = false, message = "Please enter Book information" }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(model.NewsModel.NewsTitle))
            {
                return Json(new { success = false, message = "Please enter Headline" }, JsonRequestBehavior.AllowGet);
            }

            _userId = Session["UserId"].ToString();
            //Session["UserId"] = _userId;
            model.UserId = _userId;

            model.NewsModel.AddUser = model.UserId;
            var response = _service.SaveNews(model.NewsModel);
            if (response)
            {
                return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Error occurred while saving News" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["UserId"] = null;

            return View("~/Views/Home/Index.cshtml");
        }
    }
}