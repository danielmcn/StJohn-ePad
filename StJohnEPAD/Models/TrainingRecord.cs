using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StJohnEPAD.Models
{
    public class TrainingRecord
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TrainingRecordID { get; set; }

        [Required]
        [Display(Name = "Training Type")]
        public string TrainingType { get; set; }

        [Required]
        [Display(Name = "Aquired on")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "N/a", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime TrainingDate { get; set; }

        [Display(Name = "Valid Until")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "N/a", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? RevalidationDate { get; set; }

        [Display(Name = "Assessment Venue")]
        public string AssessmentVenue { get; set; }

        [Display(Name = "Assessor")]
        public string Assessor { get; set; }

        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}