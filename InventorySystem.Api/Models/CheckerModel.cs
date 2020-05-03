using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorySystem.Api.Models
{
    public class CheckerModel
    {
        public string SuperAdminId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }

    public class RemoveCheckerOrMaker
    {
        public string SuperAdminId { get; set; }

        public string Email { get; set; }
    }

    public class EditMakerChecker
    {
        public string MakerCheckerId { get; set; }

        public string SuperAdminId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public string UserRole { get; set; }
    }

    public class UsersWithRole
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}