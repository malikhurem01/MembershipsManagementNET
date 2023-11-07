using MembershipsManagement.DTOs.BazaDTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.PaymentDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberInfoDTOs
{
    public class GetMemberInfoDTO
    {
        public GetMemberDto Member { get; set; }
        public GetSpreadsheetDto Spreadsheet { get; set; }
        public List<GetPaymentDto>? Payments { get; set; } = new List<GetPaymentDto>();
        public double TotalAmountPayed { get; set; }
        public double Debt { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
