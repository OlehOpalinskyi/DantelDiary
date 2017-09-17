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
    [RoutePrefix("person")]
    public class PersonController : ApiController
    {
        private DiaryContext db = new DiaryContext();

        [Route("all")]
        [HttpGet]
        public ICollection<PersonViewModel> GetPersons()
        {
            var persons = db.Persons.ToList();
            return Map<ICollection<PersonViewModel>>(persons);
        }
        [Route("{id}")]
        [HttpGet]
        public PersonViewModel GetPerson(int id)
        {
            var person = db.Persons.Single(p => p.Id == id);
            return Map<PersonViewModel>(person);
        }

        [HttpGet]
        [Route("get-receptions/{id}")]
        public ICollection<ReceptionViewModel> GetReceptions(int id)
        {
            var receptions = db.Receptions.Where(r => r.PersonId == id).ToList();
            return Map<ICollection<ReceptionViewModel>>(receptions);
        }

        [Route("create")]
        [HttpPost]
        public PersonViewModel AddPerson(PersonViewModel person)
        {
            var dataPerson = Map<PersonDataModel>(person);
            db.Persons.Add(dataPerson);
            db.SaveChanges();
            return Map<PersonViewModel>(dataPerson);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public PersonViewModel DeletePerson(int id)
        {
            var person = db.Persons.Single(p => p.Id == id);
            db.Persons.Remove(person);
            db.SaveChanges();
            return Map<PersonViewModel>(person);
        }

        [Route("edit/{id}")]
        [HttpPut]
        public PersonViewModel EditUser(int id, PersonViewModel person)
        {
            var originPerson = db.Persons.Single(op => op.Id == id);
            person.Id = id;

            db.Entry(originPerson).CurrentValues.SetValues(person);
            db.SaveChanges();

            return Map<PersonViewModel>(originPerson);

        }
    }
}
