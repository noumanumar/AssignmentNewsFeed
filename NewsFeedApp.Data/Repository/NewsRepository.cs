using NewsFeedApp.Data;
using NewsFeedApp.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApp.Data.Repository
{
    public class NewsRepository : BaseRepository, INewsRepository
    {
        /// <summary>
        /// Gets all news.
        /// </summary>
        /// <returns></returns>
        public List<News> GetAllNews()
        {
            try
            {
                var response = QueryExecutor.Query<News>(Sqls.GetAllNews, null, CommandType.Text).ToList();
                return response;
            }
            catch (Exception ex)
            {
                //log exception
                return null;
            }
        }
        public News GetNewsInfoById(int newsId)
        {
            try
            {
                var response = QueryExecutor.Query<News>(Sqls.GetNewsInfoById, new { newsId }, CommandType.Text).FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                //log exception
                return null;
            }
        }
        //public bool SaveNews(News newsDetails)
        //{
        //    if (newsDetails == null)
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        var response = QueryExecutor.Execute(Sqls.AddNewNews,
        //            new
        //            {
        //                bookTitle = newsDetails.NewsTitle,
        //                bookSubTitle = newsDetails.NewsSubTitle,
        //                publishYear = newsDetails.NewsPublishDate,
        //                newsDetail = newsDetails.NewsDetails,
        //                newsImageUrl = newsDetails.NewsImageUrl,
        //                newsAuthor = newsDetails.NewsAuthor,
        //                newsLanguage = newsDetails.NewsLanguage,
        //                categoryId = newsDetails.CategoryId,
        //                addDate = DateTime.Now,
        //                addUser = newsDetails.AddUser
        //            }, CommandType.Text);

        //        return response > 0 ? true : false;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log exception
        //        return false;
        //    }
        //}
    }
}
