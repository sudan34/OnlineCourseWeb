using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace OnlineCourseWeb.Models
{
    public class MenuLocationModel
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int Links { get; set; }
    }
}