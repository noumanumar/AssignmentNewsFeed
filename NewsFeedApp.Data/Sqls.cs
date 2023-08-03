using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeedApp.Data
{
    public static class Sqls
    {
        public const string IsValidLogin = "select 't' from NewsFeedApplication.dbo.UserLogin(nolock) where UserId=@userId and Password=@password and IsActive=1";
        public const string GetAllNews =
            @"select NewsId,NewsTitle,NewsSubTitle,NewsDetails,NewsImageUrl,NewsAuthor,NewsPublishDate,NewsLanguage,CategoryId,AddDate,AddUser from NewsFeedApplication.dbo.News(nolock) where  isnull(IsDeleted,0) = 0";
                
        public const string GetAllMembers = "select MembershipNumber, MemberName, Gender, DOB,MobileNumber,AddDate,AddUser from NewsFeedApplication.dbo.Members(nolock) where isnull(IsDeleted,0) = 0";
        public const string GetMemberInfoByUserId = @"select MembershipNumber, MemberName, Gender, DOB,MobileNumber,AddDate,AddUser 
            from NewsFeedApplication.dbo.Members(nolock) where isnull(IsDeleted,0) = 0 and UserId=@userId";

        public const string GetNewsInfoById =
            @"select NewsId,NewsTitle,NewsSubTitle,NewsDetails,NewsImageUrl,NewsAuthor,NewsPublishDate,NewsLanguage,CategoryId,AddDate,AddUser from NewsFeedApplication.dbo.News(nolock)
                where  isnull(IsDeleted,0) = 0 and NewsId = @newsId";
       
        public const string IsUserAdmin = "select IsAdmin from NewsFeedApplication.dbo.Staff(nolock) where UserId=@userId";

        public const string AddNewNews =
            @"insert into NewsFeedApplication.dbo.News(NewsTitle,NewsSubTitle, NewsDetails,NewsImageUrl,NewsAuthor,NewsPublishDate,NewsLanguage,CategoryId,AddDate,AddUser)
            values (@newsTitle,@newsSubTitle,@newsDetail,@newsImageUrl,@newsAuthor,@newsPublishDate,@newsLanguage,@categoryId,@addDate,@addUser)";

    }
}
