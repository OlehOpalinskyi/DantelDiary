﻿using AutoMapper;
using DentalDiary.Data.Models;
using DentalDiary.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DentalDiary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API routes
            config.MapHttpAttributeRoutes();
            Initialize();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<PriceDataModel, PriceViewModel>().ReverseMap();
                config.CreateMap<CityDataModel, CityViewModel>().ReverseMap();
                config.CreateMap<ReceptionDataModel, ReceptionViewModel>()
                .ForMember(dest => dest.PriceName, opt => opt.ResolveUsing(x => x.Price.Name))
                .ForMember(dest => dest.KindOfWork, opt => opt.ResolveUsing(x => x.Price.KindOfWork))
                .ForMember(dest => dest.PriceCount, opt => opt.ResolveUsing(x => x.Price.Price))
                .ForMember(dest => dest.Customer, opt => opt.ResolveUsing(x => x.Preson.FullName))
                .ReverseMap();
                config.CreateMap<PersonDataModel, PersonViewModel>().ReverseMap();
            });
        }
    }
}
