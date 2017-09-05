using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalDiary.Models
{
    public class Order
    {
        public PersonViewModel Person { get; set; }
        public ReceptionViewModel RecInfo { get; set; }
    }
}