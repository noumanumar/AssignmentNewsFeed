using NewsFeedApp.Common;
using NewsFeedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApp.Services
{
    public interface INewsService
    {
        List<News> GetAllNews();
        bool SaveNews(News newsDetails);
        News GetNewsInfoById(int newsId);
        List<RssFeed> GetRssFeeds(string searchText);
    }
}
