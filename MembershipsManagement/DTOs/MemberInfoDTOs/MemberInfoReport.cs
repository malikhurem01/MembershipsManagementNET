using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.DTOs.ProjectDTOs;
using MembershipsManagement.Models;

namespace MembershipsManagement.DTOs.MemberInfoDTOs
{
    public class MemberInfoReport
    {
        public GetMemberDto Member { get; set; }
        public List<GetMemberInfoDTO> Information { get; set; }
    }
}
