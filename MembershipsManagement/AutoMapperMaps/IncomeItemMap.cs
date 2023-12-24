using AutoMapper;
using MembershipsManagement.DTOs.ProjectDTOs;
using MembershipsManagement.Entities;
using MembershipsManagement.Models;

namespace MembershipsManagement.AutoMapperMaps
{
    public class IncomeItemMap : Profile
    {
        public IncomeItemMap() {
            CreateMap<CreateIncomeItem, IncomeItem>();
            CreateMap<IncomeItem, GetIncomeItemDto>();
            CreateMap<AddIncomeDto, Income>();
            CreateMap<Income, GetIncomeDto>();
            CreateMap<AddDonatorDto, Donator>();
            CreateMap<Donator, GetDonatorDto>();
        }
    }
}
