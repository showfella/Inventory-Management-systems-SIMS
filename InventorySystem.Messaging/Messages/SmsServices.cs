using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace InventorySystem.Messaging.Messages
{
    public class SmsServices : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var sid = ConfigurationManager.AppSettings["twilioID"];
            var tok = ConfigurationManager.AppSettings["twilioToken"];
            var from = ConfigurationManager.AppSettings["twilioFrom"];

            TwilioClient.Init(sid, tok);
            await MessageResource.CreateAsync(new PhoneNumber(message.Destination), from: new PhoneNumber(from), body: message.Body);
        }
    }
}
