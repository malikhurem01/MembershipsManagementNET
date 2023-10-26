using AutoMapper;
using MembershipsManagement.DTOs.PaymentDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.AutoMapperMaps
{
    public class PaymentMap : Profile
    {
        public PaymentMap()
        {
            CreateMap<AddPaymentDto, Payment>();
            CreateMap<Payment, GetPaymentDto>();
        }
    }
}
