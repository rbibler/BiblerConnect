namespace Humidity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Humidity.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Humidity.Models.BiblerConnectDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Humidity.Models.BiblerConnectDBContext context)
        {
            AddUserAndRole(context);
            context.Users.AddOrUpdate(p => p.Name,
         new User
         {
             Name = "Debra Garcia",
             Address = "1234 Main St",
             City = "Redmond",
             State = "WA",
             Zip = "10999",
             Email = "debra@example.com",
         },
          new User
          {
              Name = "Thorsten Weinrich",
              Address = "5678 1st Ave W",
              City = "Redmond",
              State = "WA",
              Zip = "10999",
              Email = "thorsten@example.com",
          },
          new User
          {
              Name = "Yuhong Li",
              Address = "9012 State st",
              City = "Redmond",
              State = "WA",
              Zip = "10999",
              Email = "yuhong@example.com",
          },
          new User
          {
              Name = "Jon Orton",
              Address = "3456 Maple St",
              City = "Redmond",
              State = "WA",
              Zip = "10999",
              Email = "jon@example.com",
          },
          new User
          {
              Name = "Diliana Alexieva-Bosseva",
              Address = "7890 2nd Ave E",
              City = "Redmond",
              State = "WA",
              Zip = "10999",
              Email = "diliana@example.com",
          }
          );
        }

        bool AddUserAndRole(Humidity.Models.BiblerConnectDBContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };
            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }
    }
}
