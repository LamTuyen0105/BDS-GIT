using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.Service.NewsManager
{
    public class NewsCreateRequest
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
