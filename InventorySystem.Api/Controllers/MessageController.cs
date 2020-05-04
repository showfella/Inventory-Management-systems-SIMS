using InventorySystem.Api.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventorySystem.Api.Controllers
{
    public class MessageController : ApiController
    {
        public bool SendActivationMail(string toAddress, string title, string userId)
        {
            try
            {
                
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }
        public bool SendPasswordLink(string toAddress, string title, string userId)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }

        public bool SendEmailSG(string toAddress, string title, string fullname, string username, string password, string interest, string loan)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return false;
            }
        }
    }
}
