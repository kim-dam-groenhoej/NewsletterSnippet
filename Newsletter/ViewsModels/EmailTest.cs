using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newsletter.ViewsModels
{
    public class EmailTest : Email
    {
 
        public string Title { get; set; }

        public string Content { get; set; }

        public string To { get; set; }

        public string From { get; set; }
    }
}