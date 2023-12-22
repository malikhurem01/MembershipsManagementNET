using MembershipsManagement.DTOs.ProjectDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.ProjectServiceImpl
{
    public interface IExpenseItemService
    {
        Task<ServiceResponse<GetExpenseItemDto>> CreateExpenseItem(string token, CreateExpenseItem project);
        //Task<ServiceResponse<GetIncomeDto>> AddIncome(string token, AddIncomeDto income);
        //Task<ServiceResponse<GetIncomeDto>> DeleteIncome(string token, DeleteIncomeDto income);
        Task<ServiceResponse<GetExpenseDto>> AddExpense(string token, AddExpenseDto expense);
        Task<ServiceResponse<GetExpenseDto>> DeleteExpense(string token, DeleteExpenseDto expense);
        Task<ServiceResponse<GetActiveExpenseItems>> GetActiveExpenseItems(string token);
        Task<ServiceResponse<List<GetExpenseItemDto>>> GetArchivedExpenseItems(string token);
    }
}
