namespace InventorySystem.Infrastructure.Migrations
{
    using InventorySystem.Core.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InventorySystem.Infrastructure.Context.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(InventorySystem.Infrastructure.Context.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );

            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole { Name = "Manager", Description = "The Manager Role", Status = Core.Enums.Status.Active };

                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole { Name = "User", Description = "The user role", Status = Core.Enums.Status.Active };

                manager.Create(role);
            }
            if (!context.Users.Any(u => u.UserName == "Manager"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "Manager",
                    Lastname = "Shoala",
                    FirstName = "Shoala",
                    Email = "shoala@gmail.com",
                    status = Core.Enums.Status.Active.ToString(),
                    RegistrationDate = DateTime.Now,
                    CompanyName = "SIMS"
        
                };
                manager.Create(user, "manager@123");
                manager.AddToRole(user.Id, "Manager");
            }

        }
    }
}
