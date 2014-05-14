using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StJohnEPAD.Models;


namespace StJohnEPAD.DAL
{
    public class SJAContext : DbContext
    {
        public SJAContext()
            : base("DefaultConnection")
        {
            //Database.SetInitializer<SJAContext>(new CreateDatabaseIfNotExists<SJAContext>());
            //Database.SetInitializer<SJAContext>(new DropCreateDatabaseIfModelChanges<SJAContext>());
            //Database.SetInitializer<SJAContext>(new DropCreateDatabaseAlways<SJAContext>());
            //Database.SetInitializer<SJAContext>(new SJAInitializer());
            //Database.SetInitializer<SJAContext>(null);

        }

        //public DbSet<Member> Members { get; set; }

        public DbSet<Duty> Duties { get; set; }
        public DbSet<DutyAvailability> DutyAvailabilty { get; set; }
        public DbSet<PostDutyReport> PostDutyReport { get; set; }
        public DbSet<ConfirmedDutyHours> ConfirmedDutyHours { get; set; }
        public DbSet<TrainingRecord> TrainingRecord { get; set; }

        public DbSet<Equipment> Equipment { get; set; }

        public DbSet<UserProfile> Users { get; set; }

        public DbSet<Document> Documents { get; set; }
    }
}
