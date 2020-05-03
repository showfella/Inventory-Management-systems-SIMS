using InventorySystem.Core.Enums;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? LastLoginTime { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastLockedDate { get; set; }
        public bool? IsSuperUser { get; set; }
        public bool? ForceChangeOfPassword { get; set; }
        public DateTime? LastDatePasswordWasChanged { get; set; }
        public string Lastname { get; set; }
        public string FirstName { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string status { get; set; }

        public DateTime? DateAdminUpdated { get; set; }
    }
}
