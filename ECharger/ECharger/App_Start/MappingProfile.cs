using AutoMapper;
using ECharger.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECharger.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ChargingStation, ChargingStationDto>();
        }
    }
}