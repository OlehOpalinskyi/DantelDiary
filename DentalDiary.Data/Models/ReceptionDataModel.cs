using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalDiary.Data.Models
{
    public class ReceptionDataModel
    {
        [Key]
        public int Id { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; }
        public string PriceName { get; set; }

        public int PersonId { get; set; }
        public int CityId { get; set; }

        public virtual PersonDataModel Preson { get; set; }
        public virtual CityDataModel City { get; set; }
    }
}
