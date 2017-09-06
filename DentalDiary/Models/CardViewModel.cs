using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalDiary.Models
{
    public class CardViewModel
    {
        public int Id { get; set; }
        public int PesrsonId { get; set; }
        public string Complaints { get; set; }
        public string LastTreatment { get; set; }
        public string LastDiagnosis { get; set; }
        public string FinalDiagnosis { get; set; }
        public string AnotherOpinion { get; set; }
        public string TreatmentPlan { get; set; }
    }
}