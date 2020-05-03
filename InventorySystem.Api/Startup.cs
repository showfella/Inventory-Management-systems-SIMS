using InventorySystem.Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using Microsoft.AspNet.Identity.Owin;
using InventorySystem.Infrastructure.Context;
using InventorySystem.Messaging.Messages;

[assembly: OwinStartup(typeof(InventorySystem.Api.Startup))]
namespace InventorySystem.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.CreatePerOwinContext(() => new AppDbContext());
            app.CreatePerOwinContext<UserStore<ApplicationUser>>((opt, cont) => new UserStore<ApplicationUser>(new AppDbContext()));

            app.CreatePerOwinContext<UserManager<ApplicationUser>>((opt, cont) => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AppDbContext())));

            app.CreatePerOwinContext<SignInManager<ApplicationUser, string>>((opt, cont) => new SignInManager<ApplicationUser, string>(cont.Get<UserManager<ApplicationUser>>(), cont.Authentication));

            app.CreatePerOwinContext<UserManager<ApplicationUser>>((opt, cont) =>
            {
                var userManager = new UserManager<ApplicationUser>(cont.Get<UserStore<ApplicationUser>>());
                userManager.RegisterTwoFactorProvider("SMS", new PhoneNumberTokenProvider<ApplicationUser> { MessageFormat = "Token: {0}" });
                userManager.SmsService = new SmsServices();
                userManager.EmailService = new EmailServices();
                userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(opt.DataProtectionProvider.Create());
                return userManager;
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie });
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5.0));



        }
    }
}