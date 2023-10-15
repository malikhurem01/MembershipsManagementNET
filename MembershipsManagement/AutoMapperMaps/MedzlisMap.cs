using AutoMapper;
using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.AutoMapperMaps
{
    public class MedzlisMap : Profile
    {
        public MedzlisMap()
        {
            CreateMap<AddMedzlisDto, Medzlis>();
            CreateMap<AddMedzlisDto, GetMedzlisDto>();
            CreateMap<Medzlis, GetMedzlisDto>();
        }
    }
}
