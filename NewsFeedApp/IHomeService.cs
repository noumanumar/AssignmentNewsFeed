using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApp
{
    public interface IHomeService
    {
        string IsValidLogin(string userId, string password);
    }
}
