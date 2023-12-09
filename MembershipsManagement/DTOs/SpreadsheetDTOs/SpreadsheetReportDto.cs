using MembershipsManagement.DTOs.MemberDTOs;

namespace MembershipsManagement.DTOs.SpreadsheetDTOs
{
    public class SpreadsheetReportDto 
    {
        public string? MedzlisName { get; set; } = string.Empty;
        public string? DzematName { get; set;} = string.Empty;
        public int? MarriageStatusMembersCount { get; set; }
        public int? WidowerStatusMembersCount { get; set; }
        public int? AgeLimitStatusMembersCount { get; set; }
        public double? Budget { get; set; }
        public double? TotalPaymentsAmount
        {
            get
            {
                double value = 0;
                if (this.TotalPaymentsAmountListed?.Count() > 0)
                {
                    this.TotalPaymentsAmountListed.ElementAt(0).ForEach(p =>
                    {
                            value += p;
                        
                    });
                }
                return value;
            }
        }
        public List<List<double>>? TotalPaymentsAmountListed { get; set; }
        public List<MemberThatHasPayedDto>? MembersPayed { get; set; } = new List<MemberThatHasPayedDto>();
        public List<MemberThatHasNotPayedDto>? MembersNotPayed { get; set; } = new List<MemberThatHasNotPayedDto>();
    }
}
