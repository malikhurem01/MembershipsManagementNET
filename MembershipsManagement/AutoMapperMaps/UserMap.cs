using AutoMapper;
using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;
using MembershipsManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace MembershipsManagement.AutoMapperMaps
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<Korisnik, GetUserDto>();
            CreateMap<Admin, GetUserDto>();
            CreateMap<Supervisor, GetUserDto>();
            CreateMap<RegisterUserDto, Admin>();
            CreateMap<RegisterUserDto, Supervisor>();
            CreateMap<RegisterUserDto, Korisnik>();
            CreateMap<RegisterUserDto, UserLoginDto>();
            CreateMap<RegisterDzematDto, DzematLoginDto>();
            CreateMap<RegisterDzematDto, Dzemat>();
            CreateMap<Dzemat, GetDzematDto>();

        }
    }
}
