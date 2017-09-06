using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalDiary.Data.Models
{
    public class PersonDataModel
    {
        public PersonDataModel()
        {
            Receptions = new List<ReceptionDataModel>();
        }

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
        public DateTime DateOfBirth { get; set; }
        public double Debt { get; set; }

        public int CardId { get; set; }
        public virtual CardDataModel Card { get; set; }
        public virtual ICollection<ReceptionDataModel> Receptions { get; set; }
    }
}
