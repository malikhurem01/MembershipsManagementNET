using AutoMapper;
using MembershipsManagement.DTOs.ProjectDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.AutoMapperMaps
{
    public class ExpenseItemMap : Profile
    {
        public ExpenseItemMap() {
            CreateMap<CreateExpenseItem, ExpenseItem>();
            CreateMap<ExpenseItem, GetExpenseItemDto>();
            CreateMap<AddExpenseDto, Expense>();
            CreateMap<Expense, GetExpenseDto>();
        }
    }
}
