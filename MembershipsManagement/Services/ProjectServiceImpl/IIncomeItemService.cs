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
        Task<ServiceResponse<GetIncomeItemsView>> GetActiveDonationIncomeItems(string token, int? dzematId);
        Task<ServiceResponse<GetIncomeItemsView>> GetArchivedDonationIncomeItems(string token, int spreadsheetId, int? dzematId);
        Task<ServiceResponse<GetIncomeItemsView>> GetActiveIncomeItems(string token, int? dzematId);
        Task<ServiceResponse<GetIncomeItemsView>> GetArchivedIncomeItems(string token, int spreadsheetId, int? dzematId);
        Task<ServiceResponse<GetDonatorDto>> AddDonator(string token, AddDonatorDto donator);
        Task<ServiceResponse<List<GetDonatorDto>>> GetDonators(string token);
    }
}
