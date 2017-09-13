using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalDiary.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
        public string LinkToImages { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Debt { get; set; }
        public string Complaints { get; set; }
        public string LastTreatment { get; set; }
        public string LastDiagnosis { get; set; }
        public string FinalDiagnosis { get; set; }
        public string AnotherOpinion { get; set; }
        public string TreatmentPlan { get; set; }
    }
}