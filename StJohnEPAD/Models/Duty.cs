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

        //[Display(Name = "Members")]
        //public List<UserProfile> DutyMembers { get; set; }
        #endregion

        #region Organiser details
        [Display(Name = "Orgraniser")]
        public string DutyOrganiser { get; set; }
        [Display(Name = "Organiser phone")]
        public string DutyOrganiserPhoneNumber { get; set; }
        [Display(Name = "Organiser e-mail")]
        public string DutyOrganiserEmailAddress { get; set; }
        #endregion

        #region Map overrides
        //Manually provide the lat & long to override the location for mapping purposes
        [Display(Name = "Map Longitude")]
        public string DutyLocationLong { get; set; }

        [Display(Name = "Map Latitude")]
        public string DutyLocationLat { get; set; }
        #endregion


        public virtual PostDutyReport PostDutyReport { get; set; }
    }
}
