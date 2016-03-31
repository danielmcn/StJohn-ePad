using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StJohnEPAD.Models;

namespace StJohnEPAD.ViewModels
{
    public class HoursPerDutyViewModel
    {
        public int DutyID { get; set; }
        public string DutyName { get; set; }
        public float HoursPerDuty { get; set; }
        public int MembersPerDuty { get; set; }

        public virtual Duty Duty { get; set; }
        public virtual ConfirmedDutyHours ConfirmedDutyHours { get; set; }
    }

    public class HoursPerUserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public float Hours { get; set; }
        public int Duties { get; set; }

        public virtual Duty Duty { get; set; }
        public virtual ConfirmedDutyHours ConfirmedDutyHours { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
