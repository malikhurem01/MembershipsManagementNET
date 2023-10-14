using AutoMapper;
using MembershipsManagement.DTOs.Member;
using MembershipsManagement.Entities;

namespace MembershipsManagement.AutoMapperMaps
{
    public class MemberMap : Profile
    {
        public MemberMap()
        {
            CreateMap<Clan, GetMemberDto>();
            CreateMap<AddMemberDto, Clan>();
            CreateMap<AddMemberFamilyDto, Clan>();
        }
    }
}
