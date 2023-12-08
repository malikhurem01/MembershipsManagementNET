using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.PaymentDTOs;

namespace MembershipsManagement.DTOs.MemberInfoDTOs
{
    public class GetSpreadsheetMemberInfo
    {
        public double TotalAmountPayed { get; set; }
        public GetMemberDto Member { get; set; }
        public int MembershipFee { get; set; }
        public List<GetSpreadsheetPaymentDto> Payments { get; set; }
    }
}
