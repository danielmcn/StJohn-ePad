using StJohnEPAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StJohnEPAD.ViewModels
{
    public class UserDutyListWithSignupViewModel : Duty
    {
        public bool? SingleDutySignupResponse { get; set; }
    }
}