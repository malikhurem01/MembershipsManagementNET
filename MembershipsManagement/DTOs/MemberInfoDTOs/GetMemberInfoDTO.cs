using MembershipsManagement.DTOs.SpreadsheetDTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.PaymentDTOs;

namespace MembershipsManagement.DTOs.MemberInfoDTOs
{
    public class GetMemberInfoDTO
    {
        public GetMemberDto Member { get; set; }
        public GetSpreadsheetDto Spreadsheet { get; set; }
        public List<GetPaymentDto>? Payments { get; set; } = new List<GetPaymentDto>();
        public int MembershipFee { get; set; }
        public double TotalAmountPayed { get; set; }
        public double Debt { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
