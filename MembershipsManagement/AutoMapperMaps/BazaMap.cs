using AutoMapper;
using MembershipsManagement.DTOs.Baza;
using MembershipsManagement.Entities;

namespace MembershipsManagement.AutoMapperMaps
{
    public class BazaMap : Profile
    {
        public BazaMap()
        {
            CreateMap<Baza, GetBazaDto>();
            CreateMap<AddBazaDto, Baza>();
        }
    }
}
