using DentalDiary.Data;
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
    }
}
