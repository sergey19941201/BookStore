using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }   //General number of books
        public int ItemsPerPage { get; set; } //Number of books on page
        public int CurrentPage { get; set; }  //The number of current page
        public int TotalPages                 //General number of pages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}