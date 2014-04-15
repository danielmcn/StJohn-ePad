using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StJohnEPAD.Models
{
    public class DutyAvailability
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DutyAvailabilityID { get; set; }

        [DisplayFormat(NullDisplayText = "No response")]
        public bool? DutyAvailabilityResponse { get; set; }

        public int DutyID { get; set; }
        public virtual Duty Duty { get; set; }

        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

    }
}