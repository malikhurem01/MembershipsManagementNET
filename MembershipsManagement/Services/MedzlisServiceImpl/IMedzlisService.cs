using MembershipsManagement.DTOs.MedzlisDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.MedzlisServiceImpl
{
    public interface IMedzlisService
    {
        Task<ServiceResponse<GetMedzlisDto>> CreateMedzlis(AddMedzlisDto medzlis);
        Task<ServiceResponse<GetMedzlisDto>> GetMedzlis(int medzlisId);
        Task<ServiceResponse<List<GetMedzlisDto>>> GetMedzlisList();
        Task<ServiceResponse<GetMedzlisDto>> ModifyMedzlis(ModifyMedzlisDto medzlis);
        Task<ServiceResponse<string>> DeleteMedzlis(int medzlisId);
    }
}
