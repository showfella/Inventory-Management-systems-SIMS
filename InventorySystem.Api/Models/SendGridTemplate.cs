using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventorySystem.Api.Models
{
    public class SendGridModel
    {
    }
    public class SendGridEmail
    {

    }
    public class To
    {
        public string email { get; set; }
        public string name { get; set; }
    }
    public class DynamicContent
    {
        public string NAME { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string LOANAMT { get; set; }
        public string INTEREST { get; set; }
        public string VERIFICATIONLINK { get; set; }
    }

    public class Personalization
    {
        public List<To> to { get; set; }
        public string subject { get; set; }
        public DynamicContent dynamic_template_data { get; set; }
    }

    public class From
    {
        public string email { get; set; }
        public string name { get; set; }
    }

    public class ReplyTo
    {
        public string email { get; set; }
        public string name { get; set; }
    }

    public class Content
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class SendGridTemplate
    {
        public List<Personalization> personalizations { get; set; }
        public From from { get; set; }
        public ReplyTo reply_to { get; set; }
        public List<Content> content { get; set; }
        public string template_id { get; set; }
        public string FULLNAME { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string LOANAMT { get; set; }
        public string INTEREST { get; set; }
    }
}