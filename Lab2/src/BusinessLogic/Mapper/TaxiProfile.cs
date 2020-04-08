using AutoMapper;
using BusinessLogic.Models;
using Taxi.DAL.Models;

namespace BusinessLogic.Services.Mapper
{
    public class TaxiProfile : Profile
    {
        public TaxiProfile()
        {
            CreateMap<CarDto, Car>().ReverseMap();
            CreateMap<ClientDto, Client>().ReverseMap();
            CreateMap<DriverDto, Driver>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
        }
    }
}