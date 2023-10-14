using AutoMapper;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;
using Microsoft.AspNetCore.Identity;

namespace MembershipsManagement.AutoMapperMaps
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<Korisnik, GetUserDto>();
            CreateMap<IdentityUser, Korisnik>();
            CreateMap<IdentityUser, GetUserDto>();
            CreateMap<RegisterUserDto, Korisnik>();
            CreateMap<RegisterUserDto, UserLoginDto>();
        }
    }
}
