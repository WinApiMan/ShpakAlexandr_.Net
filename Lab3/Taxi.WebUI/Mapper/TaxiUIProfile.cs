﻿using AutoMapper;
using BusinessLogic.Models;
using Taxi.WebUI.ViewModels;

namespace Taxi.WebUI.Mapper
{
    public class TaxiUIProfile : Profile
    {
        public TaxiUIProfile()
        {
            CreateMap<CarViewModel, Car>().ReverseMap();
        }
    }
}