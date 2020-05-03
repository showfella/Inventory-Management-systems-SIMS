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
    public class VendorController : ApiController
    {

        [Route("api/Vendor/AddVendor")]
        [HttpPost]
        public IHttpActionResult AddVendor(VendorModel model)
        {
            if (model == null)
            {
                return BadRequest("invalid model");
            }

            try
            {
                AppDbContext db = new AppDbContext();

                Vendor vendor = new Vendor();
                vendor.VendorName = model.VendorName;
                vendor.VendorType = model.VendorType;
                vendor.PhoneNumber = model.PhoneNumber;
                vendor.ContactPerson = model.ContactPerson;
                vendor.Email = model.Email;
                vendor.City = model.City;
                db.Vendors.Add(vendor);
                db.SaveChanges();

                return Ok(vendor);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [Route("api/Vendor/EditVendor")]
        [HttpPost]
        public IHttpActionResult EditVendor(EditVendorModel model)
        {
            if (model == null)
            {
                return BadRequest("invalid model");
            }

            try
            {
                AppDbContext db = new AppDbContext();
                var vendor = db.Vendors.FirstOrDefault(x => x.ID == model.Id);
                if (vendor == null)
                {
                    return BadRequest("Vendor does not exist");
                }
                else
                {
                    vendor.VendorName = model.VendorName;
                    vendor.VendorType = model.VendorType;
                    vendor.PhoneNumber = model.PhoneNumber;
                    vendor.ContactPerson = model.ContactPerson;
                    vendor.Email = model.Email;
                    vendor.City = model.City;

                    db.Entry(vendor).State = EntityState.Modified;
                    db.SaveChanges();

                    return Ok("Vendor updated successfully");
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("api/Vendor/GetAllVendors")]
        [HttpGet]
        public IHttpActionResult GetAllVendors()
        {
            AppDbContext db = new AppDbContext();
            var vendor = db.Vendors.Select(x => x).ToList();
            return Ok(vendor);
        }

        [Route("api/Vendor/DeleteVendor")]
        [HttpPost]
        public IHttpActionResult DeleteVendor(long Id)
        {
            AppDbContext db = new AppDbContext();
            var Vendor = db.Vendors.FirstOrDefault(x => x.ID == Id);
            if (Vendor == null)
            {
                return BadRequest("Vendor not found");
            }
            else
            {
                db.Vendors.Remove(Vendor);
                db.SaveChanges();

                return Ok("Vendor deleted succcessfully");
            }
        }
    }
}
