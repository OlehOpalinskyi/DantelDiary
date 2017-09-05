using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DentalDiary.Data.Models
{
    public class PersonDataModel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(70)]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
        public string LinkToImages { get; set; }
        public string Recivier { get; set; }

        public virtual ICollection<ReceptionDataModel> Receptions { get; set; }
    }
}
