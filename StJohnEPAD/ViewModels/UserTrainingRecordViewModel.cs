using StJohnEPAD.DAL;
using StJohnEPAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StJohnEPAD.ViewModels
{
    public class UserTrainingRecordViewModel
    {
        public IEnumerable<UserProfile> Users { get; set; }
        public IEnumerable<TrainingRecord> TrainingRecords { get; set; }
    }
}