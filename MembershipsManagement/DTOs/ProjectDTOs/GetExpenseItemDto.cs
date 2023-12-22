using MembershipsManagement.DTOs.SpreadsheetDTOs;

namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class GetExpenseItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public GetSpreadsheetDto Spreadsheet { get; set; }
        public List<GetExpenseDto> Expenses { get; set; } = new List<GetExpenseDto>();
        public double TotalExpenses { get; set; }
    }
}
