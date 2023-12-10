using MembershipsManagement.DTOs.AuthDTOs;
using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.DzematServiceImpl
{
    public interface IDzematService
    {
        Task<ServiceResponse<GetDzematDto>> RegisterDzemat(RegisterDzematDto registerDzemat);
    }
}
