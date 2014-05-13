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

        public string PRFNumbers { get; set; }
        public string AnyOtherComments { get; set; }

        public ICollection<ConfirmedDutyHours> Attendees { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastUpdated { get; set; }

        [Display(Name = "Last updated by")]
        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public virtual Duty Duty { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public class ConfirmedDutyHours
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DutyHourID { get; set; }

        public float DutyHourAmount { get; set; }

        public string DutyHourComments { get; set; }

        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public int? DutyID { get; set; }
        public virtual Duty Duty { get; set; }

    }
}