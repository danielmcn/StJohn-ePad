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
            SeedUsers(context);
            SeedDuties(context);
        }

        private void SeedDuties(SJAContext context)
        {

            context.Duties.AddOrUpdate(
                d => d.DutyName,

                new Duty
                {
                    DutyName = "Special Olympics Ulster",
                    DutyDate = new DateTime(2014, 04, 26),
                    DutyStartTime = new DateTime(2014, 04, 26, hour: 8, minute: 0, second: 0),
                    DutyEndTime = new DateTime(2014, 04, 26, hour: 18, minute: 0, second: 0),
                    DutyDescription = "Training Day",
                    DutyLocation = "Jim Baker Stadium, Antrim",
                },
                new Duty
                {
                    DutyName = "Larne Somme Society",
                    DutyDate = new DateTime(2014, 04, 26),
                    DutyStartTime = new DateTime(2014, 04, 26, hour: 11, minute: 0, second: 0),
                    DutyEndTime = new DateTime(2014, 04, 26, hour: 16, minute: 0, second: 0),
                    DutyDescription = "Gun Running Celebration!",
                },
                new Duty
                {
                    DutyName = "Belfast Marathon",
                    DutyDate = new DateTime(2014, 05, 05),
                    DutyStartTime = new DateTime(2014, 05, 05, hour: 09, minute: 0, second: 0),
                    DutyEndTime = new DateTime(2014, 05, 05, hour: 19, minute: 0, second: 0),
                    DutyDescription = "Annual Marathon",
                },
                new Duty
                {
                    DutyName = "Writers Suarez & Cathedral",
                    DutyDate = new DateTime(2014, 05, 09),
                    DutyStartTime = new DateTime(2014, 05, 09, hour: 12, minute: 0, second: 0),
                    DutyEndTime = new DateTime(2014, 05, 09, hour: 18, minute: 0, second: 0),
                    DutyLocation = "Belfast",
                });
            
            context.SaveChanges();




            /*List<Duty> duties = new List<Duty>
            {
                new Duty {
                    DutyName = "Special Olympics Ulster",
                    DutyDate = new DateTime(2014, 04, 26),
                    DutyStartTime = new DateTime(2014, 04, 26, hour: 8, minute: 0, second: 0),
                    DutyEndTime = new DateTime(2014, 04, 26, hour: 18, minute: 0, second: 0),
                    DutyDescription = "Training Day",
                    DutyLocation = "Jim Baker Stadium, Antrim",
                },
                new Duty {
                    DutyName = "Larne Somme Society",
                    DutyDate = new DateTime(2014, 04, 26),
                    DutyStartTime = new DateTime(2014, 04, 26, hour: 11, minute: 0, second: 0),
                    DutyEndTime = new DateTime(2014, 04, 26, hour: 16, minute: 0, second: 0),
                    DutyDescription = "Gun Running Celebration!",
                },
                new Duty {
                    DutyName = "Belfast Marathon",
                    DutyDate = new DateTime(2014, 05, 05),
                    DutyStartTime = new DateTime(2014, 05, 05, hour: 09, minute: 0, second: 0),
                    DutyEndTime = new DateTime(2014, 05, 05, hour: 19, minute: 0, second: 0),
                    DutyDescription = "Annual Marathon",
                },
                new Duty {
                    DutyName = "Writers Suarez & Cathedral",
                    DutyDate = new DateTime(2014, 05, 09),
                    DutyStartTime = new DateTime(2014, 05, 09, hour: 12, minute: 0, second: 0),
                    DutyEndTime = new DateTime(2014, 05, 09, hour: 18, minute: 0, second: 0),
                    DutyLocation = "Belfast",
                },

            };

            foreach (Duty d in duties)
            {
                context.Duties.AddOrUpdate(d);
            }
            context.SaveChanges();
            */
        }

        private void SeedUsers(SJAContext context)
        {
            //Initilaise our WebSecurity
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

            UserProfile new1 = context.Users.First();
            new1.Qualifications = new List<TrainingRecord>();
            new1.Qualifications.Add(new TrainingRecord { TrainingType = "AED", TrainingDate = new DateTime(1990, 02, 01), });
            context.SaveChanges();
        }
    }
}
