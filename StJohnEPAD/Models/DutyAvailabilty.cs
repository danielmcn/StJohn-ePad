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

        [DisplayFormat(NullDisplayText = "Not answered")]
        public DutyResponseValue? DutyAvailabilityResponse { get; set; }

        [ForeignKey("Duty")]
        public int DutyID { get; set; }
        public virtual Duty Duty { get; set; }
        
        [ForeignKey("UserProfile")]
        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
    public enum DutyResponseValue
    {
        NoResponse,
        Available,
        Unavailable,
    }
}