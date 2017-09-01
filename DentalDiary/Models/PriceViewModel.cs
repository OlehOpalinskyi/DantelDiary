using System.ComponentModel.DataAnnotations;

namespace DentalDiary.Models
{
    public class PriceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string KindOfWork { get; set; }

        public int CityId { get; set; }
    }
}