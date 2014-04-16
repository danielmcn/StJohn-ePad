using StJohnEPAD.DAL;
using StJohnEPAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StJohnEPAD.ViewModels
{
    public class SingleUserTrainingRecordViewModel
    {
        public UserProfile User { get; set; }
        public IEnumerable<TrainingRecord> TrainingRecords { get; set; }
    }
}