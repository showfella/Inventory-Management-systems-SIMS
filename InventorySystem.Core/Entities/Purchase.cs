using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Entities
{
    public class Purchase
    {
        public long ID { get; set; }
        public string InvoiceNo { get; set; }
        public decimal PurchasePrice { get; set; }
        public string SellingPrice { get; set; }
        public int Unit { get; set; }
        public decimal Tax { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }

    }
}
