using NewsFeedApp.Models;
using NewsFeedApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedApp.Controllers
{
    public class HomeController : BaseController
    {
        private IHomeService _service;
        public HomeController()
        {
            _service = new HomeService();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "Login";

            return View();
        }
        [HttpPost]
        public JsonResult Login(LoginViewModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Please enter username & password" }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(model.UserName))
            {
                return Json(new { success = false, message = "Please enter username" }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                return Json(new { success = false, message = "Please enter password" }, JsonRequestBehavior.AllowGet);
            }

            var response = _service.IsValidLogin(model.UserName, model.Password);
            if (response != null)
            {
                UserId = model.UserName;
                Session["UserId"] = UserId;
                Session["IsAdmin"] = response;
                return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Username or password is invalid" }, JsonRequestBehavior.AllowGet);
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