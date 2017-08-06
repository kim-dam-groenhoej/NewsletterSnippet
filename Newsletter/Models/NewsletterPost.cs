using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newsletter.Models
{
    public class NewsletterPost
    {
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }
    }
}