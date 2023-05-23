using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCourseWeb.Models
{
    public class ModelLinks
    {
        public int LinksId { get; set; }
        public int MenuLocId { get; set; }
        public string LinksName { get; set; }
        public string LinksURL { get; set; }
    }
}