namespace StJohnEPAD.Migrations
{
    using StJohnEPAD.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StJohnEPAD.DAL.SJAContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StJohnEPAD.DAL.SJAContext context)
        {
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
            context.SaveChanges();

        }
    }
}
