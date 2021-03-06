﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalDiary.Data.Models
{
    public class ReceptionDataModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Recivier { get; set; }
        public double Payment { get; set; }
        public bool Done { get; set; }
        public int Priority { get; set; }
        public bool Recomended { get; set; }
        public string Comment { get; set; }
        public double Return { get; set; }
        public bool IsReturn { get; set; }
        public string ApproximateTime { get; set; }

        public int PersonId { get; set; }
        public int CityId { get; set; }
        public int PriceId { get; set; }

        [ForeignKey("PersonId")]
        public virtual PersonDataModel Preson { get; set; }
        [ForeignKey("CityId")]
        public virtual CityDataModel City { get; set; }
        [ForeignKey("PriceId")]
        public virtual PriceDataModel Price { get; set; }

    }
}
