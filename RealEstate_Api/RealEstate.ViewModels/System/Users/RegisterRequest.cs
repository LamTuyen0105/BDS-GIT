using Microsoft.AspNetCore.Http;
using RealEstate.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.System.Users
{
    public class RegisterRequest
    {
        public IFormFile ThumbnailImage { get; set; }
        public string FullName { get; set; }

        public string Address { get; set; }

        public Gender Gender { get; set; }

        public string IdentityNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
