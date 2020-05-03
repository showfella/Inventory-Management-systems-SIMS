using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Entities
{
    public class Product
    {
        public long ID { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int NumberOfStock { get; set; }
        public string WareHouse { get; set; }
        public string Vendor { get; set; }
    }
}
