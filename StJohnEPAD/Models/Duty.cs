using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StJohnEPAD.Models
{
    public class Duty
    {
        #region Required properties
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DutyID { get; set; }
        [Required]
        [Display(Name = "Duty name")]
        public string DutyName { get; set; }
        #endregion

        #region Event detailsiun
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DutyDate { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start time")]
        public DateTime DutyStartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "End time")]
        public DateTime DutyEndTime { get; set; }
        
        [Display(Name = "Location")]
        public string DutyLocation { get; set; }
        [Display(Name = "Description")]
        public string DutyDescription { get; set; }
        [Display(Name = "Additional notes")]
        public string DutyAdditionalNotes { get; set; }

        [Display(Name = "Creator")]
        public UserProfile DutyCreator { get; set; }

        [Display(Name = "Members")]
        public List<UserProfile> DutyMembers { get; set; }
        #endregion

        #region Organiser details
        [Display(Name = "Orgraniser")]
        public string DutyOrganiser { get; set; }
        [Display(Name = "Organiser phone")]
        public string DutyOrganiserPhoneNumber { get; set; }
        [Display(Name = "Organiser e-mail")]
        public string DutyOrganiserEmailAddress { get; set; }
        #endregion

    }

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

    public class PostDutyReport
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PostDutyReportID { get; set; }

        public string PRFNumbers { get; set; }
        public string AnyOtherComments { get; set;}

        public int DutyID { get; set; }
        public virtual Duty Duty { get; set; }

    }
}
