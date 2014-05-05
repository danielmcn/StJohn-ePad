using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StJohnEPAD.Models
{
    public class PostDutyReport
    {
        [Key]
        [ForeignKey("Duty")]
        public int DutyID { get; set; }

        public ICollection<string> PRFNumbers { get; set; }
        public string AnyOtherComments { get; set; }

        public ICollection<UserProfile> Attendees { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastUpdated { get; set; }

        public UserProfile LastUpdateBy { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual Duty Duty { get; set; }
    }
}