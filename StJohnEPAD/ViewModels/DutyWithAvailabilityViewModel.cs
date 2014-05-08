using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StJohnEPAD.Models;

namespace StJohnEPAD.ViewModels
{
    public class DutyWithAvailabilityViewModel
    {
        public Duty duty { get; set; }
        public DutyAvailability dutyAvailability { get; set; }
    }
}
