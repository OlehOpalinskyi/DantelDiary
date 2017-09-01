using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalDiary.Data.Models
{
    public class PriceDataModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string KindOfWork { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual CityDataModel City { get; set; }
    }

}
