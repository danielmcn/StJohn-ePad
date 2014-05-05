using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

///Extended from the ASP.NET SimpleMembership classes
///Will of course need customised: http://brockallen.com/2012/09/02/think-twice-about-using-membershipprovider-and-simplemembership/
///http://www.itorian.com/2012/11/adding-email-details-field-in.html
///But does for now
namespace StJohnEPAD.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }


    }

    [Table("UserProfile")]
    public class UserProfile
    {
        #region Essential/key fields

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        #endregion

        #region Personal details

        [Display(Name = "Name")]
        public string Name { get; set; }

        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Phone number input.  Regex allows 11 digit numbers, starting 07.  We only want mobiles for our text service to function.
        /// </summary>
        [RegularExpression(@"^07\d{9}",
         ErrorMessage = "Must be a continuous 11 digit number, starting with 07.")]
        [Display(Name = "UK Mobile",Prompt = "11 digit UK Mobile (07....)")]
        public string TelephoneNumber { get; set; }

        #endregion

        #region SJA Properties
        //Navigation property.  Still not sure how it works, but it does *shrug*
        public ICollection<TrainingRecord> Qualifications { get; set; }
        
        //To store, eg, OFA, PTA, Inactive...
        //public String CurrentRole { get; set; }
        


        [Display(Name = "Current Role")]
        public CurrentRoleEnum CurrentRole { get; set; }

        /*
        public DateTime? JoinDate { get; set; }
        public DateTime? TFADate { get; set; }
        public DateTime? OFADate { get; set; }
        public DateTime? PTADate { get; set; }
        public DateTime? ETADate { get; set; }
        public DateTime? AEDDate { get; set; }
        public DateTime? CycleDate { get; set; }
        */
        //Refactored into collection
        #endregion

        //Navigation properties
        public virtual ICollection<DutyAvailability> DutySignups { get; set; }
    }

    public enum CurrentRoleEnum
    {
        Unknown,
        Doctor,
        Nurse,
        Paramedic,
        ETA,
        PTA,
        OFA,
        TFA,
        Observer,
        Inactive,
        ExMember
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

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

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        #region Essential
        
        //TODO: Remove user name and replace with Email??
        [Required]
        [Key]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        [Key]
        [Display(Name = "E-mail address")]
        public string EmailAddress { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Confirm e-mail")]
        [Compare("EmailAddress", ErrorMessage = "The email and confirmation email do not match.")]
        public string ConfirmEmail{ get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Name can not be empty.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        #endregion


    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
