using NewsFeedApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApp.Data
{
    public interface INewsRepository
    {
        List<News> GetAllNews();
        //bool SaveNews(News newsDetails);
        News GetNewsInfoById(int newsId);
    }
}
