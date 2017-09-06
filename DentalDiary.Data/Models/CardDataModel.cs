using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalDiary.Data.Models
{
    public class CardDataModel
    {
        [Key, ForeignKey("Person")]
        public int Id { get; set; }
        public int PesrsonId { get; set; }
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

        public virtual PersonDataModel Person { get; set; }
    }
}
