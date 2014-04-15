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
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PostDutyReportID { get; set; }

        public string PRFNumbers { get; set; }
        public string AnyOtherComments { get; set; }

        public int DutyID { get; set; }
        public virtual Duty Duty { get; set; }

    }
}