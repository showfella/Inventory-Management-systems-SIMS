using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorySystem.Api.Models
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int NumberOfStock { get; set; }
        public string WareHouse { get; set; }
        public string Vendor { get; set; }
    }

    public class VendoModel
    {

        public string VendorName { get; set; }

        //creae category for vendor
        public string VendorType { get; set; }
       public string ContactPerson { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }

    public class WareHouseModel
    {
        public string WareHouseName { get; set; }

        public string WareHouseLocation { get; set; }

        public string WareHousePersonnel { get; set; }

        public string WareHouseCode { get; set; }

        public string WareHousePhoneNumber { get; set; }
    }

    public class EditProductModel
    {
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