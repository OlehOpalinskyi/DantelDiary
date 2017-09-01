using DentalDiary.Data;
using DentalDiary.Data.Models;
using DentalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static AutoMapper.Mapper;

namespace DentalDiary.Controllers
{
    [RoutePrefix("receptions")]
    public class ReceptionController : ApiController
    {
        private DiaryContext db = new DiaryContext();

        [Route("all")]
        public ICollection<ReceptionViewModel> GetAllReception()
        {
            var receptions = db.Receptions.ToList();
            return Map<ICollection<ReceptionViewModel>>(receptions);
        }

        [Route("bycity/{id}")]
        public ICollection<ReceptionViewModel> GetReceptionByCity(int id)
        {
            var receptions = db.Receptions.Where(r => r.CityId == id).ToList();
            return Map<ICollection<ReceptionViewModel>>(receptions);
        }
        [Route("create")]
        [HttpPost]
        public ReceptionViewModel CreateReception(ReceptionViewModel rec)
        {
            var dataRecertion = Map<ReceptionDataModel>(rec);
            var person = db.Persons.Single(p => p.Id == rec.PersonId);
            person.LastVisit = rec.Date;
            var city = db.Cities.Single(c => c.Id == rec.CityId);
            dataRecertion.Customer = person.FullName;
            dataRecertion.Preson = person;
            dataRecertion.City = city;
            db.Receptions.Add(dataRecertion);
            db.SaveChanges();
            return Map<ReceptionViewModel>(dataRecertion);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public ReceptionViewModel DeleteReception(int id)
        {
            var rec = db.Receptions.Single(r => r.Id == id);
            db.Receptions.Remove(rec);
            db.SaveChanges();
            return Map<ReceptionViewModel>(rec);
        }
    }
}
