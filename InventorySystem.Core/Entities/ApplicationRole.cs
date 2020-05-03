using InventorySystem.Core.Enums;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name, string description, Status status)
            : base(name)
        {
            this.Description = description;
            this.Status = status;
        }

        //[Auditable]
        public string Description { get; set; }
        //[Auditable]
        public Status Status { get; set; }
    }
}
