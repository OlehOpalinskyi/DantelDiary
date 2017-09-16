using DentalDiary.Data;
using DentalDiary.Data.Models;
using DentalDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Route("diary/{id}")]
        public ICollection<ReceptionViewModel> GetDiary(int id)
        {
            var receptions = db.Receptions.Where(r => r.CityId == id && r.Done == false).ToList();
            return Map<ICollection<ReceptionViewModel>>(receptions);
        }

        [Route("create/withuser")]
        [HttpPost]
        public ReceptionViewModel AddReception(ReceptionViewModel rec)
        {
            var dataReception = CreateRecption(rec);
            db.Receptions.Add(dataReception);
            db.SaveChanges();
            return Map<ReceptionViewModel>(dataReception);
        }

        [HttpPut]
        [Route("edit-diary/{id}")]
        public ReceptionViewModel EditDiary(int id, Diary diary)
        {
            var rec = db.Receptions.Single(r => r.Id == id);
            rec.Date = diary.Date;
            var person = db.Persons.Single(p => p.Id == diary.UserId);
            var price = db.PriceList.Single(p => p.Id == diary.PriceId);
            rec.Preson = person;
            rec.Price = price;
            db.SaveChanges();
            return Map<ReceptionViewModel>(rec);
        }

        [HttpPut]
        [Route("edit/{id}")]
        public ReceptionViewModel EditReception(int id, ReceptionViewModel rec)
        {
            var originReception = db.Receptions.Single(r => r.Id == id);
            var newRec = CreateRecption(rec);
            newRec.Id = id;
            db.Entry(originReception).CurrentValues.SetValues(newRec);
            db.SaveChanges();

            return Map<ReceptionViewModel>(newRec);
        }

        [Route("create")]
        [HttpPost]
        public ReceptionViewModel ReceptionWithoutPerson(Order order)
        {
            var dataPerson = Map<PersonDataModel>(order.Person);
            dataPerson.FirstVisit = DateTime.Now;
            dataPerson.LastVisit = DateTime.Now;
            dataPerson.DateOfBirth = DateTime.Now;
            db.Persons.Add(dataPerson);
            db.SaveChanges();
            order.RecInfo.PersonId = dataPerson.Id;
            return AddReception(order.RecInfo);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public void DeleteReception(int id)
        {
            var rec = db.Receptions.Single(r => r.Id == id);
            db.Receptions.Remove(rec);
            db.SaveChanges();
          //  return Map<ReceptionViewModel>(rec);
        }

        [HttpPut]
        [Route("edit-recivier/{id}")]
        public ReceptionViewModel EditRcivier(int id, string recivier)
        {
            var rec = db.Receptions.Single(r => r.Id == id);
            rec.Recivier = recivier;
            db.SaveChanges();
            return Map<ReceptionViewModel>(rec);
        }

        [HttpGet]
        [Route("pay/{id}/{price}")]
        public ReceptionViewModel Pay(int id, double price)
        {
            var rec = db.Receptions.Single(r => r.Id == id);
            rec.Payment = price;
            rec.Done = true;
            var debt = price - rec.Price.Price;
            var person = db.Persons.Single(p => p.Id == rec.PersonId);
            person.Debt += debt;
            db.SaveChanges();
            return Map<ReceptionViewModel>(rec);
        }

        [HttpPost]
        [Route("pay/withorder")]
        public ReceptionViewModel PayWithOrder(PayOrder pay)
        {
            var payFirst = Pay(pay.IdOrder, pay.PriceOne);
            payFirst.PriceId = pay.PriceId;
            var rec = AddReception(payFirst);
            var payTwo = Pay(rec.Id, pay.PriceTwo);
            return payTwo;
        }

        private ReceptionDataModel CreateRecption(ReceptionViewModel rec)
        {
            var dataRecertion = Map<ReceptionDataModel>(rec);
            var price = db.PriceList.Single(p => p.Id == rec.PriceId);
            var person = db.Persons.Single(p => p.Id == rec.PersonId);
            person.LastVisit = rec.Date;
            var city = db.Cities.Single(c => c.Id == rec.CityId);
            dataRecertion.Preson = person;
            dataRecertion.City = city;
            dataRecertion.Price = price;

            return dataRecertion;
        }
    }
}
