using AutoMapper;
using MembershipsManagement.DTOs.BazaDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.AutoMapperMaps
{
    public class BazaMap : Profile
    {
        public BazaMap()
        {
            CreateMap<SpreadSheet, GetBazaDto>();
            CreateMap<AddBazaDto, SpreadSheet>();
        }
    }
}
