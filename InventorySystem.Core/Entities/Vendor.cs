using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Entities
{
    public class Vendor
    {
        public long ID { get; set; }

        public string VendorName { get; set;}

        //creae category for vendor
        public string VendorType { get; set; }

        public string ContactPerson { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set;}
    }
}
