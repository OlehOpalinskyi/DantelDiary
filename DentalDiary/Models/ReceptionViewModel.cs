using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalDiary.Models
{
    public class ReceptionViewModel
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public DateTime? Date { get; set; }
        public string PriceName { get; set; }
        public string KindOfWork { get; set; }
        public double PriceCount { get; set; }
        public string Recivier { get; set; }
        public double Payment { get; set; }
        public bool Done { get; set; }
        public int Priority { get; set; }

        public int PersonId { get; set; }
        public int CityId { get; set; }
        public int PriceId { get; set; }
    }
}