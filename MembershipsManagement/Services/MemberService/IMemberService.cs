using MembershipsManagement.DTOs.MemberDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.MemberServiceImpl
{
    public interface IMemberService
    {
        Task<ServiceResponse<GetMemberDto>> AddMember(string token, AddMemberDto member);
        Task<ServiceResponse<GetMemberDto>> AddMemberFamily(AddMemberFamilyListDto familyMembers);
        Task<ServiceResponse<object>> GetMemberByNameAndLastName(string token, string name, string lastName, int spreadsheetId);
        Task<ServiceResponse<List<GetMemberDto>>> GetMemberList(string token);
        Task<ServiceResponse<GetMemberDto>> GetSingleMember(string token, int memberId);
        Task<ServiceResponse<string>> DeleteMember(DeleteMemberDto member);
        Task<ServiceResponse<GetMemberDto>> ModifyMember(ModifyMemberDto memberModified);
    }
}
