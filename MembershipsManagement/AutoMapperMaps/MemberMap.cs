using AutoMapper;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.MemberInfoDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.AutoMapperMaps
{
    public class MemberMap : Profile
    {
        public MemberMap()
        {
            CreateMap<Member, GetMemberDto>();
            CreateMap<MemberInfo, Member>();
            CreateMap<MemberInfo, GetMemberDto>();
            CreateMap<MemberInfo, GetMemberInfoDTO>();
            CreateMap<AddMemberDto, Member>();
            CreateMap<AddMemberFamilyDto, Member>();
        }
    }
}
