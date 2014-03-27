using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StJohnEPAD.Models
{
    public class Member
    {
        #region Key properties
        [Key]
        public int MemberID { get; set; }
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
}
