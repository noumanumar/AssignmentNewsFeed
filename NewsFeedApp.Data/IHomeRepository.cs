using NewsFeedApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApp.Data
{
    public interface IHomeRepository
    {
        bool IsLoginUserValid(string userId, string password);
        bool IsUserAdmin(string userId);
    }
}
