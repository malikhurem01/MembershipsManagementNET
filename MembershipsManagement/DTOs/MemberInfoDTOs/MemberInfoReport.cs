using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.PaymentDTOs;
using MembershipsManagement.DTOs.SpreadsheetDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.MemberInfoDTOs
{
    public class MemberInfoReport
    {
        public GetMemberDto Member { get; set; }
        public List<GetMemberInfoDTO> Information { get; set; }
    }
}
