using AutoMapper;
using SPaas.Api.Models;

namespace SPaas.Api
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<PowerPlant, SPaas.Services.DataModels.PowerPlant>().ReverseMap();
            CreateMap<PowerPlantType, SPaas.Services.DataModels.PowerPlantType>().ReverseMap();
            CreateMap<Fuel, SPaas.Services.DataModels.Fuel>().ReverseMap();
        }
    }
}