using InventorySystem.Api.Models;
using InventorySystem.Core.Entities;
using InventorySystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventorySystem.Api.Controllers
{
    public class ProductController : ApiController
    {
        [Route("api/Product/AddProduct")]
        [HttpPost]
        public IHttpActionResult CreateProduct(ProductModel model)
        {
            if(model == null)
            {
                return BadRequest("invalid model");
            }

            try
            {
                AppDbContext db = new AppDbContext();

                Product product = new Product();
                product.Barcode = Guid.NewGuid().ToString();
                product.CostPrice = model.CostPrice;
                product.Manufacturer = model.Manufacturer;
                product.NumberOfStock = model.NumberOfStock;
                product.ProductName = model.ProductName;
                product.SellingPrice = model.SellingPrice;
                product.Vendor = model.Vendor;
                product.WareHouse = model.WareHouse;

                db.Products.Add(product);
                db.SaveChanges();

                return Ok(product);
            }
            catch(Exception ex)
            {
                return NotFound();
            }

        }

        [Route("api/Product/EditProduct")]
        [HttpPost]
        public IHttpActionResult EditProduct(EditProductModel model)
        {
            if(model == null)
            {
                return BadRequest("invalid model");
            }

            try
            {
                AppDbContext db = new AppDbContext();
                var product = db.Products.FirstOrDefault(x => x.Barcode == model.Barcode);
                if(product == null)
                {
                    return BadRequest("product does not exist");
                }
                else
                {
                    product.CostPrice = model.CostPrice;
                    product.Manufacturer = model.Manufacturer;
                    product.NumberOfStock = model.NumberOfStock;
                    product.ProductName = model.ProductName;
                    product.SellingPrice = model.SellingPrice;
                    product.Vendor = model.Vendor;
                    product.WareHouse = model.WareHouse;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    return Ok("product updated successfully");
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("api/Product/GetAllProduct")]
        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            AppDbContext db = new AppDbContext();
            var product = db.Products.Select(x => x).ToList();
            return Ok(product);
        }

        [Route("api/Product/DeleteProduc")]
        [HttpPost]
        public IHttpActionResult DeleteProduct(string barcode)
        {
            AppDbContext db = new AppDbContext();
            var product = db.Products.FirstOrDefault(x => x.Barcode == barcode);
            if(product == null)
            {
                return BadRequest("product not found");
            }
            else
            {
                db.Products.Remove(product);
                db.SaveChanges();

                return Ok("product deleted succcessfully");
            }
        }
    }
}
