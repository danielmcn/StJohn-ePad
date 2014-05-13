using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StJohnEPAD.Models
{
    public class Document
    {
        #region Required properties
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DocumentID { get; set; }
        
        [Required]
        [Display(Name = "Document Name")]
        public string DocumentName { get; set; }

        
        [Display(Name= "Document GUID")]
        public string DocumentGUID { get; set; }

        
        [Display(Name = "Document Location")]
        public string DocumentLocation { get; set; }

        #endregion

        #region Details
        [DataType(DataType.Date)]
        [Display(Name = "Valid From")]
        public DateTime? DocumentValidFrom { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Next Review")]
        public DateTime? DocumentNextReview { get; set; }

        [Display(Name = "Description")]
        public string DocumentDescription { get; set; }

        #endregion

        #region 
        [Display(Name = "Uploader")]
        public int UserId;
        public virtual UserProfile UserProfile { get; set; }

        
        [Display(Name = "Supersedes")]
        public int? DocumentSupersedes { get; set; }

        [Display(Name = "Superseded By")]
        public int? DocumentSuperededBy { get; set; }
        
         
        [Display(Name = "Document Version")]
        public string DocumentVersion { get; set; }

        [Display(Name = "Previous Version")]
        public int? DocumentPreviousVersion { get; set; }

        [Display(Name = "Restricted?")]
        public bool? DocumentRestricted { get; set; }
        #endregion

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}