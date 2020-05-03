using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorySystem.Api.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
    }

    public class AdminLoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

}