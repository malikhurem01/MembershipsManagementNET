using MembershipsManagement.DTOs.ProjectDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.ProjectServiceImpl
{
    public interface IExpenseItemService
    {
        Task<ServiceResponse<GetExpenseItemDto>> CreateExpenseItem(string token, CreateExpenseItem expenseItem);
        Task<ServiceResponse<GetExpenseItemDto>> DeleteExpenseItem(string token, DeleteExpenseItemDto expenseItem);
        Task<ServiceResponse<GetExpenseDto>> AddExpense(string token, AddExpenseDto expense);
        Task<ServiceResponse<GetExpenseDto>> DeleteExpense(string token, DeleteExpenseDto expense);
        Task<ServiceResponse<GetExpenseItemsView>> GetActiveExpenseItems(string token);
        Task<ServiceResponse<GetExpenseItemsView>> GetArchivedExpenseItems(string token, int spreadsheetId);
    }
}
