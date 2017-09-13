using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalDiary.Models
{
    public class PayOrder
    {
        public int IdOrder { get; set; }
        public double PriceOne { get; set; }
        public int PriceId { get; set; }
        public double PriceTwo { get; set; }
    }
}