using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsFeedApp.Models
{
    public class RssFeed
    {
        public string Heading { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public string PublishDate { get; set; }
        public string Link { get; set; }
    }
}