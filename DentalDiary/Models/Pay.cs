using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalDiary.Models
{
    public class Pay
    {
        public int OrderId { get; set; }
        public double Price { get; set; }
        public string Comment { get; set; }
    }
}