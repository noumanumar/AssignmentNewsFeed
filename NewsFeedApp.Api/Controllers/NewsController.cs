using NewsFeedApp.Common;
using NewsFeedApp.Data;
using NewsFeedApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace NewsFeedApp.Api.Controllers
{
    public class NewsController : ApiController
    {
        private INewsRepository _repository;
        public NewsController()
        {
            _repository = new NewsRepository();
        }

        [HttpGet]
        [Route("~/api/news/getallnews/")]
        public IHttpActionResult GetAllBooks()
        {
            var response = _repository.GetAllNews();
            return Ok(response);
        }

        //[HttpPost]
        //[Route("~/api/news/savenews")]
        //public IHttpActionResult SaveNews(News newsDetails)
        //{
        //    var response = _repository.SaveNews(newsDetails);
        //    return Ok(response);
        //}
       
        [HttpGet]
        [Route("~/api/news/getnewsinfobyid/{newsId}")]
        public IHttpActionResult GetNewsInfoById(int newsId)
        {
            var response = _repository.GetNewsInfoById(newsId);
            return Ok(response);
        }
    }
}
