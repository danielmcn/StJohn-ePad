namespace StJohnEPAD.Migrations
{
    using StJohnEPAD.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    using WebMatrix.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<StJohnEPAD.DAL.SJAContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StJohnEPAD.DAL.SJAContext context)
        {
            WebSecurity.InitializeDatabaseConnection(
            "DefaultConnection",
            "UserProfile",
            "UserId",
            "UserName", autoCreateTables: true);
            /*
            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!WebSecurity.UserExists("AdminAcc"))
                WebSecurity.CreateUserAndAccount(
                    "AdminAcc",
                    "password",
                    new { Name = "Admin User" });

            if (!Roles.GetRolesForUser("AdminAcc").Contains("Administrator"))
            Roles.AddUsersToRoles(new[] { "AdminAcc" }, new[] { "Administrator" });
          
            List<Duty> duties = new List<Duty>
            {
                new Duty {DutyName = "Test Duty 1"},
                new Duty {DutyName = "Test Duty 2"},
                new Duty {DutyName = "Test Duty 3"},
            };

            foreach (Duty d in duties)
            {
                context.Duties.Add(d);
            }
            */
            context.SaveChanges();

        }
    }
}
