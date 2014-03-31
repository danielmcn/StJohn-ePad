namespace StJohnEPAD.Migrations
{
    using StJohnEPAD.Models;
    using StJohnEPAD.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;
    using WebMatrix.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<SJAContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SJAContext context)
        {
            SeedUsers();
            SeedDuties(context);
        }

        private void SeedDuties(SJAContext context)
        {
            List<Duty> duties = new List<Duty>
            {
                new Duty {
                    DutyName = "Test Duty 1",
                    DutyDate = new DateTime(2012, 02, 01),
                    DutyStartTime = new DateTime(2012, 02, 01, hour: 0, minute: 1, second: 0),
                    DutyEndTime = new DateTime(2012, 02, 01, hour: 0, minute: 2, second: 0)
                },
            };

            foreach (Duty d in duties)
            {
                context.Duties.Add(d);
            }
            context.SaveChanges();

        }

        private void SeedUsers()
        {
            //Inilaise our WebSecurity
            WebSecurity.InitializeDatabaseConnection(
                "DefaultConnection",
                "UserProfile",
                "UserId",
                "UserName", autoCreateTables: true);

            //Create admin role & account
            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!WebSecurity.UserExists("AdminAcc"))
                WebSecurity.CreateUserAndAccount(
                    "AdminAcc",
                    "password",
                    new { Name = "Admin User" });

            if (!Roles.GetRolesForUser("AdminAcc").Contains("Administrator"))
                Roles.AddUsersToRoles(new[] { "AdminAcc" }, new[] { "Administrator" });

            //And now a user account
            if (!WebSecurity.UserExists("UserAcc"))
                WebSecurity.CreateUserAndAccount(
                    "UserAcc",
                    "password",
                    new { Name = "Standard User", });
        }
    }
}
