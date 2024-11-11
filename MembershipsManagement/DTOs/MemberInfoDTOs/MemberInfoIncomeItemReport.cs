using MembershipsManagement.DTOs.ProjectDTOs;

namespace MembershipsManagement.DTOs.MemberInfoDTOs
{
    public class MemberInfoIncomeItemReport
    {
        public int SpreadsheetYear { get; set; }
        public List<GetIncomeItemDto> IncomeItems { get; set; }
    }
}
