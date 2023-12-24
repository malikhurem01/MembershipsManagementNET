using MembershipsManagement.DTOs.ProjectDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.ProjectServiceImpl
{
    public interface IIncomeItemService
    {
        Task<ServiceResponse<GetIncomeItemDto>> CreateIncomeItem(string token, CreateIncomeItem incomeItem);
        Task<ServiceResponse<GetIncomeItemDto>> DeleteIncomeItem(string token, DeleteIncomeItemDto incomeItem);
        Task<ServiceResponse<GetIncomeDto>> AddIncome(string token, AddIncomeDto income);
        Task<ServiceResponse<GetIncomeDto>> DeleteIncome(string token, DeleteIncomeDto income);
        Task<ServiceResponse<GetActiveIncomeItems>> GetActiveIncomeItems(string token);
        Task<ServiceResponse<List<GetIncomeItemDto>>> GetArchivedIncomeItems(string token);
        Task<ServiceResponse<GetDonatorDto>> AddDonator(string token, AddDonatorDto donator);
        Task<ServiceResponse<List<GetDonatorDto>>> GetDonators(string token);
    }
}
