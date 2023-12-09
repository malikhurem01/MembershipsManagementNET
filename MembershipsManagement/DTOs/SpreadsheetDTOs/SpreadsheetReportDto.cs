using MembershipsManagement.DTOs.MemberDTOs;

namespace MembershipsManagement.DTOs.SpreadsheetDTOs
{
    public class SpreadsheetReportDto 
    {
        public string MedzlisName { get; set; } = string.Empty;
        public string DzematName { get; set;} = string.Empty;
        public int MarriageStatusMembersCount { get; set; }
        public int WidowerStatusMembersCount { get; set; }
        public int AgeLimitStatusMembersCount { get; set; }
        public double? Budget { get; set; }
        public double TotalPaymentsAmount { get; set; }
        public List<MemberThatHasPayedDto> MembersPayed { get; set; } = new List<MemberThatHasPayedDto>();
        public List<MemberThatHasNotPayedDto> MembersNotPayed { get; set; } = new List<MemberThatHasNotPayedDto>();
    }
}
