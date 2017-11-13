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
    [Authorize]
    public class ReceptionController : ApiController
    {
        private DiaryContext db = new DiaryContext();

        [Route("bycity/{id}")]
        public ICollection<ReceptionViewModel> GetReceptionByCity(int id)
        {
            var receptions = db.Receptions.Where(r => r.CityId == id).OrderByDescending(r => r.Priority).ToList();
            return Map<ICollection<ReceptionViewModel>>(receptions);
        }

        [HttpGet]
        [Route("get-reception/{id}")]
        public ReceptionViewModel GetReception(int id)
        {
            var reception = db.Receptions.Single(s => s.Id == id);
            return Map<ReceptionViewModel>(reception);
        }

        [Route("reciviers/bycity/{id}")]
        public ICollection<ReceptionViewModel> WithRecivier(int id)
        {
            var receptions = db.Receptions.Where(r => r.CityId == id && r.Recivier != null).OrderBy(r => r.IsReturn).ToList();
            return Map<ICollection<ReceptionViewModel>>(receptions);
        }

        [HttpGet]
        [Route("search-by-customer/{id}")]
        public ICollection<ReceptionViewModel> SortByCustomer(int id, string customer)
        {
            var rec = db.Receptions.Where(r => r.CityId == id && r.Preson.FullName.Contains(customer) && r.Done == false).ToList();
            return Map<ICollection<ReceptionViewModel>>(rec);
        }

        [HttpGet]
        [Route("search-by-recivier/{id}")]
        public ICollection<ReceptionViewModel> SortByRecivier(int id, string customer)
        {
            var rec = db.Receptions.Where(r => r.CityId == id && (r.Recivier.Contains(customer) || r.Preson.FullName.Contains(customer))).ToList();
            return Map<ICollection<ReceptionViewModel>>(rec);
        }

        [Route("sort-by-date")]
        public ICollection<ReceptionViewModel>SortByDate(DateTime date, int cityId)
        {
            var recs = new List<ReceptionDataModel>();
            var dbr = new List<ReceptionDataModel>();
            if(DateTime.Compare(date, DateTime.Now) < 0)
                dbr = db.Receptions.Where(r => r.CityId == cityId).OrderBy(r => r.Date).ToList();
            else
            {
                dbr = db.Receptions.Where(r => r.CityId == cityId && r.Done == false).OrderBy(r => r.Date).ToList();
            }
            foreach(var item in dbr)
            {
                if (item.Date.Value.Date == date.Date)
                    recs.Add(item);
            }
            return Map<ICollection<ReceptionViewModel>>(recs);
        }

        [Route("diary/{id}")]
        public ICollection<ReceptionViewModel> GetDiary(int id)
        {
            var receptions = db.Receptions.Where(r => r.CityId == id && r.Done == false).OrderByDescending(r => r.Date).ToList();
            return Map<ICollection<ReceptionViewModel>>(receptions);
        }

        [HttpPost]
        [Route("add-history")]
        public ReceptionViewModel AddHistory(ReceptionViewModel rec)
        {
            var person = db.Persons.Single(p => p.Id == rec.PersonId);
            var city = db.Cities.Single(c => c.Id == rec.CityId);
            var price = db.PriceList.Single(pr => pr.Id == rec.PriceId);
            var dataRec = Map<ReceptionDataModel>(rec);
            dataRec.Price = price;
            dataRec.City = city;
            dataRec.Preson = person;
            dataRec.Payment = price.Price;
            dataRec.Done = true;
            dataRec.Priority = 1;

            db.Receptions.Add(dataRec);
            db.SaveChanges();

            return Map<ReceptionViewModel>(dataRec);
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

        [HttpPost]
        [Route("create-recomedation")]
        public ReceptionViewModel CreateRecomendation(ReceptionViewModel rec)
        {
            var dataReception = CreateRecption(rec);
            db.Persons.Single(p => p.Id == rec.PersonId).RecomendDate = rec.Date;
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
            dataPerson.FirstVisit = order.RecInfo.Date;
            dataPerson.LastVisit = order.RecInfo.Date;
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
        public ReceptionViewModel EditRcivier(int id, Recivier recivier)
        {
            var rec = db.Receptions.Single(r => r.Id == id);
            rec.Recivier = recivier.Name;
            rec.Return = recivier.ReturnPay;
            rec.IsReturn = recivier.IsReturn;
            db.SaveChanges();
            return Map<ReceptionViewModel>(rec);
        }

        [HttpPost]
        [Route("pay")]
        public ReceptionViewModel Pay(Pay pay)
        {
            var rec = db.Receptions.Single(r => r.Id == pay.OrderId);
            rec.Payment = pay.Price;
            rec.Done = true;
            rec.Comment = pay.Comment;
            var debt = pay.Price - rec.Price.Price;
            var person = db.Persons.Single(p => p.Id == rec.PersonId);
            person.Debt += debt;
            db.SaveChanges();
            return Map<ReceptionViewModel>(rec);
        }

        [HttpPost]
        [Route("pay/withorder")]
        public ReceptionViewModel PayWithOrder(PayOrder pay)
        {
            var pay1 = new Pay()
            {
                OrderId = pay.IdOrder,
                Price = pay.PriceOne,
                Comment = pay.Comment
            };
            var payFirst = Pay(pay1);
            payFirst.PriceId = pay.PriceId;
            var rec = AddReception(payFirst);
            var pay2 = new Pay()
            {
                OrderId = rec.Id,
                Price = pay.PriceTwo,
                Comment = pay.Comment
            };
            var payTwo = Pay(pay2);
            return payTwo;
        }

        private ReceptionDataModel CreateRecption(ReceptionViewModel rec)
        {
            var dataRecertion = Map<ReceptionDataModel>(rec);
            if(rec.PriceId != 0)
            {
                var price = db.PriceList.Single(p => p.Id == rec.PriceId);
                dataRecertion.Price = price;
            }
                
            var person = db.Persons.Single(p => p.Id == rec.PersonId);
            person.LastVisit = rec.Date;
            if (person.FirstVisit == null)
                person.FirstVisit = rec.Date;
            var city = db.Cities.Single(c => c.Id == rec.CityId);
            dataRecertion.Preson = person;
            dataRecertion.City = city;

            return dataRecertion;
        }
    }
}
