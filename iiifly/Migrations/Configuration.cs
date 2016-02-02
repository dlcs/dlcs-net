using System.Configuration;
using iiifly.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace iiifly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<iiifly.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        bool AddUserAndRoles(iiifly.Models.ApplicationDbContext context)
        {
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            rm.Create(new IdentityRole("canCallDlcs"));
            rm.Create(new IdentityRole("canApproveUsers"));

            IdentityResult ir;
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser
            {
                UserName = ConfigurationManager.AppSettings["admin-user-name"],
                DisplayName = "Tom Crane",
                Affiliation = "Digirati"
            };
            ir = um.Create(user, ConfigurationManager.AppSettings["admin-user-password"]);
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canCallDlcs");
            ir = um.AddToRole(user.Id, "canApproveUsers");
            return ir.Succeeded;

        }

        private void DeleteImageSets(ApplicationDbContext context)
        {
            context.ImageSets.RemoveRange(context.ImageSets);
        }

        protected override void Seed(iiifly.Models.ApplicationDbContext context)
        {
            DeleteImageSets(context);
            AddUserAndRoles(context);
        }

    }
}
