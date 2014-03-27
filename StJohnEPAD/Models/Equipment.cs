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
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EquipmentID { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string EquipmentName { get; set; }
        #endregion

        #region Equipment details
        [DataType(DataType.Date)]
        [Display(Name = "Last Check")]
        public DateTime LastCheck { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Next Check")]
        public DateTime NextCheck { get; set; }

        [Display(Name = "Description")]
        public String ItemDescription { get; set; }
        #endregion

        #region Equipment check in/out & responsibility details
        [Display(Name = "Last checked out")]
        public Member LastCheckedOut { get; set; }
        [Display(Name = "Current checked out")]
        public Member CurrentCheckedOut { get; set; }
        #endregion

    }
}
