using NewsFeedApp.Models;
using NewsFeedApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedApp.Controllers
{
    public class BaseController : Controller
    {
        public string UserId { get; set; }
        public BaseController()
        {
        }
    }
}