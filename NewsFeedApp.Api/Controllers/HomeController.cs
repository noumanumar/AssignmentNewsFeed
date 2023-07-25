using NewsFeedApp.Data;
using NewsFeedApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace NewsFeedApp.Api.Controllers
{
    public class HomeController : ApiController
    {
        private IHomeRepository _repository;
        public HomeController()
        {
            _repository = new HomeRepository();
        }

        [HttpGet]
        [Route("~/api/home/IsValidLogin/{userId}/{password}")]
        public IHttpActionResult IsValidLogin(string userId, string password)
        {
            bool? response = _repository.IsLoginUserValid(userId, password);
            var isAdmin = false;
            if (response != null && response == true)
            {
                isAdmin = _repository.IsUserAdmin(userId);
            }
            response = response != null && response == true ? isAdmin : (bool?)null;
            return Ok(response);
        }
    }
}
