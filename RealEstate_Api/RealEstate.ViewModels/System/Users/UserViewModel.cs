using RealEstate.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.System.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string IdentityNumber { get; set; }
    }
}
