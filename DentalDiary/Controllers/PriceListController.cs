using static AutoMapper.Mapper;
using DentalDiary.Data;
using DentalDiary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DentalDiary.Data.Models;

namespace DentalDiary.Controllers
{
    [RoutePrefix("pricelist")]
    [Authorize]
    public class PriceListController : ApiController
    {
        private DiaryContext db = new DiaryContext();

        [Route("bycity/{id}")]
        [HttpGet]
        public IEnumerable<PriceViewModel> GetPriceList(int id)
        {
            var priceList = db.PriceList.Where(pl => pl.CityId == id);
            return Map<IEnumerable<PriceViewModel>>(priceList);
        }

        [HttpGet]
        [Route("get-price/{id}")]
        public PriceViewModel GetPrice(int id)
        {
            var price = db.PriceList.Single(p => p.Id == id);
            return Map<PriceViewModel>(price);
        }

        [Route("create")]
        [HttpPost]
        public PriceViewModel CreatePrice(PriceViewModel price)
        {
            var city = db.Cities.Single(c => c.Id == price.CityId);
            var dataPrice = Map<PriceDataModel>(price);
            dataPrice.City = city;
            db.PriceList.Add(dataPrice);
            db.SaveChanges();
            return Map<PriceViewModel>(dataPrice);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public PriceViewModel DeletePrice(int id)
        {
            var price = db.PriceList.Single(pl => pl.Id == id);
            db.PriceList.Remove(price);
            db.SaveChanges();
            return Map<PriceViewModel>(price);
        }

        [Route("edit/{id}")]
        [HttpPut]
        public PriceViewModel EditPrice(int id, PriceViewModel price)
        {
            var city = db.Cities.Single(c => c.Id == price.CityId);
            var editPrice = db.PriceList.Single(pl => pl.Id == id);
            editPrice.Id = id;
            editPrice.CityId = price.CityId;
            editPrice.Name = price.Name;
            editPrice.Price = price.Price;
            editPrice.City = city;
            db.SaveChanges();
            return Map<PriceViewModel>(editPrice);
        }
    }
}
