using AutoMapper;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.AutoMapperMaps
{
    public class MemberMap : Profile
    {
        public MemberMap()
        {
            CreateMap<Member, GetMemberDto>();
            CreateMap<AddMemberDto, Member>();
            CreateMap<AddMemberFamilyDto, Member>();
        }
    }
}
