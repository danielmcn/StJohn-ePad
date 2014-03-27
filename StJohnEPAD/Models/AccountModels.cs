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
        public int UserId { get; set; }
        public string UserName { get; set; }
        //public int MemberID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        #endregion

        #region Personal details
        /// <summary>Address for the user</summary>
        public string Address { get; set; }

        /// <summary>Telephone number for the user</summary>
        public string TelephoneNumber { get; set; }

        /// <summary>Emergency contact for the user.  Takes a new instance of Member</summary>
        public Member EmergencyContact { get; set; } //TODO: Doesnt have "relationship" - new class extending Member? New "Non-member" class?

        /// <summary>General skills for the user - organisation, computer use, driving, leadership, etc</summary>
        public string Skills { get; set; }
        #endregion

        #region SJA Properties
        public string Rank { get; set; }

        //TODO: fixed roles within division - enum? class? - FA, OFA, AED, etc
        public string OperationalRoles { get; set; }
        public string NonOperationalRoles { get; set; }
        #endregion
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
        
        //TODO: Remove user name and replace with Email
        [Required]
        [Key]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        [Key]
        [Display(Name = "E-mail address")]
        public string EmailAddress { get; set; }

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

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        #endregion

        #region Personal details
        /// <summary>Address for the user</summary>
        
        public string Address { get; set; }

        /// <summary>Telephone number for the user</summary>
        [Phone]
        public string TelephoneNumber { get; set; }

        /// <summary>Emergency contact for the user.  Takes a new instance of Member</summary>
        //public Member EmergencyContact { get; set; } //TODO: Doesnt have "relationship" - new class extending Member? New "Non-member" class?

        /// <summary>General skills for the user - organisation, computer use, driving, leadership, etc</summary>
        public string Skills { get; set; }
        #endregion

        #region SJA Properties
        public string Rank { get; set; }

        //TODO: fixed roles within division - enum? class? - FA, OFA, AED, etc
        public string OperationalRoles { get; set; }
        public string NonOperationalRoles { get; set; }
        #endregion
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
