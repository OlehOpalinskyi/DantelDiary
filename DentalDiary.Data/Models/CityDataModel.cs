using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalDiary.Data.Models
{
    public class CityDataModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PriceDataModel> PriceLists { get; set; }
        public virtual ICollection<ReceptionDataModel> Receptions { get; set; }
    }
}
