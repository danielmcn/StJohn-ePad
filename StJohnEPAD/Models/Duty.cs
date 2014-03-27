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

        #region Event details
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
        public Member DutyCreator { get; set; }

        [Display(Name = "Members")]
        public List<Member> DutyMembers { get; set; }
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
    /*
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
     * */
}
