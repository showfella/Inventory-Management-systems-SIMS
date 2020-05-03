using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Messaging.Messages
{
    public class EmailServices : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var client = new SendGridClient(ConfigurationManager.AppSettings["SendGridApiKey"]);
            var from = new EmailAddress("okhakumhegideon@gmail.com");
            var to = new EmailAddress(message.Destination);
            var email = MailHelper.CreateSingleEmail(@from, to, message.Subject, message.Body, message.Body);

            await client.SendEmailAsync(email);
        }
    }
}
