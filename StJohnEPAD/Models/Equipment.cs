using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StJohnEPAD.Models
{
    public class Equipment
    {
        #region Required properties
        //Although all equioment ids should be unique, we will generate a surrogate key for performance & stability reasons
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Database Unique ID - do not use")]
        public int EquipmentID { get; set; }
        //Should be Unique but not an option until EF6
        //Not yet decided on a convention within the unit, but use ReGex to enforce once it is
        [Display(Name = "Identifier")]
        public string EquipmentInventoryID { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string EquipmentName { get; set; }
        #endregion

        #region Equipment details
        [DataType(DataType.Date)]
        [Display(Name = "Last Check")]
        public DateTime? EquipmentLastCheck { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Next Check")]
        public DateTime? EquipmentNextCheck { get; set; }
        [Display(Name = "Description")]
        public String EquipmentDescription { get; set; }
        [Display(Name = "Notes")]
        public String EquipmentNotes { get; set; }

        //todo: send to file system instead
        [NotMapped]
        public HttpPostedFileBase ProfileImage { get; set; }
        public String ImageFilename { get; set; }
        #endregion

        #region Equipment check in/out & responsibility details
        [Display(Name = "Checked in by")]
        public UserProfile EquipmentCheckInBy { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Last checked out date")]
        public DateTime? EquipmentCheckInDate { get; set; }

        [Display(Name = "Checked out by")]
        public UserProfile EquipmentCheckOutBy { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Last checked out date")]
        public DateTime? EquipmentCheckOutDate { get; set; }
        #endregion

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
