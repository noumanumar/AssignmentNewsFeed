using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApp.Data.Repository
{
    public class BaseRepository
    {
        protected SqlQueryExecutor QueryExecutor;
        public BaseRepository()
        {
            QueryExecutor = new SqlQueryExecutor(ConfigurationManager.ConnectionStrings["NewsFeedApplicationDb"].ConnectionString);
        }
    }
}
