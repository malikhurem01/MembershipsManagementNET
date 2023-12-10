using MembershipsManagement.DTOs.DzematDTOs;
using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.MemberInfoDTOs;
using MembershipsManagement.DTOs.PaymentDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.DTOs.BazaDTOs
{
    public class GetSpreadsheetDto
    {
        public int Id { get; set; }
        public int DzematId { get; set; }
        public bool Archived { get; set; }
        public int Year { get; set; }
        public int MarriageFee { get; set; }
        public int WidowerFee { get; set; }
        public int AgeLimitFee { get; set; }
        public GetDzematDto Dzemat { get; set; }
        public List<GetPaymentDto> Payments { get; set; } = new List<GetPaymentDto>();
        public List<GetMemberDto> Members { get; set; } = new List<GetMemberDto>();
        public List<GetMemberInfoDTO> MemberInfo { get; set; } = new List<GetMemberInfoDTO>();
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
