using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalDiary.Models
{
    public class Diary
    {
        public int UserId { get; set; }
        public int PriceId { get; set; }
        public DateTime Date { get; set; }
    }
}