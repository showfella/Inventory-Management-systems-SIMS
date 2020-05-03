using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorySystem.Api.Models
{
    public class ForgottenPasswordModel
    {
        public string Email { get; set; }
    }

    public class PasswordResetModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class PassWordTokenModel
    {
        public string UserId { get; set; }

    }
}