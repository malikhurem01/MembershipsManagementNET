using MembershipsManagement.DTOs.SpreadsheetDTOs;

namespace MembershipsManagement.DTOs.ProjectDTOs
{
    public class GetIncomeItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public GetSpreadsheetDto Spreadsheet { get; set; }
        public List<GetIncomeDto> Income { get; set; } = new List<GetIncomeDto>();
        public bool? NotifyMembers { get; set; }
        public double TotalIncome { get; set; }
    }
}
