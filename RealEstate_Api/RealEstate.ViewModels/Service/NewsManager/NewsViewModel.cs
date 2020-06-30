using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.Service.NewsManager
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public int View { get; set; }
        public int Share { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
