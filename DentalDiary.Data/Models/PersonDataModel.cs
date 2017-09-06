using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalDiary.Data.Models
{
    public class PersonDataModel
    {
        public PersonDataModel()
        {
            Receptions = new List<ReceptionDataModel>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(70)]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
        public string LinkToImages { get; set; }
        public string Recivier { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Debt { get; set; }

        [StringLength(100)]
        public string Complaints { get; set; }
        [StringLength(100)]
        public string LastTreatment { get; set; }
        [StringLength(100)]
        public string LastDiagnosis { get; set; }
        [StringLength(50)]
        public string FinalDiagnosis { get; set; }
        [StringLength(100)]
        public string AnotherOpinion { get; set; }
        [StringLength(150)]
        public string TreatmentPlan { get; set; }

        public virtual ICollection<ReceptionDataModel> Receptions { get; set; }
    }
}
