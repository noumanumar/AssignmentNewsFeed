using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApp.Data.Repository
{
    public class HomeRepository : BaseRepository, IHomeRepository
    {
        public HomeRepository()
        {

        }
        public bool IsLoginUserValid(string userId, string password)
        {
            var response = QueryExecutor.Query<string>(Sqls.IsValidLogin,
                                new
                                {
                                    userId = userId,
                                    password = password
                                }, CommandType.Text).FirstOrDefault();
            return response != null ? true : false;
        }
        public bool IsUserAdmin(string userId)
        {
            var response = QueryExecutor.Query<bool>(Sqls.IsUserAdmin,
                                new
                                {
                                    userId = userId
                                }, CommandType.Text).FirstOrDefault();
            return response;
        }
    }
}
