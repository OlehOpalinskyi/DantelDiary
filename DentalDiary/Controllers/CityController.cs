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
    [RoutePrefix("city")]
    public class CityController : ApiController
    {
        private DiaryContext db = new DiaryContext();

        [Route("all")]
        public ICollection<CityViewModel> GetAll()
        {
            var cities = db.Cities.ToList();
            return Map<ICollection<CityViewModel>>(cities);
        }
        [Route("create")]
        [HttpPost]
        public CityViewModel CreateCity(CityViewModel city)
        {
            var dataCity = Map<CityDataModel>(city);
            db.Cities.Add(dataCity);
            db.SaveChanges();
            return Map<CityViewModel>(dataCity);
        }
    }
}
