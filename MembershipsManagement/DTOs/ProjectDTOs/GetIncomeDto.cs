using MembershipsManagement.DTOs.User;
using MembershipsManagement.Entities;
using MembershipsManagement.Models;

namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class GetIncomeDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public GetExpenseItemDto Project { get; set; }
        public string SupervisorId { get; set; } = string.Empty;
        public GetUserDto Supervisor { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public IncomeType IncomeType { get; set; }
    }
}
