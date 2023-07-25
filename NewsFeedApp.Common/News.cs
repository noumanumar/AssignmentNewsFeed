using System;
using System.ComponentModel.DataAnnotations;

namespace NewsFeedApp.Common
{
    public class News
    {
        [Display(Name = "News Id")]
        public int NewsId { get; set; }
        [Display(Name = "News Title")]
        public string NewsTitle { get; set; }
        [Display(Name = "News SubTitle")]
        public string NewsSubTitle { get; set; }
        [Display(Name = "News Details")]
        public string NewsDetails { get; set; }
        [Display(Name = "News Image Url")]
        public string NewsImageUrl { get; set; }
        [Display(Name = "News Big Image Url")]
        public string NewsBigImageUrl { get; set; }
        [Display(Name = "News Author")]
        public string NewsAuthor { get; set; }
        [Display(Name = "News Publish Date")]
        public DateTime NewsPublishDate { get; set; }
        [Display(Name = "News Language")]
        public string NewsLanguage { get; set; }
        
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUser { get; set; }
    }
}
