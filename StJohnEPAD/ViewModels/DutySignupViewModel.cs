using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StJohnEPAD.Models;

namespace StJohnEPAD.ViewModels
{
    public class DutySignupViewModel
    {
        public List<Duty> allDuties { get; set; }
        public List<UserProfile> allUsers { get; set; }
        public List<DutyAvailability> allSignups { get; set; }
    }
}